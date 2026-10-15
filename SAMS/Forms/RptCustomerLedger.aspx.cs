using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Customer Ledger Report
/// </summary>
public partial class Forms_RptCustomerLedger : Page
{

    readonly DistributorController _dController = new DistributorController();
    readonly DistributorAreaController _mController = new DistributorAreaController();
    readonly CustomerDataController _mCdControllerontroller = new CustomerDataController();
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();

    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptCustomerController _rptCustomerCtl = new RptCustomerController();

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
            LoadArea();
            LoadData();

            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    public static string opType = "CR";
    private void LoadDistributor()
    {

        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

   
    private void LoadArea()
    {      
       
        DataTable dt = _mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);        
    }

   
    private void LoadData()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {

            DataTable dt = _mCdControllerontroller.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, 0);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, true);            
        }
    }

    /// <summary>
    /// Gets Customer Opening Balance
    /// </summary>
    private decimal LoadCustomerOpBalance()
    {
        if (drpDistributor.Items.Count <= 0 || DrpCustomer.Items.Count <= 0) return 0;
        var dt = _mCdControllerontroller.SelectPrincipalCustomerOpBalance(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpCustomer.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), Constants.IntNullValue);
        
        if (Convert.ToDecimal(dt.Rows[0][0]) < 0)
        {
            opType = "DR";

        }
        else if (Convert.ToDecimal(dt.Rows[0][0]) == 0)
        {
            opType = "DR";
        }
        else
        {
            opType = "CR";
        }
        //if (decimal.Parse(dt.Rows[0][0].ToString()) < 0)
        //{
        //    return -1 * decimal.Parse(dt.Rows[0][0].ToString());
        //}
        //else
        //{
        //    return decimal.Parse(dt.Rows[0][0].ToString());
        //}
        return decimal.Parse(dt.Rows[0][0].ToString());
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //DrpPrincipal.Items.Clear();

        //var mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //DrpPrincipal.Items.Add(new ListItem("All",Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, mDt, 0, 1);
    }

    /// <summary>
    /// Loads Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadData();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    
    /// <summary>
    /// Shows Customer Ledger Either in PDF or in Excel
    /// </summary>
    /// <param name="pReportType">ReportType</param>
    private void ShowReport(int pReportType)
    {
        if (DrpCustomer.Items.Count > 0 )
        {
            var ds = _rptCustomerCtl.CustomerLedger_View(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(DrpCustomer.SelectedValue),
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            var dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

            CrpCustomerLedger crpReport = new CrpCustomerLedger();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            crpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("Principal", "");
            crpReport.SetParameterValue("Town_Name", DrpRoute.SelectedItem.Text);
            crpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            crpReport.SetParameterValue("Op_Balance", LoadCustomerOpBalance());
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Op_Type", opType);
            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Customer Ledger Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Customer Ledger Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}
