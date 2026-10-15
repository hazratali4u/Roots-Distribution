using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_rptGeoHierarchyLocation : System.Web.UI.Page
{
    GeoHierarchyController GControler = new GeoHierarchyController();
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadRegion();
            this.LoadZone();
            this.LoadTerritory();
            this.LoadCity();
            LoadLocation();
            LoadArea();
            LoadMarket();
        }
    }
    #region Load functions
    protected void LoadRegion()
    {
        DrpTownRegion.Items.Clear();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DrpTownRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpTownRegion, dt, 0, 4);
    }
    private void LoadZone()
    {
        DrpTownZone.Items.Clear();
        if (DrpTownRegion.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            DrpTownZone.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTownZone, dt, 0, 4);

        }
    }
    private void LoadTerritory()
    {
        DrpTerritory.Items.Clear();
        if (DrpTownZone.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            DrpTerritory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTerritory, dt, 0, 4);
        }
    }
    protected void LoadCity()
    {
        drpCIty.Items.Clear();
        if (DrpTerritory.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            drpCIty.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpCIty, dt, 0, 4);
        }
    }
    protected void LoadLocation()
    {
        DrpLocation.Items.Clear();
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.GetTownWistUnAssignDistributorNew(Constants.IntNullValue, int.Parse(DrpTownRegion.SelectedValue), int.Parse(DrpTownZone.SelectedValue), int.Parse(DrpTerritory.SelectedValue), int.Parse(drpCIty.SelectedValue), int.Parse(Session["UserID"].ToString()));
            DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, 0, 1);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadArea()
    {
        DrpRoute.Items.Clear();
        if (DrpLocation.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
    }

    private void LoadMarket()
    {
        DrpMarket.Items.Clear();
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            DistributorRouteController gController = new DistributorRouteController();
            DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpRoute.SelectedValue.ToString()));
            DrpMarket.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDropDownList(DrpMarket, dt, 0, 8, false);
        }
        else
        {
            DrpMarket.Items.Clear();
        }
    }
    #endregion
    #region index change click functions
    protected void DrpTownRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
        this.LoadTerritory();
        this.LoadCity();
        LoadLocation();
        LoadArea();
        LoadMarket();
    }
    protected void DrpTownZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
        this.LoadCity();
        LoadLocation();
        LoadArea();
        LoadMarket();
    }
    protected void DrpTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCity();
        LoadLocation();
        LoadArea();
        LoadMarket();
    }
    protected void drpCIty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLocation();
        LoadArea();
        LoadMarket();
    }
    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadMarket();
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMarket();
    }
    #endregion

    /// <summary>
    /// Shows  Catagory Wise Customer Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows  Catagory Wise Customer Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int ReportType)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSalesCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

            CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrpReport = new SAMSBusinessLayer.Reports.crpGeoHirerchyLocation();
            DataSet st = RptSalesCtl.SelectGeoHirerchyLocation(int.Parse(DrpTownRegion.SelectedValue), int.Parse(DrpTownZone.SelectedValue), int.Parse(DrpTerritory.SelectedValue), int.Parse(drpCIty.SelectedValue), int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpMarket.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()));
            CrpReport.SetDataSource(st);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
            CrpReport.SetParameterValue("reportName", "Geo Hierarchy Report");

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", ReportType);
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