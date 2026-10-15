using CrystalDecisions.CrystalReports.Engine;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPriceGroupsReport : System.Web.UI.Page
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
            LoadGroup();
            this.LoadDistributor();
            LoadPrincipal();
            LoadCatagory();
        }
    }
    private void LoadGroup()
    {
        DrpGroup.Items.Clear();
        DistributorController DController = new DistributorController();
        try
        {
            DataTable dt = DController.SelectAllGroups();
            DrpGroup.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpGroup, dt, 0, 1);
            Session.Add("dtGroup", dt);
        }
        catch (Exception)
        {

            throw;
        }
    }
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    protected void LoadCatagory()
    {
        //SkuHierarchyController Hierarchy = new SkuHierarchyController();
        //DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        //DataView dv = new DataView(dtCatagory);
        //dv.RowFilter = "Company_id = " + Convert.ToInt32(drpPrincipal.SelectedValue.ToString());
        //dtCatagory = dv.ToTable();
        //this.DrpCatagory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpCatagory, dtCatagory, "Category_Id", "Category_Name");
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            //SKUPriceDetailController PController = new SKUPriceDetailController();
            //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            //drpPrincipal.Items.Add(new clsListItems("All", Constants.LongNullValue.ToString()));
            //clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Categories
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpCatagory.Items.Clear();
        LoadCatagory();
    }

    /// <summary>
    /// Shows Report in Excel Or PDF
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSaleCtl = new RptSaleController();
            ReportDocument CrpReport = new ReportDocument();
            if (int.Parse(DrpGroup.SelectedValue.ToString()) == Constants.IntNullValue)
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpAllPriceGroupsReport();
            }
            else
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpPriceGroupsReport();
            }
            
            DataSet ds = null;
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue.ToString()));
            ds = RptSaleCtl.PriceGroups(int.Parse(DrpDistributor.SelectedValue.ToString()),int.Parse(DrpGroup.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("Distributor", this.DrpDistributor.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("GroupName", this.DrpGroup.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("USERNAME", this.Session["UserName2"].ToString());
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", p_Report_Type);
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
    /// Shows SKU Price Structure in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows SKU Price Structure in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}