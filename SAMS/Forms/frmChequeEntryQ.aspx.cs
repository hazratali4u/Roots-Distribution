using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;


public partial class Forms_frmChequeEntryQ : System.Web.UI.Page
{
    int Count = 0;
    string Inoices = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DrpStatus.Items.Add(new ListItem("Cheque Received", "527"));
            DrpStatus.Items.Add(new ListItem("Cheque Deposit", "528"));
            DrpStatus.Items.Add(new ListItem("Cheque Realize", "529"));
            DrpStatus.Items.Add(new ListItem("Cheque Bounce", "530"));
            DrpStatus.Items.Add(new ListItem("Cheque Cancel", "560"));
            this.LoadAccountHead();
            txtTransactionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTransactionDate.Attributes.Add("readonly", "readonly");
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadArea();
            this.LoadData();
            this.SelectCreditInvoice();
            this.LoadReceviedCheque();
            this.LoadDeliveryman();
            //btnSave.Attributes.Add("onclick", "return ValidateForm();");

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
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        CustomerDataController mController = new CustomerDataController();

        if (DrpChequeType.SelectedIndex == 1)
        {
            //DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue.ToString()));
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dtCustomer, 0, 4, true);
            DrpRoute.Enabled = true;
        }
        else
        {
            if (drpDistributor.Items.Count > 0)
            {
                LedgerController LedgerCtl = new LedgerController();
                //DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.LongNullValue, Constants.IntNullValue);
                DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, dtCredit, 0, 1, true);

                DrpRoute.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtBankName.Text = "";
        txtStartDate.Text = "";
        btnSave.Text = "Save";
        txtReceivedDate.Text = "";
        txtSlipNo.Text = "";
        txtRemarks.Text = "";

    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Cheques To Grid
    /// </summary>
    private void LoadReceviedCheque()
    {
        if (DrpStatus.SelectedValue.ToString() != Constants.Cheque_Clear.ToString())
        {
            ChequeEntryController CController = new ChequeEntryController();
            DataTable dt = CController.SelectChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(txtTransactionDate.Text), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DrpChequeType.SelectedIndex);
            this.Session.Add("dt", dt);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
        else
        {
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();
        }
    }

    /// <summary>
    /// Loads Crdit Invoices To Grid
    /// </summary>
    private void SelectCreditInvoice()
    {
        LedgerController CDC = new LedgerController();
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0 && DrpChequeType.SelectedIndex != 1)
        {   DataTable dtCredit = CDC.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue.ToString()), 0);
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
    /// Saves Cheque Realization
    /// </summary>
    private void ChequeRealization()
    {
        LedgerController LController = new LedgerController();
        LedgerController _ledgerCtl = new LedgerController();
        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        Session.Add("VoucherNo", MaxDocumentId);
        decimal realizeAmount = 0;
        int CustomerbankId = 0;
        int PaymentMode = Constants.ChequePayment;
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        string Remarks = "";
        if (!txtRemarks.Text.Equals(""))
        {
            Remarks = " - " + txtRemarks.Text;
        }
        if (DrpChequeType.SelectedIndex == 0)
        {
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    if (decimal.Parse(dr.Cells[3].Text) > OfferAmount)
                    {
                        realizeAmount += OfferAmount;

                        LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                        DateTime.Parse(txtTransactionDate.Text), "Cheque Relization" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                        txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString());

                        LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                        DateTime.Parse(txtTransactionDate.Text), "Cheque Relization" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                        txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString());

                        OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                        LController.UpdateSaleInvoice(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);
                        break;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        realizeAmount += decimal.Parse(dr.Cells[3].Text);

                        LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(dr.Cells[3].Text),
                        DateTime.Parse(txtTransactionDate.Text), "Cheque Relization" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                        txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString());

                        LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(dr.Cells[3].Text), 0,
                        DateTime.Parse(txtTransactionDate.Text), "Cheque Relization" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                        txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString());

                        OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                        LController.UpdateSaleInvoice(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), 0);
                    }
                }
            }
            if (realizeAmount < decimal.Parse(txtAmount.Text))
            {
                _ledgerCtl.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), int.Parse(Configuration.AccountReceivable)
                    , int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount, DateTime.Parse(txtTransactionDate.Text)
                    , "Cheque Relization" + " (Advance)" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId, txtChequeNo.Text, int.Parse(Session["UserId"].ToString())
                    , 0, "0", Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, PaymentMode, "Group Payment");

                _ledgerCtl.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString())
                    , int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0, DateTime.Parse(txtTransactionDate.Text)
                , "Cheque Relization" + " (Advance)" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId, txtChequeNo.Text, int.Parse(Session["UserId"].ToString())
                , 0, "0", Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, PaymentMode, "Group Payment");
            }
        }
        else
        {
            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                DateTime.Parse(txtTransactionDate.Text), "Cheque Advance" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, 20, "");

            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                DateTime.Parse(txtTransactionDate.Text), "Cheque Advance" + Remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, 20, "");
        }

    }

    /// <summary>
    /// Loads Deliverymen To Deliverman Comb
    /// </summary>
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3, true);
        }

    }

    /// <summary>
    /// Loads Account Heads To Account Combo
    /// </summary>
    private void LoadAccountHead()
    {
        SAMSCommon.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        //DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
        string ListSelectedAccoundHead = "749";//"40,747,35,461,41,749,42,750,43,752,608,908,622,917,632,924,984,61,58,461,781,60,791";
        DataTable dt = mAccountController.SelectAccountHeadForBankCash(Constants.AC_AccountHeadId, ListSelectedAccoundHead);
        clsWebFormUtil.FillDropDownList(DrpBankAccount, dt, 0, 4, true);
    }

    /// <summary>
    /// Save Or Updates a Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
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
            ChequeEntryController CController = new ChequeEntryController();
            DateTime ChequeDate;
            if (txtStartDate.Text.Length == 10)
            {
                ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));
            }
            else
            {
                ChequeDate = DateTime.Now;
            }
            if (DrpChequeType.SelectedIndex != 1)
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
            }

            if (txtAmount.Text.Equals("") || txtAmount.Text.Equals("0"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Must Enter Amount.')", true);
                txtAmount.Focus();
                return;
            }
            if (btnSave.Text == "Save")
            {
                if (DrpStatus.SelectedIndex == 0)
                {
                    HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue.ToString()), txtChequeNo.Text, txtBankName.Text, ChequeDate,
                        DateTime.Parse(txtTransactionDate.Text), Constants.DateNullValue, Constants.DateNullValue, decimal.Parse(txtAmount.Text), int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Now, DrpChequeType.SelectedIndex, txtSlipNo.Text, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));


                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]));
                        }
                    }
                }
            }
            else
            {
                if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Pending && txtReceivedDate.Text == DateTime.Parse(txtTransactionDate.Text).ToString("dd/MM/yyyy"))
                {
                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue.ToString()), txtChequeNo.Text, txtBankName.Text, ChequeDate, DateTime.Parse(txtTransactionDate.Text), Constants.DateNullValue, Constants.DateNullValue,
                       decimal.Parse(txtAmount.Text), int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()));

                    CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);

                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]));
                        }
                    }
                }
                else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Deposit)
                {
                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(txtTransactionDate.Text), Constants.DateNullValue,
                         Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()));
                }
                else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Bons || int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Cancel)
                {
                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(txtTransactionDate.Text),
                                                     Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));

                }

                else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Clear)
                {

                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(txtTransactionDate.Text),
                                                     Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));

                    CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);

                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]));
                        }
                    }
                    this.ChequeRealization();
                    this.InsertGL();
                    Session.Remove("VoucherNo");
                    this.LoadData();
                    this.SelectCreditInvoice();
                }
            }
            this.ClearAll();
            this.LoadReceviedCheque();
        }
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
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedValue);
        }
        else
        {
            // For Roots
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedValue);
        }
        dr["REMARKS"] = DrpBankAccount.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
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
        dr1["REMARKS"] = DrpBankAccount.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);

        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Bank_Voucher, DateTime.Parse(txtTransactionDate.Text), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Default " + DrpBankAccount.SelectedItem.Text + " Voucher" + "-" + txtRemarks.Text, DateTime.Parse(txtTransactionDate.Text), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Bank_Voucher);
        PrintReport(MaxDocumentId);
    }

    /// <summary>
    /// Cancels Cheque Entry
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    /// <summary>
    /// Deletes Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ChequeEntryController CController = new ChequeEntryController();
        CController.DeleteChequeEntry(long.Parse(GrdOrder.Rows[e.RowIndex].Cells[0].Text));
        this.LoadReceviedCheque();
    }

    /// <summary>
    /// Loads Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SelectCreditInvoice();
        DrpStatus.Focus();
    }

    /// <summary>
    /// Loads Routes, Customers, Credit Invoices And Cheques
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadData();
        this.SelectCreditInvoice();
        this.LoadReceviedCheque();
        //DrpPrincipal.Focus();
    }

    /// <summary>
    /// Loads Customers, Credit Invoices And Cheques
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
        this.SelectCreditInvoice();
        this.LoadReceviedCheque();
        DrpCustomer.Focus();
    }

    /// <summary>
    /// Sets Cheque Data For Edit. This Function Runs When An Existing Cheque Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ChequeEntryController Ccontroller = new ChequeEntryController();
            HFChqueProcessId.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
            DrpRoute.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[11].Text;

            this.LoadData();

            DrpCustomer.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;

            txtChequeNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
            txtBankName.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
            txtStartDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
            txtReceivedDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
            txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
            txtSlipNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
            txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
            DrpBankAccount.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[13].Text;
            try
            {
                DrpDeliveryMan.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[14].Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Sale Force not found.');", true);
            }

            btnSave.Text = "Update";
            this.SelectCreditInvoice();

            DataTable dt = Ccontroller.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 0);

            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                foreach (DataRow dbr in dt.Rows)
                {

                    if (Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]) == Convert.ToInt64(dbr["SALE_INVOICE_ID"]))
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        chRelized.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice not found for selected cheque');", true);
        }
    }

    /// <summary>
    /// Loads Cheques
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            this.LoadReceviedCheque();
        }
        DrpBankAccount.Focus();
    }

    /// <summary>
    /// Loads Customers And Cheques
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpChequeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
        this.LoadReceviedCheque();
        DrpCustomer_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// Loads Customers And Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
        this.SelectCreditInvoice();
    }

    /// <summary>
    /// Filters Cheuqe Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)this.Session["dt"];
        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 4:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                break;
        }
        GrdOrder.DataSource = dt.DefaultView;
        GrdOrder.DataBind();
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
                        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), p_Voucher_No, Constants.Bank_Voucher);
                        CrpReport.SetDataSource(ds);
                        CrpReport.Refresh();
                        string VoucherType2 = "Bank Receipt Voucher";
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
}