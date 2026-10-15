using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptChequeBookRegister : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly CustomerDataController _mCdControllerontroller = new CustomerDataController();
    readonly DistributorController _dController = new DistributorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            LoadDistributor();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadLocation()
    {

        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //  drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillDropDownList(this.drpLocation, dt, 0, 2);
    }
    private void LoadDistributor1()
    {
        if (drpLocation.Items.Count > 0)
        {

            DataTable dt = _mCdControllerontroller.SelectAllCustomer(int.Parse(drpLocation.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, 0);
            //DrpBanks.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpBanks, dt, 0, 3, true);
        }
    }
    private void LoadDistributor()
    {
        try
        {
            if (drpLocation.Items.Count > 0)
            {
                Configuration.GetAccountHead();
                AccountHeadController mAccountController = new AccountHeadController();
                DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
                clsWebFormUtil.FillDropDownList(DrpBanks, dt, 0, 4, true);
                if (dt.Rows.Count > 0)
                {
                    DrpBanks.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    public void ShowReport(int p_Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        SAMSBusinessLayer.Reports.CrpChequeRegister CrpReport = new SAMSBusinessLayer.Reports.CrpChequeRegister();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpLocation.SelectedValue));
        DataSet ds = RptInventoryCtl.SelectChequeRegister(int.Parse(drpLocation.SelectedValue), int.Parse(DrpBanks.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text),0);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("Location", drpLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("Customer", "");
            CrpReport.SetParameterValue("Route", DrpBanks.SelectedItem.Text);
        CrpReport.SetParameterValue("fromdate", txtStartDate.Text);
        CrpReport.SetParameterValue("todate", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //CrpReport.SetParameterValue("Collection", RbtReportType.SelectedItem.Text.ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void RbtReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbtReportType.SelectedValue != "0")
        {
            //PanelDistributor.Visible = true;
            //LoadDistributor();
        }
        else
        {
           // PanelDistributor.Visible = false;
        }
    }
}