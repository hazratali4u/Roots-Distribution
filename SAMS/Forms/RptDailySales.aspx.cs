using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;

public partial class Forms_RptDailySales : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadCatagory();
            LoadLocation();
            DateTime dt = (DateTime)this.Session["CurrentWorkDate"];
            this.txtFromDate.Text = dt.ToString("dd-MMM-yyyy");
            this.txtToDate.Text = dt.ToString("dd-MMM-yyyy");
        }
    }
    protected void LoadCatagory()
    {
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        ListCatagory.Items.Clear();
        if (int.Parse(drpPrincipal.SelectedValue.ToString()) == Constants.IntNullValue)
        {
            DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(8, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(this.ListCatagory, dtCatagory, 0, 2);
        }
        else
        {
            DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
            DataView dv = new DataView(dtCatagory);
            dv.RowFilter = "Company_id = " + Convert.ToInt32(drpPrincipal.SelectedValue.ToString());
            dtCatagory = dv.ToTable();
            clsWebFormUtil.FillListBox(this.ListCatagory, dtCatagory, 4, 6);
        }
    }
    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            drpPrincipal.Items.Clear();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            //this.drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
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
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.DataSource = dt;
            this.DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            this.DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ListCatagory.Items.Clear();
        LoadCatagory();
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    private void ShowReport(int ReportType)
    {
        try
        {
            decimal TotalProductsTotal = 0;
            decimal TotalProTotal = 0;
            //DateTime dtFrom = new DateTime();
            //DateTime dtTo = new DateTime();
            //DateTime dtFromMonth = DateTime.Parse(txtFromDate.Text);
            //dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);
            //DateTime dtToMonth = DateTime.Parse(txtToDate.Text);
            //dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            //dtTo = dtTo.AddMonths(1).AddDays(-1);
            int SkuType;
            int TYPE_ID;
            if (int.Parse(DrpType.SelectedValue.ToString()) != 0)
            {
                SkuType = int.Parse(DrpType.SelectedValue.ToString());
                TYPE_ID = 2;
            }
            else
            {
                SkuType = Constants.IntNullValue;
                TYPE_ID = 2;
            }
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSalesCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
            string Catagories_IDs = null;
            CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            if (rblType.SelectedValue == "1")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpSKUCtnValue();
            }
            else
            {
                if (rblType.SelectedValue == "2")
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpDailySale();
                }
                else
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpDailySaleValue();
                }
            }
            DataSet ds = null;
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                if (this.ListCatagory.Items[i].Selected == true)
                {
                    Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                }
            }
            if (Catagories_IDs == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Category is required.');", true);
                return;
            }

            ds = RptSalesCtl.GetDailySales(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToInt32(this.drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(Session["UserID"]), Catagories_IDs, SkuType, DateTime.Parse(txtFromDate.Text + " 00:00:00"), DateTime.Parse(txtToDate.Text + " 23:59:59"), TYPE_ID);
            foreach (DataRow dr in ds.Tables["uspGetSKUCtnSales"].Rows)
            {
                TotalProductsTotal += Convert.ToDecimal(dr["Ctn"]);
                TotalProTotal += Convert.ToDecimal(dr["Pro_Ctn"]);
            }
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Location", this.DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("FromMonth", this.txtFromDate.Text);
            CrpReport.SetParameterValue("ToMonth", this.txtToDate.Text);
            CrpReport.SetParameterValue("ReportName", "Daily Sales Tracker (" + rblType.SelectedItem.Text + ")");
            CrpReport.SetParameterValue("TotalProductsTotal", TotalProductsTotal);
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