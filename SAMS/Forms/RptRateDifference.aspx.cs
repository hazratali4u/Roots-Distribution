using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptRateDifference : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly DistributorController _dController = new DistributorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        try
        {
            if (drpDistributor.Items.Count > 0)
            {
                CustomerDataController mController = new CustomerDataController();
                DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,1);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    private void LoadPrincipal()
    {
        DataTable mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, mDt, 0, 1);
    }

   
    private void LoadDistributor()
    {

        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //  drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

   
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    public void ShowReport(int P_Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        SAMSBusinessLayer.Reports.CrpRateDiffrence CrpReport = new SAMSBusinessLayer.Reports.CrpRateDiffrence();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = RptInventoryCtl.SelectRateDifference(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpCustomer.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), Convert.ToInt32(rblRate.SelectedValue));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("todate", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", P_Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }


    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
}