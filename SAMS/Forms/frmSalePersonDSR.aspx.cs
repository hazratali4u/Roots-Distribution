using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;    
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Sale Person DSR Report
/// </summary>
public partial class Forms_frmSalePersonDSR : System.Web.UI.Page
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
             LoadDistributor();
             LoadPrincipal();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime) Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            drpDistributor_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse( Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse( Session["CurrentWorkDate"].ToString()));
        //DrpPrincipal.Items.Add(new ListItem("All",Constants.IntNullValue.ToString()));       
        //clsWebFormUtil.FillDropDownList( DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse( Session["UserId"].ToString()), int.Parse( Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList( drpDistributor, dt, 0, 2, true);
        Session.Add("dtDistributor", dt);
    }

    /// <summary>
    /// Shows Sale Person DSR in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Sale Person DSR in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Gets Sale Person DSR And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType">Type</param>
    private void ShowReport(int p_ReprotType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        DataSet ds = null;

        if (RbReportType.SelectedIndex == 0)
        {
            if (DrpSaleForceType.SelectedIndex == 0)
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectOrderBookerDSRProDuctWise(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse( Session["UserId"].ToString()));

                SAMSBusinessLayer.Reports.CrpSaleReport_ProductWise CrpReport = new SAMSBusinessLayer.Reports.CrpSaleReport_ProductWise();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                 Session.Add("CrpReport", CrpReport);
                 Session.Add("ReportType", p_ReprotType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype =  GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else 
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectSalePersonDSRProDuctWise(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse( Session["UserId"].ToString()));

                SAMSBusinessLayer.Reports.CrpSaleReport_ProductWise CrpReport = new SAMSBusinessLayer.Reports.CrpSaleReport_ProductWise();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                 Session.Add("CrpReport", CrpReport);
                 Session.Add("ReportType", p_ReprotType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype =  GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        else if (RbReportType.SelectedIndex == 1)
        {
            if (DrpSaleForceType.SelectedIndex == 0)
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectOrderBookerDSR(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse( Session["UserId"].ToString()));

                SAMSBusinessLayer.Reports.CrpSalePersonDSR CrpReport = new SAMSBusinessLayer.Reports.CrpSalePersonDSR();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                 Session.Add("CrpReport", CrpReport);
                 Session.Add("ReportType", p_ReprotType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype =  GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectSalePersonDSR(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse( Session["UserId"].ToString()));

                SAMSBusinessLayer.Reports.CrpSalePersonDSR CrpReport = new SAMSBusinessLayer.Reports.CrpSalePersonDSR();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                 Session.Add("CrpReport", CrpReport);
                 Session.Add("ReportType", p_ReprotType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype =  GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        else if (RbReportType.SelectedIndex == 2)
        {
            DataControl dc = new DataControl();
            ds = RptSaleCtl.GetSalePersonDSRDetail(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse( Session["UserId"].ToString()));

            SAMSBusinessLayer.Reports.CrpSalePersonDSRDetail CrpReport = new SAMSBusinessLayer.Reports.CrpSalePersonDSRDetail();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Principal", "");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

             Session.Add("CrpReport", CrpReport);
             Session.Add("ReportType", p_ReprotType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype =  GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DrpPrincipal.Enabled = true;
        int DistributorType = 1;
        DataTable dtDistributor = (DataTable)Session["dtDistributor"];
        foreach (DataRow dr in dtDistributor.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                DistributorType = Convert.ToInt32(dr["SUBZONE_ID"]);
                break;
            }
        }
        if (DistributorType == 6 && RbReportType.SelectedIndex != 0)
        {
            //DrpPrincipal.SelectedValue = Constants.IntNullValue.ToString();
            //DrpPrincipal.Enabled = false;
        }
    }

    protected void RbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpDistributor_SelectedIndexChanged(null, null);
    }
}