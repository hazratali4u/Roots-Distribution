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
public partial class Forms_RptPhysicalStockTaking : System.Web.UI.Page
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
            LoadPrincipal();
            LoadLocation();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            this.txtFromDate.Attributes.Add("readonly", "readonly");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            drpPrincipal.Items.Clear();
   
            if (RblType.SelectedIndex == 0)
            {
                drpPrincipal.Items.Add(new ListItem("", Constants.IntNullValue.ToString()));
            }
            else if (RblType.SelectedIndex == 1)
            {
                this.drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            }
            else
            {
                drpPrincipal.Items.Add(new ListItem("", Constants.IntNullValue.ToString()));
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
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

            if (RblType.SelectedIndex == 0)
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
                PanelReport.Visible = true;
            }
            else if (RblType.SelectedIndex == 1)
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
                this.DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
                PanelReport.Visible = false;
                ddlType.SelectedValue = "0";
            }
            else
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
                PanelReport.Visible = true;
            }
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
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        try
        {
            
            if (RblType.SelectedIndex == 0)
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                string FromDate = null;
                SAMSBusinessLayer.Reports.CrpPhysicalStockTaking CrpReport = new SAMSBusinessLayer.Reports.CrpPhysicalStockTaking();
                DataSet ds = null;
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                ds = RptInventoryCtl.PhysicalStockTaking(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(ddlType.SelectedValue));
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                CrpReport.SetParameterValue("Principal", ddlType.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else if (RblType.SelectedIndex == 1)
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                string FromDate = null;
                SAMSBusinessLayer.Reports.RptPhysicalStockSummary CrpReport = new SAMSBusinessLayer.Reports.RptPhysicalStockSummary();
                DataSet ds = null;
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ds = RptInventoryCtl.PhysicalStockTakingValueWise(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(this.Session["UserId"].ToString()));
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                int Type = 0;
                if (ddlType.SelectedValue == "0")
                {
                    Type = 2;
                }
                else
                {
                    Type = 3;
                }
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                string FromDate = null;
                SAMSBusinessLayer.Reports.CrpPhysicalStockTakingExpiryWise CrpReport = new SAMSBusinessLayer.Reports.CrpPhysicalStockTakingExpiryWise();
                DataSet ds = null;
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                ds = RptInventoryCtl.PhysicalStockTaking(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Type);
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                CrpReport.SetParameterValue("Principal", ddlType.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Physical Stock Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        try
        {
            if (RblType.SelectedIndex == 0)
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();

                string FromDate = null;

                SAMSBusinessLayer.Reports.CrpPhysicalStockTaking CrpReport = new SAMSBusinessLayer.Reports.CrpPhysicalStockTaking();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();

                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                ds = RptInventoryCtl.PhysicalStockTaking(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(ddlType.SelectedValue));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                CrpReport.SetParameterValue("Principal", ddlType.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else if (RblType.SelectedIndex == 1)
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                string FromDate = null;

                SAMSBusinessLayer.Reports.RptPhysicalStockSummary CrpReport = new SAMSBusinessLayer.Reports.RptPhysicalStockSummary();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();


                ds = RptInventoryCtl.PhysicalStockTakingValueWise(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(this.Session["UserId"].ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                int Type = 0;
                if (ddlType.SelectedValue == "0")
                {
                    Type = 2;
                }
                else
                {
                    Type = 3;
                }
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                string FromDate = null;
                SAMSBusinessLayer.Reports.CrpPhysicalStockTakingExpiryWise CrpReport = new SAMSBusinessLayer.Reports.CrpPhysicalStockTakingExpiryWise();
                DataSet ds = null;
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                ds = RptInventoryCtl.PhysicalStockTaking(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Type);
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                CrpReport.SetParameterValue("Principal", ddlType.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Principals And Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPrincipal();
        LoadLocation();
    }
}
