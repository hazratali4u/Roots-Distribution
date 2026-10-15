using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_RptProfitLossReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();

            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    private void LoadDistributor()
    {
        drpDistributor.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2,false);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
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
    private void ShowReport(int p_ReprotType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        DataSet ds = null;

        DataControl dc = new DataControl();
        ds = RptSaleCtl.SelectProfitLossReport(int.Parse(drpDistributor.SelectedValue.ToString()),
        DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), 0);
        CrpProfirLossQ CrpReport = new CrpProfirLossQ();
        ReportDocument SubReport = new ReportDocument();
        ReportDocument SubReport2 = new ReportDocument();
        ReportDocument SubReport3 = new ReportDocument();
         
        SubReport = CrpReport.OpenSubreport("subreport1");
        SubReport2 = CrpReport.OpenSubreport("subreport2");
        SubReport3 = CrpReport.OpenSubreport("subreport3");
        CrpReport.SetDataSource(ds);

        SubReport.SetDataSource(ds);
        SubReport2.SetDataSource(ds);
        SubReport3.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", p_ReprotType);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

    }
}