using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Linq;

public partial class Forms_frmPurchaseDocuments : System.Web.UI.Page
{
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    readonly DataControl dc = new DataControl();

    private static int RowNo;
    private static int PrivouseQty, FreePrivousQty;

    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal(true);
            LoadDistributor();
            LoadDistributorType();
            LoadSKUDetail();
            CreatTable();
            GetDocumentNo();
            LoadAccount();
            LoadExpenseAccount();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
        }
    }

    /// <summary>
    /// Creats Datatable For Document
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
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(int));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        PurchaseSKU.Columns.Add("FREE_SKU", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("UNITS_IN_CASE", typeof(int));
        PurchaseSKU.Columns.Add("SKU_WEIGHT", typeof(decimal));
        PurchaseSKU.Columns.Add("TOTAL_SKU_WEIGHT", typeof(decimal));
        PurchaseSKU.Columns.Add("prevQty", typeof(decimal));
        PurchaseSKU.Columns.Add("AVG_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("PRE_AVG_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("PRE_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("CLOSING_STOCK", typeof(int));
        Session.Add("PurchaseSKU", PurchaseSKU);
    }

    private void LoadAccount()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 28);//
        clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
    }
    private void LoadExpenseAccount()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable ExpHd = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 840);//
        clsWebFormUtil.FillDropDownList(DrpExpenseAccountHead, ExpHd, 0, 4, true);
        Session.Add("ExpHd", ExpHd);
        DrpExpenseAccountHead_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// Sets Form Controls According To Selected Document Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedIndex == 0)
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Purchase For";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;

            GetDocumentNo();
            lblAccount.Visible = true;
            DrpAccountDetail.Visible = true;
            PanelPrincipal.Visible = true;
            PanelTransfer.Visible = false;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;
            LoadPrincipal(true);
            LoadSKUDetail();
            Label1.Visible = true;
            DrpExpenseAccountHead.Visible = true;
            Label8.Visible = true;
            txtExpenseAmount.Visible = true;
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            lblfromLocation.Text = "Transfer From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            LoadToDistributor();
            DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Transfer To";
            GetDocumentNo();
            PanelTransfer.Visible = true;
            lblAccount.Visible = false;
            DrpAccountDetail.Visible = false;
            PanelPrincipal.Visible = false;
            LoadPrincipal(false);
            LoadSKUDetail();
            txtprice.Enabled = true;
            Label1.Visible = false;
            DrpExpenseAccountHead.Visible = false;
            Label8.Visible = false;
            txtExpenseAmount.Visible = false;
        }
        else if (DrpDocumentType.SelectedIndex == 2)
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Return From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;
            GetDocumentNo();
            PanelTransfer.Visible = false;
            lblAccount.Visible = true;
            DrpAccountDetail.Visible = true;
            PanelPrincipal.Visible = true;
            LoadPrincipal(true);
            LoadSKUDetail();
            Label1.Visible = false;
            DrpExpenseAccountHead.Visible = false;
            Label8.Visible = false;
            txtExpenseAmount.Visible = false;
        }
        //else if (DrpDocumentType.SelectedIndex == 3)
        //{
        //    lblfromLocation.Text = "Transfer to";
        //    drpDistributor.Enabled = true;
        //    drpPrincipal.Enabled = true;
        //    LoadToDistributor();
        //    DrpTransferFor.Visible = true;
        //    Label4.Visible = true;
        //    Label4.Text = "Transfer From";
        //    GetDocumentNo();
        //    PanelTransfer.Visible = true;
        //    lblAccount.Visible = false;
        //    DrpAccountDetail.Visible = false;
        //    PanelPrincipal.Visible = false;
        //    LoadPrincipal(false);
        //    LoadSKUDetail();
        //    Label1.Visible = false;
        //    DrpExpenseAccountHead.Visible = false;
        //    Label8.Visible = false;
        //    txtExpenseAmount.Visible = false;
        //}
        else
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Location";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;
            GetDocumentNo();
            PanelTransfer.Visible = false;
            lblAccount.Visible = false;
            DrpAccountDetail.Visible = false;
            PanelPrincipal.Visible = false;
            LoadPrincipal(false);
            LoadSKUDetail();
            Label1.Visible = false;
            DrpExpenseAccountHead.Visible = false;
            Label8.Visible = false;
            txtExpenseAmount.Visible = false;
        }
        LoadDistributorType();
        ddlSkus_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            CreatTable();
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            ClearAll();
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
        }
        else
        {
            txtRemarks.Text = "";
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            DrpDocumentType.Enabled = false;
            LoadDocumentDetail();
            //LoadSKUDetail();
        }
        LoadDistributorType();
    }

    /// <summary>
    /// Loads Principals To Principal Comb
    /// </summary>
    private void LoadPrincipal(bool p_flag)
    {
        if (p_flag)
        {
            drpPrincipal.Items.Clear();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, 0, 1, true);
        }
        else
        {
            drpPrincipal.Items.Clear();
            drpPrincipal.Items.Add(new ListItem("All", "0"));
        }

    }


    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        this.Session.Add("DisDt", dt);

    }

    private void LoadDistributorType()
    {
        //if (DrpDocumentType.SelectedValue == "5")
        //{
        //    txtprice.Enabled = true;
        //}
        //else
        //{
        txtprice.Enabled = false;
        //}
        lblAccount.Visible = false;
        DrpAccountDetail.Visible = false;
        if (DrpDocumentType.SelectedValue == "2" || DrpDocumentType.SelectedValue == "3" || DrpDocumentType.SelectedValue == "6" || DrpDocumentType.SelectedValue == "10")//purchase, and purchase return,damage(expense),damage claimable
        {
            DataTable DisDt = (DataTable)this.Session["DisDt"];
            if (DisDt != null)
            {
                if (DisDt.Rows.Count > 0)
                {
                    DataRow[] foundRows = DisDt.Select("DISTRIBUTOR_ID  = '" + drpDistributor.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        if (foundRows[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                        {
                            lblAccount.Visible = true;
                            DrpAccountDetail.Visible = true;

                            txtprice.Enabled = true;

                        }

                    }
                }
            }


        }

    }
    /// <summary>
    /// Loads Locations To Location To Combo
    /// </summary>
    private void LoadToDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpTransferFor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            if (DrpDocumentType.SelectedIndex == 0)
            {
                LoadPrincipal(true);
                drpPrincipal.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                DrpExpenseAccountHead.SelectedValue = dt.Rows[0]["EXPENSE_HEAD_ID"].ToString();
                LoadSKUDetail();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtExpenseAmount.Text = dt.Rows[0]["TOTAL_EXPENSE_AMOUNT"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 3)
            {
                LoadPrincipal(false);
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 1)
            {
                LoadPrincipal(false);
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 2)
            {
                LoadPrincipal(true);
                drpPrincipal.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                LoadSKUDetail();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
            else
            {
                LoadPrincipal(false);
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
            PurchaseSKU = mPurchase.SelectPurchaseDetail(int.Parse(drpDistributor.SelectedValue), long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalCtn = 0;
        int TotalUnit = 0;
        decimal TotalAmount = 0;
        decimal TotalSKUWeight = 0;
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        if (DrpDocumentType.SelectedValue == "2")
        {
            decimal ExpAmount = decimal.Parse(txtExpenseAmount.Text);
            if (ExpAmount < 0)
            {
                TotalAmount = 0;
            }
            else
            {
                TotalAmount = decimal.Parse(txtExpenseAmount.Text);
            }
        }
        if (PurchaseSKU.Rows.Count > 0)
        {
            foreach (DataRow dr in PurchaseSKU.Rows)
            {
                TotalCtn += int.Parse(dr["QUANTITY_CTN"].ToString());
                TotalUnit += int.Parse(dr["Quantity"].ToString());
                TotalAmount += decimal.Parse(dr["Amount"].ToString());
                TotalSKUWeight += decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
            }
        }
        txtTotalCtn.Text = TotalCtn.ToString();
        txtTotalUnit.Text = TotalUnit.ToString();
        txtTotalAmount.Text = TotalAmount.ToString();
        hfTotalWeight.Value = TotalSKUWeight.ToString();
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtCtn.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        PrivouseQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text);
        FreePrivousQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text);
        txtFreeSKU.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtBatchNo.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        ddlSkus.Enabled = false;
        txtprice.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text;
        txtQuantity.Focus();
        btnSave.Text = "Update";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            ClearAll();
        }
        ddlSkus_SelectedIndexChanged(null, null);
        ddlSkus.Focus();

    }

    /// <summary>
    /// Loads SKU Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        if (drpPrincipal.Items.Count > 0)
        {
            if (DrpDocumentType.SelectedValue == "2" || DrpDocumentType.SelectedValue == "3" || DrpDocumentType.SelectedValue == "4")
            {
                DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 5, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 9, true);
                Session.Add("Dtsku_Price", Dtsku_Price);
            }
            else
            {
                DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 9, true);
                Session.Add("Dtsku_Price", Dtsku_Price);
            }
        }
    }

    /// <summary>
    /// Enables/Disables Batch No TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbBatchNo_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbBatchNo.Checked == true)
        {

            txtBatchNo.Enabled = true;
        }
        else
        {
            txtBatchNo.Text = "N/A";
            txtBatchNo.Enabled = false;
        }
    }

    /// <summary>
    /// Enables/Disables Apply Free SKU TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbFreeSKU_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbFreeSKU.Checked == true)
        {

            txtFreeSKU.Enabled = true;
        }
        else
        {
            txtFreeSKU.Text = "0";
            txtFreeSKU.Enabled = false;
        }
    }

    public bool CheckStock()
    {
        if (DrpDocumentType.SelectedValue == "3" || DrpDocumentType.SelectedValue == "5" || DrpDocumentType.SelectedValue == "6" || DrpDocumentType.SelectedValue == "10")
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
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal SKUWeight;
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            decimal mStdDiscount = decimal.Parse(dc.chkNull_0(foundRows[0]["DISTRIBUTOR_DISCOUNT"].ToString()));
            decimal mGSTRate = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString()));
            int CurrentStock = CheckStockStatus(int.Parse(dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));
            decimal mPrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));
            int UOM = int.Parse(dc.chkNull_0(foundRows[0]["UOM"].ToString()));
            decimal Weight = decimal.Parse(dc.chkNull_0(foundRows[0]["SKU_WEIGHT"].ToString()));
            decimal ExpenseAmount = decimal.Parse(txtExpenseAmount.Text);
            if (UOM == 2 || UOM == 4)
            {
                SKUWeight = Weight * 1000;
            }
            else
            {
                SKUWeight = decimal.Parse(dc.chkNull_0(foundRows[0]["SKU_WEIGHT"].ToString()));
            }
            if (DrpDocumentType.SelectedValue == "2" || DrpDocumentType.SelectedValue == "3" || DrpDocumentType.SelectedValue == "6" || DrpDocumentType.SelectedValue == "10")// purchase and purhcae return,damage (expense) damage (claimable)
            {
                DataTable DisDt = (DataTable)this.Session["DisDt"];
                if (DisDt != null)
                {
                    if (DisDt.Rows.Count > 0)
                    {
                        DataRow[] found = DisDt.Select("DISTRIBUTOR_ID  = '" + drpDistributor.SelectedValue + "'");
                        if (found.Length > 0)
                        {
                            if (found[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                            {
                                mPrice = decimal.Parse(dc.chkNull_0(txtprice.Text));
                            }
                        }
                    }
                }
            }
            if (btnSave.Text == "Add")
            {
                if (CheckDublicateSKU())

                {
                    if (txtprice.Text.Equals(""))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Price is required.');", true);
                        txtprice.Focus();
                        return;
                    }
                    if (txtprice.Text.Equals("0") || txtprice.Text.Equals("0.0") || txtprice.Text.Equals("0.00"))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Price must be greater then zero.');", true);
                        txtprice.Focus();
                        return;
                    }
                    if (CheckStock())
                    {
                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["BATCH_NO"] = txtBatchNo.Text;
                        dr["FREE_SKU"] = int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                        dr["PRICE"] = mPrice;
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                        dr["UNITS_IN_CASE"] = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
                        dr["AMOUNT"] = mPrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                        int TotalUnits = (int.Parse(dc.chkNull_0(txtCtn.Text)) * int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()))) + int.Parse(dc.chkNull_0(txtQuantity.Text));
                        decimal TotalWeight = decimal.Parse(TotalUnits.ToString()) * SKUWeight;
                        decimal TotalWeightinKg = TotalWeight / 1000;
                        decimal SKUWeightinKg = SKUWeight / 1000;
                        dr["SKU_WEIGHT"] = SKUWeightinKg;
                        dr["TOTAL_SKU_WEIGHT"] = TotalWeightinKg;

                        // Average Price
                        #region Average Price
                        //decimal Closing_Stock_Amount = Closing_Stock * Last_Avg_Price;
                        //decimal Total_Stock_Amount = Closing_Stock_Amount + decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) + OHAmount;
                        //decimal Total_Quantity = Closing_Stock + TotalUnits; //+ decimal.Parse(dc.chkNull_0(dr["Quantity"].ToString()));
                        //dr["AVG_PRICE"] = Total_Stock_Amount / Total_Quantity;
                        //dr["PRE_AVG_RATE"] = Last_Avg_Price;
                        //dr["PRE_QUANTITY"] = 0;
                        //dr["CLOSING_STOCK"] = Total_Quantity;
                        #endregion
                        PurchaseSKU.Rows.Add(dr);
                        AverageCalculationOnSave();
                        Session.Add("PurchaseSKU", PurchaseSKU);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.Text + " Current closing Stock is " + CurrentStock.ToString() + "');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnSave.Text == "Update")
            {
                //if (DrpDocumentType.SelectedValue == "3" || DrpDocumentType.SelectedValue == "5" || DrpDocumentType.SelectedValue == "6" || DrpDocumentType.SelectedValue == "10")
                //{
                if (txtprice.Text.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Price is required.');", true);
                    txtprice.Focus();
                    return;
                }
                if (txtprice.Text.Equals("0") || txtprice.Text.Equals("0.0") || txtprice.Text.Equals("0.00"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Price must be greater then zero.');", true);
                    txtprice.Focus();
                    return;
                }
                if (CheckStock())
                {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["BATCH_NO"] = txtBatchNo.Text;
                    dr["PRICE"] = mPrice;
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["FREE_SKU"] = int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                    dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                    dr["UNITS_IN_CASE"] = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
                    dr["AMOUNT"] = mPrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                    int TotalUnits = (int.Parse(dc.chkNull_0(txtCtn.Text)) * int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()))) + int.Parse(dc.chkNull_0(txtQuantity.Text));
                    decimal TotalWeight = decimal.Parse(TotalUnits.ToString()) * SKUWeight;
                    decimal TotalWeightinKg = TotalWeight / 1000;
                    decimal SKUWeightinKg = SKUWeight / 1000;
                    dr["SKU_WEIGHT"] = SKUWeightinKg;
                    dr["TOTAL_SKU_WEIGHT"] = TotalWeightinKg;

                    Session.Add("PurchaseSKU", PurchaseSKU);
                    AverageCalculationOnUpdate();

                    // Average Price
                    #region Average Price
                    //decimal Closing_Stock_Amount = (Closing_Stock - Pre_Quantity) * Last_Avg_Price;
                    //decimal Total_Stock_Amount = Closing_Stock_Amount + decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) + OHAmount;
                    //decimal Total_Quantity = (Closing_Stock - Pre_Quantity) + TotalUnits; //+ decimal.Parse(dc.chkNull_0(dr["Quantity"].ToString()));
                    //dr["AVG_PRICE"] = Total_Stock_Amount / Total_Quantity;
                    //dr["PRE_AVG_RATE"] = Last_Avg_Price;
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.Text + "Current closing Stock is " + CurrentStock.ToString() + "');", true);
                    return;
                }
            }
            
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
            ddlSkus_SelectedIndexChanged(null, null);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }

    public void AverageCalculationOnSave()
    {
        int TotalQty = 0;
        decimal TotalWeight = 0;
        decimal OHAmount;
        decimal OHItemAmount;
        decimal OHItemWeight;
        DataTable PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        decimal ExpenseAmount = decimal.Parse(txtExpenseAmount.Text);
        if (ExpenseAmount < 0)
        {
            OHAmount = 0;
        }
        else
        {
            OHAmount = decimal.Parse(txtExpenseAmount.Text);
        }
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalQty += (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString())) + int.Parse(dr["Quantity"].ToString());
            TotalWeight += decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
        }
        if (TotalWeight == 0)
        {
            TotalWeight = 1;
        }
        OHItemAmount = OHAmount / TotalQty;
        OHItemWeight = OHAmount / TotalWeight;
        decimal SKUWeight;
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + int.Parse(dr["SKU_ID"].ToString()) + "'");
            if (foundRows.Length > 0)
            {
                decimal Last_Avg_Price = decimal.Parse(foundRows[0]["LAST_AVG_PRICE"].ToString());
                decimal Closing_Stock = decimal.Parse(foundRows[0]["CLOSING_STOCK"].ToString());
                int UOM = int.Parse(dc.chkNull_0(foundRows[0]["UOM"].ToString()));
                decimal Weight = decimal.Parse(dc.chkNull_0(foundRows[0]["SKU_WEIGHT"].ToString()));
                if (UOM == 2 || UOM == 4)
                {
                    SKUWeight = Weight;
                }
                else
                {
                    SKUWeight = decimal.Parse(dc.chkNull_0(foundRows[0]["SKU_WEIGHT"].ToString())) / 1000;
                }
                //// Average Price
                #region Average Price
                int TotalUnits = (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString())) + int.Parse(dr["Quantity"].ToString());
                decimal Closing_Stock_Amount = Closing_Stock * Last_Avg_Price;
                decimal Total_Stock_Amount = (Closing_Stock_Amount + decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()))) + (OHItemWeight * SKUWeight * TotalUnits); //(OHItemAmount * TotalUnits);
                decimal Total_Quantity = Closing_Stock + TotalUnits;
                decimal TatalWeight = Total_Quantity * SKUWeight;
                decimal AVG_PRICE = Total_Stock_Amount / Total_Quantity;
                dr["AVG_PRICE"] = AVG_PRICE;
                dr["PRE_AVG_RATE"] = Last_Avg_Price;
                dr["PRE_QUANTITY"] = 0;
                dr["CLOSING_STOCK"] = Total_Quantity;
                #endregion
            }
        }
    }
    public void AverageCalculationOnUpdate()
    {
        int TotalQty = 0;
        decimal TotalWeight = 0;
        decimal OHAmount;
        decimal OHItemAmount;
        decimal OHItemWeight;
        DataTable PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        decimal ExpenseAmount = decimal.Parse(txtExpenseAmount.Text);
        if (ExpenseAmount < 0)
        {
            OHAmount = 0;
        }
        else
        {
            OHAmount = decimal.Parse(txtExpenseAmount.Text);
        }
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalQty += (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString())) + int.Parse(dr["Quantity"].ToString());
            TotalWeight += decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
        }
        if (TotalWeight == 0)
        {
            TotalWeight = 1;
        }
        OHItemAmount = OHAmount / TotalQty;
        OHItemWeight = OHAmount / TotalWeight;
        decimal SKUWeight;
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            decimal Pre_Quantity = decimal.Parse(dr["PRE_QUANTITY"].ToString());
            decimal Last_Avg_Price = decimal.Parse(dr["PRE_AVG_RATE"].ToString());
            decimal Closing_Stock = decimal.Parse(dr["CLOSING_STOCK"].ToString());
            int UOM = int.Parse(dc.chkNull_0(dr["UOM"].ToString()));
            decimal Weight = decimal.Parse(dc.chkNull_0(dr["SKU_WEIGHT"].ToString()));
            if (UOM == 2 || UOM == 4)
            {
                SKUWeight = Weight;
            }
            else
            {
                SKUWeight = decimal.Parse(dc.chkNull_0(dr["SKU_WEIGHT"].ToString())) / 1000;
            }
            //// Average Price
            #region Average Price
            int TotalUnits = (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString())) + int.Parse(dr["Quantity"].ToString());
            decimal Closing_Stock_Amount = (Closing_Stock - Pre_Quantity) * Last_Avg_Price;
            decimal Total_Stock_Amount = (Closing_Stock_Amount + decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()))) + (OHItemWeight * SKUWeight * TotalUnits); //(OHItemAmount * TotalUnits);
            decimal Total_Quantity = (Closing_Stock - Pre_Quantity) + TotalUnits;
            decimal TatalWeight = Total_Quantity * SKUWeight;
            decimal AVG_PRICE = Total_Stock_Amount / Total_Quantity;
            dr["AVG_PRICE"] = AVG_PRICE;
            dr["PRE_AVG_RATE"] = Last_Avg_Price;
            #endregion
        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
        if (DrpDocumentType.SelectedIndex == 2) // || DrpDocumentType.SelectedIndex == 3
        {
            if (drpDistributor.SelectedValue.ToString() == DrpTransferFor.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' Transfer to Location must be different ');", true);
                return;
            }
        }
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
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
                    if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                    {
                        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
                        PurchaseSKU.Rows.Clear();
                        Session.Add("PurchaseSKU", PurchaseSKU);
                        Session["PurchaseSKU"] = null;
                        CreatTable();
                        LoadGird();
                        GetDocumentNo();
                        LoadSKUDetail();
                        drpDistributor.Enabled = true;
                        drpPrincipal.Enabled = true;
                        DrpDocumentType.Enabled = true;
                        ClearAll();
                        LoadExpenseAccount();
                        txtBuiltyNo.Text = "";
                        txtDocumentNo.Text = "";
                        txtDocumentNo.Text = "";
                        DisAbaleOption(false);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);
        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        CreatTable();
        LoadGird();
        ClearAll();
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = "";
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];

        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime MWorkDate)
    {
        string Voucher_No;
        decimal mTotalAmount = 0;
        decimal OHAmount;
        decimal TWeight;
        
        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];

        #region CHECKS
        foreach (DataRow dr in dtPurchaseDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

        }
        decimal ExpenseAmount = decimal.Parse(txtExpenseAmount.Text);
        if (ExpenseAmount < 0)
        {
            OHAmount = 0;
        }
        else
        {
            OHAmount = decimal.Parse(txtExpenseAmount.Text);
        }
        if (decimal.Parse(hfTotalWeight.Value) < 0)
        {
            TWeight = 0;
        }
        else
        {
            TWeight = decimal.Parse(hfTotalWeight.Value);
        }
        int p_Account_Head_ID = Constants.IntNullValue;
        string p_Account_Name = null;
        DataTable DisDt = (DataTable)this.Session["DisDt"];
        if (DisDt != null)
        {
            if (DisDt.Rows.Count > 0)
            {
                DataRow[] foundRows = DisDt.Select("DISTRIBUTOR_ID  = '" + drpDistributor.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    if (foundRows[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                    {
                        if (DrpDocumentType.SelectedValue == "2" || DrpDocumentType.SelectedValue == "3")
                        {
                            p_Account_Head_ID = Convert.ToInt32(DrpAccountDetail.SelectedValue);
                            p_Account_Name = DrpAccountDetail.SelectedItem.Text;
                        }
                    }
                }
            }
        }
        #endregion

        if (DrpDocumentType.SelectedIndex == 0)              //purchase
        {
            #region PURCHASES
            if (DrpAccountDetail.Items.Count > 0)
            {
                if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
                {
                    bool mResult = mController.InsertStockDocumentGLForPurchaseForQureshi(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                    , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), p_Account_Head_ID, p_Account_Name, out Voucher_No, txtRemarks.Text, drpPrincipal.SelectedItem.Text, int.Parse(DrpExpenseAccountHead.SelectedValue), OHAmount, TWeight, 1);
                    PrintReport(Voucher_No);
                    return mResult;
                }
                else
                {
                    bool mResult = mController.UpdateStockDocumentGLForPurchaseForQureshi(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                    , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), p_Account_Head_ID, p_Account_Name, out Voucher_No, txtRemarks.Text, drpPrincipal.SelectedItem.Text, int.Parse(DrpExpenseAccountHead.SelectedValue), OHAmount, TWeight, 1);
                    PrintReport(Voucher_No);
                    return mResult;
                }
            }
            else
            {
                return false;
            }
            #endregion
        }
        else if (DrpDocumentType.SelectedIndex == 1)        // Transfer OUT
        {
            #region TRANSFER OUT
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                bool mResult = mController.InsertStockDocumentGL(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
                return mResult;
            }
            else
            {
                bool mResult = mController.UpdateStockDocumentGL(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
                return mResult;
            }
            #endregion
        }
        else if (DrpDocumentType.SelectedIndex == 2)        // Purchase Return
        {
            #region PURCHASES RETURN
            if (DrpAccountDetail.Items.Count > 0)
            {
                if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
                {
                    bool mResult = mController.InsertStockDocumentGLForPurchase(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                    , MWorkDate, 0, int.Parse(drpDistributor.SelectedValue.ToString())
                    , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), p_Account_Head_ID, p_Account_Name, out Voucher_No, txtRemarks.Text, drpPrincipal.SelectedItem.Text, Constants.IntNullValue, OHAmount, TWeight, 1);
                    PrintReport(Voucher_No);
                    return mResult;
                }
                else
                {
                    bool mResult = mController.UpdateStockDocumentGLForPurchase(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                    , MWorkDate, 0, int.Parse(drpDistributor.SelectedValue.ToString())
                    , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), p_Account_Head_ID, p_Account_Name, out Voucher_No, txtRemarks.Text, drpPrincipal.SelectedItem.Text, Constants.IntNullValue, OHAmount, TWeight, 1);
                    PrintReport(Voucher_No);
                    return mResult;
                }
            }
            else
            {
                return false;
            }
            #endregion
        }
        //else if (DrpDocumentType.SelectedIndex == 3)        //Transfer IN
        //{
        //    #region TRANSFER IN
        //    if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        //    {
        //        //bool mResult = mController.InsertStockDocumentGL(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
        //        //, MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
        //        //, mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
        //        bool mResult = mController.InsertStockDocumentGLForPurchaseForQureshi(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
        //            , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), p_Account_Head_ID, p_Account_Name, out Voucher_No, txtRemarks.Text, drpPrincipal.SelectedItem.Text, int.Parse(DrpExpenseAccountHead.SelectedValue), OHAmount, TWeight, 1);
        //        return mResult;
        //    }
        //    else
        //    {
        //        //bool mResult = mController.UpdateStockDocumentGL(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
        //        //, MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
        //        //, mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
        //        bool mResult = mController.UpdateStockDocumentGLForPurchaseForQureshi(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
        //            , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), p_Account_Head_ID, p_Account_Name, out Voucher_No, txtRemarks.Text, drpPrincipal.SelectedItem.Text, int.Parse(DrpExpenseAccountHead.SelectedValue), OHAmount, TWeight, 1);
        //        return mResult;
        //    }
        //    #endregion
        //}
        else if (DrpDocumentType.SelectedIndex == 3)        //Damage
        {
            #region DAMAGE
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                bool mResult = mController.InsertStockDocumentGL(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
                return mResult;
            }
            else
            {
                bool mResult = mController.UpdateStockDocumentGL(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
                return mResult;
            }
            #endregion
        }
        else if (DrpDocumentType.SelectedIndex == 4)        //Damage claimable
        {
            #region DAMAGE CLAIMABLE
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                bool mResult = mController.InsertStockDocumentGL(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
                return mResult;
            }
            else
            {
                bool mResult = mController.UpdateStockDocumentGL(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, null, out Voucher_No, txtRemarks.Text);
                return mResult;
            }
            #endregion
        }
        else                                                //By Defualt
        {
            #region IF NOT SELECTED
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                bool mResult = mController.InsertStockDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), txtRemarks.Text);
                return mResult;
            }
            else
            {
                bool mResult = mController.UpdateStockDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), txtRemarks.Text);
                return mResult;
            }
            #endregion
        }
    }

    /// <summary>
    /// Checks SKU Stock
    /// </summary>
    /// <param name="SKU_ID">SKU</param>
    /// <returns>SKU Stock as Integer</returns>
    private int CheckStockStatus(int SKU_ID)
    {
        if (DrpDocumentType.SelectedIndex == 0 || DrpDocumentType.SelectedIndex == 3)
        {
            return -1;
        }
        else
        {
            PhaysicalStockController mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue.ToString()), SKU_ID, txtBatchNo.Text, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            if (dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty >= int.Parse(dc.chkNull_0(txtQuantity.Text)))
                {
                    return -1;
                }
                else
                {
                    return int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty;
                }
            }
        }

        return 0;
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
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
            DrpExpenseAccountHead.Enabled = false;
        }
        else
        {
            DrpExpenseAccountHead.Enabled = true;
            DrpDocumentType.Enabled = true;
            drpPrincipal.Enabled = true;
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
        txtFreeSKU.Text = "0";
        txtBatchNo.Text = "N/A";
        ddlSkus.Enabled = true;
        btnSave.Text = "Add";
        PrivouseQty = 0;
        FreePrivousQty = 0;
        txtCtn.Text = "";
        txtprice.Text = "";
        txtTotalAmount.Text = "";
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
        LoadDistributorType();
    }

    private void PrintReport(string p_Voucher_No)
    {
        DataTable DisDt = (DataTable)this.Session["DisDt"];
        if (DisDt != null)
        {
            if (DisDt.Rows.Count > 0)
            {
                DataRow[] foundRows = DisDt.Select("DISTRIBUTOR_ID  = '" + drpDistributor.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    if (foundRows[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                    {
                        #region print report
                        DocumentPrintController DPrint = new DocumentPrintController();
                        RptAccountController RptAccountCtl = new RptAccountController();
                        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();

                        DataSet ds = null;
                        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), p_Voucher_No, Constants.Journal_Voucher);
                        CrpReport.SetDataSource(ds);
                        CrpReport.Refresh();
                        string VoucherType2 = "Journal Voucher";
                        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
                        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
                        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                        CrpReport.SetParameterValue("Voucher_Type", VoucherType2);
                        CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
                        this.Session.Add("CrpReport", CrpReport);
                        this.Session.Add("ReportType", 0);
                        string url = "'Default.aspx'";
                        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                        Type cstype = this.GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(cstype, "OpenWindow", script);
                        #endregion

                    }

                }
            }
        }


    }

    protected void ddlSkus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedValue == "4" || DrpDocumentType.SelectedValue == "5")
        {
            DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
            DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
            decimal Price = decimal.Parse(foundRows[0]["TRADE_PRICE"].ToString());
            txtprice.Text = Math.Round(Price, 2).ToString();
        }
        else
        {
            txtprice.Text = "0.00";
        }

    }

    protected void DrpExpenseAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)this.Session["ExpHd"];
        DataRow[] foundRows = dt.Select("ACCOUNT_HEAD_ID  = '" + DrpExpenseAccountHead.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            decimal Price = decimal.Parse(foundRows[0]["DEBIT_AMOUNT"].ToString());
            txtExpenseAmount.Text = Math.Round(Price, 2).ToString();
        }
        else
        {
            txtExpenseAmount.Text = "0.00";
        }
            
    }
}