using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Linq;

/// <summary>
/// Form To Take Order, Invoice And Sale Return(Step2)
/// </summary>
public partial class Forms_frmOrderEntry : System.Web.UI.Page
{
    #region Variables

    DataTable PurchaseSKU;
    DataTable dtFreeSKU;
    private static int mCustomerTypeId;
    private static int mCustomerVolClassId;
    private int mTownId;
    private static int RowId;
    private static int OrderNo;
    private static int AriaId;
    DataControl dc = new DataControl();
    #endregion

    /// <summary>
    /// Page_Load Function Populates All Combos, Grids And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadTransporter();
            OrderNo = (int)this.Session["OrderNo"];
            txtprincipal.Text = this.Session["Route"].ToString();
            txtDeliveryMan.Text = this.Session["SaleMan"].ToString();
            AriaId = int.Parse(this.Session["AreaId"].ToString());
            this.Session.Remove("Route");
            this.Session.Remove("SaleMan");
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);
            lblBillBook.Text = "Bill Book No:";
            if (OrderNo == -1)
            {
                btnSaveOrder.Text = "Save Invoice";
                this.LoadInvoiceToEdit(0);
                lblBillBook.Text = "Bill Book No:";
            }
            else if (OrderNo == -2)
            {
                this.LoadInvoiceToEdit(1);
                ChbDiscount.Visible = false;
                btnSaveOrder.Text = "Sale Return";
                //lblBillBook.Visible = false;
                //txtBillBookNo.Visible = false;
                lblBillBook.Text = "Reference Inv #:";
                txtBillBookNo.Visible = true;
                ScriptManager.GetCurrent(Page).SetFocus(txtOutletCode);
            }
            else
            {
                this.LoadPendingOrder();
            }
            this.LoadCustomerData();
            this.LoadSkuDetail();
            this.CreatTable();
            this.LoadPromotion();
            this.CreateFreeSKU();
            this.LoadFreeGrid();
            if(Session["DistTypeID"].ToString() == "6")//Location Type Distributor
            {
                numtxtTotalExtraDiscntpr.Visible = false;
            }
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            string strProcessScript = "this.value='Wait...';this.disabled=true;";
            btnSaveOrder.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSaveOrder, "").ToString());
        }
    }

    /// <summary>
    /// Creates Datatable For Order, Invoice And Sale Return
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("DistributorId", typeof(int));
        PurchaseSKU.Columns.Add("SALE_ORDER_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("RETAIL_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_EXTRA_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_SPECIAL_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_EXPIRY_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("SED_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(int));
        PurchaseSKU.Columns.Add("UNITS_IN_CASE", typeof(int));
        PurchaseSKU.Columns.Add("ORG_QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("ORG_QUANTITY_CTN", typeof(int));
        PurchaseSKU.Columns.Add("IS_DELETED", typeof(bool));
        PurchaseSKU.Columns.Add("SPECIAL_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("SPECIAL_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("EXPIRY_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("EXPIRY_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("PRINCIPAL_ID", typeof(int));

        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }

    private void LoadPrincipal()
    {
        //DrpPrincipal.Items.Clear();
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //if (Session["DistTypeID"].ToString() == "6")//Location Type Distributor
        //{
        //    DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //    DrpPrincipal.Visible = false;
        //    lblPrincipal.Visible = false;
        //}
        //else
        //{
        //    DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //    clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
        //}
    }

    private void LoadTransporter()
    {
        //DistributorController _dController = new DistributorController();

        //DataTable dt = _dController.SelectTranspoter();
        //if (dt != null)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        clsWebFormUtil.FillDropDownList(DrpTransporter, dt, 0, 1);
        //    }
        //}
        DrpTransporter.Items.Clear();
        //AccountHeadController mAccountController = new AccountHeadController();
        //DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 917);
        //clsWebFormUtil.FillDropDownList(DrpTransporter, dt, 0, 10, true);
        DrpTransporter.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));


    }
    /// <summary>
    /// Creates Datatable For Free SKU
    /// </summary>
    private void CreateFreeSKU()
    {
        dtFreeSKU = new DataTable();
        dtFreeSKU.Columns.Add("SKU_ID", typeof(int));
        dtFreeSKU.Columns.Add("SKU_Code", typeof(string));
        dtFreeSKU.Columns.Add("SKU_Name", typeof(string));
        dtFreeSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        dtFreeSKU.Columns.Add("Quantity", typeof(int));
        dtFreeSKU.Columns.Add("ORG_QUANTITY", typeof(int));
        dtFreeSKU.Columns.Add("AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_RATE", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("PROMOTION_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_DETAIL_ID", typeof(int));
        dtFreeSKU.Columns.Add("PROMOTION_OFFER_ID", typeof(int));
        Session.Add("dtFreeSKU", dtFreeSKU);
    }

    /// <summary>
    /// Loads Pending Order Nos To Docuemnt No Combo
    /// </summary>
    private void LoadPendingOrder()
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(this.Session["OrderDate"]),2);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, "SALE_ORDER_ID", "SALE_ORDER_ID2");
        this.Session.Add("dtOrder", dtOrder);
    }

    private void LoadInvoiceToEdit(int TypeID)
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtDoc = or.SelectPendingInvoices(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(this.Session["DeliveryManId"].ToString()), Convert.ToDateTime(this.Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), TypeID, int.Parse(this.Session["OrderBookerId"].ToString()));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtDoc, "DocID", "DocNo");
        this.Session.Add("dtDoc", dtDoc);
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (long.Parse(drpDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
            {
                if (OrderNo == -1)
                {
                    if (btnSaveOrder.Text == "Save Invoice" || btnSaveOrder.Text == "Update Invoice")
                    {
                        if (drpDocumentNo.Items.Count > 0)
                        {
                            decimal ObjGrossSales = 0;
                            decimal ObjStandardDiscount = 0;
                            decimal ObjExtraDiscount = 0;
                            decimal ObjTotalGST = 0;
                            decimal ObjTotalSED = 0;
                            decimal ObjTotalTST = 0;
                            decimal ObjClaimableDiscount = 0;

                            DataControl dc = new DataControl();
                            DataTable dt = (DataTable)this.Session["dtDoc"];
                            DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");

                            txtBillBookNo.Text = "";
                            if (foundRows.Length > 0)
                            {
                                txtBillBookNo.Text = foundRows[0]["MANUAL_INVOICE_ID"].ToString();
                                RblPayMode.SelectedValue = foundRows[0]["PayMode"].ToString();
                                btnSaveOrder.Text = "Update Invoice";
                                txtOutletCode.Text = foundRows[0]["CUSTOMER_CODE"].ToString();
                                txtOutletName.Text = foundRows[0]["CUSTOMER_NAME"].ToString();
                                numTxtTotalStndrdDiscnt.Text = foundRows[0]["DISCOUNT_AMOUNT"].ToString();
                                numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                                numtxtSpecialDiscValue.Text = foundRows[0]["SPECIAL_DISCOUNT"].ToString();
                                numTxtTotlAmnt.Text = foundRows[0]["TOTAL_NET_AMOUNT"].ToString();
                                txtBuiltyNo.Text = foundRows[0]["BUILTY_NO"].ToString();
                                txtBuiltyAmount.Text = foundRows[0]["BUILTY_AMOUNT"].ToString();
                                numtxtExpiryDiscValue.Text = foundRows[0]["EXPIRY_DISCOUNT"].ToString();
                                txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
                                txtPriceGroup.Text = foundRows[0]["GROUP_TYPE"].ToString();

                                this.LoadDocumentDetail(Convert.ToInt64(drpDocumentNo.SelectedValue),0);

                                DataTable dt2 = (DataTable)this.Session["dtDoc2"];

                                foreach (DataRow dr in dt2.Rows)
                                {
                                    ObjStandardDiscount = Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) + Convert.ToDecimal(dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
                                    ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                                    ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                                    ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                                    ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                                    ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                                    ObjClaimableDiscount += decimal.Parse(dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString()));

                                }

                                txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                                numtxtUnClaimabledist.Text = Convert.ToString(Math.Round(ObjTotalSED, 2));
                                numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST, 2));
                                numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalTST, 2));                                
                                if (decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                                {
                                    numtxtTotalExtraDiscntpr.Text = Convert.ToString(Math.Round((decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) - ObjTotalSED - ObjTotalGST - ObjTotalTST)) * 100, 2));
                                }
                                if (decimal.Parse(dc.chkNull_0(numtxtSpecialDiscValue.Text)) > 0)
                                {
                                    numtxtSpecialDiscPr.Text = Convert.ToString(Math.Round((decimal.Parse(dc.chkNull_0(numtxtSpecialDiscValue.Text)) / (ObjGrossSales - Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) - ObjTotalSED - ObjTotalGST - ObjTotalTST)) * 100, 0));
                                }

                            }
                        }

                    }
                }
                else if (OrderNo == -2)
                {
                    if (btnSaveOrder.Text == "Sale Return" || btnSaveOrder.Text == "Update Return")
                    {
                        if (drpDocumentNo.Items.Count > 0)
                        {
                            decimal ObjGrossSales = 0;
                            decimal ObjStandardDiscount = 0;
                            decimal ObjExtraDiscount = 0;
                            decimal ObjTotalGST = 0;
                            decimal ObjTotalSED = 0;
                            decimal ObjTotalTST = 0;
                            decimal ObjClaimableDiscount = 0;

                            DataControl dc = new DataControl();
                            DataTable dt = (DataTable)this.Session["dtDoc"];
                            DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");

                            //txtBillBookNo.Text = "";
                            if (foundRows.Length > 0)
                            {
                                btnSaveOrder.Text = "Update Return";
                                txtOutletCode.Text = foundRows[0]["CUSTOMER_CODE"].ToString();
                                txtOutletName.Text = foundRows[0]["CUSTOMER_NAME"].ToString();
                                numTxtTotalStndrdDiscnt.Text = foundRows[0]["DISCOUNT_AMOUNT"].ToString();
                                numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                                numtxtSpecialDiscValue.Text = foundRows[0]["SPECIAL_DISCOUNT"].ToString();
                                numTxtTotlAmnt.Text = foundRows[0]["TOTAL_NET_AMOUNT"].ToString();
                                numtxtExpiryDiscValue.Text = foundRows[0]["EXPIRY_DISCOUNT"].ToString();
                                txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
                                txtPriceGroup.Text = foundRows[0]["GROUP_TYPE"].ToString();
                                this.LoadDocumentDetail(Convert.ToInt64(drpDocumentNo.SelectedValue),1);
                                txtBillBookNo.Text = foundRows[0]["SALE_INVOICE_ID"].ToString();
                                DataTable dt2 = (DataTable)this.Session["dtDoc2"];

                                foreach (DataRow dr in dt2.Rows)
                                {
                                    ObjStandardDiscount = Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) + Convert.ToDecimal(dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
                                    ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                                    ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                                    ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                                    ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                                    ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));

                                }

                                txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                                numtxtUnClaimabledist.Text = Convert.ToString(Math.Round(ObjTotalSED, 2));
                                numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST, 2));
                                numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalTST, 2));
                                if (decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                                {
                                    numtxtTotalExtraDiscntpr.Text = Convert.ToString(Math.Round((decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) - ObjTotalSED - ObjTotalGST - ObjTotalTST)) * 100, 2));
                                }
                                if (decimal.Parse(dc.chkNull_0(numtxtSpecialDiscValue.Text)) > 0)
                                {
                                    numtxtSpecialDiscPr.Text = Convert.ToString(Math.Round((decimal.Parse(dc.chkNull_0(numtxtSpecialDiscValue.Text)) / (ObjGrossSales - Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) - ObjTotalSED - ObjTotalGST - ObjTotalTST)) * 100, 0));
                                }

                            }
                        }

                    }
                }
                else
                {
                    txtBillBookNo.Text = string.Empty;
                    DataTable dt = (DataTable)this.Session["dtOrder"];
                    DataRow[] foundRows = dt.Select("SALE_ORDER_ID  = '" + drpDocumentNo.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        txtGrossAmount.Text = foundRows[0]["TOTAL_AMOUNT"].ToString();
                        numTxtTotalStndrdDiscnt.Text = foundRows[0]["STANDARD_DISCOUNT_AMOUNT"].ToString();
                        numTxtTotalGST.Text = foundRows[0]["GST_AMOUNT"].ToString();
                        numTxtTotlAmnt.Text = foundRows[0]["TOTAL_NET_AMOUNT"].ToString();
                        numTxtTotalTST.Text = foundRows[0]["TST_AMOUNT"].ToString();
                        txtOutletCode.Text = foundRows[0]["CUSTOMER_CODE"].ToString();
                        txtOutletName.Text = foundRows[0]["CUSTOMER_NAME"].ToString();
                        numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                        numtxtSpecialDiscValue.Text = foundRows[0]["SPECIAL_DISCOUNT"].ToString();
                        numtxtUnClaimabledist.Text = foundRows[0]["SED_AMOUNT"].ToString();
                        txtBuiltyNo.Text = foundRows[0]["BUILTY_NO"].ToString();
                        txtBuiltyAmount.Text = foundRows[0]["BUILTY_AMOUNT"].ToString();
                        txtPriceGroup.Text = foundRows[0]["GROUP_TYPE"].ToString();
                        numtxtExpiryDiscValue.Text = foundRows[0]["EXPIRY_DISCOUNT"].ToString();
                        txtRemarks.Text = foundRows[0]["REMARKS"].ToString();

                        if (decimal.Parse(numtxtTotalExtraDiscnt.Text) > 0)
                        {
                            numtxtTotalExtraDiscntpr.Text = Convert.ToString(Math.Round((decimal.Parse(numtxtTotalExtraDiscnt.Text) / (Convert.ToDecimal(txtGrossAmount.Text) - Convert.ToDecimal(numTxtTotalStndrdDiscnt.Text) + Convert.ToDecimal(numTxtTotalGST.Text))) * 100, 2));
                        }
                        if (decimal.Parse(numtxtSpecialDiscValue.Text) > 0)
                        {
                            numtxtSpecialDiscPr.Text = Convert.ToString(Math.Round((decimal.Parse(numtxtSpecialDiscValue.Text) / (Convert.ToDecimal(txtGrossAmount.Text) - Convert.ToDecimal(numtxtTotalExtraDiscnt.Text) - Convert.ToDecimal(numTxtTotalStndrdDiscnt.Text))) * 100, 0));
                        }
                        this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
                        txtBillBookNo.Text = foundRows[0]["MANUAL_ORDER_ID"].ToString();
                        this.Session.Add("hfBillBookNo", foundRows[0]["MANUAL_ORDER_ID"].ToString());
                        EnableDisableController(false);
                        btnSaveOrder.Text = "Update Order";
                        btnSaveOrder.Enabled = false;
                        btnCalculate.Enabled = false;
                        this.ClearAll();
                        ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
                        RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
                    }
                    else
                    {
                        this.CreateFreeSKU();
                        this.CreatTable();
                        this.LoadGird();
                        this.LoadFreeGrid();
                        ClearMasterALL();
                        btnSaveOrder.Text = "Save Order";
                        this.Session.Remove("hfBillBookNo");
                    }
                }
                LoadSkuDetail();
                //SKUPriceDetailController PController = new SKUPriceDetailController();
                //DataTable Dtsku_Price = new DataTable();

                //ddlSkus.Items.Clear();

                //if (txtPriceGroup.Text.Trim() == "Default")
                //{

                //    Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                //    clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 10, true);
                //    Session.Add("Dtsku_Price", Dtsku_Price);
                //}
                //else
                //{
                //    FindCustomer();

                //    Dtsku_Price = PController.GetCustomerSKU(Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), Convert.ToInt32(this.Session["CUSTOMER_ID"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                //    clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, "SKU_ID", "SkuPriceDetail", true);
                //    this.Session.Add("Dtsku_Price", Dtsku_Price);
                //}
            }
            else
            {
                this.CreateFreeSKU();
                this.CreatTable();
                ClearMasterALL();
                if (OrderNo != -1)
                {
                    btnSaveOrder.Text = "Save Order";
                }
                else
                {
                    btnSaveOrder.Text = "Save Invoice";
                }
                this.Session.Remove("hfBillBookNo");
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadCustomerData()
    {
        if (int.Parse(this.Session["DistributorId"].ToString()) > 0 && long.Parse(this.Session["AreaId"].ToString()) > 0)
        {
            ListCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["AreaId"].ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillListBox(this.ListCustomer, dtCustomer, "CUSTOMER_DETAIL2", "CUSTOMER_DETAIL2", true);
            this.Session.Add("dtCustomer", dtCustomer);
        }
        else
        {
            ListCustomer.Items.Clear();
        }
    }

    private bool CheckStock(int skuId, int packSize, string qty, string ctn)
    {
        if (btnSaveOrder.Text == "Save Invoice" || btnSaveOrder.Text == "Update Invoice")
        {
            PhaysicalStockController mController = new PhaysicalStockController();

            DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), skuId, txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()));

            if (dtstock.Rows.Count > 0)
            {
                if (btnSaveOrder.Text == "Update Invoice")
                {
                    if (int.Parse(dtstock.Rows[0][0].ToString()) + (int.Parse(dc.chkNull_0(qty)) + (int.Parse(dc.chkNull_0(ctn)) * packSize)) < (int.Parse(dc.chkNull_0(txtQuantity.Text)) + (int.Parse(dc.chkNull_0(txtCtn.Text)) * packSize)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ddlSkus.SelectedItem.Text + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return false;
                    }
                }
                else if (btnSaveOrder.Text == "Save Invoice")
                {
                    if (int.Parse(dtstock.Rows[0][0].ToString()) < (int.Parse(dc.chkNull_0(txtQuantity.Text)) + (int.Parse(dc.chkNull_0(txtCtn.Text)) * packSize)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ddlSkus.SelectedItem.Text + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + ddlSkus.SelectedItem.Text + "No Stock Found');", true);
                return false;
            }
        }
        return true;
    }

    private void LoadSkuDetail()
    {

        if (ListCustomer.Items.Count > 0)
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();

            if (txtPriceGroup.Text.Trim() == "Default")
            {

                //if (DrpPrincipal.Items.Count > 0)
                //{
                DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 10, true);
                this.Session.Add("Dtsku_Price", Dtsku_Price);
                //}

            }
            else if (txtPriceGroup.Text.Trim() != "")
            {
                ddlSkus.Items.Clear();

                FindCustomer();

                DataTable Dtsku_Price = PController.GetCustomerSKU(Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), Convert.ToInt32(this.Session["CUSTOMER_ID"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, "SKU_ID", "SkuPriceDetail", true);

                this.Session.Add("Dtsku_Price", Dtsku_Price);

            }
            else
            {
                ddlSkus.Items.Clear();

                if (FindCustomer())
                {
                    DataTable Dtsku_Price = PController.GetCustomerSKU(Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), Convert.ToInt32(this.Session["CUSTOMER_ID"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                    clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, "SKU_ID", "SkuPriceDetail", true);
                    this.Session.Add("Dtsku_Price", Dtsku_Price);
                }
            }
        }

    }

    /// <summary>
    /// Enables/Disables Discount Fields For Manual And Auto Promotion And Loads Promotion Controler For Auto Promotion
    /// </summary>
    private void LoadPromotion()
    {
        //if (DrpPrincipal.Items.Count > 0)
        //{
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["OrderDate"].ToString()));
            if (bool.Parse(dt.Rows[0]["Is_ManualDiscount"].ToString()) == false)
            {
                OrderEntryController orderController = new OrderEntryController();
                PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DistributorId"].ToString()), Constants.IntNullValue, Convert.ToDateTime(this.Session["OrderDate"]));
                this.Session.Add("Arpc", Arpc);
                txtDiscountType.Text = "Auto Discount";
                numtxtUnClaimabledist.ReadOnly = true;
                txtDiscount.Text = "0";
                ChbDiscount.Checked = true;
            }
            else
            {
                if(Session["DistTypeID"].ToString() == "6")//Location Type Distributor
                {
                    OrderEntryController orderController = new OrderEntryController();
                    PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DistributorId"].ToString()), Constants.IntNullValue, Convert.ToDateTime(this.Session["OrderDate"]));
                    this.Session.Add("Arpc", Arpc);
                }
                txtDiscountType.Text = "Manual Discount";
                numtxtTotalExtraDiscnt.ReadOnly = false;
                numtxtSpecialDiscValue.ReadOnly = false;
                numtxtUnClaimabledist.ReadOnly = false;
                txtDiscount.Enabled = true;
                ChbDiscount.Checked = false;
            }
        //}
    }

    /// <summary>
    /// Loads Order To Order Grid     
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        //  txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtCtn.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtDiscount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        txtBatchNo.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text;

        //if (UnitType == 0)
        //{
        //    txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        //}
        //else
        //{
        //    txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[8].Text;
        //}
        ddlSkus.Enabled = false;
        btnSave.Text = "Update";

    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird();
        btnCalculate.Enabled = true;
        btnSaveOrder.Enabled = false;
        ddlSkus.Focus();
    }
    
    /// <summary>
    /// Loads Free SKUS To SKU Grid
    /// </summary>
    private void LoadFreeGrid()
    {
        dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
        GrdFreeSKU.DataSource = dtFreeSKU;
        GrdFreeSKU.DataBind();
    }

    private void LoadDocumentDetail(long DocID,int TypeID)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.SelectInvoiceDetail(DocID, TypeID);
        dtFreeSKU = ord.SelectInvoicePromotion(int.Parse(this.Session["DistributorId"].ToString()), DocID);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.Session.Add("dtDoc2", PurchaseSKU);
        this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
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
            txtBatchNo.Text = "";
            txtBatchNo.Enabled = true;
        }
        else
        {
            txtBatchNo.Enabled = false;
            txtBatchNo.Text = "N/A";
        }
    }

    /// <summary>
    /// Verifies Customer Code
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool FindCustomer()
    {
        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];

        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + txtOutletCode.Text.Trim() + "'");
        if (foundRows.Length > 0)
        {
            this.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
            this.Session.Add("CUSTOMER_NAME", foundRows[0]["CUSTOMER_NAME"].ToString());

            mCustomerTypeId = int.Parse(foundRows[0]["CHANNEL_TYPE_ID"].ToString());
            mCustomerVolClassId = int.Parse(foundRows[0]["VOLUME_CLASS_ID"].ToString());
            mTownId = int.Parse(foundRows[0]["TOWN_ID"].ToString());
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSKU()
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
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
    /// Checks Free SKU in Free SKU Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private int FreeSKUExist(DataTable dt, int Sku_id)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (int.Parse(dt.Rows[i]["SKU_ID"].ToString()) == Sku_id)
            {
                return i;
            }

        }
        return -1;
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="OrderId">Order</param>
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.SelectOrderDetail(int.Parse(this.Session["DistributorId"].ToString()), OrderId);
        dtFreeSKU = ord.SelectOrderPromotion(int.Parse(this.Session["DistributorId"].ToString()), OrderId);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
    }

    /// <summary>
    /// Checks Bill Book No in System
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || this.Session["hfBillBookNo"].ToString() != txtBillBookNo.Text) && txtBillBookNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(Convert.ToInt32(this.Session["DistributorId"].ToString()), txtBillBookNo.Text, 0);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
            DataTable dtBillBookNo2 = OEC.SelectBillBookNo(Convert.ToInt32(this.Session["DistributorId"].ToString()), txtBillBookNo.Text, 1);
            if (dtBillBookNo2.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }
    
    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="CValue">Value</param>
    private void EnableDisableController(bool CValue)
    {
         
        if (CValue == true)
        {
            if (decimal.Parse(txtGrossAmount.Text) > 0)
            {               
                txtOutletCode.Enabled = true;
                txtOutletName.Enabled = true;
                //DrpPrincipal.Enabled = true;
               // RblPayMode.Enabled = true;
            }
        }
        else
        {
           // RblPayMode.Enabled = false;
            //DrpPrincipal.Enabled = false;
            txtOutletCode.Enabled = false;
            txtOutletName.Enabled = false;           
        }
    }
            
    /// <summary>
    /// Clears Session Variables And Redircts To Step1
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Session.Remove("CUSTOMER_ID");
        this.Session.Remove("AreaId");
        this.Session.Remove("OrderBookerId");
        this.Session.Remove("DeliveryManId");
        Response.Redirect("~/Forms/frmOrderEntryStep1.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
    }

    /// <summary>
    /// Calcualtes Discount And Free SKU For Order
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (FindCustomer())
        {
            decimal ObjGrossSales = 0;
            decimal ObjStandardDiscount = 0;
            decimal ObjSpecialDiscount = 0;
            decimal ObjExpiryDiscount = 0;
            decimal ObjExtraDiscount = 0;
            decimal ObjTotalGST = 0;
            decimal ObjTotalSED = 0;
            decimal ObjTotalTST = 0;
            decimal ObjFreeGSTAmount = 0;
            decimal ObjFreeGrossAmt = 0;
            decimal ObjFreeTST = 0;
            decimal ProductExtDist = 0;
            decimal ProductStdDist = 0;

            DataControl dc = new DataControl();
            OrderEntryController or = new OrderEntryController();
            SKUGroupController GroupCtl = new SKUGroupController();
            DataTable dt = (DataTable)this.Session["PurchaseSKU"];
            foreach (DataRow dr in dt.Rows)
            {
                dr["STANDARD_DISCOUNT"] = 0;
                dr["STANDARD_DISCOUNT_PER"] = 0;
                dr["CLAIM_STANDARD_DISCOUNT"] = 0;

                dr["EXTRA_DISCOUNT"] = 0;
                dr["EXTRA_DISCOUNT_PER"] = 0;
                dr["CLAIM_EXTRA_AMOUNT"] = 0;

                dr["SPECIAL_DISCOUNT"] = 0;
                dr["SPECIAL_DISCOUNT_PER"] = 0;
                dr["CLAIM_SPECIAL_AMOUNT"] = 0;

                dr["EXPIRY_DISCOUNT"] = 0;
                dr["EXPIRY_DISCOUNT_PER"] = 0;
                dr["CLAIM_EXPIRY_AMOUNT"] = 0;

                dr["CLAIM_PER"] = 0;

            }
            PromotionCollections_Controller Arpc = (PromotionCollections_Controller)this.Session["Arpc"];
            this.CreateFreeSKU();
            this.LoadFreeGrid();

            if (dt.Rows.Count > 0 && txtOutletCode.Text.Length > 0)
            {
                if (ChbDiscount.Checked == true || Session["DistTypeID"].ToString() == "6")//Location Type Distributor
                {
                    ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, mCustomerVolClassId, mCustomerTypeId, dt); //, false
                    for (int i = 0; i < ARlistOffer.Count; i++)
                    {
                        PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];
                        if (pofferCol.Is_Claimable)
                        {
                            if (pofferCol.SKU_ID > 0)
                            {
                                #region slab on SKU
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                    {

                                        if (pofferCol.Is_And == 0) // Standard Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                //dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                //dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else if (pofferCol.Is_And == 2) // Special Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_SPECIAL_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_SPECIAL_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_SPECIAL_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_SPECIAL_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else if (pofferCol.Is_And == 3) // Expiry Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_EXPIRY_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXPIRY_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_EXPIRY_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXPIRY_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (ChbDiscount.Checked == true)
                                            {
                                                if (pofferCol.Offer_Value > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    //dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    //dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                                }
                                                if (pofferCol.Discount > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                    dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                                    //dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                    //dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                }
                                            }
                                        }
                                        if (pofferCol.Free_SKU_ID > 0)
                                        {
                                            if (ChbDiscount.Checked == true || pofferCol.Is_And != 1)
                                            {
                                                DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                                DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                                int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                                if (foundRows.Length > 0)
                                                {
                                                    if (RowId < 0)
                                                    {
                                                        DataRow dr1 = dtFreeSKU.NewRow();
                                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                        dr1["ORG_QUANTITY"] = 0;
                                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                            dr1["TST_AMOUNT"] = 0;
                                                        }
                                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = 0;
                                                            dr1["TST_AMOUNT"] = 0;
                                                        }

                                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                        dtFreeSKU.Rows.Add(dr1);
                                                    }
                                                    else
                                                    {
                                                        DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                        ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                        ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                        dr1["ORG_QUANTITY"] = 0;
                                                        dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                        {

                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                            dr1["TST_AMOUNT"] = 0;

                                                        }
                                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        }
                                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                    }

                                                }
                                            }
                                        }
                                        this.Session.Add("dtFreeSKU", dtFreeSKU);
                                        this.LoadFreeGrid();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region slab on SKU Group
                                if (ChbDiscount.Checked == true || pofferCol.Is_And != 1)
                                {
                                    #region Free SKU
                                    if (pofferCol.Free_SKU_ID > 0)
                                    {
                                        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                        int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                        if (foundRows.Length > 0)
                                        {
                                            if (RowId < 0)
                                            {
                                                DataRow dr1 = dtFreeSKU.NewRow();
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["ORG_QUANTITY"] = 0;
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                dtFreeSKU.Rows.Add(dr1);
                                            }
                                            else
                                            {

                                                DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["ORG_QUANTITY"] = 0;
                                                dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            }
                                            this.Session.Add("dtFreeSKU", dtFreeSKU);
                                            this.LoadFreeGrid();

                                        }
                                    }
                                    #endregion
                                }
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                    {
                                        if (pofferCol.Is_And == 0) // Standard Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                //dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                //dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else if (pofferCol.Is_And == 2) // Special Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_SPECIAL_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_SPECIAL_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_SPECIAL_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_SPECIAL_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else if (pofferCol.Is_And == 3) // Expiry Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_EXPIRY_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXPIRY_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_EXPIRY_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXPIRY_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (ChbDiscount.Checked == true)
                                            {
                                                if (pofferCol.Offer_Value > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    //dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    //dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                                }
                                                if (pofferCol.Discount > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                    dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                                    //dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                    //dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                }
                                            }
                                        }

                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            if (pofferCol.SKU_ID > 0)
                            {
                                #region slab on SKU
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                    {
                                        if (pofferCol.Is_And == 2) // Special Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        else if (pofferCol.Is_And == 3) // Expiry Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        else if (pofferCol.Is_And == 0) // Standard Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                //dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        else
                                        {
                                            if (ChbDiscount.Checked == true)
                                            {
                                                if (pofferCol.Offer_Value > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    //dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                }
                                                if (pofferCol.Discount > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                    //dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                }
                                            }
                                        }
                                        if (ChbDiscount.Checked == true || pofferCol.Is_And != 1)
                                        {
                                            if (pofferCol.Free_SKU_ID > 0)
                                            {
                                                DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                                DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                                int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                                if (foundRows.Length > 0)
                                                {
                                                    if (RowId < 0)
                                                    {
                                                        DataRow dr1 = dtFreeSKU.NewRow();
                                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                        dr1["ORG_QUANTITY"] = 0;
                                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                        {

                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                            dr1["TST_AMOUNT"] = 0;

                                                        }
                                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = 0;
                                                            dr1["TST_AMOUNT"] = 0;
                                                        }

                                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                        dtFreeSKU.Rows.Add(dr1);
                                                    }
                                                    else
                                                    {

                                                        DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                        ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                        ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                        dr1["ORG_QUANTITY"] = 0;
                                                        dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;

                                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                        {

                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            //dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                            dr1["TST_AMOUNT"] = 0;

                                                        }
                                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            //dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                            dr1["GST_AMOUNT"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                            //dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        }
                                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                    }

                                                }
                                            }
                                        }
                                        this.Session.Add("dtFreeSKU", dtFreeSKU);
                                        this.LoadFreeGrid();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region slab on SKU Group
                                if (ChbDiscount.Checked == true || pofferCol.Is_And != 1)
                                {
                                    #region Free SKU
                                    if (pofferCol.Free_SKU_ID > 0)
                                    {
                                        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                        int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                        if (foundRows.Length > 0)
                                        {
                                            if (RowId < 0)
                                            {
                                                DataRow dr1 = dtFreeSKU.NewRow();
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["ORG_QUANTITY"] = 0;
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                dtFreeSKU.Rows.Add(dr1);
                                            }
                                            else
                                            {

                                                DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["ORG_QUANTITY"] = 0;
                                                dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            }
                                            this.Session.Add("dtFreeSKU", dtFreeSKU);
                                            this.LoadFreeGrid();

                                        }
                                    }
                                    #endregion
                                }
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                    {
                                        if (pofferCol.Is_And == 2) // Special Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["SPECIAL_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        else if (pofferCol.Is_And == 3) // Expiry Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXPIRY_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        else if (pofferCol.Is_And == 0) // Standard Discount
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                //dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        else
                                        {
                                            if (ChbDiscount.Checked == true)
                                            {
                                                if (pofferCol.Offer_Value > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                    //dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                }
                                                if (pofferCol.Discount > 0)
                                                {
                                                    dr["EXTRA_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                    //dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #region Recalulate Products
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())));
                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) + decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - Convert.ToDecimal(dr["STANDARD_DISCOUNT"]));
                        decimal SP = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        dr["SPECIAL_DISCOUNT"] = decimal.Parse(dr["SPECIAL_DISCOUNT"].ToString()) + decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - SP);
                        decimal SPX = SP + decimal.Parse(dr["SPECIAL_DISCOUNT"].ToString());
                        dr["EXPIRY_DISCOUNT"] = decimal.Parse(dr["EXPIRY_DISCOUNT"].ToString()) + decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - SPX);
                        dr["CLAIM_STANDARD_DISCOUNT"] = decimal.Parse(dr["CLAIM_STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())));
                        dr["SED_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString()));
                        ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjStandardDiscount += decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        ObjSpecialDiscount += decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString()));
                        ObjExpiryDiscount += decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString()));
                        decimal TempAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        decimal TempDiscount = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        decimal TempExtraDisAmt = 0;
                        TempExtraDisAmt = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        decimal TemSpecial = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString()));
                        decimal TemExpiry = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString()));
                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt - TemSpecial - TemExpiry) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt - TemSpecial - TemExpiry) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        //decimal dNetAmount = Math.Round(ObjGrossSales - ObjStandardDiscount - ObjExtraDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST + ObjFreeTST, 2);
                        //numtxtSpecialDiscValue.Text = Convert.ToString(dNetAmount * Convert.ToDecimal(dc.chkNull_0(numtxtSpecialDiscPr.Text)) / 100);
                        
                        //if (Session["DistTypeID"].ToString() == "6")//Location Type Distributor
                        //{
                        //    ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        //}
                        //else
                        //{
                        //    ObjExtraDiscount += (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - Convert.ToDecimal(dr["STANDARD_DISCOUNT"])) * decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscntpr.Text)) / 100;
                        //}
                        
                    }
                    #endregion
                }
                else
                {
                    #region Reversecalulate Products

                    //decimal ObjGrossSales = 0;
                    decimal ObjTaxableAmount = 0;
                    decimal ObjClaimAblePer = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    }
                    if (decimal.Parse(dc.chkNull_0(numtxtUnClaimabledist.Text)) > 0)
                    {
                        ObjClaimAblePer = decimal.Parse(dc.chkNull_0(numtxtUnClaimabledist.Text)) / ObjGrossSales;
                    }
                    if (decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                    {
                        ProductExtDist = decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                    }
                    ObjGrossSales = 0;

                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal TempAmount = 0;
                        decimal TempDiscount = 0;
                        decimal TempExtraDisAmt = 0;
                        ProductStdDist = decimal.Parse(dc.chkNull_0(dr["UNIT_PRICE"].ToString())) * (int.Parse(dc.chkNull_0(dr["QUANTITY_UNIT"].ToString())) + (int.Parse(dc.chkNull_0(dr["QUANTITY_CTN"].ToString())) * int.Parse(dc.chkNull_0(dr["UNITS_IN_CASE"].ToString())))) * (decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) / 100);
                        dr["SED_AMOUNT"] = ObjClaimAblePer * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));

                        #region GST Calculation

                        TempAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        TempDiscount = ProductStdDist;//decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        TempExtraDisAmt = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) * decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscntpr.Text)) / 100;
                        decimal TemSpecial = decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString()));
                        decimal TemExpiry = decimal.Parse(dc.chkNull_0(dr["EXPIRY_DISCOUNT"].ToString()));
                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt - TemSpecial - TemExpiry) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt - TemSpecial - TemExpiry) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                        ObjStandardDiscount += ProductStdDist;//decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) * decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscntpr.Text)) / 100;//decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        ObjSpecialDiscount += (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()))) * decimal.Parse(dc.chkNull_0(numtxtSpecialDiscPr.Text)) / 100;
                        ObjExpiryDiscount += (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["SPECIAL_DISCOUNT"].ToString()))) * decimal.Parse(dc.chkNull_0(numtxtExpiryDiscPr.Text)) / 100; ;
                        #endregion
                    }
                    #endregion
                }

                #region Set Total Values

                txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                numtxtTotalExtraDiscnt.Text = Convert.ToString(Math.Round(ObjExtraDiscount, 2));
                numTxtTotalStndrdDiscnt.Text = Convert.ToString(Math.Round(ObjStandardDiscount, 2));
                numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST + ObjFreeGSTAmount, 2));
                numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalTST + ObjFreeTST, 2));
                //decimal dNemtAmount = Math.Round(ObjGrossSales - ObjStandardDiscount - ObjExtraDiscount + ObjSpecialDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST + ObjFreeTST, 2);
                //numtxtSpecialDiscValue.Text = Convert.ToString(dNemtAmount * Convert.ToDecimal(dc.chkNull_0(numtxtSpecialDiscPr.Text)) / 100);
                numtxtSpecialDiscValue.Text = Convert.ToString(Math.Round(ObjSpecialDiscount, 2));
                numtxtExpiryDiscValue.Text = Convert.ToString(Math.Round(ObjExpiryDiscount, 2));
                numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjGrossSales - ObjStandardDiscount - ObjExtraDiscount - ObjSpecialDiscount - ObjExpiryDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST + ObjFreeTST, 2));
                numtxtUnClaimabledist.Text = Convert.ToString(Math.Round(ObjTotalSED, 2));

                #endregion

                btnSaveOrder.Enabled = true;
                if (RblPayMode.SelectedIndex == 1)
                {
                    txtCashReceived.Enabled = true;
                    ScriptManager.GetCurrent(Page).SetFocus(txtCashReceived);
                }
                else
                {
                    txtCashReceived.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Adds/Updates Row To Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            decimal discount = 0;
            DataControl dc = new DataControl();
            DataTable dtskuPrice = (DataTable)this.Session["Dtsku_Price"];
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
            decimal mTradePrice = Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text));
            if (txtDiscount.Text != "")
            {
                discount = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)) / 100;
            }
            else
            {
                txtDiscount.Text = "0";
            }
            int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
            if (btnSave.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    if (CheckStock(0,0))
                    {
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["PRINCIPAL_ID"] = foundRows[0]["PRINCIPAL_ID"];
                        dr["UNITS_IN_CASE"] = mPackSize;
                        dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                        dr["ORG_QUANTITY_UNIT"] = 0;
                        dr["ORG_QUANTITY_CTN"] = 0;
                        dr["UNIT_PRICE"] = mTradePrice.ToString();
                        discount = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * discount;
                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                        {
                            dr["AMOUNT"] = mTradePrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));                            //dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());// -discount;
                            dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                            dr["TST_AMOUNT"] = 0;
                            //dr["BATCH_NO"] = "T";
                            dr["BATCH_NO"] = txtBatchNo.Text;
                        }
                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                        {
                            dr["AMOUNT"] = mTradePrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                            dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                            dr["GST_RATE"] = 0;
                            //dr["BATCH_NO"] = "R";
                            dr["BATCH_NO"] = txtBatchNo.Text;
                        }
                        else
                        {
                            dr["AMOUNT"] = mTradePrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                            dr["TST_AMOUNT"] = 0;
                            dr["GST_RATE"] = 0;
                            //dr["BATCH_NO"] = "E";
                            dr["BATCH_NO"] = txtBatchNo.Text;
                        }
                        dr["STANDARD_DISCOUNT"] = decimal.Parse(txtDiscount.Text);
                        dr["EXTRA_DISCOUNT"] = 0;
                        dr["EXPIRY_DISCOUNT"] = 0;
                        dr["SPECIAL_DISCOUNT"] = 0;
                        dr["GST_AMOUNT"] = 0;
                        dr["NET_AMOUNT"] = 0;
                        PurchaseSKU.Rows.Add(dr);
                    }
                    else
                    {
                        return;
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
                }
            }
            else
            {
                DataRow dr = PurchaseSKU.Rows[RowId];
                if (CheckStock(Convert.ToInt32(dr["QUANTITY_UNIT"]),Convert.ToInt32(dr["QUANTITY_CTN"])))
                {
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["PRINCIPAL_ID"] = foundRows[0]["PRINCIPAL_ID"];
                    dr["BATCH_NO"] = txtBatchNo.Text;
                    dr["UNITS_IN_CASE"] = mPackSize;
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtCtn.Text));
                    dr["UNIT_PRICE"] = mTradePrice.ToString();
                    discount = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * discount;
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                    {
                        dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                        dr["AMOUNT"] = mTradePrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                        dr["TST_AMOUNT"] = 0;
                        //dr["BATCH_NO"] = "T";
                        dr["BATCH_NO"] = txtBatchNo.Text;
                    }
                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                    {
                        dr["AMOUNT"] = mTradePrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                        dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                        dr["GST_RATE"] = 0;
                        //dr["BATCH_NO"] = "R";
                        dr["BATCH_NO"] = txtBatchNo.Text;
                    }
                    else
                    {
                        dr["AMOUNT"] = mTradePrice * ((Convert.ToDecimal(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString())) * (Convert.ToDecimal(dc.chkNull_0(txtCtn.Text)))) + decimal.Parse(dc.chkNull_0(txtQuantity.Text)));
                        dr["TST_AMOUNT"] = 0;
                        dr["GST_RATE"] = 0;
                        //dr["BATCH_NO"] = "E";
                        dr["BATCH_NO"] = txtBatchNo.Text;
                    }
                    dr["STANDARD_DISCOUNT"] = decimal.Parse(txtDiscount.Text);
                    dr["EXTRA_DISCOUNT"] = 0;
                    dr["EXPIRY_DISCOUNT"] = 0;
                    dr["SPECIAL_DISCOUNT"] = 0;
                    dr["GST_AMOUNT"] = 0;
                    dr["NET_AMOUNT"] = 0;
                    dr["STANDARD_DISCOUNT_PER"] = 0;
                }
                else
                {
                    return;
                }
            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.EnableDisableController(false);
            this.LoadGird();
            this.ClearAll();
            ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
            if (ChbDiscount.Checked == false)
            {
                decimal ObjGrossSales = 0;
                decimal ObjTaxableAmount = 0;
                foreach (DataRow dr in PurchaseSKU.Rows)
                {
                    ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));//-discount;
                    //if (dr["BATCH_NO"].ToString() == "T")
                    //{
                    //    ObjTaxableAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));//-discount;
                    //}
                }
                txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                numTxtTotalSED.Text = Convert.ToString(Math.Round(ObjTaxableAmount, 2));
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        string p_voucher_No;
        if (FindCustomer())
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
                string ManualID = null;
                if (txtBillBookNo.Text.Trim().Length > 0)
                {
                    ManualID = txtBillBookNo.Text.ToUpper();
                }
                DataControl DC = new DataControl();
                PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
                OrderEntryController mOrderController = new OrderEntryController();
                bool is_voucher_hit = false;
                DataTable DisDt = (DataTable)this.Session["DisDt"];
                if (DisDt != null)
                {
                    if (DisDt.Rows.Count > 0)
                    {
                        DataRow[] foundRows = DisDt.Select("DISTRIBUTOR_ID  = '" + this.Session["DistributorId"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            if (foundRows[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                            {
                                is_voucher_hit = true;
                            }
                        }
                    }
                }
                if (txtRemarks.Text.Equals(""))
                {
                    txtRemarks.Text = "";
                }
                if (btnSaveOrder.Text == "Save Order")
                {
                    if (!IsBillBookNoExist())
                    {

                        bool flag = true;
                        DataTable dtDistinctSKUS = new DataTable();
                        dtDistinctSKUS.Columns.Add("SKU_ID", typeof(int));
                        dtDistinctSKUS.Columns.Add("Qty", typeof(int));
                        dtDistinctSKUS.Columns.Add("packSize", typeof(int));

                        DataRow drDistinct = dtDistinctSKUS.NewRow();
                        foreach (DataRow dr in PurchaseSKU.Rows)
                        {
                            drDistinct = dtDistinctSKUS.NewRow();
                            drDistinct["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                            drDistinct["packSize"] = Convert.ToInt32(dr["UNITS_IN_CASE"]);
                            drDistinct["Qty"] = Convert.ToInt32(dr["QUANTITY_UNIT"]) + Convert.ToInt32(dr["QUANTITY_CTN"]) * Convert.ToInt32(dr["UNITS_IN_CASE"]);
                            dtDistinctSKUS.Rows.Add(drDistinct);
                        }

                        foreach (DataRow drFree in dtFreeSKU.Rows)
                        {
                            DataRow[] foundRows = dtDistinctSKUS.Select("SKU_ID  = '" + drFree["SKU_ID"].ToString() + "'");
                            if (foundRows.Length > 0)
                            {
                                int rowIndex = (int)dtDistinctSKUS.Rows.IndexOf(foundRows[0]);
                                drDistinct = dtDistinctSKUS.Rows[rowIndex];
                                drDistinct["Qty"] = Convert.ToInt32(drDistinct["Qty"]) + Convert.ToInt32(drFree["Quantity"]);
                            }
                            else
                            {
                                drDistinct = dtDistinctSKUS.NewRow();
                                drDistinct["SKU_ID"] = Convert.ToInt32(drFree["SKU_ID"]);
                                drDistinct["Qty"] = Convert.ToInt32(drFree["Quantity"]);
                                dtDistinctSKUS.Rows.Add(drDistinct);
                            }
                        }

                        foreach (DataRow dr in dtDistinctSKUS.Rows)
                        {
                            flag = ChkStkOrder(Convert.ToInt32(dc.chkNull_0(dr["SKU_ID"].ToString())), Convert.ToInt32(dc.chkNull_0(dr["Qty"].ToString())));
                        }
                        if (flag)
                        {
                            bool IsValidInsert = mOrderController.Add_Order(int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, (long)this.Session["AreaId"], Constants.IntNullValue, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString())
                              , int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()), decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text))
                              , decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, Constants.Order_Pending_Id, PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString())
                              , DateTime.Parse(this.Session["OrderDate"].ToString()), decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)), Convert.ToDecimal(DC.chkNull_0(numtxtSpecialDiscValue.Text))
                              , txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), int.Parse(DrpTransporter.SelectedValue), txtPriceGroup.Text, Convert.ToDecimal(DC.chkNull_0(numtxtExpiryDiscValue.Text)), txtRemarks.Text);
                            if (IsValidInsert)
                            {
                                this.LoadPendingOrder();
                                this.ClearMasterALL();
                                this.Session.Remove("hfBillBookNo");
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Bill Book No already exist,Kindly enter different Bill Book No');", true);
                    }
                }
                else if(btnSaveOrder.Text == "Update Order")
                {
                    if (!IsBillBookNoExist())
                    {
                        bool flag = true;
                        DataTable dtDistinctSKUS = new DataTable();
                        dtDistinctSKUS.Columns.Add("SKU_ID", typeof(int));
                        dtDistinctSKUS.Columns.Add("Qty", typeof(int));
                        dtDistinctSKUS.Columns.Add("packSize", typeof(int));

                        DataRow drDistinct = dtDistinctSKUS.NewRow();
                        foreach (DataRow dr in PurchaseSKU.Rows)
                        {
                            drDistinct = dtDistinctSKUS.NewRow();
                            drDistinct["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                            drDistinct["packSize"] = Convert.ToInt32(dr["UNITS_IN_CASE"]);
                            drDistinct["Qty"] = (Convert.ToInt32(dr["QUANTITY_UNIT"]) + Convert.ToInt32(dr["QUANTITY_CTN"]) * Convert.ToInt32(dr["UNITS_IN_CASE"])) - (Convert.ToInt32(dr["ORG_QUANTITY_UNIT"]) + Convert.ToInt32(dr["ORG_QUANTITY_CTN"]) * Convert.ToInt32(dr["UNITS_IN_CASE"]));
                            dtDistinctSKUS.Rows.Add(drDistinct);
                        }

                        foreach (DataRow drFree in dtFreeSKU.Rows)
                        {
                            DataRow[] foundRows = dtDistinctSKUS.Select("SKU_ID  = '" + drFree["SKU_ID"].ToString() + "'");
                            if (foundRows.Length > 0)
                            {
                                int rowIndex = (int)dtDistinctSKUS.Rows.IndexOf(foundRows[0]);
                                drDistinct = dtDistinctSKUS.Rows[rowIndex];
                                drDistinct["Qty"] = Convert.ToInt32(drDistinct["Qty"]) + (Convert.ToInt32(drFree["Quantity"]) - Convert.ToInt32(drFree["ORG_QUANTITY"]));
                            }
                            else
                            {
                                drDistinct = dtDistinctSKUS.NewRow();
                                drDistinct["SKU_ID"] = Convert.ToInt32(drFree["SKU_ID"]);
                                drDistinct["Qty"] = Convert.ToInt32(drFree["Quantity"]) - Convert.ToInt32(drFree["ORG_QUANTITY"]);
                                dtDistinctSKUS.Rows.Add(drDistinct);
                            }
                        }

                        foreach (DataRow dr in dtDistinctSKUS.Rows)
                        {
                            flag = ChkStkOrder(Convert.ToInt32(dc.chkNull_0(dr["SKU_ID"].ToString())), Convert.ToInt32(dc.chkNull_0(dr["Qty"].ToString())));
                        }
                        if (flag)
                        {
                            bool IsValidInsert = mOrderController.Update_Order(Convert.ToInt64(drpDocumentNo.SelectedValue), int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, (long)this.Session["AreaId"], Constants.IntNullValue, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString())
                              , int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()), decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text))
                              , decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, Constants.Order_Pending_Id, PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString())
                              , DateTime.Parse(this.Session["OrderDate"].ToString()), decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)), Convert.ToDecimal(DC.chkNull_0(numtxtSpecialDiscValue.Text))
                              , txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), int.Parse(DrpTransporter.SelectedValue), txtPriceGroup.Text, Convert.ToDecimal(DC.chkNull_0(numtxtExpiryDiscValue.Text)), txtRemarks.Text);
                            if (IsValidInsert)
                            {
                                this.LoadPendingOrder();
                                this.ClearMasterALL();
                                this.Session.Remove("hfBillBookNo");
                                btnSaveOrder.Text = "Save Order";
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Bill Book No already exist,Kindly enter different Bill Book No');", true);
                    }
                }
                else if (btnSaveOrder.Text == "Save Invoice")
                {
                    if (Convert.ToDateTime(this.Session["CurrentWorkDate"]) >= Convert.ToDateTime(Session["OrderDate"].ToString()))
                    {
                        if (!IsBillBookNoExist())
                        {
                            if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Credit_Order_Id || int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
                            {
                                CustomerDataController CDC = new CustomerDataController();
                                DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(this.Session["CUSTOMER_ID"].ToString()), Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()));
                                decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(DC.chkNull_0(txtCashReceived.Text));    
                            }

                            DataTable dtskuPrice = (DataTable)this.Session["Dtsku_Price"];
                            bool flag = true;
                            DataTable dtDistinctSKUS = new DataTable();
                            dtDistinctSKUS.Columns.Add("SKU_ID", typeof(int));
                            dtDistinctSKUS.Columns.Add("Qty", typeof(int));
                            dtDistinctSKUS.Columns.Add("packSize", typeof(int));

                            DataRow drDistinct = dtDistinctSKUS.NewRow();
                            foreach (DataRow dr in PurchaseSKU.Rows)
                            {
                                drDistinct = dtDistinctSKUS.NewRow();
                                drDistinct["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                                drDistinct["packSize"] = Convert.ToInt32(dr["UNITS_IN_CASE"]);
                                drDistinct["Qty"] = Convert.ToInt32(dr["QUANTITY_UNIT"]) + Convert.ToInt32(dr["QUANTITY_CTN"]) * Convert.ToInt32(dr["UNITS_IN_CASE"]);
                                dtDistinctSKUS.Rows.Add(drDistinct);
                            }

                            foreach (DataRow drFree in dtFreeSKU.Rows)
                            {
                                DataRow[] foundRows = dtDistinctSKUS.Select("SKU_ID  = '" + drFree["SKU_ID"].ToString() + "'");
                                if (foundRows.Length > 0)
                                {
                                    int rowIndex = (int)dtDistinctSKUS.Rows.IndexOf(foundRows[0]);
                                    drDistinct = dtDistinctSKUS.Rows[rowIndex];
                                    drDistinct["Qty"] = Convert.ToInt32(drDistinct["Qty"]) + Convert.ToInt32(drFree["Quantity"]);
                                }
                                else
                                {
                                    drDistinct = dtDistinctSKUS.NewRow();
                                    drDistinct["SKU_ID"] = Convert.ToInt32(drFree["SKU_ID"]);
                                    drDistinct["Qty"] = Convert.ToInt32(drFree["Quantity"]);
                                    dtDistinctSKUS.Rows.Add(drDistinct);
                                }
                            }

                            foreach (DataRow dr in dtDistinctSKUS.Rows)
                            {
                                flag = CheckStockInvoice(Convert.ToInt32(dc.chkNull_0(dr["SKU_ID"].ToString())), Convert.ToInt32(dc.chkNull_0(dr["Qty"].ToString())));
                            }
                            if (flag)
                            {
                                string CustomerName = "";
                                DateTime Date = DateTime.Parse(this.Session["CurrentWorkDate"].ToString());
                                string InvoiceDate = Date.ToString("dd-MMM-yyyy");
                                if (txtOutletName.Equals(""))
                                {
                                    CustomerName = "";
                                }
                                else
                                {
                                    CustomerName = txtOutletName.Text;
                                }

                                bool IsValidInsert = mOrderController.Add_Invoice(int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, long.Parse(this.Session["AreaId"].ToString()), Constants.IntNullValue, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()),
                                decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()),
                                PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), decimal.Parse(DC.chkNull_0(txtCashReceived.Text)), DateTime.Parse(this.Session["OrderDate"].ToString()), decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)), decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), Convert.ToDecimal(DC.chkNull_0(numtxtSpecialDiscValue.Text))
                                , txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), int.Parse(DrpTransporter.SelectedValue), txtPriceGroup.Text,out p_voucher_No,is_voucher_hit, InvoiceDate, CustomerName, Convert.ToDecimal(DC.chkNull_0(numtxtExpiryDiscValue.Text)),txtRemarks.Text,0);
                                if (IsValidInsert)
                                {
                                    if (OrderNo == -1)
                                    {
                                        LoadInvoiceToEdit(0);
                                        btnSaveOrder.Text = "Save Invoice";
                                    }
                                    else if (OrderNo == -2)
                                    {
                                        this.LoadInvoiceToEdit(1);
                                        btnSaveOrder.Text = "Sale Return";
                                    }
                                    else
                                    {
                                        this.LoadPendingOrder();
                                        btnSaveOrder.Text = "Save Order";
                                    }
                                    PrintReport(p_voucher_No);
                                    ClearMasterALL();
                                    Session.Remove("hfBillBookNo");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice Save successfully.');", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Bill Book No already exist,Kindly enter different Bill Book No');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Order Date Can not be greater than current working date');", true);
                    }
                }
                else if (btnSaveOrder.Text == "Update Invoice")
                {
                    bool flag = true;
                    string CustomerName = "";
                    DateTime Date = DateTime.Parse(this.Session["CurrentWorkDate"].ToString());
                    string InvoiceDate = Date.ToString("dd-MMM-yyyy");
                    if (txtOutletName.Equals(""))
                    {
                        CustomerName = "";
                    }
                    else
                    {
                        CustomerName = txtOutletName.Text;
                    }

                    DataTable dtDistinctSKUS = new DataTable();
                    dtDistinctSKUS.Columns.Add("SKU_ID", typeof(int));
                    dtDistinctSKUS.Columns.Add("Qty", typeof(int));
                    dtDistinctSKUS.Columns.Add("packSize", typeof(int));

                    DataRow drDistinct = dtDistinctSKUS.NewRow();
                    foreach (DataRow dr in PurchaseSKU.Rows)
                    {
                        drDistinct = dtDistinctSKUS.NewRow();
                        drDistinct["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                        drDistinct["packSize"] = Convert.ToInt32(dr["UNITS_IN_CASE"]);
                        drDistinct["Qty"] =(Convert.ToInt32(dr["QUANTITY_UNIT"]) + Convert.ToInt32(dr["QUANTITY_CTN"]) * Convert.ToInt32(dr["UNITS_IN_CASE"]))- (Convert.ToInt32(dr["ORG_QUANTITY_UNIT"]) + Convert.ToInt32(dr["ORG_QUANTITY_CTN"]) * Convert.ToInt32(dr["UNITS_IN_CASE"]));
                        dtDistinctSKUS.Rows.Add(drDistinct);
                    }

                    foreach (DataRow drFree in dtFreeSKU.Rows)
                    {
                        DataRow[] foundRows = dtDistinctSKUS.Select("SKU_ID  = '" + drFree["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            int rowIndex = (int)dtDistinctSKUS.Rows.IndexOf(foundRows[0]);
                            drDistinct = dtDistinctSKUS.Rows[rowIndex];
                            drDistinct["Qty"] = Convert.ToInt32(drDistinct["Qty"]) + (Convert.ToInt32(drFree["Quantity"]) - Convert.ToInt32(drFree["ORG_QUANTITY"]));
                        }
                        else
                        {
                            drDistinct = dtDistinctSKUS.NewRow();
                            drDistinct["SKU_ID"] = Convert.ToInt32(drFree["SKU_ID"]);
                            drDistinct["Qty"] = Convert.ToInt32(drFree["Quantity"]) - Convert.ToInt32(drFree["ORG_QUANTITY"]);
                            dtDistinctSKUS.Rows.Add(drDistinct);
                        }
                    }

                    foreach (DataRow dr in dtDistinctSKUS.Rows)
                    {
                        flag = CheckStockInvoice(Convert.ToInt32(dc.chkNull_0(dr["SKU_ID"].ToString())), Convert.ToInt32(dc.chkNull_0(dr["Qty"].ToString())));
                    }

                    if (flag)
                    {
                        bool IsValidInsert = mOrderController.Update_Invoice(Convert.ToInt64(drpDocumentNo.SelectedValue), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), ManualID, long.Parse(this.Session["CUSTOMER_ID"].ToString()), 0,
                       decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), Constants.DecimalNullValue, int.Parse(RblPayMode.SelectedValue.ToString()),
                       PurchaseSKU, int.Parse(this.Session["UserId"].ToString()), 0, 0, decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), 0, 0, Constants.DateNullValue, Convert.ToDecimal(DC.chkNull_0(numtxtSpecialDiscValue.Text))
                       , txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), int.Parse(DrpTransporter.SelectedValue),out p_voucher_No, is_voucher_hit, Convert.ToDecimal(DC.chkNull_0(numtxtExpiryDiscValue.Text)), InvoiceDate,txtRemarks.Text, dtFreeSKU, CustomerName,Constants.IntNullValue,0);
                        if (IsValidInsert)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice updated successfully.');", true);
                            LoadInvoiceToEdit(0);
                            ClearMasterALL();
                            PrintReport(p_voucher_No);
                            btnSaveOrder.Text = "Save Invoice";

                            Session.Remove("hfBillBookNo");
                        }
                    }
                }
                else if (btnSaveOrder.Text == "Sale Return")
                {
                    bool IsValidInsert = mOrderController.Add_SaleReturn(int.Parse(this.Session["DistributorId"].ToString()), mTownId, (long)this.Session["AreaId"], Constants.IntNullValue, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), DC.chkLongNull(txtBillBookNo.Text),
                    decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)), decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)),out p_voucher_No, decimal.Parse(DC.chkNull_0(numtxtExpiryDiscValue.Text)),txtRemarks.Text, Convert.ToDecimal(DC.chkNull_0(numtxtSpecialDiscValue.Text)),0);
                    if (IsValidInsert)
                    {
                        PrintReport(p_voucher_No);
                        LoadInvoiceToEdit(1);
                        this.ClearMasterALL();
                    }
                }
                else if (btnSaveOrder.Text == "Update Return")
                {
                    bool IsValidInsert = mOrderController.Update_SaleReturn(Convert.ToInt64(drpDocumentNo.SelectedValue), int.Parse(this.Session["DistributorId"].ToString()), mTownId, (long)this.Session["AreaId"], Constants.IntNullValue, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), DC.chkLongNull(txtBillBookNo.Text),
                    decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)), decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), out p_voucher_No, decimal.Parse(DC.chkNull_0(numtxtExpiryDiscValue.Text)), txtRemarks.Text, Convert.ToDecimal(DC.chkNull_0(numtxtSpecialDiscValue.Text)),0);
                    if (IsValidInsert)
                    {
                        btnSaveOrder.Text = "Sale Return";
                        PrintReport(p_voucher_No);
                        LoadInvoiceToEdit(1);
                        this.ClearMasterALL();
                    }
                }
            }
        }
    }

    protected void txtOutletCode_TextChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();

    }

    protected void btnOutletCode_Click(object sender, EventArgs e)
    {
        txtOutletCode_TextChanged(null, null);
        //this.LoadSkuDetail();
        this.LoadPromotion();
        ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
    }

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {
        
     //   txtUnitRate.Text = "";
        txtQuantity.Text = "";
        txtDiscount.Text = "0";
        txtBatchNo.Text = "";
        btnSave.Text = "Add";
        btnSaveOrder.Enabled = false;
        btnCalculate.Enabled = true;
        ddlSkus.Enabled = true;
        txtCtn.Text = "";
    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        this.EnableDisableController(true);
        this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtFreeSKU");
        btnSaveOrder.Enabled = false;
        this.CreatTable();
        this.CreateFreeSKU();
        txtOutletCode.Text = "";
        txtOutletName.Text = "";
        this.LoadGird();
        this.LoadFreeGrid();
        txtGrossAmount.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        numtxtSpecialDiscValue.Text = string.Empty;
        numtxtTotalExtraDiscntpr.Text = "0";
        numtxtSpecialDiscPr.Text = "0";
        numTxtTotalStndrdDiscnt.Text = "";
        numtxtExpiryDiscValue.Text = "";
        numtxtExpiryDiscPr.Text = "0";
        numTxtTotalGST.Text = "";
        numTxtTotlAmnt.Text = "";
        numTxtTotalSED.Text = "";
        numTxtTotalTST.Text = "";
        numTxtTotalSED.Text = "";
        numtxtUnClaimabledist.Text = "";
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;
        drpDocumentNo.SelectedIndex = 0;
        txtCashReceived.Text = "";
        txtBillBookNo.Text = string.Empty;
        txtBuiltyAmount.Text = "";
        txtBuiltyNo.Text = "";
        txtPriceGroup.Text = "";
        ddlSkus.Items.Clear();
        txtRemarks.Text = "";
       
        if (btnSaveOrder.Text != "Sale Return")
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);
        }
        else
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtOutletCode);
        }
    }

    private bool IsDayClosed()
    {
        bool flag = false;
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(Session["DistributorId"]), 3);
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

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSkuDetail();
        this.LoadPromotion();
    }
    private void PrintReport(string p_Voucher_No)
    {
        DataTable DisDt = (DataTable)this.Session["DisDt"];
        if (DisDt != null)
        {
            if (DisDt.Rows.Count > 0)
            {
                DataRow[] foundRows = DisDt.Select("DISTRIBUTOR_ID  = '" + this.Session["DistributorId"].ToString() + "'");
                if (foundRows.Length > 0)
                {
                    if (foundRows[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                    {
                        #region print report
                        DocumentPrintController DPrint = new DocumentPrintController();
                        RptAccountController RptAccountCtl = new RptAccountController();
                        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();

                        DataSet ds = null;
                        DataTable dt = DPrint.SelectReportTitle(int.Parse(this.Session["DistributorId"].ToString()));
                        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(this.Session["DistributorId"].ToString()), p_Voucher_No, Constants.Journal_Voucher);
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


    public bool CheckStock(int qty,int ctn)
    {
        if (btnSaveOrder.Text == "Sale Return" || btnSaveOrder.Text == "Update Return")
        {
            return true;
        }
        else
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
            int avaliableUnits = int.Parse(Unt) + (int.Parse(Ctn) * int.Parse(UNITS_IN_CASE));
            int enteredUnits = Convert.ToInt32(dc.chkNull_0(txtQuantity.Text)) + Convert.ToInt32(dc.chkNull_0(txtCtn.Text)) * Convert.ToInt32(UNITS_IN_CASE);
            
            if (btnSave.Text == "Update" && btnSaveOrder.Text == "Update Order")
            {
                avaliableUnits = avaliableUnits + qty + (ctn * int.Parse(UNITS_IN_CASE));
                if (enteredUnits > avaliableUnits)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + SKU_NAME + " Current Stock is " + Stock + "');", true);
                    txtQuantity.Focus();
                    txtQuantity.Attributes.Add("onfocus", "javascript:this.select();");
                    return false;
                }
            }
            else
            {   
                if (enteredUnits > avaliableUnits)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + SKU_NAME + " Current Stock is " + Stock + "');", true);
                    txtQuantity.Focus();
                    txtQuantity.Attributes.Add("onfocus", "javascript:this.select();");
                    return false;
                }
            }
            return true;
        }
    }

    private bool CheckStockInvoice(int skuId, int qty)
    {
        if (btnSaveOrder.Text == "Save Invoice" || btnSaveOrder.Text == "Update Invoice")
        {
            PhaysicalStockController mController = new PhaysicalStockController();
            DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), skuId, txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()));
            if (dtstock.Rows.Count > 0)
            {
                if (btnSaveOrder.Text == "Update Invoice")
                {
                    if (int.Parse(dtstock.Rows[0][0].ToString()) < qty)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ddlSkus.SelectedItem.Text + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return false;
                    }
                }
                else if (btnSaveOrder.Text == "Save Invoice")
                {
                    if (int.Parse(dtstock.Rows[0][0].ToString()) < qty)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ddlSkus.SelectedItem.Text + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + ddlSkus.SelectedItem.Text + "No Stock Found');", true);
                return false;
            }
        }
        return true;
    }
    public bool CheckStockOrder(int skuId, int qty)
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
        int avaliableUnits = int.Parse(Unt) + (int.Parse(Ctn) * int.Parse(UNITS_IN_CASE));
        if (qty > avaliableUnits)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + SKU_NAME + " Current Stock is " + Stock + "');", true);
            txtQuantity.Focus();
            txtQuantity.Attributes.Add("onfocus", "javascript:this.select();");
            return false;
        }
        return true;
    }

    public bool ChkStkOrder(int skuId, int qty)
    {
        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + skuId + "'");
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
        int avaliableUnits = int.Parse(Unt) + (int.Parse(Ctn) * int.Parse(UNITS_IN_CASE));
        if (qty > avaliableUnits)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + SKU_NAME + " Current Stock is " + Stock + "');", true);
            txtQuantity.Focus();
            txtQuantity.Attributes.Add("onfocus", "javascript:this.select();");
            return false;
        }
        return true;
    }

    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Edit")
        //{
        //    //int index = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;
        //    int index = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;

        //    ddlSkus.SelectedValue = GrdPurchase.Rows[index].Cells[0].Text;
        //    txtCtn.Text = GrdPurchase.Rows[index].Cells[3].Text;
        //    txtQuantity.Text = GrdPurchase.Rows[index].Cells[4].Text;
        //    txtDiscount.Text = GrdPurchase.Rows[index].Cells[5].Text;
        //    txtUnitRate.Text = GrdPurchase.Rows[index].Cells[6].Text;
        //    txtBatchNo.Text = GrdPurchase.Rows[index].Cells[7].Text;
        //    ddlSkus.Enabled = false;
        //    btnSave.Text = "Update";

        //}
    }
}