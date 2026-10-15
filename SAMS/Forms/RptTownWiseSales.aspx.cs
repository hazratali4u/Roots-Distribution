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
/// Form For Town Wise Sales Report
/// </summary>
public partial class Forms_RptTownWiseSales : System.Web.UI.Page
{
    readonly DocumentPrintController DPrint = new DocumentPrintController();
    readonly RptCustomerController RptCustomerCtl = new RptCustomerController();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             LoadDistributor();
             LoadPrincipal();

            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime) Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse( Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse( Session["CurrentWorkDate"].ToString()));
         DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList( DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse( Session["UserId"].ToString()), int.Parse( Session["CompanyId"].ToString()));
         drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList( drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Shows Town Wise Sales in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
       
        DataSet ds;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        ds = RptCustomerCtl.SelectTownWiseSales(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), RbtType.SelectedIndex, int.Parse(Session["UserId"].ToString()));
        if (RbtType.SelectedIndex == 2)
        {
            var crpReport = new CrpTownWiseSales2();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", "(" + RbtType.SelectedItem.Text + ")");

            Session.Add("ReportType", 0);
            Session.Add("CrpReport", crpReport);
        }
        else
        {
            var crpReport = new CrpTownWiseSales();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", "(" + RbtType.SelectedItem.Text + ")");
            Session.Add("ReportType", 0);
            Session.Add("CrpReport", crpReport);
        }
       

        
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype =  GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Town Wise Sales in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {

        DataSet ds;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        ds = RptCustomerCtl.SelectTownWiseSales(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), RbtType.SelectedIndex, int.Parse(Session["UserId"].ToString()));
        if (RbtType.SelectedIndex == 2)
        {
            var crpReport = new CrpTownWiseSales2();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", "(" + RbtType.SelectedItem.Text + ")");

            Session.Add("ReportType", 1);
            Session.Add("CrpReport", crpReport);
        }
        else
        {
            var crpReport = new CrpTownWiseSales();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", "(" + RbtType.SelectedItem.Text + ")");
            Session.Add("ReportType", 1);
            Session.Add("CrpReport", crpReport);
        }



        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}
