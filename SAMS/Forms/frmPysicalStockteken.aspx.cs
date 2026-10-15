using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;    

public partial class Forms_frmPysicalStockteken : System.Web.UI.Page
{
    DataTable PurchaseSKU;
    DataControl dc = new DataControl();
    private static int RowNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadSKUDetail();
            this.LoadGird(Constants.IntNullValue,Constants.DateNullValue, Constants.IntNullValue,"Empty");
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtExpiryDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid And SKU Detail To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.LoadGird();
        this.LoadSKUDetail();
    }

    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));

            clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 9, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }

    /// <summary>
    ///  Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadGird(int Distributor, DateTime Stock_Date, int SKU_ID, string Empty)
    {
        Session["PurchaseSKU"] = null;
        if (Empty == "")
        {
            PhaysicalStockController MController = new PhaysicalStockController();
            DataTable dt = MController.SelectPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            GrdPurchase.DataSource = dt;
            GrdPurchase.DataBind();
            Session["PurchaseSKU"] = dt;
        }
        else
        {
            PhaysicalStockController MController = new PhaysicalStockController();
            DataTable dt = MController.SelectPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            GrdPurchase.DataSource = dt;
            GrdPurchase.DataBind();
            Session["PurchaseSKU"] = dt;
        }
        
    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["PAYSICAL_ID"] = null;
        Session["PAYSICAL_ID"] = int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[7].Text);
        PhaysicalStockController MController = new PhaysicalStockController();
        MController.DELETEPysicalStock(int.Parse(Session["PAYSICAL_ID"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[0].Text));

        this.LoadGird(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[0].Text),"");
        Session["PAYSICAL_ID"] = null;
    }

    /// <summary>
    /// Sets Document Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["PAYSICAL_ID"] = null;
        RowNo = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        //txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtusaleableqty.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtExpiryDate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        txtRemarks.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[8].Text;
        txtQuantity.Focus();
        ddlSkus.Enabled = false;
        Session["PAYSICAL_ID"] = GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text;
        btnSave.Text = "Update";
    }
        
    /// <summary>
    /// Checks Duplicate SKU in Document Detail Grid
    /// </summary>
    /// <returns></returns>
    private bool CheckDublicateSKU()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("sku_id  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            DateTime Date = DateTime.Parse(txtExpiryDate.Text);
            DateTime SKUDATE = DateTime.Parse(foundRows[0]["EXPIRY_DATE"].ToString());
            if (SKUDATE == Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Saves/Updates Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EvemtArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        DataControl dc = new DataControl();
 
        if (foundRows.Length > 0)
        {
            if (CheckDublicateSKU())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ddlSkus.SelectedItem.Text + " Already exists with same expiry date');", true);
            }
            else
            {
                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                    this.Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    PhaysicalStockController MController = new PhaysicalStockController();
                    if (btnSave.Text == "Save")
                    {
                        MController.InsertPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(foundRows[0]["SKU_ID"].ToString()), int.Parse(dc.chkNull_0(txtQuantity.Text)), int.Parse(dc.chkNull_0(txtusaleableqty.Text)), decimal.Parse(txtUnitRate.Text), 0, Constants.IntNullValue, DateTime.Parse(txtExpiryDate.Text), txtRemarks.Text);
                    }
                    else
                    {
                        MController.UpdatePysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(foundRows[0]["SKU_ID"].ToString()), int.Parse(dc.chkNull_0(txtQuantity.Text)), int.Parse(dc.chkNull_0(txtusaleableqty.Text)), decimal.Parse(txtUnitRate.Text), 0, Constants.IntNullValue, DateTime.Parse(txtExpiryDate.Text), int.Parse(Session["PAYSICAL_ID"].ToString()), txtRemarks.Text);
                    }
                    Session["PAYSICAL_ID"] = null;
                    this.LoadGird(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(foundRows[0]["SKU_ID"].ToString()), "");
                    this.ClearAll();
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
                }
            }
            
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtRemarks.Text = "";
        txtQuantity.Text = "";
        txtusaleableqty.Text = "";
        txtUnitRate.Text = "0";
        ddlSkus.Enabled = true;
        btnSave.Text = "Save";
    }

    private bool IsDayClosed()
    {
        bool flag = false;
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(drpDistributor.SelectedValue), 3);
        if (Convert.ToDateTime(Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
        {
            flag = false;
        }
        else
        {
            flag = true;
        }

        return flag;
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGird(Constants.IntNullValue, Constants.DateNullValue, Constants.IntNullValue, "Empty");
    }
}
