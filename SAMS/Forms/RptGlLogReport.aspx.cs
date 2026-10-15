using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For GL Log Detail Report
/// </summary>
public partial class Forms_RptGlLogReport : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadUser();
            this.VoucherType();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            DrpVoucherType_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        if (this.Session["RoleID"].ToString() == "3")
        {
            DrpUser.Items.Add(new ListItem(Convert.ToString(this.Session["UserName2"]), Convert.ToString(this.Session["UserID"])));
        }
        else
        {
            Distributor_UserController mController = new Distributor_UserController();
            DataTable dt = mController.SelectGLUser(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()));
            DrpUser.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpUser, dt, 0, 1);
        }
    }

    /// <summary>
    /// Loads Voucher Types To VoucherType Combo
    /// </summary>
    private void VoucherType()
    {
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpVoucherType, dt, 0, 1);
    }

    /// <summary>
    /// Shows GL Log Detail in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Shows GL Log Detail in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows GL Log Detail Either in PDF or In Excel
    /// </summary>
    /// <param name="p_Report_Type">Type</param>
    private void ShowReport(int p_Report_Type)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
       
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DataControl dc = new DataControl();
        int VT2 = 16;
        if (DrpVoucherType.SelectedValue == "16")
        {
            if (DrpVoucherSubType.SelectedItem.Text == "All")
            {
                VT2 = int.Parse(DrpVoucherType.SelectedValue);
            }
            else
            {
                VT2 = int.Parse(DrpVoucherSubType.SelectedValue);
            }
            
        }
        else
        {
            VT2 = int.Parse(DrpVoucherSubType.SelectedValue);
        }
        ds = RptAccountCtl.SelectGLLog_VoucherDetail(int.Parse(drpDistributor.SelectedValue.ToString()), VT2, int.Parse(DrpUser.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),rblFilter.SelectedIndex);

        SAMSBusinessLayer.Reports.CrpPrintGLLogVoucherDetail CrpReport = new SAMSBusinessLayer.Reports.CrpPrintGLLogVoucherDetail();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        string VoucherType2 = "";
        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType2 = DrpVoucherType.SelectedItem.Text;
        }
        else
        {
            VoucherType2 = DrpVoucherSubType.SelectedItem.Text;
        }
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
        CrpReport.SetParameterValue("VoucherSubType", VoucherType2);
        CrpReport.SetParameterValue("User", DrpUser.SelectedItem.Text);
        CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());


        this.Session.Add("ReportType", p_Report_Type);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
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
            DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            //DrpVoucherSubType.Items.Add(new ListItem("Purchase Voucher", "2"));
            //DrpVoucherSubType.Items.Add(new ListItem("Purchase Return Voucher", "3"));
            //DrpVoucherSubType.Items.Add(new ListItem("Stock Damage Voucher", "6"));
            //DrpVoucherSubType.Items.Add(new ListItem("Stock Short Voucher", "8"));
            //DrpVoucherSubType.Items.Add(new ListItem("Stock Excess Voucher", "9"));
            //DrpVoucherSubType.Items.Add(new ListItem("Sale Voucher", "13"));
            //DrpVoucherSubType.Items.Add(new ListItem("Sale Return Voucher", "14"));
            //DrpVoucherSubType.Items.Add(new ListItem("Customer Claim Voucher", "19"));
            //DrpVoucherSubType.Items.Add(new ListItem("Freight Voucher", "40"));
            //DrpVoucherSubType.Items.Add(new ListItem("Manual Voucher", "1"));
            DrpVoucherSubType.Enabled = true;
        }
        else
        {
            //   DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));
            DrpVoucherSubType.Enabled = true;
        }
    }
}
