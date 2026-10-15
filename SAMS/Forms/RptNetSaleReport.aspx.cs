using CrystalDecisions.CrystalReports.Engine;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptNetSaleReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadChannelType();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillListBox(this.ListCatagory, m_dt, 0, 1);
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        ddlChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlChannelType, dt, 0, 2);
    }
    protected void ChbAllCatagories_CheckedChanged(object sender, EventArgs e)
    {
        CheckAll();
    }
    protected void CheckAll()
    {
        if (this.ChbAllCatagories.Checked == true)
        {
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                this.ListCatagory.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                this.ListCatagory.Items[i].Selected = false;
            }
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
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DateTime parsed_date_fromdate = DateTime.Parse(this.txtStartDate.Text);
        DateTime parsed_date_todate = DateTime.Parse(this.txtEndDate.Text);
        string FromDate = parsed_date_fromdate.ToShortDateString();
        string ToDate = parsed_date_todate.ToShortDateString();
        string Catagories_IDs = null;
        ReportDocument CrpReport = new ReportDocument();
        
        
        DataSet ds = null;
        for (int i = 0; i < this.ListCatagory.Items.Count; i++)
        {
            if (this.ListCatagory.Items[i].Selected == true)
            {
                Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
            }
        }

        ds = RptCustomerCtl.NetSaleReport(Convert.ToInt32(ddlChannelType.SelectedValue), Convert.ToInt32(this.drpDistributor.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, int.Parse(RbtType.SelectedValue.ToString()));

        
        if (int.Parse(RbtType.SelectedValue.ToString()) == 1)
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpNetSaleReport();
            //Customer Wise Principal Wise Net Sales Report
        }
        else
        {
            //CrpReport = new SAMSBusinessLayer.Reports.CrpNetSalePrincipalWiseReport();
            CrpReport = new SAMSBusinessLayer.Reports.CrpPrincipalNetSaleReport();
        }
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        if (int.Parse(RbtType.SelectedValue.ToString()) == 1)
        {
            CrpReport.SetParameterValue("ReportTitle", "Customer Wise Principal Wise Net Sales Report");
            //Customer Wise Principal Wise Net Sales Report
        }
        else
        {
            CrpReport.SetParameterValue("ReportTitle", "Principal Wise Net Sales Report");
            //Principal Wise Net Sales Report
        }
        CrpReport.SetParameterValue("PRINT_TITLE_C", Convert.ToBoolean(dt.Rows[0]["PRINT_TITLE_C"].ToString()));
        CrpReport.SetParameterValue("ChannelType", ddlChannelType.SelectedItem.Text);
        CrpReport.SetParameterValue("Branch", this.drpDistributor.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("FromDate", this.txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", this.txtEndDate.Text);
        CrpReport.SetParameterValue("USERNAME", Session["UserName2"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_Type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}