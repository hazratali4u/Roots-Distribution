using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class Forms_frmOrderInvoiceStep1 : System.Web.UI.Page
{
    public static int mCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopupExtender.Hide();
            TransectionType();
            LoadDistributor();
            LoadPrincipal();
            LoadArea();
            LoadOrderBooker();
            LoadDeliveryman();
            DateTime pOrderDate = DateTime.Parse(Session["CurrentWorkDate"].ToString()).AddDays(1);
            txtToDate.Text = pOrderDate.ToString("dd-MMM-yyyy");
            DrpDocumentType_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Enables/Disables Orderbooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedValue.ToString() == "")
        {

        }
        else if (Convert.ToInt32(DrpDocumentType.SelectedValue) == 0)
        {
            DrpOrderBooker.Enabled = true;
            DateTime pOrderDate = DateTime.Parse(this.Session["CurrentWorkDate"].ToString()).AddDays(1);
            //DateTime pOrderDate = DateTime.Parse(this.Session["CurrentWorkDate"].ToString());
            txtToDate.Text = pOrderDate.ToString("dd-MMM-yyyy");
        }
        else
        {
            DrpOrderBooker.Enabled = false;
            DateTime pOrderDate = (DateTime)this.Session["CurrentWorkDate"];
            txtToDate.Text = pOrderDate.ToString("dd-MMM-yyyy");
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
        this.Session.Add("DisDt", dt);
    }

    /// <summary>
    /// Loads Routes To Route Combo, Orderbookers To Orderbooker Combo And Deliverymen To Deliveryman Comob
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadOrderBooker();
        this.LoadDeliveryman();
        drpDistributor.Focus();
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }

    private void TransectionType()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectTransectionTypes(int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue);
        //clsWebFormUtil.FillDropDownList(this.DrpDocumentType, m_dt, 0, 1, true);
        var qry = m_dt.AsEnumerable().CopyToDataTable();
        if (qry.Rows.Count > 0)
        {
            bool ORDERENTERY = Convert.ToBoolean(m_dt.Rows[0]["ORDERENTERY"].ToString());
            bool SPOT_SALE = Convert.ToBoolean(m_dt.Rows[0]["SPOT_SALE"].ToString());
            bool SALE_RETURN = Convert.ToBoolean(m_dt.Rows[0]["SALE_RETURN"].ToString());
            if (ORDERENTERY)
            {
                DrpDocumentType.Items.Add(new ListItem("Order Entry", "0"));
            }
            if (SPOT_SALE)
            {
                DrpDocumentType.Items.Add(new ListItem("Spot Sale", "1"));
            }
            if (SALE_RETURN)
            {
                DrpDocumentType.Items.Add(new ListItem("Sale Return", "2"));
            }
            if (!ORDERENTERY && !SPOT_SALE && !SALE_RETURN)
            {
                DrpDocumentType.Items.Add(new ListItem("", ""));
            }
            DrpDocumentType_SelectedIndexChanged(null, null);
        }
        else
        {
            DrpDocumentType.Items.Add(new ListItem("", ""));
        }



    }

    /// <summary>
    /// Loads Orderbookers To Orderbooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
        DrpPrincipal.Focus();
    }

    /// <summary>
    /// Loads Routes To Route Combo
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
    /// Loads Orderbookers To Orderbooker Combo And Deliverymen To Deliveryman Comob
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
        this.LoadDeliveryman();
        DrpRoute.Focus();
    }

    /// <summary>
    /// Loads Orderbookers To Orderbooker Combo
    /// </summary>
    private void LoadOrderBooker()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpPrincipal.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpOrderBooker, m_dt, 0, 3, true);
        }
        else
        {
            DrpOrderBooker.Items.Clear();
        }
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
    /// Checks/UnChecks All Orders In Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAll.Checked == true)
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                System.Web.UI.WebControls.CheckBox ChbInvoice = (System.Web.UI.WebControls.CheckBox)dr.FindControl("ChbInvoice");
                ChbInvoice.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                System.Web.UI.WebControls.CheckBox ChbInvoice = (System.Web.UI.WebControls.CheckBox)dr.FindControl("ChbInvoice");
                ChbInvoice.Checked = false;
            }
        }
    }

    /// <summary>
    /// Loads Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        if (DrpOrderBooker.Items.Count > 0)
        {
            if (DrpDocumentType.SelectedItem.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please assign first Trnsection Type to this user.');", true);
                return;
            }
            this.LoadPendingOrder();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Orderbooker');", true);
        }
    }

    /// <summary>
    /// Loads Order Grid
    /// </summary>
    private void LoadPendingOrder()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()),
            Constants.IntNullValue, int.Parse(DrpOrderBooker.SelectedValue.ToString()), int.Parse(DrpDeliveryMan.SelectedValue.ToString()),
            Constants.Order_Pending_Id, int.Parse(ddSearchType.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(txtToDate.Text), 2);
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();
        mCount = 0;
    }

    /// <summary>
    /// Stores Related Data To Session Variables And Redirects To Order/Invoice Step 2
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedItem.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please assign Trnsection Type first to this user.');", true);
            return;
        }
        if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(Session["CurrentWorkDate"]))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid Working Date');", true);
            return;
        }
        if (DrpOrderBooker.Items.Count > 0)
        {
            if (drpDistributor.Items.Count > 0 && DrpPrincipal.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpDeliveryMan.Items.Count > 0)
            {
                this.Session.Add("DistributorId", int.Parse(drpDistributor.SelectedValue.ToString()));
                //  this.Session.Add("PrincipalId", int.Parse(DrpPrincipal.SelectedValue.ToString()));
                this.Session.Add("AreaId", long.Parse(DrpRoute.SelectedValue.ToString()));
                this.Session.Add("Route", DrpRoute.SelectedItem.Text);
                this.Session.Add("SaleMan", DrpDeliveryMan.SelectedItem.Text);
                this.Session.Add("OrderDate", Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy"));
                // this.Session.Add("UnitType", RbUnitType.SelectedIndex);

                if (Convert.ToInt32(DrpDocumentType.SelectedValue) == 0)
                {
                    this.Session.Add("OrderNo", 0);
                    this.Session.Add("OrderBookerId", int.Parse(DrpOrderBooker.SelectedValue.ToString()));
                    this.Session.Add("DeliveryManId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                }
                else if (Convert.ToInt32(DrpDocumentType.SelectedValue) == 1)
                {
                    this.Session.Add("OrderNo", -1);
                    this.Session.Add("OrderBookerId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                    this.Session.Add("DeliveryManId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                }
                else
                {
                    this.Session.Add("OrderNo", -2);
                    this.Session.Add("OrderBookerId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                    this.Session.Add("DeliveryManId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                }
                Response.Redirect("~/Forms/frmOrderInvoiceStep2.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Orderbooker');", true);
        }
    }

    /// <summary>
    /// Converts Checked Orders In Order Grid To Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPost_Click(object sender, EventArgs e)
    {
        #region Convert To Invoice
       
        if (DrpOrderBooker.Items.Count > 0)
        {
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
                PopupExtender.Show();
                mCount += 1;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Orderbooker');", true);
        }
        #endregion
    }

    private bool IsDayClosed()
    {
        bool flag = false;
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(Session["Distributor_Id"]), 3);
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

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        if (mCount == 1)
        {
            CustomerDataController CDC = new CustomerDataController();
            OrderEntryController ORD = new OrderEntryController();
            DataControl dc = new DataControl();
            DataTable dtStock;
            GrdFreeSKU.Visible = false;
            gvRateDifference.Visible = false;
            bool is_voucher_hit = false;
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
                            is_voucher_hit = true;
                        }
                    }
                }
            }

            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                System.Web.UI.WebControls.CheckBox ChbInvoice = (System.Web.UI.WebControls.CheckBox)dr.FindControl("ChbInvoice");
                if (ChbInvoice.Checked == true)
                {
                    if (Convert.ToDateTime(this.Session["CurrentWorkDate"]) >= Convert.ToDateTime(ConvertDate.British_To_American2(dr.Cells[5].Text)))
                    {

                        #region Convert Opration
                        if (int.Parse(ddSearchType.SelectedValue.ToString()) == Constants.Cash_Order_Id)
                        {
                            string PriName = "";
                            string CustName = "";
                            DateTime DATE = DateTime.Parse(this.Session["CurrentWorkDate"].ToString());
                            if (dr.Cells[0].Text.Equals(""))
                            {
                                CustName = "";
                            }
                            else
                            {
                                CustName = dr.Cells[3].Text;
                            }
                            dtStock = ORD.ConvertOrder_to_InvoiceForQureshi(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[12].Text, long.Parse(dr.Cells[0].Text), Convert.ToInt32(DrpPrincipal.SelectedValue),
                                                long.Parse(dr.Cells[4].Text), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), decimal.Parse(dc.chkNull_0(dr.Cells[6].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[7].Text)),
                                                decimal.Parse(dc.chkNull_0(dr.Cells[8].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[9].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[10].Text)), Constants.Order_Posted_Id, Constants.Cash_Order_Id, int.Parse(this.Session["UserId"].ToString()), dr.Cells[11].Text
                                                , "", Convert.ToDecimal(dc.chkNull_0(dr.Cells[14].Text)), Constants.IntNullValue, decimal.Parse(dr.Cells[16].Text), is_voucher_hit, PriName, CustName, decimal.Parse(dr.Cells[17].Text), dc.chkNull_Empty(dr.Cells[18].Text), decimal.Parse(dr.Cells[19].Text));

                            if (dtStock.Columns.Count > 1)
                            {
                                if (dtStock.Columns.Count == 7)
                                {
                                    gvRateDifference.DataSource = dtStock;
                                    gvRateDifference.DataBind();
                                    gvRateDifference.Visible = true;
                                }
                                else
                                {
                                    GrdFreeSKU.DataSource = dtStock;
                                    GrdFreeSKU.DataBind();
                                    GrdFreeSKU.Visible = true;
                                }
                                this.LoadPendingOrder();
                                return;
                            }

                        }
                        else
                        {
                            string PriName = "";
                            string CustName = "";
                            DateTime DATE = DateTime.Parse(this.Session["CurrentWorkDate"].ToString());
                            if (dr.Cells[0].Text.Equals(""))
                            {
                                CustName = "";
                            }
                            else
                            {
                                CustName = dr.Cells[3].Text;
                            }
                            DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(dr.Cells[0].Text), int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(ddSearchType.SelectedValue.ToString()));
                            //if (decimal.Parse(dc.chkNull_0(dt.Rows[0][0].ToString())) >= decimal.Parse(dr.Cells[10].Text))
                            //{
                            dtStock = ORD.ConvertOrder_to_InvoiceForQureshi(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[12].Text, long.Parse(dr.Cells[0].Text), Convert.ToInt32(DrpPrincipal.SelectedValue),
                                   long.Parse(dr.Cells[4].Text), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), decimal.Parse(dc.chkNull_0(dr.Cells[6].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[7].Text)),
                                   decimal.Parse(dc.chkNull_0(dr.Cells[8].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[9].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[10].Text)), Constants.Order_Posted_Id, int.Parse(ddSearchType.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), dr.Cells[11].Text
                                   , "", Convert.ToDecimal(dc.chkNull_0(dr.Cells[14].Text)), Constants.IntNullValue, decimal.Parse(dr.Cells[16].Text), is_voucher_hit, PriName, CustName, decimal.Parse(dr.Cells[17].Text), dc.chkNull_Empty(dr.Cells[18].Text), decimal.Parse(dr.Cells[19].Text));

                            if (dtStock.Columns.Count > 1)
                            {
                                if (dtStock.Columns.Count == 7)
                                {
                                    gvRateDifference.DataSource = dtStock;
                                    gvRateDifference.DataBind();
                                    gvRateDifference.Visible = true;
                                }
                                else
                                {
                                    GrdFreeSKU.DataSource = dtStock;
                                    GrdFreeSKU.DataBind();
                                    GrdFreeSKU.Visible = true;
                                }
                                this.LoadPendingOrder();
                                return;
                            }
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Order #  " + dr.Cells[4].Text + " Customer Credit Limit " + dc.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                            //}
                        }
                        #endregion
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Order Date Can not be greater than current working date');", true);
                        break;
                    }
                }
            }
            this.LoadPendingOrder();
            PopupExtender.Hide();
        }
    }

    protected void BtnNo_Click(object sender, EventArgs e)
    {
        PopupExtender.Hide();
    }
}