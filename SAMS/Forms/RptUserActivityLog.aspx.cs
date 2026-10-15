using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptUserActivityLog : System.Web.UI.Page
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
            txtStartDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            txtEndDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            LoadDistributor();
            LoadUser();
        }
    }
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);

    }
    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        if (drpDistributor.Items.Count > 0)
        {
            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(15000, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlUser, dt, 0, 6, true);
        }
    }

    /// <summary>
    /// Shows User Login History in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows User Login History in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }
    public void ShowReport(int Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        SAMSBusinessLayer.Reports.CrpUserActivityDetail CrpReport = new SAMSBusinessLayer.Reports.CrpUserActivityDetail();
        DataTable dt = mController.SelectReportTitle(Constants.IntNullValue);
        DataSet ds = RptSaleCtl.GetUserActivityDetail(DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Convert.ToInt32(ddlUser.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue));

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportFor", ddlUser.SelectedItem.Text);
        CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", Type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}