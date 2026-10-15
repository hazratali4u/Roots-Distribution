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

public partial class Forms_RptInventoryDocumentsQ : System.Web.UI.Page
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
        if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 2)
        {
            ShowPurchaseReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 3)
        {
            ShowPurchaseReturnReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 4)
        {
            ShowTransferInReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 5)
        {
            ShowTransferOutReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 6)
        {
            ShowDamageReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 7)
        {
            ShowOpeningStockReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 8)
        {
            ShowShortReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 9)
        {
            ShowExcessReport(0);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 15 || int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 16)
        {
            ShowStockIssuanceReport(0);
        }

    }
    public void DailPurchaseTransfer()
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));

        DataControl dc = new DataControl();
        ds = RptInventoryCtl.SelectPurchaseTransferStock(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue,
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedItem.Value.ToString()), 0);

        SAMSBusinessLayer.Reports.CrpDailyPurchaseTransfer CrpReport = new SAMSBusinessLayer.Reports.CrpDailyPurchaseTransfer();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("PRINCIPAL", "");
        CrpReport.SetParameterValue("LOCATION", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("ReportTitle", "Date Wise " + DrpDocumentType.SelectedItem.Text + " Report");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("Price", "");

        this.Session.Add("ReportType", 0);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 2)
        {
            ShowPurchaseReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 3)
        {
            ShowPurchaseReturnReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 4)
        {
            ShowTransferInReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 5)
        {
            ShowTransferOutReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 6)
        {
            ShowDamageReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 7)
        {
            ShowOpeningStockReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 8)
        {
            ShowShortReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 9)
        {
            ShowExcessReport(1);
        }
        else if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 15 || int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 16)
        {
            ShowStockIssuanceReport(1);
        }
    }


    public void ShowPurchaseReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        //SAMSBusinessLayer.Reports.CrpPurchaseDocument CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        DataSet ds = null;
        ReportDocument CrpReport = new ReportDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseInventoryDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseInventoryDocumentWithValue();
        }
        ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpPrincipal.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 2);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Purchase Document");
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    public void ShowPurchaseReturnReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        //SAMSBusinessLayer.Reports.CrpPurchaseDocument CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        DataSet ds = null;
        ReportDocument CrpReport = new ReportDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseInventoryDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseInventoryDocumentWithValue();
        }
        ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpPrincipal.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 3);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Purchase Return Document");
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    public void ShowTransferInReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();

        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpTransferInn();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpTransferDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpTransferInn();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpTransferInnWithValue();
        }

        DataSet ds = null;
        ds = RptInventoryCtl.SelectTransferDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 4);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Transfer In Document");
        CrpReport.SetParameterValue("Principal", "Trade Price");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    public void ShowTransferOutReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();

        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpTransferOUT();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpTransferDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            //CrpReport = new SAMSBusinessLayer.Reports.CrpTransferOUT();
            CrpReport = new SAMSBusinessLayer.Reports.CrpTransferOUTQ();
        }
        else
        {
            //CrpReport = new SAMSBusinessLayer.Reports.CrpTransferOUTWithValue();
            CrpReport = new SAMSBusinessLayer.Reports.CrpTransferOUTWithValueQ();
        }
        DataSet ds = null;
        ds = RptInventoryCtl.SelectTransferDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 5);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        
        CrpReport.SetParameterValue("DocumentType", "Transfer Out Document");
        CrpReport.SetParameterValue("Principal", "Trade Price");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    public void ShowDamageReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();

        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpDamageDocument();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocumentWithValue();
        }
        DataSet ds = null;
        ds = RptInventoryCtl.SelectTransferDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 6);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Damage Document");
        CrpReport.SetParameterValue("Principal", "Price");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    public void ShowShortReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocumentWithValue();
        }
        DataSet ds = null;
        ds = RptInventoryCtl.SelectTransferDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 8);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Short Document");
        CrpReport.SetParameterValue("Principal", "Trade Price");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    public void ShowExcessReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        //CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocumentWithValue();
        }
        DataSet ds = null;
        ds = RptInventoryCtl.SelectTransferDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 9);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Excess Document");
        CrpReport.SetParameterValue("Principal", "Trade Price");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    private void ShowStockIssuanceReport(int type)
    {
        try
        {
            //DocumentPrintController mController = new DocumentPrintController();
            //RptInventoryController RptInventoryCtl = new RptInventoryController();
            //DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            //DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()),
            //    int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue.ToString()));
            //ReportDocument CrpReport = new ReportDocument();
            //if (int.Parse(DrpDocumentType.SelectedValue.ToString()) == 15)
            //{
            //    CrpReport = new CrpIssueDocument();
            //}
            //else
            //{
            //    CrpReport = new CrpReturnDocument();
            //}


            //CrpReport.SetDataSource(ds);
            //CrpReport.Refresh();


            //CrpReport.SetParameterValue("DocumentType", "Raw Material " + DrpDocumentType.SelectedItem.Text + " Document");
            //CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            //CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            //Session.Add("CrpReport", CrpReport);
            //Session.Add("ReportType", type);
            //const string url = "'Default.aspx'";
            //const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            //Type cstype = this.GetType();
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public void ShowOpeningStockReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        //rpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        if (RbtReportType.SelectedValue == "0")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocument();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpShortDocumentWithValue();
        }
        DataSet ds = null;
        ds = RptInventoryCtl.SelectTransferDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 7);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Opening Document");
        CrpReport.SetParameterValue("Principal", "Distributor Price");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

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
            PanelSupplier.Visible = true;
        }
        else
        {
            PanelSupplier.Visible = false;
        }
    }
}