using CrystalDecisions.CrystalReports.Engine;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptDateWiseCollectionReport : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly CustomerDataController _mCdControllerontroller = new CustomerDataController();
    readonly DistributorController _dController = new DistributorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            LoadRoute();
            LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            PanelDistributor.Visible = true;
        }
    }
    private void LoadLocation()
    {

        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //  drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, 0, 2);
    }
   
    private void LoadCustomer()
    {
       
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            DrpCustomer.Items.Clear();
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, false);
            if (dt.Rows.Count > 0)
            {
                DrpCustomer.SelectedIndex = 0;
            }
        }
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    public void ShowReport(int p_Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        //SAMSBusinessLayer.Reports.CrpDateWiseCollection CrpReport = new SAMSBusinessLayer.Reports.CrpDateWiseCollection();
        DataTable dt = mController.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));
        ReportDocument CrpReport = new ReportDocument();
        DataSet ds = null;
        if (RbtReportType.SelectedValue == "1")
        {
            ds = RptInventoryCtl.SelectDateWiseCollection(int.Parse(DrpLocation.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), Convert.ToInt32(RbtReportType.SelectedValue));
            CrpReport = new SAMSBusinessLayer.Reports.CrpDistributorWiseCollection();
        }
        else
        {

            ds = RptInventoryCtl.SelectDateWiseCollection(int.Parse(DrpLocation.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), Convert.ToInt32(RbtReportType.SelectedValue));
            CrpReport = new SAMSBusinessLayer.Reports.CrpDateWiseCollection();
        }
        
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("Location", DrpLocation.SelectedItem.Text);
        if (RbtReportType.SelectedValue == "1")
        {
            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
        }
        else
        {
            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
        }
       
        CrpReport.SetParameterValue("fromdate", txtStartDate.Text);
        CrpReport.SetParameterValue("todate", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("Collection", RbtReportType.SelectedItem.Text.ToString());
        //CrpReport.SetParameterValue("Price", rblRate.SelectedItem.Text);
        //CrpReport.SetParameterValue("ReportType", "Stock Reconciliation Report ( " + ddlType.SelectedItem.Text + " )");

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void RbtReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbtReportType.SelectedValue != "0")
        {
            PanelDistributor.Visible = true;
            LoadRoute();
            LoadCustomer();
        }
        else
        {
            PanelDistributor.Visible = true;
            LoadRoute();
            LoadCustomer();
        }
    }
    private void LoadRoute()
    {
        
        if (DrpLocation.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, false);
            if (dt.Rows.Count > 0)
            {
                DrpRoute.SelectedIndex = 0;
            }
        }
    }
   
    protected void drpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRoute();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();

    }
}