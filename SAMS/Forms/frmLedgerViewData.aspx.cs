using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using System.Collections;

/// <summary>
/// Form To Edit Vouchers
/// </summary>
public partial class Forms_frmLedgerViewData : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.VoucherType();
            this.LoadUser();
            btnView.Attributes.Add("onclick", "return ValidateForm();");
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            DrpVoucherType_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //this.DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("GENERAL ENTRY", "0"));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2,true);
    }
    
    /// <summary>
    /// Loads Voucher Types To Voucher Type Combo
    /// </summary>
    private void VoucherType()
    {
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpVoucherType, dt, 0, 1, true);
    }
    
    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        Distributor_UserController mController = new Distributor_UserController();
        DataTable dt = mController.SelectGLUser(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()));
        DrpUser.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));        
        clsWebFormUtil.FillDropDownList(this.DrpUser, dt, 0, 1);
    }

    /// <summary>
    /// Loads UnPosted Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnView_Click(object sender, EventArgs e)
    {
        int VoucherType = Constants.IntNullValue;
        int VoucherType2 = Constants.IntNullValue;
        int payment_mode = Constants.IntNullValue;

        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            payment_mode = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
        }
        else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
        {
            if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
            {
                VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }
        }
        LedgerController mController = new LedgerController();
        DataTable dt = mController.SelectUnPostVoucherNo(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpUser.SelectedValue.ToString()), bool.Parse(RbdList.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 2, VoucherType2, payment_mode);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();  
    }

    /// <summary>
    /// Checks/UnChecks All Vouchers in Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbSelect_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow dr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
            ChbSelect.Checked = true;
        }
    }

    /// <summary>
    /// Shows Voucher in Crystal Report For Print
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void GrdLedger_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        int VoucherType = Constants.IntNullValue;
        int VoucherType2 = Constants.IntNullValue;
        int payment_mode = Constants.IntNullValue;

        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            payment_mode = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
        }
        else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
        {
            if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
            {
                VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }
        }

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), GrdLedger.Rows[e.NewEditIndex].Cells[1].Text, VoucherType);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        string VoucherType3 = "";
        if (DrpVoucherType.SelectedValue == "16")
        {

            VoucherType3 = DrpVoucherType.SelectedItem.Text;
        }
        else
        {
            VoucherType3 = DrpVoucherSubType.SelectedItem.Text;
        }
        CrpReport.SetParameterValue("Voucher_Type", VoucherType3);
        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0); 
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

                
    }

    /// <summary>
    /// Cancels Voucher Posting
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LedgerController lController = new LedgerController();
        foreach (GridViewRow dr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
                lController.PostSelectVoucher(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[1].Text, int.Parse(dr.Cells[4].Text), 1, DateTime.Parse(dr.Cells[5].Text));
            }
        }
        DataTable dt = lController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(DrpPrincipal.SelectedValue.ToString()),bool.Parse(RbdList.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),1);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();
    }

    /// <summary>
    /// Enables/Disables Cancel Voucher Button
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RbdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbdList.SelectedIndex == 1)
        {
            btnDelete.Enabled = false;
        }
        else
        {
            btnDelete.Enabled = true;
        }
    }

    /// <summary>
    /// Stores Variables To Session And Redirects To Voucher Editing Form
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void GrdLedger_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        this.Session.Add("VoucherNo", GrdLedger.Rows[e.RowIndex].Cells[1].Text);
        this.Session.Add("DistributorId", drpDistributor.SelectedValue.ToString());
        this.Session.Add("VoucherTypeId", DrpVoucherType.SelectedValue.ToString());

        string url = "'frmVoucherEditing.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Voucher in Crystal Report For Print
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPrintVoucher_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();


        ArrayList alVoucherNos = new ArrayList();

        foreach (GridViewRow gvr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)gvr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
                alVoucherNos.Add(gvr.Cells[1].Text);
            }
        }

        if (alVoucherNos.Count > 0)
        {
            int VoucherType = Constants.IntNullValue;
            int VoucherType2 = Constants.IntNullValue;

            if (DrpVoucherType.SelectedValue == "16")
            {
                VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
            {
                if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
                {
                    VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
                }
                else
                {
                    VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
                }
            }

            DataSet ds = null;
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            ds = DPrint.PrintVouchers(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), RbdList.SelectedIndex, int.Parse(DrpUser.SelectedValue.ToString()), VoucherType2);

            DataTable dtSelectedVoucher = ds.Tables["RptVoucherView"].Clone();

            for (int i = 0; i < alVoucherNos.Count; i++)
            {
                DataRow[] foundRecord = ds.Tables["RptVoucherView"].Select("VOUCHER_NO = '" + alVoucherNos[i].ToString() + "'");
                if (foundRecord.Length > 0)
                {
                    for (int j = 0; j < foundRecord.Length; j++)
                    {
                        DataRow dr = dtSelectedVoucher.NewRow();
                        dtSelectedVoucher.ImportRow(foundRecord[j]);
                    }
                }
            }
            CrpReport.SetDataSource(dtSelectedVoucher);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
            CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
            string VoucherType3 = "";
            if (DrpVoucherType.SelectedValue == "16")
            {
                if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
                {
                    VoucherType3 = DrpVoucherType.SelectedItem.Text;
                }
                else
                {
                    VoucherType3 = DrpVoucherSubType.SelectedItem.Text;
                }
            }
            else
            {
                VoucherType3 = DrpVoucherSubType.SelectedItem.Text;
            }
            CrpReport.SetParameterValue("Voucher_Type", VoucherType3);
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpVoucherSubType.Items.Clear();
        if (DrpVoucherType.SelectedValue == "14")
        {
            //  DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));
            DrpVoucherSubType.Enabled = true;
        }
        else if (DrpVoucherType.SelectedValue == "16")
        {
            // DrpVoucherSubType.Enabled = false;
            DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpVoucherSubType.Items.Add(new ListItem("Purchase Voucher", "2"));
            DrpVoucherSubType.Items.Add(new ListItem("Purchase Return Voucher", "3"));
            DrpVoucherSubType.Items.Add(new ListItem("Stock Damage Voucher", "6"));
            DrpVoucherSubType.Items.Add(new ListItem("Stock Short Voucher", "8"));
            DrpVoucherSubType.Items.Add(new ListItem("Stock Excess Voucher", "9"));
            DrpVoucherSubType.Items.Add(new ListItem("Sale Voucher", "13"));
            DrpVoucherSubType.Items.Add(new ListItem("Sale Return Voucher", "14"));
            DrpVoucherSubType.Items.Add(new ListItem("Customer Claim Voucher", "19"));
            DrpVoucherSubType.Enabled = true;

        }
        else
        {
            //  DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));
            DrpVoucherSubType.Enabled = true;
        }
    }
}
