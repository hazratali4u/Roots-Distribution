using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form To Add, Edit Vochers
/// </summary>
public partial class Forms_frmVoucherEntry : System.Web.UI.Page
{
    DataTable dtVoucher;
    private static int RowId = -1;
    readonly BankController _bnkController = new BankController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                this.LoadDistributor();
                this.LoadAccountDetail();
                this.LoadPrincipal();
                this.CreatTable();
                this.LoadAccountHead();
                this.HideShowControls();
                txtChequeNo.Visible = true;
                drpCheque.Visible = false;
                //LoadCheques(2);
                DrpVoucherType_SelectedIndexChanged(null, null);
                DrpVoucherSubType_SelectedIndexChanged(null, null);
                DateTime Date = DateTime.Now;
                txtVDate.Text = Date.ToString("dd/MM/yyyy");
                //txtVoucherDate.Text = DateTime.Now.ToString("dd/mm/yyyy");
                btnSave.Attributes.Add("onclick", "return ValidateForm();");
                RowId = -1;
                Session["MANUAL_VOUCHER"] = "0";
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
            }
         
        }
    }

  
    private void LoadAccountHead()
    {
        try
        {
            SAMSCommon.Classes.Configuration.GetAccountHead();
            if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == 14)
            {
                AccountHeadController mAccountController = new AccountHeadController();
                // DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.CashDefaultType));
                DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 750);

                clsWebFormUtil.FillDropDownList(drpBanks, dt, 0, 4, true);
            }
            else if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == 15)
            {
                AccountHeadController mAccountController = new AccountHeadController();
                // DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
                DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 749);

                clsWebFormUtil.FillDropDownList(drpBanks, dt, 0, 4, true);
                // drpBanks.SelectedValue = "599";
            }
            else
            {
                drpBanks.Items.Clear();
                drpBanks.Items.Add(new ListItem("N/A", Constants.IntNullValue.ToString()));
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    
    private void CreatTable()
    {
        dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("InvoiceNo", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
    }
    
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("General Entry","0"));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    
    private void LoadAccountDetail()
    {
        try
        {
            //Load Voucher Type by User Rights 

            LedgerController mLedgerController = new LedgerController();
            DataTable dt = mLedgerController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpVoucherType, dt, 0, 1, true);

            // Load Account Head 
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
            clsWebFormUtil.FillDropDownList(ddlAccounts, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_HEAD", true);
            this.Session.Add("dtHead", dtHead);
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    
    private void ClearAll()
    {
        txtTotalCredit.Text = "0";
        txtTotalDebit.Text = "0";
        txtRemarks.Text = "";
        txtChequeDate.Text = "";
        txtChequeNo.Text = "";
        txtSlipNo.Text = "";
        txtDueDate.Text = "";
        txtChequeDate.Text = "";
        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        GrdOrder.DataSource = null;
        GrdOrder.DataBind();
        this.Session.Remove("dtVoucher");
        this.CreatTable();
        txtpayeesName.Text = "";
        drpDistributor.Enabled = true;
        DrpVoucherType.Enabled = true;
        DrpVoucherSubType.Enabled = true;
        drpCheque.Enabled = true;
        //Session["MANUAL_VOUCHER"] = null;
        //this.LoadDistributor();
        //this.LoadAccountDetail();
        //this.LoadPrincipal();
        //this.CreatTable();
        //this.LoadAccountHead();
        //this.HideShowControls();
        //txtChequeNo.Visible = true;
        //drpCheque.Visible = false;
        ////LoadCheques(2);
        //DrpVoucherSubType_SelectedIndexChanged(null, null);
        //txtVoucherDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //btnSave.Attributes.Add("onclick", "return ValidateForm();");
        //RowId = -1;
        //Session["MANUAL_VOUCHER"] = "0";
    }
    
    private void PrintVoucher(string VoucherNo)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        int VoucherType = Constants.IntNullValue;
        string VoucherType2 = "";
        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            VoucherType2 = DrpVoucherType.SelectedItem.Text;
        }
        else
        {
            VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            VoucherType2 = DrpVoucherSubType.SelectedItem.Text;
        }
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), VoucherNo, VoucherType);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("Voucher_Type", VoucherType2);
        CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    public void LoadCheques(int Type)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Convert.ToInt32(drpBanks.SelectedItem.Value), null, null, null, true, Constants.DateNullValue, Type);
            if (dt.Rows.Count > 0 && dt != null)
            {
                clsWebFormUtil.FillDropDownList(this.drpCheque, dt, 0, 1, true);
                drpCheque.SelectedIndex = 0;
            }
            else
            {
                drpCheque.Items.Add(new ListItem("", "0"));
                drpCheque.Items.Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please add cheque book first for " + drpBanks.SelectedItem.Text + "')", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
            ExceptionPublisher.PublishException(ex);
        }
    }
    
    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.ClearAll();
            this.LoadAccountHead();
            this.HideShowControls();
            //LoadCheques(2);
            Session["MANUAL_VOUCHER"] = null;
            if (DrpVoucherType.SelectedIndex == 2)
            {
                lblChequeNo.Text = "Invoice No";
                lblChequedate.Text = "Invoice Date";
                Session["MANUAL_VOUCHER"] = "1";
                drpCheque.Items.Clear();
            }
            else if (DrpVoucherType.SelectedIndex == 0)
            {
                Session["MANUAL_VOUCHER"] = "0";
                if (DrpPaymentMode.SelectedValue == "30")
                {
                    txtChequeNo.Visible = true;
                    drpCheque.Visible = false;
                }
                else if (DrpPaymentMode.SelectedValue == "18")
                {
                    lblChequeNo.Text = "Invoice No";
                    lblChequedate.Text = "Invoice Date";
                    txtChequeNo.Visible = true;
                    drpCheque.Visible = false;
                    //LoadCheques(2);
                }
                else
                {
                    lblChequeNo.Text = "Invoice No";
                    lblChequedate.Text = "Invoice Date";
                    txtChequeNo.Visible = true;
                    drpCheque.Visible = false;
                }

            }
            else
            {
                Session["MANUAL_VOUCHER"] = "0";
                if (DrpPaymentMode.SelectedValue == "30")
                {
                    txtChequeNo.Visible = true;
                    drpCheque.Visible = false;
                }
                else if (DrpPaymentMode.SelectedValue == "18")
                {
                    if (DrpVoucherType.SelectedValue == "15")
                    {
                        if (DrpVoucherSubType.SelectedValue == "15")
                        {
                            lblChequeNo.Text = "Cheque No";
                            lblChequedate.Text = "Cheque Date";
                            txtChequeNo.Visible = true;
                            drpCheque.Visible = false;
                        }
                        else if (DrpVoucherSubType.SelectedValue == "17")
                        {
                            txtChequeNo.Visible = false;
                            drpCheque.Visible = true;
                            lblChequeNo.Text = "Cheque No";
                            lblChequedate.Text = "Cheque Date";
                            LoadCheques(2);
                        }
                    }
                }
                else
                {
                    lblChequeNo.Text = "Cheque No";
                    lblChequedate.Text = "Cheque Date";
                    txtChequeNo.Visible = true;
                    drpCheque.Visible = false;
                    //drpCheque.Items.Add(new ListItem("N/A", "0"));
                    //drpCheque.SelectedValue = "0";
                }

            }
            //if (DrpVoucherType.SelectedIndex != 2)
            //{
                DrpVoucherSubType_SelectedIndexChanged(null, null);
            //}
        }
        catch(Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;

        DataControl dc = new DataControl();
        DataTable dtHead = (DataTable)this.Session["dtHead"];
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        DataRow[] foundRows = dtHead.Select("ACCOUNT_HEAD_ID  = '" + ddlAccounts.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            if (RowId >= 0)
            {
                DataRow dr = dtVoucher.Rows[RowId];
                dr["ACCOUNT_HEAD_ID"] = foundRows[0]["ACCOUNT_HEAD_ID"];
                dr["ACCOUNT_CODE"] = foundRows[0]["ACCOUNT_CODE"];
                dr["ACCOUNT_NAME"] = foundRows[0]["ACCOUNT_NAME"];
                dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
                dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
                dr["Principal"] = DrpPrincipal.SelectedItem.Text;
                dr["Principal_id"] = DrpPrincipal.SelectedItem.Value;
                dr["REMARKS"] = txtAccountDes.Text;

            }
            else
            {
                DataRow dr = dtVoucher.NewRow();
                dr["Ledger_ID"] = Constants.LongNullValue.ToString();
                dr["ACCOUNT_HEAD_ID"] = foundRows[0]["ACCOUNT_HEAD_ID"];
                dr["ACCOUNT_CODE"] = foundRows[0]["ACCOUNT_CODE"];
                dr["ACCOUNT_NAME"] = foundRows[0]["ACCOUNT_NAME"];
                dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
                dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
                dr["REMARKS"] = txtAccountDes.Text;
                dr["Principal"] = DrpPrincipal.SelectedItem.Text;
                dr["Principal_id"] = DrpPrincipal.SelectedItem.Value;
                dtVoucher.Rows.Add(dr);
            }
        }

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();

        #endregion

        #region Clear Txtbox

        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        RowId = -1;

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        if (GrdOrder.Rows.Count > 0)
        {
            DrpVoucherSubType.Enabled = false;
            DrpVoucherType.Enabled = false;
            drpDistributor.Enabled = false;
        }
        this.Session.Add("dtVoucher", dtVoucher);
        ScriptManager.GetCurrent(Page).SetFocus(ddlAccounts);        
    }

    private void HideShowControls()
    {
        if (DrpVoucherType.SelectedValue == "14")
        {
            

            DrpVoucherSubType.Items.Clear();
            DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));
            lblSubType.Visible = true;
            DrpVoucherSubType.Visible = true;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
        else if (DrpVoucherType.SelectedValue == "16")
        {
            DrpVoucherSubType.Items.Clear();
            lblSubType.Visible = false;
            DrpVoucherSubType.Visible = false;
            drpCheque.Visible = false;

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = true;
            txtChequeNo.Visible = true;
        }
        else
        {
            


            DrpVoucherSubType.Items.Clear();
            DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));

            lblSubType.Visible = true;
            DrpVoucherSubType.Visible = true;
            drpCheque.Visible = true;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
            txtChequeNo.Visible = false;
        }
    }
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        txtTotalDebit.Text = TotalDebit.ToString();
        txtTotalCredit.Text = TotalCredit.ToString();

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        RowId = -1;
    }

    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            RowId = e.NewEditIndex;
            ddlAccounts.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
            // txtAccountName.Text = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
            txtAccountDes.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
            DrpPrincipal.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;
            txtDebitAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
            txtCreditAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        LedgerController mLController = new LedgerController();
        DataControl dc = new DataControl();
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        DataTable dtHead = (DataTable)this.Session["dtHead"];

            decimal TotalDebit = 0;
            decimal TotalCredit = 0;

            #region Set Total

            foreach (DataRow dr in dtVoucher.Rows)
            {
                TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
                TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
            }
            #endregion

            #region In Case of Bank & Cash Voucher

            if (drpBanks.SelectedItem.Text != "N/A")
            {
                decimal differnceAmt = TotalDebit - TotalCredit;

                if (differnceAmt > 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    DataRow[] foundSubRows = dtHead.Select("ACCOUNT_HEAD_ID  = " + drpBanks.SelectedValue.ToString());
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = foundSubRows[0]["ACCOUNT_HEAD_ID"];
                    dr1["ACCOUNT_CODE"] = foundSubRows[0]["ACCOUNT_CODE"];
                    dr1["ACCOUNT_NAME"] = foundSubRows[0]["ACCOUNT_NAME"];
                    dr1["REMARKS"] = txtAccountDes.Text;
                    dr1["CREDIT"] = differnceAmt;
                    dr1["DEBIT"] = 0;
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);

                }
                else if (differnceAmt < 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    DataRow[] foundSubRows = dtHead.Select("ACCOUNT_HEAD_ID  = " + drpBanks.SelectedValue.ToString());
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = foundSubRows[0]["ACCOUNT_HEAD_ID"];
                    dr1["ACCOUNT_CODE"] = foundSubRows[0]["ACCOUNT_CODE"];
                    dr1["ACCOUNT_NAME"] = foundSubRows[0]["ACCOUNT_NAME"];
                    dr1["REMARKS"] = txtAccountDes.Text;
                    dr1["CREDIT"] = 0;
                    dr1["DEBIT"] = -(differnceAmt);
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);
                }                                                   
            }
        #endregion

        int VoucherType = Constants.IntNullValue;

        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
        }
        else
        {
            VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
        }

        if (DrpVoucherType.SelectedValue == "15")
        {
            if (DrpVoucherSubType.SelectedValue == "15")
            {
                if (txtChequeNo.Text.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Cheque Number');", true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtChequeNo);
                    return;
                }
                if (txtChequeDate.Text.Length < 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Cheque Date');", true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtChequeDate);
                    return;
                }
                if (txtDueDate.Text.Length < 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Due Date');", true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtDueDate);
                    return;
                }
            }
            else if(DrpVoucherSubType.SelectedValue == "17")
            {
                if (drpCheque.SelectedValue.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Select Cheque Number');", true);
                    ScriptManager.GetCurrent(Page).SetFocus(drpCheque);
                    return;
                }
                if (txtChequeDate.Text.Length < 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Cheque Date');", true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtChequeDate);
                    return;
                }
                if (txtDueDate.Text.Length < 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Due Date');", true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtDueDate);
                    return;
                }
            }
        }
        DateTime ChequeDate;
        DateTime  Voucherdate;
        DateTime DueDate;

        if (txtVDate.Text.Length < 10)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Voucher Date');", true);
            return;
        }
        else
        {
            //DateTime VDate = DateTime.Parse(txtVDate.Text + " 00:00:00");
            //string vdate = VDate.ToString("dd/MM/yyyy");
            Voucherdate = DateTime.Parse(ConvertDate.British_To_American(txtVDate.Text));
        }

        if (txtChequeDate.Text.Length == 10)
        {
            //DateTime ChqDate = DateTime.Parse(txtChequeDate.Text);
            //string chqdate = ChqDate.ToString("dd/MM/yyyy");
            ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtChequeDate.Text));

        }
        else
        {
            ChequeDate = Constants.DateNullValue;
        }
        if (txtDueDate.Text.Length == 10)
        {
            //DateTime DDate = DateTime.Parse(txtDueDate.Text);
            //string ddate = DDate.ToString("dd/MM/yyyy");
            DueDate  = DateTime.Parse(ConvertDate.British_To_American(txtDueDate.Text));

        }
        else
        {
            DueDate = Constants.DateNullValue;
        }

        dtVoucher = (DataTable)this.Session["dtVoucher"];
        string MaxDocumentId = mLController.SelectMaxVoucherId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()),Voucherdate);
        string cheque = "";
        if (DrpVoucherSubType.SelectedValue == "17")
        {
            cheque = drpCheque.SelectedItem.Text;
        }
        else if (DrpVoucherSubType.SelectedValue == "15")
        {
            cheque = txtChequeNo.Text;
        }
        else
        {
            cheque = txtChequeNo.Text;
        }
        bool IsSaveVoucher = mLController.Add_Voucher(
            int.Parse(drpDistributor.SelectedValue.ToString())
            , int.Parse(DrpPrincipal.SelectedValue.ToString())
            , MaxDocumentId
            , VoucherType
            , Voucherdate
            , int.Parse(DrpPaymentMode.SelectedValue.ToString())
            , txtpayeesName.Text
            , txtRemarks.Text
            , ChequeDate
            , cheque
            , dtVoucher
            , int.Parse(this.Session["UserId"].ToString())
            , txtSlipNo.Text
            , DueDate
            , int.Parse(Session["MANUAL_VOUCHER"].ToString())
            );
        
        if (IsSaveVoucher == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);
            this.ClearAll();
            drpBanks_SelectedIndexChanged(null, null);
            this.PrintVoucher(MaxDocumentId); 

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occur Wrong Entry');", true);
        }
    }

    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = txtTotalDebit.Text;
            e.Row.Cells[7].Text = txtTotalCredit.Text;
        }
    }

    protected void DrpVoucherSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpPaymentMode.Items.Clear();
        if (DrpVoucherSubType.SelectedIndex == 0)
        {
            if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "16")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }
            else if (DrpVoucherType.SelectedValue == "15")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }
            if (DrpVoucherType.SelectedValue == "16")
            {
                txtDebitAmount.Enabled = true;
                txtCreditAmount.Enabled = true;
            }
            else
            {
                txtDebitAmount.Enabled = false;
                txtCreditAmount.Enabled = true;
            }

        }
        else if (DrpVoucherSubType.SelectedIndex == 1)
        {
            if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "16")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }
            else if (DrpVoucherType.SelectedValue == "15")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }
            if (DrpVoucherType.SelectedValue == "16")
            {
                txtDebitAmount.Enabled = true;
                txtCreditAmount.Enabled = true;
            }
            else
            {
                txtDebitAmount.Enabled = true;
                txtCreditAmount.Enabled = false;
            }

        }
        else
        {
            if (DrpVoucherType.SelectedValue == "16")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
                txtDebitAmount.Enabled = true;
                txtCreditAmount.Enabled = true;
            }
        }

        if (DrpVoucherType.SelectedValue != "16")
        {
            if (DrpVoucherSubType.SelectedValue == "15")
            {
                txtChequeNo.Visible = true;
                drpCheque.Visible = false;
            }
            else if (DrpVoucherSubType.SelectedValue == "17")
            {
                txtChequeNo.Visible = false;
                drpCheque.Visible = true;
                LoadCheques(2);
            }
        }
    }

    protected void drpBanks_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpPaymentMode.SelectedValue == "30")
            {
                txtChequeNo.Visible = true;
                drpCheque.Visible = false;
            }
            else if (DrpPaymentMode.SelectedValue == "18")
            {
                if (DrpVoucherSubType.SelectedValue == "15")
                {
                    txtChequeNo.Visible = true;
                    drpCheque.Visible = false;
                }
                else if (DrpVoucherSubType.SelectedValue == "17")
                {
                    txtChequeNo.Visible = false;
                    drpCheque.Visible = true;
                    LoadCheques(2);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }

    protected void DrpPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int d = Convert.ToInt32(DrpVoucherType.SelectedValue);
            if (DrpPaymentMode.SelectedValue == "30")
            {
                txtChequeNo.Visible = true;
                drpCheque.Visible = false;
            }
            else if (DrpPaymentMode.SelectedValue == "18")
            {
                if (DrpVoucherType.SelectedValue == "15")
                {
                    if (DrpVoucherSubType.SelectedValue == "15")
                    {
                        txtChequeNo.Visible = true;
                        drpCheque.Visible = false;
                    }
                    else if (DrpVoucherSubType.SelectedValue == "17")
                    {
                        txtChequeNo.Visible = false;
                        drpCheque.Visible = true;
                        LoadCheques(2);
                    }
                }
            }
            else
            {
                txtChequeNo.Visible = true;
                drpCheque.Visible = false;
            }

        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
}