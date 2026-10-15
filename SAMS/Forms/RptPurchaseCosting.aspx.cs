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

public partial class Forms_RptPurchaseCosting : System.Web.UI.Page
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
    }
    
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        if (int.Parse(DrpDocumentType.SelectedItem.Value.ToString()) == 2)
        {
            ShowPurchaseReport(1);
        }
    }


    public void ShowPurchaseReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        DataSet ds = null;
        ReportDocument CrpReport = new ReportDocument();

        CrpReport = new SAMSBusinessLayer.Reports.CrpPurchaseCosting();
        ds = RptInventoryCtl.SelectPurchaseCosting(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpPrincipal.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 2);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("DocumentType", "Purchase Costing Report");
        CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
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