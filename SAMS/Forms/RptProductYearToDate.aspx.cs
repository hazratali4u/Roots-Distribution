using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class Forms_RptProductYearToDate : System.Web.UI.Page
{
    DocumentPrintController DPrint = new DocumentPrintController();
    RptCustomerController RptCustomerCtl = new RptCustomerController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadSKU();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("yyyy");
            
        }
    }

    private void LoadPrincipal()
    {
        var pController = new SKUPriceDetailController();
        DataTable dt = pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        //DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, dt, 0, 1);
    }

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
       // drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    private void LoadSKU()
    {

        DrpSKU.Items.Clear();
        SkuController mSKUController = new SkuController();
        DataTable m_SKUDt = mSKUController.SelectSkuInfo(int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));

     //   DrpSKU.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpSKU, m_SKUDt, "SKU_ID", "SKUDETAIL");
    }
    
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadSKU();
    }
    
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {

        DataSet ds = null;
        int NoOfMonths = 1;
        NoOfMonths = CalculateNoOfMonths();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        ds = RptCustomerCtl.SelectProductMonthlySales(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + "-01-01 00:00:00"), int.Parse(rblUnitType.SelectedValue), Convert.ToInt32(DrpSKU.SelectedValue), NoOfMonths);

        var crpReport = new CrpProductMontlySale();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("Unit", rblUnitType.SelectedItem.Text);
        crpReport.SetParameterValue("Year", txtStartDate.Text);
        crpReport.SetParameterValue("SkuName", DrpSKU.SelectedItem.Text);
       

        Session.Add("ReportType", 0);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows  Party Statement in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        int NoOfMonths = 1;
        NoOfMonths = CalculateNoOfMonths();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        ds = RptCustomerCtl.SelectProductMonthlySales(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + "-01-01 00:00:00"), int.Parse(rblUnitType.SelectedValue), Convert.ToInt32(DrpSKU.SelectedValue), NoOfMonths);

        var crpReport = new CrpProductMontlySale();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("Unit", rblUnitType.SelectedItem.Text);
        crpReport.SetParameterValue("Year", txtStartDate.Text);
        crpReport.SetParameterValue("SkuName", DrpSKU.SelectedItem.Text);

        Session.Add("ReportType", 1);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    private int CalculateNoOfMonths()
    {
        int NoOfMonths = 1;
        DistributorController Dist = new DistributorController();
        DataTable dtMonths = Dist.GetNoOfWorkingDays(Convert.ToDateTime("01-01-" + txtStartDate.Text), 0, Convert.ToInt32(drpDistributor.SelectedValue),Convert.ToInt32(DrpPrincipal.SelectedValue),Convert.ToInt32(DrpSKU.SelectedValue));
        if (dtMonths.Rows.Count > 0)
        {
            NoOfMonths = Convert.ToInt32(dtMonths.Rows[0]["NoOfMonths"]);
        }
        return NoOfMonths;
    }
}