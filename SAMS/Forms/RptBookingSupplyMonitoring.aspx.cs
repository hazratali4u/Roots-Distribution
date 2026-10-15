using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Booking vs Execution Report
/// </summary>
public partial class Forms_RptBookingSupplyMonitoring : System.Web.UI.Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadLocation();
            LoadSalesForce();
            this.LoadDeliveryman();
            this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DrpReportType_SelectedIndexChanged(null, null);
        }
    }


    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            this.drpPrincipal.DataSource = m_dt;
            this.drpPrincipal.DataTextField = "Company_Name";
            this.drpPrincipal.DataValueField = "Company_Id";
            this.drpPrincipal.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.DataSource = dt;
            this.DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            this.DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void LoadSalesForce()
    {
        SaleForceController ds = new SaleForceController();
        DataTable dt = ds.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(drpPrincipal.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            drpSaleForce.Items.Clear();
            drpSaleForce.Items.Add(new ListItem("All", "0"));
            clsWebFormUtil.FillDropDownList(this.drpSaleForce, dt, "USER_ID", "USER_NAME");
        }
        else
        {
            this.drpSaleForce.Items.Clear();
        }
    }
    private void LoadDeliveryman()
    {
        this.DrpDeliveryMan.Items.Clear();
        if (DrpLocation.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            DrpDeliveryMan.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3);
        }
    }

    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSalesForce();
        this.LoadDeliveryman();
    }
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSalesForce();
    }

    private void ShowReport(int pReportType)
    {
        try
        {
            RptCustomerController rptCustomerCtl = new RptCustomerController();
            switch (DrpReportType.SelectedIndex)
            {
                case 0:
                    {

                        DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));

                        string fromDate = null;
                        string toDate = null;


                        CrpBookingSupplyMonitoring crpReport = new CrpBookingSupplyMonitoring();

                        DateTime parsedDateFromdate = DateTime.Parse(txtFromDate.Text);
                        DateTime parsedDateTodate = DateTime.Parse(txtToDate.Text);
                        fromDate = parsedDateFromdate.ToShortDateString();
                        toDate = parsedDateTodate.ToShortDateString();
                        DataSet ds = rptCustomerCtl.BookingVsExecution(int.Parse(drpPrincipal.SelectedValue), int.Parse(DrpLocation.SelectedValue), int.Parse(drpSaleForce.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), Convert.ToDateTime(fromDate + " 00:00:00"), Convert.ToDateTime(toDate + " 00:00:00"), 1);

                        crpReport.SetDataSource(ds);
                        crpReport.Refresh();

                        crpReport.SetParameterValue("Branch", DrpLocation.SelectedItem.Text);
                        crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
                        crpReport.SetParameterValue("FromDate", txtFromDate.Text);
                        crpReport.SetParameterValue("ToDate", txtToDate.Text);
                        crpReport.SetParameterValue("OrderBooker", drpSaleForce.SelectedItem.Text);
                        crpReport.SetParameterValue("DELIVERYMAN", DrpDeliveryMan.SelectedItem.Text);
                        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                        Session.Add("CrpReport", crpReport);
                        Session.Add("ReportType", pReportType);
                        const string url = "'Default.aspx'";
                        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                        Type cstype = GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(cstype, "OpenWindow", script);
                    }
                    break;
                case 4:
                    {


                        DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));

                        string fromDate = null;
                        string toDate = null;

                        CrpBookingSupplyMonitoringOrderbookerWise crpReport = new CrpBookingSupplyMonitoringOrderbookerWise();

                        DateTime parsedDateFromdate = DateTime.Parse(txtFromDate.Text);
                        DateTime parsedDateTodate = DateTime.Parse(txtToDate.Text);
                        fromDate = parsedDateFromdate.ToShortDateString();
                        toDate = parsedDateTodate.ToShortDateString();
                        DataSet ds = rptCustomerCtl.BookingVsExecution(int.Parse(drpPrincipal.SelectedValue), int.Parse(DrpLocation.SelectedValue), int.Parse(this.drpSaleForce.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), Convert.ToDateTime(fromDate + " 00:00:00"), Convert.ToDateTime(toDate + " 00:00:00"), 2);

                        crpReport.SetDataSource(ds);
                        crpReport.Refresh();

                        crpReport.SetParameterValue("Branch", DrpLocation.SelectedItem.Text);
                        crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
                        crpReport.SetParameterValue("FromDate", txtFromDate.Text);
                        crpReport.SetParameterValue("ToDate", txtToDate.Text);
                        crpReport.SetParameterValue("OrderBooker", drpSaleForce.SelectedItem.Text);
                        crpReport.SetParameterValue("DELIVERYMAN", DrpDeliveryMan.SelectedItem.Text);
                        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                        Session.Add("CrpReport", crpReport);
                        Session.Add("ReportType", pReportType);
                        const string url = "'Default.aspx'";
                        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                        Type cstype = GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(cstype, "OpenWindow", script);
                    }
                    break;
                default:
                    {

                        DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));

                        string fromDate = null;
                        string toDate = null;
                        DataSet ds;

                        CrpBookingVsExecutionSKUWise crpReport = new CrpBookingVsExecutionSKUWise();

                        DateTime parsedDateFromdate = DateTime.Parse(this.txtFromDate.Text);
                        DateTime parsedDateTodate = DateTime.Parse(this.txtToDate.Text);
                        fromDate = parsedDateFromdate.ToShortDateString();
                        toDate = parsedDateTodate.ToShortDateString();
                        if (DrpReportType.SelectedIndex == 3)
                        {
                            ds = rptCustomerCtl.SKUWiseBookingVsExecution(int.Parse(drpPrincipal.SelectedValue), int.Parse(DrpLocation.SelectedValue), int.Parse(this.drpSaleForce.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), Convert.ToDateTime(fromDate + " 00:00:00"), Convert.ToDateTime(toDate + " 00:00:00"), Constants.IntNullValue);
                        }
                        else
                        {
                            ds = rptCustomerCtl.SKUWiseBookingVsExecution(int.Parse(drpPrincipal.SelectedValue), int.Parse(DrpLocation.SelectedValue), int.Parse(this.drpSaleForce.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), Convert.ToDateTime(fromDate + " 00:00:00"), Convert.ToDateTime(toDate + " 00:00:00"), 0);
                        }
                        crpReport.SetDataSource(ds);
                        crpReport.Refresh();

                        crpReport.SetParameterValue("Branch", DrpLocation.SelectedItem.Text);
                        crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
                        crpReport.SetParameterValue("FromDate", txtFromDate.Text);
                        crpReport.SetParameterValue("ToDate", txtToDate.Text);
                        crpReport.SetParameterValue("OrderBooker", drpSaleForce.SelectedItem.Text);
                        crpReport.SetParameterValue("DELIVERYMAN", DrpDeliveryMan.SelectedItem.Text);
                        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                        if (DrpReportType.SelectedIndex == 1)
                        {
                            crpReport.SetParameterValue("ReportTitle", "(Units)");
                        }
                        else if (DrpReportType.SelectedIndex == 2)
                        {
                            crpReport.SetParameterValue("ReportTitle", "(Cartons)");
                        }
                        else if (DrpReportType.SelectedIndex == 3)
                        {
                            crpReport.SetParameterValue("ReportTitle", "(Values)");
                        }


                        Session.Add("CrpReport", crpReport);
                        Session.Add("ReportType", pReportType);
                        const string url = "'Default.aspx'";
                        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                        Type cstype = GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(cstype, "OpenWindow", script);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel(object sender, EventArgs e)
    {
        ShowReport(1);
    }





    protected void DrpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DrpReportType.SelectedValue=="0" || DrpReportType.SelectedValue=="4")
        {
            lblPrincipal.Visible = false;
            drpPrincipal.Visible = false;
        }
        else
        {
            lblPrincipal.Visible = true;
            drpPrincipal.Visible = true;
        }
    }
}
