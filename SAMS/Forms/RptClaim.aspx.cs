using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptClaim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadClaimType();
            //this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadArea();
            this.LoadData();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        this.DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, false);
        clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, 0, 2);
    }
    private void LoadArea()
    {

        //DistributorAreaController mController = new DistributorAreaController();
        //DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        //this.DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, false);

        DrpRoute.Items.Clear();
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);


    }
    private void LoadData()
    {
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            //CustomerDataController mController = new CustomerDataController();
            //DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            //this.DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            //clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, false);

            DrpCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4);
        }
    }
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;
    }

    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadData();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    public void ShowReport(int Type)
    {
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataSet ds;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

        ds = RptCustomerCtl.CustomerClaim_Slip(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(RbdClaimType.SelectedValue.ToString()));

        SAMSBusinessLayer.Reports.CrpClaimSlip CrpReport = new SAMSBusinessLayer.Reports.CrpClaimSlip();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();


        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("Location", DrpLocation.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "");
        CrpReport.SetParameterValue("Town", DrpRoute.SelectedItem.Text);
        CrpReport.SetParameterValue("ClaimType", RbdClaimType.SelectedItem.Text);//
        CrpReport.SetParameterValue("distributor_address", dt.Rows[0]["ADDRESS1"].ToString() + " Ph: " + dt.Rows[0]["CONTACT_NUMBER"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", Type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}