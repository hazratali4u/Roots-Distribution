using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Customer Ledger Report
/// </summary>
public partial class Forms_rptOrderVsInvoice : Page
{

    readonly DistributorController _dController = new DistributorController();
    readonly CustomerDataController _mCdControllerontroller = new CustomerDataController();
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptSaleController _SaleCtl = new RptSaleController();

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadTown();
            LoadData();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadDistributor()
    {

        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
   
    private void LoadTown()
    {
        ddlTown.Items.Clear();
        GeoHierarchyController gController = new GeoHierarchyController();
        DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
        ddlTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlTown, dt, 0, 1);
    }
   
    private void LoadData()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0 && ddlTown.Items.Count > 0)
        {
            DataTable dt = _mCdControllerontroller.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,Convert.ToInt32(ddlTown.SelectedValue),0);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3);            
        }
    }

    /// <summary>
    /// Loads Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTown();
        LoadData();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    
    /// <summary>
    /// Shows Customer Ledger Either in PDF or in Excel
    /// </summary>
    /// <param name="pReportType">ReportType</param>
    private void ShowReport(int pReportType)
    {
        System.Text.StringBuilder sbCustomerID = new System.Text.StringBuilder();
        if (DrpCustomer.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in DrpCustomer.Items)
            {
                sbCustomerID.Append(li.Value);
                sbCustomerID.Append(",");
            }
        }
        else
        {
            sbCustomerID.Append(DrpCustomer.SelectedValue);
        }
        if (DrpCustomer.Items.Count > 0 )
        {
            var ds = _SaleCtl.GetOrderVsInvoiceDetail(int.Parse(drpDistributor.SelectedValue),sbCustomerID.ToString(), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
            var dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
            CrpOrderVsInvoice crpReport = new CrpOrderVsInvoice();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            crpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("ToDate", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
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
