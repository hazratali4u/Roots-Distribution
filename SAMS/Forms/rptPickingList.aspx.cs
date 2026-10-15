using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Physical Stock Report
/// </summary>
public partial class Forms_rptPickingList : System.Web.UI.Page
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
            LoadLocation();
            this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DrpLocation.Items.Clear();
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Physical Stock Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Physical Stock Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int ReportType)
    {
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            string FromDate = null;
            CrpPickingList CrpReport = new CrpPickingList();
            DataSet ds = null;
            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
            ds = RptInventoryCtl.GetPickingList(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"));
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("Location", this.DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Date", Convert.ToDateTime(this.txtFromDate.Text));
            CrpReport.SetParameterValue("Company", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

    }
}
