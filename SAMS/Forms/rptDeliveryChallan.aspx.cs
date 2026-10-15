using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;
using SAMSBusinessLayer;

/// <summary>
/// Form For Print Sale Document Report
/// </summary>
public partial class Forms_rptDeliveryChallan : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadSaleForce();
            this.LoadRoute();
            this.LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text =Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            drpDistributor_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //try
        //{
        //    DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //    DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //    clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        try
        {
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
            Session.Add("dtDistributor", dt);            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
        try
        {
            if (drpDistributor.Items.Count > 0)
            {
                DrpRoute.Items.Clear();
                DistributorAreaController mController = new DistributorAreaController();
                DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
                DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
            }
            else
            {
                DrpRoute.Items.Clear();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void LoadSaleForce()
    {
        DrpArea.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        try
        {
            DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(0, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            DrpArea.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpArea, m_dt, 0, 1);
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        try
        {
            if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpRoute.SelectedValue != Constants.IntNullValue.ToString())
            {
                CustomerDataController mController = new CustomerDataController();
                DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, 0);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    /// <summary>
    /// Loads Sale Forces And Routes
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
        this.LoadRoute();
        int DistributorType = 1;
        DataTable dtDistributor = (DataTable)Session["dtDistributor"];
        foreach (DataRow dr in dtDistributor.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                DistributorType = Convert.ToInt32(dr["SUBZONE_ID"]);
                break;
            }
        }
        if (DistributorType == 6)
        {
            //DrpPrincipal.SelectedValue = Constants.IntNullValue.ToString();
            //DrpPrincipal.Enabled = false;
            //DrpPrincipal_SelectedIndexChanged(null, null);
        }
    }
    
    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    /// <summary>
    /// Shows Print Sale Document in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    /// <summary>
    /// Gets Print Sale Document And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="pReportType">Type</param>
    private void ShowReport(int pReportType)
    {
        DataSet ds = null;
        int pCustomerType = Constants.IntNullValue;
        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DataControl dc = new DataControl();
            ds = RptSaleCtl.SelectDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpArea.SelectedValue.ToString()), Constants.IntNullValue,
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 4, Constants.LongNullValue, pCustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);
            ReportDocument SubReport = new ReportDocument();
            ReportDocument CrpReport = new ReportDocument();
            CrpReport = new CrpDeliveryChallanOrder();
            SubReport = CrpReport.OpenSubreport("Subreport1");
            CrpReport.SetDataSource(ds);
            SubReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("ReportTitle", "Delivery Challan By Order");
            CrpReport.SetParameterValue("Date", "Order Date :");
            CrpReport.SetParameterValue("Ord/Inv", "Order # :");
            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("CompanyAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());
            CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            var cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Print Sale Document in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}