using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Stock Reconciliation Report
/// </summary>
public partial class Forms_RptStockReconcilation : Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly DistributorController _dController = new DistributorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        
        DataTable mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, mDt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
       
        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
      //  drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
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


    public void ShowReport(int P_Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        SAMSBusinessLayer.Reports.CrpStockReconsiliation CrpReport = new SAMSBusinessLayer.Reports.CrpStockReconsiliation();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = RptInventoryCtl.SelectPrincipalStockReconcilation(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), int.Parse(this.Session["UserId"].ToString()), ddlType.SelectedIndex, Convert.ToInt32(rblRate.SelectedValue));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("division", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("fromdate", txtStartDate.Text);
        CrpReport.SetParameterValue("todate", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("Price", rblRate.SelectedItem.Text);
        CrpReport.SetParameterValue("ReportType", "Stock Reconciliation Report ( " + ddlType.SelectedItem.Text + " )");
        CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", P_Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }


    /// <summary>
    /// Shows Stock Reconciliation in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
        //DocumentPrintController mController = new DocumentPrintController();
        //RptInventoryController RptInventoryCtl = new RptInventoryController();
        //SAMSBusinessLayer.Reports.StockReConsilation CrpReport = new SAMSBusinessLayer.Reports.StockReConsilation();
        //DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        //DataSet ds = RptInventoryCtl.SelectPrincipalStockReconcilation(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), int.Parse(this.Session["UserId"].ToString()),ddlType.SelectedIndex,Convert.ToInt32(rblRate.SelectedValue));
        //CrpReport.SetDataSource(ds);
        //CrpReport.Refresh();


        //CrpReport.SetParameterValue("division", drpDistributor.SelectedItem.Text);
        //CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        //CrpReport.SetParameterValue("fromdate", txtStartDate.Text);
        //CrpReport.SetParameterValue("todate", txtEndDate.Text);
        //CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //CrpReport.SetParameterValue("Price", rblRate.SelectedItem.Text);
        //CrpReport.SetParameterValue("ReportType", "Stock Reconciliation Report ( " + ddlType.SelectedItem.Text + " )");

        //string path = Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

        //CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        //CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

        //System.IO.FileInfo file = new System.IO.FileInfo(path);

        //if (file.Exists)
        //{
        //    Response.Clear();

        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

        //    Response.AddHeader("Content-Length", file.Length.ToString());

        //    Response.ContentType = "application/octet-stream";

        //    Response.WriteFile(file.FullName);

        //    Response.End();

        //}
        //else
        //{
        //    Response.Write("This file does not exist.");
        //}        
    }
}
