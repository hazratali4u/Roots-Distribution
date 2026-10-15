using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Bank Transaction
/// </summary>
public partial class Forms_frmCustomerRelizationQ : System.Web.UI.Page
{
    readonly LedgerController _ledgerCtl = new LedgerController();
    readonly SaleForceController _saleForceCtl = new SaleForceController();
    int Count = 0;
    string Inoices = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Configuration.GetAccountHead();
            txtTransactionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTransactionDate.Attributes.Add("readonly", "readonly");
            LoadPrincipal();
            LoadDistributor();
            LoadDeliveryman();
            LoadArea();
            LoadData();
            LoadAccountHead();
            LoadGrid();
            SelectCreditInvoice();
            //btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SetTableSorter();
        }
    }

    private DataTable CreateTable()
    {
        DataTable dtLedger = new DataTable();
        dtLedger.Columns.Add("VOUCHER_TYPE_ID", typeof(int));
        dtLedger.Columns.Add("VOUCHER_NO", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtLedger.Columns.Add("Distributor_ID", typeof(int));
        dtLedger.Columns.Add("Debit", typeof(decimal));
        dtLedger.Columns.Add("Credit", typeof(decimal));
        dtLedger.Columns.Add("Ledger_Date", typeof(DateTime));
        dtLedger.Columns.Add("Remarks", typeof(string));
        dtLedger.Columns.Add("TimeStamp", typeof(DateTime));
        dtLedger.Columns.Add("Customer_ID", typeof(int));
        dtLedger.Columns.Add("Principal_ID", typeof(int));
        dtLedger.Columns.Add("Cheque_NO", typeof(string));
        dtLedger.Columns.Add("UserId", typeof(int));
        dtLedger.Columns.Add("Document_ID", typeof(long));
        dtLedger.Columns.Add("Manual_Document_ID", typeof(string));
        dtLedger.Columns.Add("DocumentTypeID", typeof(int));
        dtLedger.Columns.Add("SlipNo", typeof(string));
        dtLedger.Columns.Add("ChequeDate", typeof(DateTime));
        dtLedger.Columns.Add("PaymentMode", typeof(int));
        dtLedger.Columns.Add("PayeesName", typeof(string));
        Session.Add("dtLedger", dtLedger);
        return dtLedger;
    }

    #region Load

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        this.Session.Add("DisDt", dt);
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
    }

    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadData()
    {
        //GrdCredit.DataSource = null;
        //GrdCredit.DataBind();
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();

            if (DrpAccountType.SelectedIndex == 1 || DrpAccountType.SelectedIndex == 5)
            {

                //DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpRoute.SelectedValue), Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue));
                DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, true);
                DrpRoute.Enabled = true;
            }
            else
            {

                //DataTable dtCredit = _ledgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), Constants.LongNullValue, Constants.IntNullValue);
                DataTable dtCredit = _ledgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, dtCredit, 0, 1, true);
                DrpRoute.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Loads Bank Transactions To Grid
    /// </summary>
    private void LoadGrid()
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            string DrpAccountTypeSelectedValue = "";
            if (DrpAccountType.SelectedValue == "222")
            {
                DrpAccountTypeSelectedValue = "22";
            }
            else
            {
                DrpAccountTypeSelectedValue = DrpAccountType.SelectedValue;
            }
            if (drpDistributor.Items.Count > 0)
            {

                DataTable dt = _ledgerCtl.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Convert.ToInt32(DrpAccountTypeSelectedValue),
                    DateTime.Parse(txtTransactionDate.Text), DateTime.Parse(txtTransactionDate.Text));
                GrdOrder.DataSource = dt;
                GrdOrder.DataBind();
            }
        }
    }

    private void LoadAccountHead()
    {
        if (DrpAccountType.SelectedIndex == 0 || DrpAccountType.SelectedIndex == 1 || DrpAccountType.SelectedIndex == 5 || DrpAccountType.SelectedIndex == 7)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            string ListSelectedAccoundHead = "750";//"40,747,35,461,41,749,42,750,43,752,608,908,622,917,632,924,984,61,58,461,781,60,791";
            DataTable dt = mAccountController.SelectAccountHeadForBankCash(Constants.AC_AccountHeadId, ListSelectedAccoundHead);
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
        }
        else if (DrpAccountType.SelectedIndex == 3)
        {
            DrpAccountDetail.Items.Clear();
            DrpAccountDetail.Items.Add(new ListItem("Tax Deducted By Parties", "127"));
        }
        else if (DrpAccountType.SelectedIndex == 4)
        {
            DrpAccountDetail.Items.Clear();
            DrpAccountDetail.Items.Add(new ListItem("Credit Transfer Out", "1211"));
        }
        else if (DrpAccountType.SelectedIndex == 2 || DrpAccountType.SelectedIndex == 6 || DrpAccountType.SelectedIndex == 8)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
        }
        //else
        //{
        //    DrpAccountDetail.Items.Clear();
        //}

    }

    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }

    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0)
        {

            DataTable mDt = _saleForceCtl.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, mDt, 0, 3, true);
        }
    }

    /// <summary>
    /// Loads Sale Force Cash To Grid
    /// </summary>
    private void LoadSaleFoceCash()
    {
        gvSaleForceCash.DataSource = null;
        gvSaleForceCash.DataBind();
        DataTable dt = _saleForceCtl.GetSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToInt32(DrpDeliveryMan.SelectedValue), DateTime.Parse(txtTransactionDate.Text), DateTime.Parse(txtTransactionDate.Text));
        gvSaleForceCash.DataSource = dt;
        gvSaleForceCash.DataBind();
        gvSaleForceCash.Visible = true;
        GrdOrder.Visible = false;
    }
    #endregion

    /// <summary>
    /// Saves Cash Realization
    /// </summary>
    private void CashRealization()
    {
        // string maxDocumentId = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        decimal offerAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;
        int CustomerbankId = 0;
        int PaymentMode = Constants.CashPayment;//For Cash Advance (21) and Cash Realization (19)
        string maxDocumentId = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        Session.Add("VoucherNo", maxDocumentId);
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                #region Ledger
                DataTable dtLedger = CreateTable();
                //Credit from Account Receivable (Party Wise)
                DataRow drLedger = dtLedger.NewRow();
                drLedger["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                drLedger["VOUCHER_NO"] = maxDocumentId;
                drLedger["ACCOUNT_HEAD_ID"] = Configuration.AccountReceivable;
                drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = offerAmount;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = dr.Cells[3].Text;
                }
                drLedger["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
                drLedger["Remarks"] = txtRemarks.Text;
                drLedger["TimeStamp"] = DateTime.Now;
                drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                //drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger["Principal_ID"] = 0;
                drLedger["Cheque_NO"] = txtChequeNo.Text;
                drLedger["UserID"] = Session["UserId"].ToString();
                drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger["SlipNo"] = txtSlipNo.Text;
                drLedger["ChequeDate"] = Constants.DateNullValue;
                drLedger["PaymentMode"] = 19;
                drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger);

                //Debit to selected Account Head
                DataRow drLedger2 = dtLedger.NewRow();
                drLedger2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                drLedger2["VOUCHER_NO"] = maxDocumentId;
                drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    drLedger2["DEBIT"] = offerAmount;
                    drLedger2["CREDIT"] = 0;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    drLedger2["DEBIT"] = dr.Cells[3].Text;
                    drLedger2["CREDIT"] = 0;
                }
                drLedger2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
                drLedger2["Remarks"] = txtRemarks.Text;
                drLedger2["TimeStamp"] = DateTime.Now;
                drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                //drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger2["Principal_ID"] = 0;
                drLedger2["Cheque_NO"] = txtChequeNo.Text;
                drLedger2["UserID"] = Session["UserId"].ToString();
                drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger2["SlipNo"] = txtSlipNo.Text;
                drLedger2["ChequeDate"] = Constants.DateNullValue;
                drLedger2["PaymentMode"] = 19;
                drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger2);

                if (decimal.Parse(dr.Cells[3].Text) > offerAmount)
                {
                    realizeAmount += offerAmount;
                    offerAmount = decimal.Parse(dr.Cells[3].Text) - offerAmount;
                    _ledgerCtl.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue), offerAmount);
                    break;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    realizeAmount += decimal.Parse(dr.Cells[3].Text);
                    offerAmount = offerAmount - decimal.Parse(dr.Cells[3].Text);
                    _ledgerCtl.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue), 0);
                    //break;
                }
                #endregion
            }
        }
        if (realizeAmount < decimal.Parse(txtAmount.Text))
        {
            _ledgerCtl.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(maxDocumentId), int.Parse(Configuration.AccountReceivable)
                , int.Parse(drpDistributor.SelectedValue.ToString()), 0, offerAmount, DateTime.Parse(txtTransactionDate.Text)
                , txtRemarks.Text + " (Advance)", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId, txtChequeNo.Text, int.Parse(Session["UserId"].ToString())
                , 0, "0", Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, PaymentMode, "Group Payment");

            _ledgerCtl.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(maxDocumentId), long.Parse(DrpAccountDetail.SelectedValue.ToString())
                , int.Parse(drpDistributor.SelectedValue.ToString()), offerAmount, 0, DateTime.Parse(txtTransactionDate.Text)
            , txtRemarks.Text + " (Advance)", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId, txtChequeNo.Text, int.Parse(Session["UserId"].ToString())
            , 0, "0", Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, PaymentMode, "Group Payment");
        }
    }

    /// <summary>
    /// Saves Cash Advance
    /// </summary>
    private void CashAdvance()
    {
        string MaxDocumentID = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        Session.Add("VoucherNo", MaxDocumentID);
        DataTable dtLedger = CreateTable();

        //Credit from Account Receivable (Party Wise)
        DataRow dr = dtLedger.NewRow();
        dr["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr["VOUCHER_NO"] = MaxDocumentID;
        dr["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = txtAmount.Text;
        dr["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = DrpCustomer.SelectedValue;
        //dr["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr["Principal_ID"] = 0;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = this.Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 21;
        dr["PayeesName"] = "";
        dtLedger.Rows.Add(dr);

        //Debit to selected Account Head
        DataRow dr2 = dtLedger.NewRow();
        dr2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr2["VOUCHER_NO"] = MaxDocumentID;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = txtAmount.Text;
        dr2["CREDIT"] = 0;
        dr2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = DrpCustomer.SelectedValue;
        //dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr2["Principal_ID"] = 0;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = this.Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 21;
        dr2["PayeesName"] = "";
        dtLedger.Rows.Add(dr2);
        _ledgerCtl.PostingCash_Bank_Account(dtLedger);
    }

    /// <summary>
    /// Saves Bank Deposits For Branch And Deliveryman
    /// </summary>
    /// <param name="pSaleForceId"></param>
    private void BankDeposit(string pSaleForceId)
    {
        CustomerDataController cd = new CustomerDataController();
        string CodeFileName = "";
        DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);
        if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
        {
            CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
            Configuration.GetAppMapping(CodeFileName);
        }
        string MaxDocumentID = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        DataTable dtLedger = CreateTable();
        //Credit from NCS
        DataRow dr = dtLedger.NewRow();
        dr["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
        dr["VOUCHER_NO"] = MaxDocumentID;
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr["ACCOUNT_HEAD_ID"] = long.Parse(Configuration.SundryDebtors);
        }
        else
        {
            // For Roots
            dr["ACCOUNT_HEAD_ID"] = Configuration.CashDefault;
        }
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = txtAmount.Text;
        dr["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = Constants.IntNullValue;
        //dr["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr["Principal_ID"] = 0;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = this.Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 22;
        dr["PayeesName"] = pSaleForceId;
        dtLedger.Rows.Add(dr);

        //Debit to selected Account Head
        DataRow dr2 = dtLedger.NewRow();
        dr2["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
        dr2["VOUCHER_NO"] = MaxDocumentID;
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        }
        else
        {
            // For Roots
            dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        }
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = txtAmount.Text;
        dr2["CREDIT"] = 0;
        dr2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = Constants.IntNullValue;
        //dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr2["Principal_ID"] = 0;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 22;
        dr2["PayeesName"] = pSaleForceId;
        dtLedger.Rows.Add(dr2);
        _ledgerCtl.PostingCash_Bank_Account(dtLedger);
    }

    /// <summary>
    /// Saves Petty Cash
    /// </summary>
    private void PettyCash()
    {
        string maxDocumentId = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue));
        _ledgerCtl.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(maxDocumentId), long.Parse(Configuration.CashDefault), int.Parse(drpDistributor.SelectedValue), decimal.Parse(txtAmount.Text), 0,
                DateTime.Parse(txtTransactionDate.Text), txtRemarks.Text, DateTime.Now, Constants.IntNullValue, Constants.IntNullValue,
                txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, int.Parse(DrpAccountType.SelectedValue), "");
    }

    /// <summary>
    /// Loads Credit Invoices To Invoice Grid
    /// </summary>
    private void SelectCreditInvoice()
    {

        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0 && DrpAccountType.SelectedIndex != 1 && DrpAccountType.SelectedIndex != 5)
        {
            //DataTable dtCredit = _ledgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), long.Parse(DrpCustomer.SelectedValue), 0);
            DataTable dtCredit = _ledgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue), 0);
            if (dtCredit.Rows.Count <= 0) return;
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
            //foreach (GridViewRow dr in GrdCredit.Rows)
            //{
            //    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            //    chRelized.Checked = true;
            //    //chRelized.Enabled = false;
            //}
        }
    }

    /// <summary>
    /// Saves Income Tax
    /// </summary>
    private void IncomeTax()
    {
        string MaxDocumentID = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                #region Ledger
                DataTable dtLedger = CreateTable();
                //Credit from Account Receivable (Party Wise)
                DataRow drLedger = dtLedger.NewRow();
                drLedger["VOUCHER_TYPE_ID"] = Constants.Journal_Voucher;
                drLedger["VOUCHER_NO"] = MaxDocumentID;
                drLedger["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
                drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = OfferAmount;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = dr.Cells[3].Text;
                }
                drLedger["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
                drLedger["Remarks"] = txtRemarks.Text;
                drLedger["TimeStamp"] = DateTime.Now;
                drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                //drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger["Principal_ID"] = 0;
                drLedger["Cheque_NO"] = txtChequeNo.Text;
                drLedger["UserID"] = this.Session["UserId"].ToString();
                drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger["SlipNo"] = txtSlipNo.Text;
                drLedger["ChequeDate"] = Constants.DateNullValue;
                drLedger["PaymentMode"] = DrpAccountType.SelectedValue;
                drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger);

                //Debit to selected Account Head
                DataRow drLedger2 = dtLedger.NewRow();
                drLedger2["VOUCHER_TYPE_ID"] = Constants.Journal_Voucher;
                drLedger2["VOUCHER_NO"] = MaxDocumentID;
                drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                {
                    drLedger2["DEBIT"] = OfferAmount;
                    drLedger2["CREDIT"] = 0;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    drLedger2["DEBIT"] = dr.Cells[3].Text;
                    drLedger2["CREDIT"] = 0;
                }
                drLedger2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
                drLedger2["Remarks"] = txtRemarks.Text;
                drLedger2["TimeStamp"] = DateTime.Now;
                drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                //drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger2["Principal_ID"] = 0;
                drLedger2["Cheque_NO"] = txtChequeNo.Text;
                drLedger2["UserID"] = this.Session["UserId"].ToString();
                drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger2["SlipNo"] = txtSlipNo.Text;
                drLedger2["ChequeDate"] = Constants.DateNullValue;
                drLedger2["PaymentMode"] = DrpAccountType.SelectedValue;
                drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger2);

                if (decimal.Parse(dr.Cells[3].Text) > OfferAmount)
                {
                    OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                    _ledgerCtl.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);
                    break;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                    _ledgerCtl.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), 0);
                }
                break;
                #endregion
            }
        }
    }

    /// <summary>
    /// Saves Advance Return
    /// </summary>
    private void AdvanceReturn()
    {
        string MaxDocumentID = _ledgerCtl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        DataTable dtLedger = CreateTable();

        //Debit to Account Receivable (Party Wise)
        DataRow dr = dtLedger.NewRow();
        dr["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr["VOUCHER_NO"] = MaxDocumentID;
        dr["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = txtAmount.Text;
        dr["CREDIT"] = 0;
        dr["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = DrpCustomer.SelectedValue;
        //dr["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr["Principal_ID"] = 0;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = this.Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 29;
        dr["PayeesName"] = "";
        dtLedger.Rows.Add(dr);

        //Credit from selected Account Head
        DataRow dr2 = dtLedger.NewRow();
        dr2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr2["VOUCHER_NO"] = MaxDocumentID;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = 0;
        dr2["CREDIT"] = txtAmount.Text;
        dr2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = DrpCustomer.SelectedValue;
        //dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr2["Principal_ID"] = 0;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = this.Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 29;
        dr2["PayeesName"] = "";
        dtLedger.Rows.Add(dr2);
        _ledgerCtl.PostingCash_Bank_Account(dtLedger);
    }

    #region Sel/Index Change

    /// <summary>
    /// Loads Routes, Customers, Deliverymen And Bank Transactions
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadArea();
            LoadData();
            LoadGrid();
            gvSaleForceCash.Visible = false;
            DrpCustomer_SelectedIndexChanged(null, null);
            GrdOrder.Visible = true;
        }
        else if (DrpAccountType.SelectedIndex == 7)
        {
            LoadSaleFoceCash();
        }
        LoadDeliveryman();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Customers And Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        SelectCreditInvoice();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpAccountDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadData();
        //LoadGrid();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Account Head, Customers, Deliverymen And Bank Transactions
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex == 2 || this.DrpAccountType.SelectedIndex == 7)
        {
            HideShowControls(false);
        }
        else
        {
            HideShowControls(true);
        }
        if (DrpAccountType.SelectedValue == "30")
        {
            SelectCreditInvoice();
        }
        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadData();
            LoadAccountHead();
            LoadGrid();
            DrpCustomer_SelectedIndexChanged(null, null);
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;


        }
        else
        {
            LoadSaleFoceCash();
            LoadAccountHead();
        }
        SetTableSorter();

    }

    /// <summary>
    /// Loads Bank Transactions
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDeliveryMan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DrpAccountType.SelectedIndex == 7)
        {
            this.LoadSaleFoceCash();
        }
        else
        {
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
    }

    /// <summary>
    /// Loads Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Credit Invoices And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadData();
            SelectCreditInvoice();
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
        else
        {
            LoadSaleFoceCash();
        }
        SetTableSorter();
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsDayClosed())
        {
            UserController userCtl = new UserController();
            userCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (txtAmount.Text.Equals("") || txtAmount.Text.Equals("0"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Must Enter Amount.')", true);
                txtAmount.Focus();
                return;
            }
            if (btnSave.Text == "Update")
            {

                _saleForceCtl.DeleteSaleForceCash(Convert.ToInt32(hfSALE_FORCE_CASH_ID.Value), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPRINCIPAL_ID.Value), Convert.ToInt32(hfDELIVERYMAN_ID.Value));
            }

            if (DrpAccountType.SelectedIndex == 0)
            {
                #region Is Invoice Selected
                int invoiceCount = Constants.IntNullValue;
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        invoiceCount++;
                        break;
                    }
                }
                if (invoiceCount == Constants.IntNullValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Select Invoice');", true);
                    return;
                }
                #endregion
                #region Invoice Date Check with Transaction Date
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        DateTime TransactionDate = Convert.ToDateTime(txtTransactionDate.Text);
                        string invoicedate = Convert.ToDateTime(dr.Cells[2].Text).ToString("dd-MMM-yyyy");
                        DateTime InvoiceDate = Convert.ToDateTime(invoicedate);
                        if (TransactionDate < InvoiceDate)
                        {
                            Count += 1;
                            Inoices += GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString() + ",";
                        }
                    }
                }
                if (Count != 0)
                {
                    if (Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice numbers " + Inoices.TrimEnd(',') + " cloud not setteled because these invoices date is greater than the transaction date.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice number " + Inoices.TrimEnd(',') + " cloud not setteled because the invoice date is greater than the transaction date.');", true);
                    }
                    Count = 0;
                    return;
                }

                #endregion

                CashRealization();
                InsertGL();

                Session.Remove("VoucherNo");
                SelectCreditInvoice();
            }
            else if (DrpAccountType.SelectedIndex == 8)// Online Transfer 
            {
                #region Is Invoice Selected
                int InvoiceCount = Constants.IntNullValue;
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        InvoiceCount++;
                        break;
                    }
                }
                if (InvoiceCount == Constants.IntNullValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Select Invoice');", true);
                    return;
                }
                #endregion
                #region Invoice Date Check with Transaction Date
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        DateTime TransactionDate = Convert.ToDateTime(txtTransactionDate.Text);
                        string invoicedate = Convert.ToDateTime(dr.Cells[2].Text).ToString("dd-MMM-yyyy");
                        DateTime InvoiceDate = Convert.ToDateTime(invoicedate);
                        if (TransactionDate < InvoiceDate)
                        {
                            Count += 1;
                            Inoices += GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString() + ",";
                        }
                    }
                }
                if (Count != 0)
                {
                    if (Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice numbers " + Inoices.TrimEnd(',') + " cloud not setteled because these invoices date is greater than the transaction date.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice number " + Inoices.TrimEnd(',') + " cloud not setteled because the invoice date is greater than the transaction date.');", true);
                    }
                    Count = 0;
                    return;
                }
                #endregion

                this.OnlineTransfer();
                InsertGLOnlineTransfer();
                this.SelectCreditInvoice();
            }
            else if (DrpAccountType.SelectedIndex == 1)
            {
                CashAdvance();
                InsertGL();
            }
            else if (DrpAccountType.SelectedIndex == 2)
            {
                BankDeposit("");
            }
            else if (DrpAccountType.SelectedIndex == 6)
            {
                BankDeposit(DrpDeliveryMan.SelectedValue);
            }
            else if (DrpAccountType.SelectedIndex == 5)
            {
                AdvanceReturn();
            }
            else if (DrpAccountType.SelectedIndex == 3)
            {
                #region Is Invoice Selected
                int InvoiceCount = Constants.IntNullValue;
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        InvoiceCount++;
                        break;
                    }
                }
                if (InvoiceCount == Constants.IntNullValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Select Invoice');", true);
                    return;
                }
                #endregion
                #region Invoice Date Check with Transaction Date
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        DateTime TransactionDate = Convert.ToDateTime(txtTransactionDate.Text);
                        string invoicedate = Convert.ToDateTime(dr.Cells[2].Text).ToString("dd-MMM-yyyy");
                        DateTime InvoiceDate = Convert.ToDateTime(invoicedate);
                        if (TransactionDate < InvoiceDate)
                        {
                            Count += 1;
                            Inoices += GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString() + ",";
                        }
                    }
                }
                if (Count != 0)
                {
                    if (Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice numbers " + Inoices.TrimEnd(',') + " cloud not setteled because these invoices date is greater than the transaction date.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice number " + Inoices.TrimEnd(',') + " cloud not setteled because the invoice date is greater than the transaction date.');", true);
                    }
                    Count = 0;
                    return;
                }
                #endregion
                IncomeTax();
                SelectCreditInvoice();
            }
            else if (DrpAccountType.SelectedIndex == 4)
            {
                #region Is Invoice Selected
                int InvoiceCount = Constants.IntNullValue;
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        InvoiceCount++;
                        break;
                    }
                }
                if (InvoiceCount == Constants.IntNullValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Select Invoice');", true);
                    return;
                }
                #endregion
                #region Invoice Date Check with Transaction Date
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        DateTime TransactionDate = Convert.ToDateTime(txtTransactionDate.Text);
                        string invoicedate = Convert.ToDateTime(dr.Cells[2].Text).ToString("dd-MMM-yyyy");
                        DateTime InvoiceDate = Convert.ToDateTime(invoicedate);
                        if (TransactionDate < InvoiceDate)
                        {
                            Count += 1;
                            Inoices += GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString() + ",";
                        }
                    }
                }
                if (Count != 0)
                {
                    if (Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice numbers " + Inoices.TrimEnd(',') + " cloud not setteled because these invoices date is greater than the transaction date.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice number " + Inoices.TrimEnd(',') + " cloud not setteled because the invoice date is greater than the transaction date.');", true);
                    }
                    Count = 0;
                    return;
                }
                #endregion
                CashRealization();
                InsertGL2();
                SelectCreditInvoice();

            }
            else if (DrpAccountType.SelectedIndex == 7)
            {
                SaleForceCashReceived();
                LoadSaleFoceCash();

            }
            else
            {
                PettyCash();
            }
            ClearAll();
            LoadGrid();
            SetTableSorter();
        }
    }

    private bool Is_Advance()
    {
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                #region

                if (decimal.Parse(dr.Cells[3].Text) > OfferAmount)
                {
                    realizeAmount += OfferAmount;
                    OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                    break;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    realizeAmount += Convert.ToDecimal(dr.Cells[3].Text);
                    OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                }
                //break;
                #endregion
            }
        }
        if (realizeAmount < decimal.Parse(txtAmount.Text))
        {
            return true;
        }
        return false;

    }

    private void OnlineTransfer()
    {
        LedgerController LController = new LedgerController();
        string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        Session.Add("VoucherNo", MaxDocumentID);
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                #region
                DataTable dtLedger = CreateTable();
                //Credit from Account Receivable (Party Wise)   CD
                DataRow drLedger = dtLedger.NewRow();
                drLedger["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
                drLedger["VOUCHER_NO"] = MaxDocumentID;
                drLedger["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
                drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = OfferAmount;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = dr.Cells[3].Text;
                }
                drLedger["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
                drLedger["Remarks"] = txtRemarks.Text;
                drLedger["TimeStamp"] = DateTime.Now;
                drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger["Principal_ID"] = 0; //DrpPrincipal.SelectedValue;
                drLedger["Cheque_NO"] = txtChequeNo.Text;
                drLedger["UserID"] = this.Session["UserId"].ToString();
                drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger["SlipNo"] = txtSlipNo.Text;
                drLedger["ChequeDate"] = Constants.DateNullValue;
                drLedger["PaymentMode"] = Constants.OnlineTransfer;
                drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger);

                //Debit to selected Account Head
                DataRow drLedger2 = dtLedger.NewRow();
                drLedger2["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
                drLedger2["VOUCHER_NO"] = MaxDocumentID;
                drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                {
                    drLedger2["DEBIT"] = OfferAmount;
                    drLedger2["CREDIT"] = 0;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    drLedger2["DEBIT"] = dr.Cells[3].Text;
                    drLedger2["CREDIT"] = 0;
                }
                drLedger2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
                drLedger2["Remarks"] = txtRemarks.Text;
                drLedger2["TimeStamp"] = DateTime.Now;
                drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger2["Principal_ID"] = 0; //DrpPrincipal.SelectedValue;
                drLedger2["Cheque_NO"] = txtChequeNo.Text;
                drLedger2["UserID"] = this.Session["UserId"].ToString();
                drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger2["SlipNo"] = txtSlipNo.Text;
                drLedger2["ChequeDate"] = Constants.DateNullValue;
                drLedger2["PaymentMode"] = Constants.OnlineTransfer;
                drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger2);

                if (decimal.Parse(dr.Cells[3].Text) > OfferAmount)
                {
                    realizeAmount += OfferAmount;
                    OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                    LController.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);
                    break;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    realizeAmount += Convert.ToDecimal(dr.Cells[3].Text);
                    OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                    LController.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), 0);
                }
                //break;
                #endregion
            }
        }
        if (realizeAmount < decimal.Parse(txtAmount.Text))
        {
            CashAdvanceRemaining(OfferAmount, MaxDocumentID);
        }
    }

    private void CashAdvanceRemaining(decimal p_Amount, string MaxDocumentID)
    {
        LedgerController LController = new LedgerController();
        // string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        DataTable dtLedger = CreateTable();
        //Credit from Account Receivable (Party Wise)
        DataRow dr = dtLedger.NewRow();

        dr["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
        dr["VOUCHER_NO"] = MaxDocumentID;
        dr["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = p_Amount;
        dr["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = DrpCustomer.SelectedValue;
        dr["Principal_ID"] = 0; //DrpPrincipal.SelectedValue;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = this.Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 21;
        dr["PayeesName"] = "";
        dtLedger.Rows.Add(dr);

        //Debit to selected Account Head
        DataRow dr2 = dtLedger.NewRow();

        dr2["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
        dr2["VOUCHER_NO"] = MaxDocumentID;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = p_Amount;
        dr2["CREDIT"] = 0;
        dr2["Ledger_Date"] = DateTime.Parse(txtTransactionDate.Text);
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = DrpCustomer.SelectedValue;
        dr2["Principal_ID"] = 0; //DrpPrincipal.SelectedValue;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = this.Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 21;
        dr2["PayeesName"] = "";
        dtLedger.Rows.Add(dr2);

        LController.PostingCash_Bank_Account(dtLedger);
    }
    /// <summary>
    /// Saves Sale Force Cash
    /// </summary>
    private void SaleForceCashReceived()
    {
        try
        {
            Convert.ToDecimal(txtAmount.Text);
            //_saleForceCtl.InsertSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpPrincipal.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), Convert.ToDateTime(this.Session["CurrentWorkDate"]), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(this.Session["UserId"]));
            _saleForceCtl.InsertSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToInt32(DrpDeliveryMan.SelectedValue), DateTime.Parse(txtTransactionDate.Text), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(this.Session["UserId"]));
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Amount must be decimal')", true);
        }
    }

    #region Grid Operations

    /// <summary>
    /// Deletes Sale Force Cash
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSaleForceCash_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (_saleForceCtl.DeleteSaleForceCash(Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[0].Text), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[1].Text), Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[3].Text)))
        {
            LoadSaleFoceCash();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured. Cash received not deleted.');", true);
        }
        SetTableSorter();
    }

    /// <summary>
    /// Sets Sale Force Cash For Edit. This Function Runs When An Existing Sale Force Cash Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSaleForceCash_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //DrpPrincipal.SelectedValue = gvSaleForceCash.Rows[e.NewEditIndex].Cells[1].Text;
        DrpDeliveryMan.SelectedValue = gvSaleForceCash.Rows[e.NewEditIndex].Cells[3].Text;
        txtAmount.Text = gvSaleForceCash.Rows[e.NewEditIndex].Cells[6].Text;

        hfSALE_FORCE_CASH_ID.Value = gvSaleForceCash.Rows[e.NewEditIndex].Cells[0].Text;
        hfPRINCIPAL_ID.Value = gvSaleForceCash.Rows[e.NewEditIndex].Cells[1].Text;
        hfDELIVERYMAN_ID.Value = gvSaleForceCash.Rows[e.NewEditIndex].Cells[3].Text;

        btnSave.Text = "Update";
        SetTableSorter();
    }

    /// <summary>
    /// Deletes Bank Transaction
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataControl dc = new DataControl();
        _ledgerCtl.DeleteCashBankTransction(int.Parse(drpDistributor.SelectedValue), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)), long.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[10].Text.Replace("&nbsp;", ""))), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)));
        LoadGrid();
        SelectCreditInvoice();
        SetTableSorter();
    }

    #endregion

    private void SetTableSorter()
    {
        if (GrdOrder.Rows.Count > 1)
        {
            GrdOrder.UseAccessibleHeader = true;
            GrdOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvSaleForceCash.Rows.Count > 1)
        {
            gvSaleForceCash.UseAccessibleHeader = true;
            gvSaleForceCash.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
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
    private void HideShowControls(bool Visible)
    {
        if (DrpAccountType.SelectedIndex == 2 || DrpAccountType.SelectedIndex == 8)
        {
            Label2.Enabled = Visible;
            DrpRoute.Enabled = Visible;
            Label4.Enabled = Visible;
            DrpCustomer.Enabled = Visible;
            Panel1.Enabled = Visible;
            Label7.Enabled = Visible;
            DrpAccountDetail.Enabled = true;
            Label10.Enabled = Visible;
            DrpDeliveryMan.Enabled = Visible;
        }
        else
        {
            Label2.Enabled = Visible;
            DrpRoute.Enabled = Visible;
            Label4.Enabled = Visible;
            DrpCustomer.Enabled = Visible;
            Panel1.Enabled = Visible;
            Label7.Enabled = Visible;
            DrpAccountDetail.Enabled = Visible;
            Label5.Enabled = Visible;
            Label3.Enabled = Visible;
            txtChequeNo.Enabled = Visible;
            txtSlipNo.Enabled = Visible;
            Label9.Enabled = Visible;
            txtRemarks.Enabled = Visible;
            Label10.Enabled = true;
            DrpDeliveryMan.Enabled = true;
        }
    }
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtRemarks.Text = "";
        txtSlipNo.Text = "";
        btnSave.Text = "Save";
    }

    private void InsertGL()
    {
        Configuration.GetAccountHead();
        #region Mapped Acount Selection
        CustomerDataController cd = new CustomerDataController();
        string CodeFileName = "";
        DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);
        if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
        {
            CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
            Configuration.GetAppMapping(CodeFileName);
        }

        long p_PrincipalAccount_ID = long.Parse(Configuration.AccountReceivable);
        DataTable dtAccount = cd.SelectAccountHeadMapping(0, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue), Constants.IntNullValue);
        if (dtAccount != null && dtAccount.Rows.Count > 0)
        {
            p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
        }
        #endregion

        #region GL Ledger

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow dr = dtVoucher.NewRow();
        dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.CashInHand);
        }
        else
        {
            // For Roots
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
        }
        dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dr["DEBIT"] = decimal.Parse(txtAmount.Text);
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);
        ///Credit Side Entry
        DataRow dr1 = dtVoucher.NewRow();
        dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
        dr1["DEBIT"] = 0;
        dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
        dr1["Principal_Id"] = 0;
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.SundryDebtors);
        }
        else
        {
            // For Roots
            dr1["ACCOUNT_HEAD_ID"] = p_PrincipalAccount_ID;
        }
        dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);

        LedgerController LController = new LedgerController();
        string MaxDocumentId = "";
        if (DrpAccountType.SelectedValue == "19" || DrpAccountType.SelectedValue == "21")// Cash Realization
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Cash_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Cash_Voucher, DateTime.Parse(txtTransactionDate.Text), Constants.Cash_Relization, Session["VoucherNo"].ToString(), "Default " + DrpAccountType.SelectedItem.Text + " Voucher" + "-" + txtRemarks.Text, DateTime.Parse(txtTransactionDate.Text), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Cash_Voucher);
            PrintReport(MaxDocumentId, Constants.Cash_Voucher);
        }
        else
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Bank_Voucher, DateTime.Parse(txtTransactionDate.Text), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Default " + DrpAccountType.SelectedItem.Text + " Voucher" + "-" + txtRemarks.Text, DateTime.Parse(txtTransactionDate.Text), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Bank_Voucher);
            PrintReport(MaxDocumentId, Constants.Bank_Voucher);
        }
        #endregion
    }
    private void InsertGL2()
    {
        Configuration.GetAccountHead();
        #region Mapped Acount Selection
        CustomerDataController cd = new CustomerDataController();

        string CodeFileName = "";
        DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

        if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
        {
            CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
            Configuration.GetAppMapping(CodeFileName);
        }


        long p_PrincipalAccount_ID = long.Parse(Configuration.AccountReceivable);
        DataTable dtAccount = cd.SelectAccountHeadMapping(0, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue), Constants.IntNullValue);
        if (dtAccount != null && dtAccount.Rows.Count > 0)
        {
            p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
        }

        #endregion

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow dr = dtVoucher.NewRow();
        dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue); //Convert.ToInt64(Configuration.StockInHand);
        }
        else
        {
            // For Roots
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
        }
        dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dr["DEBIT"] = decimal.Parse(txtAmount.Text);
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);
        ///Credit Side Entry
        DataRow dr1 = dtVoucher.NewRow();
        dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
        dr1["DEBIT"] = 0;
        dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
        dr1["Principal_Id"] = 0;
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.SundryDebtors);
        }
        else
        {
            // For Roots
            dr1["ACCOUNT_HEAD_ID"] = p_PrincipalAccount_ID;
        }
        dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;

        dtVoucher.Rows.Add(dr1);
        LedgerController LController = new LedgerController();
        string MaxDocumentId = "";

        //if (DrpAccountType.SelectedValue == "19" || DrpAccountType.SelectedValue == "21")// Cash Realization
        //{
        MaxDocumentId = LController.SelectMaxVoucherId(Constants.Cash_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Cash_Voucher, DateTime.Parse(txtTransactionDate.Text), Constants.Cash_Relization, Session["VoucherNo"].ToString(), "Default " + DrpAccountType.SelectedItem.Text + " Voucher" + "-" + DrpCustomer.SelectedItem.Text + "-" + txtRemarks.Text, DateTime.Parse(txtTransactionDate.Text), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Cash_Voucher);
        PrintReport(MaxDocumentId, Constants.Cash_Voucher);
        //}
        //else
        //{
        //    MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        //    LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Bank_Voucher, DateTime.Parse(txtTransactionDate.Text), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Default " + DrpAccountType.SelectedItem.Text + " Voucher" + "-" + txtRemarks.Text, DateTime.Parse(txtTransactionDate.Text), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Bank_Voucher);
        //    PrintReport(MaxDocumentId);
        //}

    }
    private void InsertGLOnlineTransfer()
    {
        Configuration.GetAccountHead();
        #region Mapped Acount Selection
        CustomerDataController cd = new CustomerDataController();
        string CodeFileName = "";
        DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

        if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
        {
            CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
            Configuration.GetAppMapping(CodeFileName);
        }

        long p_PrincipalAccount_ID = long.Parse(Configuration.AccountReceivable);
        DataTable dtAccount = cd.SelectAccountHeadMapping(0, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue), Constants.IntNullValue);
        if (dtAccount != null && dtAccount.Rows.Count > 0)
        {
            p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
        }

        #endregion
        LedgerController LController = new LedgerController();
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow dr = dtVoucher.NewRow();
        dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
        }
        else
        {
            // For Roots
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
        }
        dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dr["DEBIT"] = decimal.Parse(txtAmount.Text);
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);
        ///Credit Side Entry
        DataRow dr1 = dtVoucher.NewRow();
        dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
        if (CodeFileName == "QureshiAccountHead.xml")
        {
            // For Qureshi
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.SundryDebtors);
        }
        else
        {
            // For Roots
            dr1["ACCOUNT_HEAD_ID"] = p_PrincipalAccount_ID;
        }
        dr1["DEBIT"] = 0;
        dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);

        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Bank_Voucher, DateTime.Parse(txtTransactionDate.Text), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Default " + DrpAccountType.SelectedItem.Text + " Voucher" + "-" + txtRemarks.Text, DateTime.Parse(txtTransactionDate.Text), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Bank_Voucher);
        PrintReport(MaxDocumentId, Constants.Bank_Voucher);
    }

    private void PrintReport(string p_Voucher_No, int p_VoucherType)
    {
        #region print report
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), p_Voucher_No, p_VoucherType);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        string VoucherType2 = "Cash Receipt Voucher";
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

    protected void txtTransactionDate_TextChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex == 7)
        {
            LoadSaleFoceCash();
        }
        LoadGrid();
    }
}