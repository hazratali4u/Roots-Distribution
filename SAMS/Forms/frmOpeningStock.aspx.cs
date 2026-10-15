using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Linq;

/// <summary>
/// From To Adjust Stock
/// </summary>
public partial class Forms_frmOpeningStock : System.Web.UI.Page
{
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    readonly DataControl dc = new DataControl();

    private static int RowNo;
    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.CreatTable();
            this.GetDocumentNo();
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadSKUDetail();

            btnSave.Attributes.Add("onclick", "return ValidateForm();");
        }
    }

    /// <summary>
    /// Creates Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(int));
        PurchaseSKU.Columns.Add("FREE_SKU", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("UNITS_IN_CASE", typeof(int));

        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDocumentNo();
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            this.LoadGird();
            drpDistributor.Enabled = true;
            //drpPrincipal.Enabled = true;
            DrpDocumentType.Enabled = true;
            this.ClearAll();
            txtDocumentNo.Text = "";
            DisAbaleOption(false);
        }
        else
        {
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
        }
    }

    /// <summary>
    /// Loads Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
    }    
    
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Document Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
    }    
    
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 2, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 9, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }
       
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Deletes Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.LoadGird();
        }
        ddlSkus.Focus();
    }
        
    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
            //drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            txtDocumentNo.Text = dt.Rows[0][2].ToString();
            PurchaseSKU = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }

    /// <summary>
    /// Checks Duplicate SKU in Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSkus.SelectedValue+ "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    
      
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            decimal mStdDiscount = decimal.Parse(dc.chkNull_0(foundRows[0]["DISTRIBUTOR_DISCOUNT"].ToString()));
            decimal mGSTRate = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_REG"].ToString()));

            decimal mPrice = decimal.Parse(dc.chkNull_0(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()));
            if (DrpDocumentType.SelectedValue == "8" || DrpDocumentType.SelectedValue == "9")// short,excess
            {
                mPrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));
            }
            if (btnSave.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    if (DrpDocumentType.SelectedValue != "8")
                    {
                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["BATCH_NO"] = txtBatchNo.Text;
                        dr["FREE_SKU"] = 0; // int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                        dr["PRICE"] = mPrice;
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                        dr["UNITS_IN_CASE"] = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

                        dr["AMOUNT"] = mPrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                        PurchaseSKU.Rows.Add(dr);
                    }
                    else
                    {
                        if (CheckStock())
                        {
                            DataRow dr = PurchaseSKU.NewRow();
                            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                            dr["BATCH_NO"] = txtBatchNo.Text;
                            dr["FREE_SKU"] = 0; // int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                            dr["PRICE"] = mPrice;
                            dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                            dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                            dr["UNITS_IN_CASE"] = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

                            dr["AMOUNT"] = mPrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                            PurchaseSKU.Rows.Add(dr);
                        }
                        else
                        {
                            return;
                        }
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSkus.SelectedItem.Text + " Already Exists ');", true);

                    return;
                }
            }
            else if (btnSave.Text == "Update")
            {
                DataRow dr = PurchaseSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["BATCH_NO"] = txtBatchNo.Text;
                dr["PRICE"] = mPrice;
                dr["Quantity"] = int.Parse(txtQuantity.Text);
                dr["FREE_SKU"] = 0; //int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                dr["UNITS_IN_CASE"] = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

                dr["AMOUNT"] = mPrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.ClearAll();
            this.LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong SKU please check in list');", true); 
        }
    }    
     
    /// <summary>
    /// Saves Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        string p_Voucher = "";
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
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

                DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

                PurchaseController mController = new PurchaseController();
                DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];
                decimal mTotalAmount = 0;
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());
                }
                if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
                {
                    bool mResult = mController.InsertStockDocumentGL(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                          , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, Constants.IntNullValue,null, out p_Voucher, txtDocumentNo.Text);
                }
                else
                {
                    bool mResult = mController.UpdateStockDocumentGL(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                       , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                       , mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, Constants.IntNullValue,null,out p_Voucher, txtDocumentNo.Text);
                }

                lblErrorMsg.Text = "Record Upated";
                PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                PurchaseSKU.Rows.Clear();
                this.Session.Add("PurchaseSKU", PurchaseSKU);
                this.LoadGird();
                this.GetDocumentNo();
                drpDistributor.Enabled = true;
                //drpPrincipal.Enabled = true;
                DrpDocumentType.Enabled = true;
                this.ClearAll();
                txtDocumentNo.Text = "";
                DisAbaleOption(false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong Location or Unassigne');", true);
        }
    }
   
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.CreatTable();
        this.LoadGird();
        drpDistributor.Enabled = true;
        //drpPrincipal.Enabled = true;
        DrpDocumentType.Enabled = true;
        this.ClearAll();
        txtDocumentNo.Text = "";
        DisAbaleOption(false);
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
            //drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;

        }
        else
        {

            DrpDocumentType.Enabled = true;
            //drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
     
        txtQuantity.Text = "";
        //txtFreeSKU.Text = "0";
        txtBatchNo.Text = "N/A";
        ddlSkus.Enabled = true;
        btnSave.Text = "Add";
        lblErrorMsg.Text = "";
        txtCtn.Text = "";
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

    public bool CheckStock()
    {
        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        string Stock = (foundRows[0]["Stock"].ToString());
        string SKU_NAME = (foundRows[0]["SKU_NAME"].ToString());
        string UNITS_IN_CASE = (foundRows[0]["UNITS_IN_CASE"].ToString());
        string a = Stock;
        int comma = a.IndexOf('-');
        string Ctn = a;
        if (comma != -1)
        {
            Ctn = a.Substring(0, comma);
        }
        string c = Stock;
        string Unt = c.Split('-').Last();
        if (txtCtn.Text.Equals("") || txtCtn.Text == "0")
        {
            int Units = int.Parse(Unt) + (int.Parse(Ctn) * int.Parse(UNITS_IN_CASE));
            if (int.Parse(txtQuantity.Text) > Units)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + SKU_NAME + " Current Stock is " + Stock + "');", true);
                txtQuantity.Focus();
                txtQuantity.Attributes.Add("onfocus", "javascript:this.select();");
                return false;
            }
        }
        else
        {
            if (int.Parse(txtCtn.Text) > int.Parse(Ctn))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + SKU_NAME + " Current Stock is " + Stock + "');", true);
                txtCtn.Focus();
                txtCtn.Attributes.Add("onfocus", "javascript:this.select();");
                return false;
            }
        }
        return true;
    }
}