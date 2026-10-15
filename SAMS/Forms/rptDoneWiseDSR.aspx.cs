using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Order Booker Reports
/// </summary>
public partial class Forms_rptDoneWiseDSR : System.Web.UI.Page
{
    GeoHierarchyController GControler = new GeoHierarchyController();
    /// <summary>
    /// 
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadRegion();
            LoadTerritoryZone();
            LoadPrincipal();


            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            this.txtFromDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            this.txtToDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }


    protected void LoadRegion()
    {
        drpRegionTerritory.Items.Clear();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.drpRegionTerritory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpRegionTerritory, dt, 0, 4, false);

    }
    private void LoadTerritoryZone()
    {
        if (drpRegionTerritory.Items.Count > 0)
        {
            DrpZone.Items.Clear();
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpRegionTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpZone.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpZone, dt, 0, 4, false);

        }
    }



    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            // this.drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    /// <summary>
    /// Shows Order Booker Reports in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSaleCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(Constants.IntNullValue);

            string FromDate = null;
            string ToDate = null;

            DataSet ds = null;

            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();


            ds = RptSaleCtl.RptZoneWiseDSR(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), int.Parse(drpRegionTerritory.SelectedValue), int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpZone.SelectedValue));
            SAMSBusinessLayer.Reports.crpZoneWiseDSR CrpReport = new SAMSBusinessLayer.Reports.crpZoneWiseDSR();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Branch", this.Session["DISTRIBUTOR_ID"].ToString());
            CrpReport.SetParameterValue("Title", "Zone Wise DSR (Summary)");
            CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

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

    /// <summary>
    /// Shows Order Booker Reports in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string FromDate = null;
            string ToDate = null;
            DataSet ds = null;
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSaleCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(Constants.IntNullValue);
            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();
            ds = RptSaleCtl.RptZoneWiseDSR(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), int.Parse(drpRegionTerritory.SelectedValue), int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpZone.SelectedValue));
            SAMSBusinessLayer.Reports.crpZoneWiseDSR CrpReport = new SAMSBusinessLayer.Reports.crpZoneWiseDSR();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Branch", this.Session["DISTRIBUTOR_ID"].ToString());
            CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 1);
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


    protected void drpRegionTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTerritoryZone();
    }
}
