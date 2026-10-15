using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form To Add Opening Credit
/// </summary>
public partial class Forms_frmOpeingCredit : System.Web.UI.Page
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
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromdate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            //btnSave.Attributes.Add("onClick", "return false;");
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadArea();
            this.LoadData();
            this.LoadDeliveryman();
            this.LoadOpeningCredit();
            DrpCreditType_SelectedIndexChanged(null, null);


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
    /// Loads Markets To Market Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadData()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillListBox(this.ListCustomer, dt, "CUSTOMER_DETAIL", "CUSTOMER_DETAIL", true);
            this.Session.Add("dt", dt);
        }
    }
    
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtOutletCode.Text = "";
        txtAmount.Text = "";
        txtOutletName.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Save";
        txtOutletCode.Focus();
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
    /// Loads Deliverymen To Deliveryman Combo
    /// </summary>
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3, true);
        }
        else
        {
            DrpDeliveryMan.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Opening Credits Detail To Grid
    /// </summary>
    private void LoadOpeningCredit()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController cdc = new CustomerDataController();
            DataTable dt = cdc.SelectOpeningCredit2(int.Parse(drpDistributor.SelectedValue.ToString()),DateTime.Parse(txtFromdate.Text));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }
        
    /// <summary>
    /// Loads Markets To Market Combo, Customers To Customer ListBox, Deliveryment To Deliveryman Combo And Opening Credits Detail To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadData();
        LoadDeliveryman();
        LoadOpeningCredit();
        btnSave.Text = "Save";
        drpDistributor.Focus();
    }

    /// <summary>
    /// Loads Customers To Customer ListBox, Deliveryment To Deliveryman Combo And Opening Credits Detail To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
       LoadData();
       LoadDeliveryman();
       ListCustomer.Focus();
    }

    /// <summary>
    /// Loads Customers To Customer ListBox And Opening Credits Detail To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
    }
    
    /// <summary>
    /// Checks Invoice No in System
    /// </summary>
    /// <param name="p_Type">Type</param>
    /// <returns>True</returns>
    private bool IsBillBookNoExist(int p_Type)
    {
        bool flag = false;
        if (txtChequeNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(Convert.ToInt32(drpDistributor.SelectedValue), txtChequeNo.Text, p_Type);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
        }

        return flag;
    }

    /// <summary>
    /// Sets Opening Credit Data For Edit. This Function Runs When An Existing Opening Credit Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DrpCreditType.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;
        DrpCreditType_SelectedIndexChanged(null, null);
        //DrpPrincipal.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;
        txtFromdate.Text = Convert.ToDateTime(GrdOrder.Rows[e.NewEditIndex].Cells[9].Text).ToString("dd-MMM-yyyy");
        txtOutletCode.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        txtOutletName.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtChequeNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
        btnSave.Text = "Update";

        DrpRoute.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[11].Text;
        hfLegendID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;
        hfPrincipalID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;
        hfCustomerID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        hfSaleInvoiceID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
        this.LoadData();
        this.LoadDeliveryman();
        DrpDeliveryMan.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[12].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[13].Text;
    }

    /// <summary>
    /// Deletes Opening Credit Record
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hfLegendID.Value = GrdOrder.Rows[e.RowIndex ].Cells[7].Text;
        hfPrincipalID.Value = GrdOrder.Rows[e.RowIndex].Cells[1].Text;
        hfCustomerID.Value = GrdOrder.Rows[e.RowIndex].Cells[0].Text;
        hfSaleInvoiceID.Value = GrdOrder.Rows[e.RowIndex].Cells[10].Text;

        LedgerController LedgerCtl = new LedgerController();
        if (LedgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCreditType.SelectedValue), Convert.ToInt32(hfLegendID.Value), Convert.ToDateTime(txtFromdate.Text), Convert.ToInt32(hfCustomerID.Value), Convert.ToInt64(hfSaleInvoiceID.Value), Convert.ToInt32(this.Session["UserId"])))
        {
            this.ClearAll();
            this.LoadOpeningCredit();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured. Opening Credit not deleted.');", true);
        }

    }

    /// <summary>
    /// Saves/Updates Opening Credit Record
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LedgerController LedgerCtl = new LedgerController();
        CustomerDataController CDC = new CustomerDataController();
        DataControl DC = new DataControl();
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

            if (btnSave.Text == "Update")
            {
                LedgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCreditType.SelectedValue), Convert.ToInt32(hfLegendID.Value), Convert.ToDateTime(txtFromdate.Text), Convert.ToInt32(hfCustomerID.Value), Convert.ToInt64(hfSaleInvoiceID.Value), Convert.ToInt32(this.Session["UserId"]));
            }

            if (!IsBillBookNoExist(1))
            {
                DataTable dtCustomer = (DataTable)this.Session["dt"];
                DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + txtOutletCode.Text.Trim() + "'");
                if (foundRows.Length > 0)
                {
                    DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), Constants.Credit_Order_Id);
                    decimal NetAmount = decimal.Parse(DC.chkNull_0(txtAmount.Text));

                    //if (decimal.Parse(DC.chkNull_0(dt.Rows[0][0].ToString())) <= NetAmount)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Credit Limit/Advance Amount is " + DC.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                    //    return;
                    //}

                    LedgerCtl.OpeningCredit(int.Parse(drpDistributor.SelectedValue.ToString()), txtChequeNo.Text.ToUpper(), int.Parse(foundRows[0]["TOWN_ID"].ToString()), int.Parse(DrpRoute.SelectedValue.ToString()),
                    DateTime.Parse(txtFromdate.Text), Constants.IntNullValue, long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()), long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()),
                    -1, int.Parse(DrpDeliveryMan.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpCreditType.SelectedValue.ToString()), int.Parse(RbCreditType.SelectedValue.ToString()),txtRemarks.Text);
                    this.ClearAll();
                    this.LoadOpeningCredit();
                    LoadDeliveryman();
                }
                ScriptManager.GetCurrent(Page).SetFocus(txtOutletCode);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Invoice No already exist,Kindly enter different Invoice No');", true);
            }
        }
    }
    
    /// <summary>
    /// Cancels Opening Credit Entry
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
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


        //long p_PrincipalAccount_ID = long.Parse(Configuration.AccountReceivable);
        //DataTable dtAccount = cd.SelectAccountHeadMapping(0, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue), Constants.IntNullValue);
        //if (dtAccount != null && dtAccount.Rows.Count > 0)
        //{
        //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
        //}

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
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.SundryDebtors);  //Convert.ToInt64(Configuration.StockInHand);
        }
        else
        {
            // For Roots
            //dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
        }
        dr["REMARKS"] = "Credit Transfer In Voucher" + "-" + txtRemarks.Text; // DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
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
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(1211);
        }
        else
        {
            // For Roots
            //dr1["ACCOUNT_HEAD_ID"] = p_PrincipalAccount_ID;
        }
        dr1["REMARKS"] = "Credit Transfer In Voucher" + "-" +txtRemarks.Text; //DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;

        dtVoucher.Rows.Add(dr1);

        LedgerController LController = new LedgerController();
        string MaxDocumentId = "";


        //if (DrpAccountType.SelectedValue == "19" || DrpAccountType.SelectedValue == "21")// Cash Realization
        //{
        MaxDocumentId = LController.SelectMaxVoucherId(Constants.Cash_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Cash_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Cash_Relization, Session["TVoucherNo"].ToString(), "Default Credit Transfer In Voucher"+ "-" + txtRemarks.Text, Convert.ToDateTime(Session["CurrentWorkDate"]), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), "", Constants.DateNullValue, true, Constants.Cash_Voucher);
        //PrintReport(MaxDocumentId);
        //}
        //else
        //{
        //    MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        //    LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, MaxDocumentId, Constants.Bank_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Default " + DrpAccountType.SelectedItem.Text + " Voucher" + "-" + txtRemarks.Text, Convert.ToDateTime(Session["CurrentWorkDate"]), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true, Constants.Bank_Voucher);
        //    PrintReport(MaxDocumentId);
        //}

    }
    protected void DrpCreditType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DrpCreditType.SelectedValue=="25")
        {
            lblOpeningType.Visible = true;
            RbCreditType.Visible = true;
        }
        else
        {

            lblOpeningType.Visible = false;
            RbCreditType.Visible = false;
        }
    }
}
