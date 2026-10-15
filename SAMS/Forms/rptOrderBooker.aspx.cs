using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using System.Drawing.Printing;
using System.Design;

/// <summary>
/// Form For Order Booker Reports
/// </summary>
public partial class Forms_rptOrderBooker : System.Web.UI.Page
{

    /// <summary>
    /// 
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();

            LoadPrincipal();
            LoadOrderBooker();
           
           Configuration.SystemCurrentDateTime = (DateTime) Session["CurrentWorkDate"];
             txtFromDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
             txtToDate.Text =Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }


    private void LoadOrderBooker()
    {
        DrpOrderBooker.Items.Clear();

        if (DrpLocation.Items.Count > 0)
        {
            
                SaleForceController mDController = new SaleForceController();
                DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, int.Parse( Session["CompanyId"].ToString()));
                DrpOrderBooker.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList( DrpOrderBooker, m_dt, 0, 3);
          
        }

    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse( Session["UserId"].ToString()), int.Parse( Session["CompanyId"].ToString()));
             DrpLocation.DataSource = dt;
             DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
             DrpLocation.DataValueField = "DISTRIBUTOR_ID";
             DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
          //  SKUPriceDetailController PController = new SKUPriceDetailController();
          //  DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse( Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse( Session["CurrentWorkDate"].ToString()));
          ////   drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
          //  clsWebFormUtil.FillDropDownList( drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    /// <summary>
    /// Shows Order Booker Reports in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSaleCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse (DrpLocation .SelectedValue ));

            string FromDate = null;
            string ToDate = null;

            DataSet ds = null;

            DateTime parsed_date_fromdate = DateTime.Parse( txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse( txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();


            ds = RptSaleCtl.RptOrderBookerWiseDSR(Constants.IntNullValue,int.Parse (DrpLocation.SelectedValue), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), int.Parse(DrpOrderBooker.SelectedValue));
            SAMSBusinessLayer.Reports.CrpOrderBookerWiseDSR CrpReport = new SAMSBusinessLayer.Reports.CrpOrderBookerWiseDSR();
           // CrpReport.PrintOptions.PaperSize = PaperSize.PaperA4;
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal",  "");
            CrpReport.SetParameterValue("Branch",  DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Title", "Order Booker Wise DSR");
            CrpReport.SetParameterValue("FromDate",  txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate",  txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
         
             Session.Add("CrpReport", CrpReport);
             Session.Add("ReportType", 0);
             string url = "'Default.aspx'";
          //  string url = "'frmtesting.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype =  GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Order Booker Reports in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSaleCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));

            string FromDate = null;
            string ToDate = null;

            DataSet ds = null;

            DateTime parsed_date_fromdate = DateTime.Parse( txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse( txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();


            ds = RptSaleCtl.RptOrderBookerWiseDSR(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), int.Parse(DrpOrderBooker.SelectedValue));
            SAMSBusinessLayer.Reports.CrpOrderBookerWiseDSR CrpReport = new SAMSBusinessLayer.Reports.CrpOrderBookerWiseDSR();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal",  "");
            CrpReport.SetParameterValue("Branch",  DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Title", "Order Booker Wise DSR");
            CrpReport.SetParameterValue("FromDate",  txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate",  txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());


             Session.Add("CrpReport", CrpReport);
             Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            // string url = "'frmtesting.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype =  GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
       
    }
    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOrderBooker();
    }
    protected void btnViewer_Click(object sender, EventArgs e)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSaleCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));

            string FromDate = null;
            string ToDate = null;

            DataSet ds = null;

            DateTime parsed_date_fromdate = DateTime.Parse( txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse( txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();


            ds = RptSaleCtl.RptOrderBookerWiseDSR(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), int.Parse(DrpOrderBooker.SelectedValue));
            SAMSBusinessLayer.Reports.CrpOrderBookerWiseDSR CrpReport = new SAMSBusinessLayer.Reports.CrpOrderBookerWiseDSR();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal",  "");
            CrpReport.SetParameterValue("Branch",  DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("Title", "Order Booker Wise DSR");
            CrpReport.SetParameterValue("FromDate",  txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate",  txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());


             Session.Add("CrpReport", CrpReport);
             Session.Add("ReportType", 1);
          //  string url = "'Default.aspx'";
           string url = "'frmtesting.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype =  GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}
