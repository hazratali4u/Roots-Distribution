using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Linq;

/// <summary>
/// Form To Gift SKU
/// </summary>
public partial class Forms_frmGiftSKU : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos, ListBox And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            LoadPrincipal();
            LoadDistributor();
            LoadSKUDetail();
            LoadCustomerDetail();
            LoadFreeSKU();
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        LoadSaleForce();
    }

    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    private void LoadSaleForce()
    {        
        if (drpDistributor.Items.Count > 0)
        {
            DrpSaleForce.Items.Clear();
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
            DrpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpSaleForce, m_dt, 0, 3, false);
        }
        LoadInvoiceNo();
        DrpSaleForce.Focus();
    }

    /// <summary>
    /// Loads Invoice Nos To Invoice No Combo
    /// </summary>
    private void LoadInvoiceNo()
    {
        OrderEntryController DOrder = new OrderEntryController();
        drpDocumentNo.Items.Clear();
        DataTable dtOrder = DOrder.SelectDocumentforView(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                       DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()),1,Constants.IntNullValue,Convert.ToInt32(DrpSaleForce.SelectedValue));
                 
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dtOrder, 8, 8);
        Session.Add("dtOrder", dtOrder);  
    }
    
    /// <summary>
    /// Loads Customer Code To Code TextBox And Name To Name TextBox
    /// </summary>
    private void LoadCustomerDetail()
    {
        if (drpDocumentNo.Items.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtOrder"];
            DataRow[] foundRows = dt.Select("DocumentNo  = '" + drpDocumentNo.SelectedItem.Text + "'");
            if (foundRows.Length > 0)
            {
                txtOutletCode.Text = foundRows[0]["CUSTOMER_CODE"].ToString();
                txtOutletName.Text = foundRows[0]["SOLD_TO"].ToString();
                ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
            }
        }
    }
    
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        if (drpDistributor.Items.Count    > 0)
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 10, true);
            Session.Add("Dtsku_Price", Dtsku_Price);
            Stock();
        }
    }
    
    /// <summary>
    /// Loads Free SKUS To Grid
    /// </summary>
    private void LoadFreeSKU()
    {
        if (drpDocumentNo.Items.Count > 0)
        {
            OrderEntryController or = new OrderEntryController();
            DataTable dt = or.SelectInvoicePromotion(int.Parse(drpDistributor.SelectedValue.ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()));
            GrdPurchase.DataSource = dt;
            GrdPurchase.DataBind();  
        }
    }
    
    /// <summary>
    /// Saves Gift SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (drpDocumentNo.Items.Count > 0)
        {
            if (IsDayClosed())
            {
                UserController UserCtl = new UserController();

                UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                Session.Clear();
                System.Web.Security.FormsAuthentication.SignOut();
                Response.Redirect("../Login.aspx");
            }
            else
            {

                OrderEntryController or = new OrderEntryController();
                DataControl dc = new DataControl();

                DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
                DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");

                if (foundRows.Length > 0)
                {
                    decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));
                    float mTaxPrice = float.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString()));
                    float mSEDPrice = float.Parse(dc.chkNull_0(foundRows[0]["SED_PRICE"].ToString()));
                    decimal mAmount = mTradePrice * int.Parse(txtQuantity.Text);
                    decimal mTaxAmt = 0;
                    decimal mTSTAmt = 0;
                    decimal mSEDAmt = 0;
                    if (ChbAllTax.Checked == true)
                    {
                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                        {
                            mTaxAmt = (decimal.Parse(mTaxPrice.ToString()) / 100) * mAmount;
                            mSEDAmt = (decimal.Parse(mSEDPrice.ToString()) / 100) * mAmount;

                        }
                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                        {
                            mTSTAmt = (decimal.Parse(mTaxPrice.ToString())) * int.Parse(txtQuantity.Text);
                            mSEDAmt = 0;
                        }

                    }
                    PhaysicalStockController mController = new PhaysicalStockController();
                    DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(foundRows[0]["SKU_ID"].ToString()), txtBatchNo.Text, DateTime.Parse(Session["CurrentWorkDate"].ToString()));

                    if (dtstock.Rows.Count > 0)
                    {
                        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(txtQuantity.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + foundRows[0]["SKU_NAME"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                            return;
                        }
                    }
                    or.InsertFreeSKU(int.Parse(drpDistributor.SelectedValue.ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(foundRows[0]["SKU_ID"].ToString()), int.Parse(txtQuantity.Text)
                    , mTradePrice, mAmount, mTaxPrice, mTaxAmt, DateTime.Parse(Session["CurrentWorkDate"].ToString()), mTSTAmt, mSEDAmt);
                    LoadFreeSKU();
                    txtQuantity.Text = "";
                   
                    txtUnitRate.Text = "";
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
                }
            }
        }                
    }
    
    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
        drpDistributor.Focus();
    }

    /// <summary>
    /// Loads Free SKUS To Grid And Customer Code To Code TextBox And Name To Name TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFreeSKU();
        LoadCustomerDetail();
    }

    /// <summary>
    /// Deletes Gift SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OrderEntryController or = new OrderEntryController();
        or.DeleteFreeSKUFromInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()), long.Parse(GrdPurchase.Rows[e.RowIndex].Cells[0].Text), int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[1].Text), int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[4].Text));
        LoadFreeSKU();
        ddlSkus.Focus();
    }

    /// <summary>
    /// Loads Invoice Nos To Invoice No Combo, Free SKUS To Grid And Customer Code To Code TextBox And Name To Name TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpSaleForce_SelectedIndexChanged(object sender, EventArgs e)
    { 
        LoadInvoiceNo();
        LoadFreeSKU();
        LoadCustomerDetail();
    }

    /// <summary>
    /// Loads Sale Forces To Sale Force Combo And SKUS To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
        LoadSKUDetail();
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
    public void Stock()
    {
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        

        if (foundRows.Length > 0)
        {
            string Stock = (foundRows[0]["Stock"].ToString());
            string UNIT_IN_CASE = (foundRows[0]["UNITS_IN_CASE"].ToString());
            string a = Stock;
            int comma = a.IndexOf('-');
            string Ctn = a;
            if (comma != -1)
            {
                Ctn = a.Substring(0, comma);
            }
            string c = Stock;
            string Unt = c.Split('-').Last();
            decimal Units = decimal.Parse(Unt) + (decimal.Parse(Ctn) * decimal.Parse(UNIT_IN_CASE));

            lblClosingStock.Text = Units.ToString();
        }
        else
        {
            lblClosingStock.Text = "0";
        }
    }

    protected void ddlSkus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Stock();
    }
}
