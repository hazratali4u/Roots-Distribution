using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit Customer Claim
/// </summary>
public partial class Forms_frmDemangedClaim : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtTransactionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTransactionDate.Attributes.Add("readonly", "readonly");
            this.LoadDistributor();
            this.LoadClaimType();
            this.LoadAccountHead();
            this.LoadPrincipal();
            this.CreatTable();
            this.LoadArea();
            this.LoadCustomer();
            this.LoadGrid();
            btnAddNew.Attributes.Add("onclick", "return ValidateForm();");
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(drpDistributor);
            lblRowId.Text = "-1";
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
    /// Loads Claim Types To RadioButtonList
    /// </summary>
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;         
    }
    
    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// Modified by: Hazrat Ali
    /// 2012-Feb-16 10:40 AM
    /// Hard coded Account Heads as per Saddruddin Sb Instructions
    /// This is valid only for FDMPL and invalid for BDN and FDM
    /// Commited old code
    /// </summary>
    private void LoadAccountHead()
    {        
        DrpAccountHead.Items.Clear();
       
        #region Commited Code
        /*
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("SECURITY DEPOSIT BOTTLE", "424"));
        }
        else if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("Adv. against Empty Bottle", "511"));
            DrpAccountHead.Items.Add(new ListItem("Sales Return (Principal Wise)", "152"));
            DrpAccountHead.Items.Add(new ListItem("Sale Discount - Claimable", "493"));
        }
        */
        AccountHeadController mAccountController = new AccountHeadController();
        //   DataTable dt = mAccountController.SelectClaimHead(int.Parse(RbdClaimType.SelectedValue));
        DataTable dt = mAccountController.SelectClaimHead(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 2, true);
       


        #endregion
    }
    
    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    
    /// <summary>
    /// Creates Datatable For Customer Claim Data
    /// </summary>
    private void CreatTable()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();  

    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        DrpRoute.Enabled = true;        
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, true);
        }
        else
        {
            DrpCustomer.Items.Clear();   
        }
    }
    
    /// <summary>
    /// Loads Customer Claims To Grid
    /// </summary>
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            LedgerController LController = new LedgerController();
            DataTable dt = LController.SelectClaimDetail(int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(RbdClaimType.SelectedValue.ToString()),
                DateTime.Parse(txtTransactionDate.Text), DateTime.Parse(txtTransactionDate.Text));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }

    /// <summary>
    /// Loads Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadCustomer();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomer(); 
    }

    /// <summary>
    /// Deletes Customer Claim
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[6].Text)),Constants.LongNullValue  , decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)),1);
        this.LoadGrid();
        
    }

    /// <summary>
    /// Saves Customer Claim
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddNew_Click(object sender, EventArgs e)
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

            LedgerController LController = new LedgerController();

            string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            this.Session.Add("VoucherNo", MaxDocumentId);
            if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
            {
                // Debit Claims
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                   DateTime.Parse(txtTransactionDate.Text), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                    null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "");
            }
            else
            {
                // Credit Claims
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                    DateTime.Parse(txtTransactionDate.Text), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue,
                     null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "");
            }
            InsertGL();
            txtAmount.Text = "";
            txtRemarks.Text = "";
            LoadGrid();
        }
    }

    /// <summary>
    /// Loads Account Heads And Claim Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();
        LoadGrid();
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


        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            if (CodeFileName == "QureshiAccountHead.xml")
            {
                // For Qureshi
                dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.SundryDebtors);
            }
            else
            {
                // For Roots
                dr["ACCOUNT_HEAD_ID"] = p_PrincipalAccount_ID;
            }
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
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
                dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.DiscountAllowed);
            }
            else
            {
                // For Roots
                dr1["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedValue;
            }
            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;

            dtVoucher.Rows.Add(dr1);

            LedgerController LController = new LedgerController();
            string MaxDocumentId = "";
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(txtTransactionDate.Text));

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(txtTransactionDate.Text), Constants.Cash_Relization, Session["VoucherNo"].ToString(), txtRemarks.Text, Convert.ToDateTime(txtTransactionDate.Text), null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, true, Constants.Journal_Voucher);
            PrintReport(MaxDocumentId);

        }
        else
        {
            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            if (CodeFileName == "QureshiAccountHead.xml")
            {
                // For Qureshi
                dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.DiscountAllowed);
            }
            else
            {
                // For Roots
                dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue);
            }
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
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
            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;

            dtVoucher.Rows.Add(dr1);

            LedgerController LController = new LedgerController();
            string MaxDocumentId = "";
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(txtTransactionDate.Text));

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(txtTransactionDate.Text), Constants.Cash_Relization, Session["VoucherNo"].ToString(), txtRemarks.Text, Convert.ToDateTime(txtTransactionDate.Text), null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, true, Constants.Journal_Voucher);
            PrintReport(MaxDocumentId);

        }
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
                        string VoucherType2 = "Customer Claim Voucher";
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

    protected void txtTransactionDate_TextChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
}
