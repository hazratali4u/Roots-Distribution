using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptCustomerStatusReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadTown();
            this.LoadArea();
            this.LoadChannelType();
            //this.LoadPrincipal();
        }
    }

    #region Load
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);

    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
    }

    /// <summary>
    /// Loads Towns To Town Combo
    /// </summary>
    private void LoadTown()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpTown.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
            DrpTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 1);
        }
    }

    /// <summary>
    /// Loads Channel Types To ChannelType Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        DrpChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpChannelType, dt, 0, 2);
    }
    #endregion
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
        this.LoadArea();
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    public void ShowReport(int type)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CrpCustomerStatus CrpReport = new CrpCustomerStatus();
        DataSet ds = null;
        ds = RptSaleCtl.SelectCustomerStatus(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(DrpChannelType.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), CheckBox1.Checked, Constants.IntNullValue, int.Parse(DrpStatus.SelectedValue.ToString()));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", drpDistributor.SelectedItem.Text);
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}