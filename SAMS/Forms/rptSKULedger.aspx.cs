using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Stock Reconciliation Report
/// </summary>
public partial class Forms_rptSKULedger : System.Web.UI.Page
{
    static string location = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadCategory();
            LoadSKU();

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        ddlPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlPrincipal, m_dt, 0, 1);        
    }

    private void LoadCategory()
    {
    //    ddlCategory.Items.Clear();
    //    SkuHierarchyController mController = new SkuHierarchyController();
    //    DataTable dt = mController.SelectSKUCategory(Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.SKUCategory);
    //    ddlCategory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
    //    clsWebFormUtil.FillDropDownList(ddlCategory, dt, "SKU_HIE_ID", "SKU_HIE_NAME");
    }

    private void LoadSKU()
    {
        ddlSKU.Items.Clear();
        SkuController mSKUController = new SkuController();
        DataTable m_SKUDt = mSKUController.SelectSkuInfo(int.Parse(ddlPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDropDownList(ddlSKU, m_SKUDt, "SKU_ID", "skuDetail", true);
        
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, false);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                location += dr[0].ToString() + ',';
            }
        }
    }

    /// <summary>
    /// Shows Stock Reconciliation in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Stock Reconciliation in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKU();
    }

    private void ShowReport(int ReportType)
    {
        
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        ReportDocument CrpReport = new ReportDocument();
        if (ddlType.SelectedValue.ToString() == "1")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpSKULedgerCtnWise();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpSKULedger();
        }
        
        DataTable dt = mController.SelectReportTitle(Convert.ToInt32(drpDistributor.SelectedValue));
        DataSet ds = null;
        if (drpDistributor.SelectedValue == "All")
        {
             ds = RptInventoryCtl.GetSKULedgerData(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text + " 23:59:59"), Constants.IntNullValue, location,Convert.ToInt32(ddlSKU.SelectedValue), Constants.IntNullValue);

        }
        else
        {
            ds = RptInventoryCtl.GetSKULedgerData(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text + " 23:59:59"), Constants.IntNullValue, drpDistributor.SelectedValue, Convert.ToInt32(ddlSKU.SelectedValue), Constants.IntNullValue);

        }
       CrpReport.SetDataSource(ds);
       CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("To_date", txtEndDate.Text);
        CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", ReportType);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);            
    }
    
    protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
        LoadSKU();
    }
}