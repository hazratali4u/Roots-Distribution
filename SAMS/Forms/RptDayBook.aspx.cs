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

public partial class Forms_RptDayBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        if (!IsPostBack)
        {
            this.LoadDistributor();
            LoadPrincipal();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
            PanelSupplier.Visible = true;
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
        if (m_dt.Rows.Count > 0)
        {
            DrpPrincipal.SelectedIndex = 0;
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
   
    public void ShowReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();

        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        DataSet ds = null;

        ds = RptInventoryCtl.SelectDayBook(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(ddlReportType.SelectedItem.Value.ToString()));

        if (ddlReportType.SelectedItem.Value.ToString() == "1" || ddlReportType.SelectedItem.Value.ToString() == "2")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpDayBookDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpDayBook();
        }
        
        

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Day Book".ToUpper());
        CrpReport.SetParameterValue("Principal", ddlReportType.SelectedItem.Text);
        if (ddlReportType.SelectedItem.Value.ToString() == "1" || ddlReportType.SelectedItem.Value.ToString() == "2")
        {
            CrpReport.SetParameterValue("Type", "Document Type");
            CrpReport.SetParameterValue("Number", "Document No");
        }
        else
        {
            CrpReport.SetParameterValue("Type", "Voucher Type");
            CrpReport.SetParameterValue("Number", "Voucher No");
        }
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString().ToUpper());
        CrpReport.SetParameterValue("Date", txtStartDate.Text);
        CrpReport.SetParameterValue("DISTRIBUTOR", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("ADDRESS", dt.Rows[0]["ADDRESS1"].ToString());
        CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 2 || int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 3)
        {
            PanelSupplier.Visible = false;
        }
        else
        {
            PanelSupplier.Visible = false;
        }
    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}