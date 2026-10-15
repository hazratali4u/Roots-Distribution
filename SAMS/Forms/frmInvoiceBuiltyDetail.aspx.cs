using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit Freight
/// </summary>
public partial class Forms_frmInvoiceBuiltyDetail : Page
{
    readonly LedgerController _lController = new LedgerController();
    readonly DistributorController _dController = new DistributorController();
    readonly UserController _userCtl = new UserController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtBuiltyDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            
            LoadDistributor();
            LoadPrincipal();
            LoadCustomer();
            LoadTransporter();
            LoadGrid();
            //Panel1.Visible = false;
            //PrintReport("0418-8519");



        }
    }

    #region Load

    /// <summary>
    /// Loads Transporter To Transporter Combo
    /// </summary>
    private void LoadTransporter()
    {
        DrpTransporter.Items.Clear();
        //AccountHeadController mAccountController = new AccountHeadController();
        //DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 917);
        DistributorController _dController = new DistributorController();
        var dt = _dController.SelectTranspoterFreight();
        clsWebFormUtil.FillDropDownList(DrpTransporter, dt, 0, 1, true);
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {  
        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        this.Session.Add("DisDt", dt);
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
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DateTime EndDate = DateTime.ParseExact(this.txtEndDate.Text, "dd-MMM-yyyy", null);
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtEndDate.Text));
            string[] colums = new string[] { "CUSTOMER_ID", "CUSTOMER_DETAIL"};
            DataTable dtCustomer2 = dtCustomer.DefaultView.ToTable("dtCustomer2", true, colums);
            
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dtCustomer2, 0, 1, true);
            
            if (dtCustomer.Rows.Count > 0)
            {
                DrpInvoiceNo.Items.Clear();   
                DataRow[] foundRows = dtCustomer.Select("Customer_id  = " + DrpCustomer.SelectedValue);
                for (int i = 0; i < foundRows.Length; i++)
                {
                    DrpInvoiceNo.Items.Add(new ListItem(foundRows[i]["MANUAL_INVOICE_ID"].ToString(), foundRows[i]["Sale_Invoice_Id"].ToString()));

                }
                Session.Add("dtCustomer", dtCustomer);
            }
        }
        else
        {
            DrpCustomer.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Freight Data To Grid
    /// </summary>
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DataTable dt = _lController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.Builty_Exp ,
                DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }

    #endregion

    #region Sel/Index Change

    /// <summary>
    /// Loads Invoices To Invoice Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];

        DrpInvoiceNo.Items.Clear();

        DataRow[] foundRows = dtCustomer.Select("Customer_id  = " + DrpCustomer.SelectedValue);

        for (int i = 0; i < foundRows.Length; i++)
        {
            DrpInvoiceNo.Items.Add(new ListItem(foundRows[i]["MANUAL_INVOICE_ID"].ToString(), foundRows[i]["Sale_Invoice_Id"].ToString()));
        }
    }

    /// <summary>
    /// Loads Customers And Freight Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
        LoadGrid();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    #endregion

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (IsDayClosed())
        {

            _userCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (txtBuiltyCtns.Text.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Builty Carton is required.');", true);
                txtBuiltyCtns.Focus();
                return;
            }
            if (txtAmount.Text.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Builty Amount is required.');", true);
                txtAmount.Focus();
                return;
            }
            
            string Voucher_No;
            bool is_voucher_hit = false;
            DataTable DisDt = (DataTable)this.Session["DisDt"];
            if (DisDt != null)
            {
                if (DisDt.Rows.Count > 0)
                {
                    DataRow[] foundRows = DisDt.Select("DISTRIBUTOR_ID  = '" + drpDistributor.SelectedValue.ToString() + "'");
                    if (foundRows.Length > 0)
                    {
                        if (foundRows[0]["SUBZONE_ID"].ToString() == "5")// Head Office 
                        {
                            is_voucher_hit = true;
                        }
                    }
                }
            }

            if (btnAddNew.Text == "Save")
            {
                DataTable dt = _lController.SelectBankCashTransction(
                    int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.Builty_Exp,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()),
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                DataRow[] foundRows = dt.Select("Document_no  = " + DrpInvoiceNo.SelectedValue);
                DataRow[] foundRows2 = dt.Select("BUILTY_NO  = '" + txtRemarks.Text.Trim() + "' AND payee_name = '" + DrpTransporter.SelectedValue.ToString() + "'");
                if (foundRows.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg","alert('This Invoice No already exist,Kindly select different Invoice No');",true);
                    return;
                }

                if (foundRows2.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Builty No for this transporter already exist!');", true);
                    return;
                }

                DateTime builtyDate = new DateTime();
                if (txtBuiltyDate.Text.Length == 11)
                {
                    builtyDate = DateTime.Parse(txtBuiltyDate.Text);
                    //builtyDate = DateTime.Parse(ConvertDate.British_To_American(txtBuiltyDate.Text));

                }
                Configuration.GetAccountHead();
                string maxDocumentId = _lController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher,int.Parse(drpDistributor.SelectedValue));
                _lController.InsertFreight(Constants.Journal_Voucher, long.Parse(maxDocumentId),
                                                      long.Parse(Configuration.BuiltyExp),
                                                      int.Parse(drpDistributor.SelectedValue),
                                                      decimal.Parse(txtAmount.Text), 0,
                                                      //builtyDate, //
                                                      DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                      txtRemarks.Text, builtyDate,
                                                      int.Parse(DrpCustomer.SelectedValue),
                                                      Constants.IntNullValue,
                                                      txtBuiltyCtns.Text, int.Parse(Session["UserId"].ToString()),
                                                      int.Parse(DrpInvoiceNo.SelectedValue),
                                                      DrpInvoiceNo.SelectedItem.Text, Constants.Document_Invoice, null,
                                                      Constants.DateNullValue, Constants.Builty_Exp,
                                                      DrpTransporter.SelectedValue, DrpTransporter.SelectedItem.Text,DrpCustomer.SelectedItem.Text, is_voucher_hit, out Voucher_No);
                PrintReport(Voucher_No);
                Clear();
                LoadCustomer();
                LoadGrid();
            }
            else if(btnAddNew .Text =="Update")
            {
                DataControl dc = new DataControl();
                _lController.DeleteWareHouseLedgerForFreight(int.Parse(drpDistributor.SelectedValue), int.Parse(dc.chkNull_0(hfDocument_type.Value)),
                  hfVoucherNo.Value.ToString(), int.Parse(hfdocument_No.Value), decimal.Parse(dc.chkNull_0(hfTotalAmount.Value)),2);
                DateTime builtyDate = new DateTime();
                if (txtBuiltyDate.Text.Length == 10)
                {
                    builtyDate = DateTime.Parse(ConvertDate.British_To_American(txtBuiltyDate.Text));
                }
                Configuration.GetAccountHead();
                string maxDocumentId = _lController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher,int.Parse(drpDistributor.SelectedValue));
                _lController.InsertFreight(Constants.Journal_Voucher, long.Parse(maxDocumentId),
                                                      long.Parse(Configuration.BuiltyExp),
                                                      int.Parse(drpDistributor.SelectedValue),
                                                      decimal.Parse(txtAmount.Text), 0,
                                                      //builtyDate, //
                                                      DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                      txtRemarks.Text, builtyDate,
                                                      int.Parse(DrpCustomer.SelectedValue),
                                                      Constants.IntNullValue,
                                                      txtBuiltyCtns.Text, int.Parse(Session["UserId"].ToString()),
                                                      int.Parse(DrpInvoiceNo.SelectedValue),
                                                      DrpInvoiceNo.SelectedItem.Text, Constants.Document_Invoice, null,
                                                      Constants.DateNullValue, Constants.Builty_Exp,
                                                      DrpTransporter.SelectedValue, DrpTransporter.SelectedItem.Text, DrpCustomer.SelectedItem.Text, is_voucher_hit, out Voucher_No);
                PrintReport(Voucher_No);
                Clear();
                LoadCustomer();
                LoadGrid();
            }
        }
    }
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataControl dc = new DataControl();
        _lController.DeleteWareHouseLedgerForFreight(int.Parse(drpDistributor.SelectedValue), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        GrdOrder.Rows[e.RowIndex].Cells[9].Text, int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[5].Text)), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)),2);
        LoadCustomer();
        LoadGrid();

    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        drpDistributor.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
        drpDistributor.Enabled = false;
        drpDistributor_SelectedIndexChanged(null, null);
        DrpTransporter.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[11].Text;
        DrpCustomer.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        DrpCustomer.Enabled = false;
        DrpCustomer_SelectedIndexChanged(null, null);
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
        hfdocument_No.Value = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
        hfDocument_type.Value = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
        hfVoucherNo.Value = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
        hfTotalAmount .Value= GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        DrpInvoiceNo.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[12].Text;
        txtBuiltyCtns.Text = GrdOrder.Rows[e.NewEditIndex].Cells[13].Text;
        DrpInvoiceNo.Enabled = false;
        btnAddNew.Text = "Update";
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
    private void Clear()
    {
        //LoadGrid();
        txtAmount.Text = "";
        txtRemarks.Text = "";
        txtBuiltyCtns.Text = "";
        DateTime pOrderDate = DateTime.Parse(Session["CurrentWorkDate"].ToString());
        txtBuiltyDate.Text = pOrderDate.ToString("dd-MMM-yyyy");
        btnAddNew.Text = "Save";
        DrpInvoiceNo.Enabled = true;
        drpDistributor.Enabled = true;
        DrpCustomer.Enabled = true;
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


   
    protected void btnGetData_Click(object sender, EventArgs e)
    {
        LoadCustomer();
        LoadGrid();
    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        LoadCustomer();
        //LoadGrid();
    }
}