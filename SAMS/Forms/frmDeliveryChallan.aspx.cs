using CrystalDecisions.CrystalReports.Engine;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmDeliveryChallan : System.Web.UI.Page
{
    readonly RptSaleController RptSaleCtl = new RptSaleController();
    readonly DocumentPrintController DPrint = new DocumentPrintController();
    readonly DataControl dc = new DataControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            LoadGrid();
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        DataTable dt2 = dt.Clone();
        DataRow[] foundRows = dt.Select("SUBZONE_ID <> 5");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dt2.ImportRow(t);
            }
        }
        clsWebFormUtil.FillDropDownList(drpDistributor, dt2, 0, 2, true);
    }
    private void LoadGrid()
    {
        grdInvoices.DataSource = null;
        grdInvoices.DataBind();

        if (drpDistributor.Items.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                DataTable table = RptSaleCtl.SelectDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue
                                , Constants.IntNullValue, 7, Convert.ToDateTime(dt.Rows[0]["CLOSING_DATE"]), Constants.DateNullValue, Constants.IntNullValue);
                if (table.Rows.Count > 0)
                {
                    grdInvoices.DataSource = table;
                    grdInvoices.DataBind();
                }
            }
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    private void ShowReport(int p_ReportType)
    {
        DataSet ds = null;

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        string InvoiceId = "";
        foreach (GridViewRow dr in grdInvoices.Rows)
        {
            CheckBox chbSelect = (CheckBox)(dr.Cells[0].FindControl("ChbInvoice"));
            if (chbSelect.Checked)
            {
                InvoiceId = InvoiceId + "," + dr.Cells[1].Text;
            }
        }

        ds = RptSaleCtl.SelectDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), InvoiceId, 1);

        ReportDocument SubReport = new ReportDocument();
        ReportDocument CrpReport = new ReportDocument();
        CrpReport = new CrpDeliveryChallan();

        SubReport = CrpReport.OpenSubreport("Subreport1");
        CrpReport.SetDataSource(ds);
        SubReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("CompanyAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
}