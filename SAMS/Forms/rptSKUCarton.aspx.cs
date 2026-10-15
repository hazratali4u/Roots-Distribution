using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;

/// <summary>
/// Form For  Catagory Wise Customer Report
/// </summary>
public partial class rptSKUCarton : System.Web.UI.Page
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
            LoadCatagory();
            LoadLocation();
            LoadTown();
            LoadCustomer();
            LoadChannelType();
            DateTime dt = (DateTime)this.Session["CurrentWorkDate"]; 
            this.txtFromDate.Text = dt.ToString("MMM-yyyy");
            this.txtToDate.Text   = dt.ToString("MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Categories To Category ListBox
    /// </summary>
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

    public void LoadTown()
    {
        //if (DrpLocation.Items.Count > 0)
        //{
        //    DrpTown.Items.Clear();
        //    DistributorTownController gController = new DistributorTownController();
        //    DataTable dt = gController.SelectAssignTown(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), 1);
        //    DrpTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //    clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 1, false);
        //}
        if (DrpLocation.Items.Count > 0)
        {
            DrpTown.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 6);
        }
        else
        {
            DrpTown.Items.Clear();
        }
    }
    private void LoadCustomer()
    {
        if (DrpLocation.Items.Count > 0)
        {
            DrpCustomer.Items.Clear();
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, false);
            if (dt.Rows.Count > 0)
            {
                DrpCustomer.SelectedIndex = 0;
            }
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
            drpPrincipal.Items.Clear();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            this.drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
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

    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        ddlChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlChannelType, dt, 0, 2);
    }

    /// <summary>
    /// Loads Categories
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ListCatagory.Items.Clear();
        LoadCatagory();
    }
    
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
            decimal TotalProductsTotal = 0;
            decimal TotalProTotal = 0;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            DateTime dtFromMonth = DateTime.Parse(txtFromDate.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);
            DateTime dtToMonth = DateTime.Parse(txtToDate.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);
            int SkuType;
            if (int.Parse(DrpType.SelectedValue.ToString()) != 0)
            {
                SkuType = int.Parse(DrpType.SelectedValue.ToString());
            }
            else
            {
                SkuType = Constants.IntNullValue;
            }
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSalesCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
            string Catagories_IDs = null;
            CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            if (rblType.SelectedValue == "3")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpSKUCtnValue();
            }
            else
            {
                //if(rblType.SelectedValue == "1")
                CrpReport = new SAMSBusinessLayer.Reports.CrpSKUUnits();
                //else
                //CrpReport = new SAMSBusinessLayer.Reports.CrpSKUCtn();
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

            ds = RptSalesCtl.GetSKUCartonSale(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToInt32(this.drpPrincipal.SelectedValue.ToString()), dtFrom, dtTo, Convert.ToInt32(Session["UserID"]), Catagories_IDs,Convert.ToInt32(rblType.SelectedValue),Convert.ToInt32(ddlChannelType.SelectedValue),int.Parse(DrpCustomer.SelectedValue), SkuType, int.Parse(DrpTown.SelectedValue));
            foreach(DataRow dr in ds.Tables["uspGetSKUCtnSales"].Rows)
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
            CrpReport.SetParameterValue("Distributor", this.DrpCustomer.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("FromMonth", this.txtFromDate.Text);
            CrpReport.SetParameterValue("ToMonth", this.txtToDate.Text);
            CrpReport.SetParameterValue("ReportName", "SKU Wise Sales (" + rblType.SelectedItem.Text + ")");
            CrpReport.SetParameterValue("TotalProductsTotal", TotalProductsTotal);
            CrpReport.SetParameterValue("ChannelType", ddlChannelType.SelectedItem.Text);
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

    protected void DrpTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTown();
        LoadCustomer();
        LoadChannelType();
    }
}