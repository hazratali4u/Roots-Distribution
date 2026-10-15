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

public partial class Forms_RptSKUWiseClosingStock : System.Web.UI.Page
{
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
            LoadCategories();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadPrincipal()
    {
        DrpPrincipal.Items.Clear();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, false);
    }
    private void LoadCategories()
    {
        
        try
        {
            LstCategory.Items.Clear();
            SkuHierarchyController mSkuHieController = new SkuHierarchyController();
            DataTable dtDist = mSkuHieController.SelectDTSkuHierarchy01(4, int.Parse(DrpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(this.LstCategory, dtDist, "Category_Id", "Category_Name");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    

    //LstCategory.Items.Clear();

    //    SkuHierarchyController sController = new SkuHierarchyController();
    //    DataTable dt = sController.SelectSKUCategories(Constants.SKUCategory, true);
    //    clsWebFormUtil.FillListBox(LstCategory, dt, 0, 3, false);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        //drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int p_Report_Type)
    {
        string Catagories_IDs = null;
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        for (int i = 0; i < LstCategory.Items.Count; i++)
        {
            if (LstCategory.Items[i].Selected == true)
            {
                Catagories_IDs += LstCategory.Items[i].Value.ToString() + ",";
            }
        }
        if (Catagories_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select Category.');", true);
            return;
        }
        //int source = Convert.ToInt32(ddlSource.SelectedValue);
        //if (ddlSource.SelectedValue == "-1")
        //{
        //    source = Constants.IntNullValue;
        //}
        DataSet ds = RptInventoryCtl.SelectStockUnitWise(DateTime.Parse(txtEndDate.Text), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Catagories_IDs, ddlSortBy.SelectedValue, ddlOrderBy.SelectedValue, int.Parse(ddlType.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue));
        ReportDocument CrpReport = new ReportDocument();
        if (ddlType.SelectedItem.Text == "Both")
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpSkuClosingBoth();
        }
        else
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpSkuClosingUnitwise();
        }
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("date", txtEndDate.Text);
        CrpReport.SetParameterValue("companyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ItemType", ddlType.SelectedItem.Text);
        CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", p_Report_Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategories();
    }
}