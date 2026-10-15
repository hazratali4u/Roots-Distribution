using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Form To Rollback Order, Invoice, Sale Return And Realized Cheque
/// </summary>
public partial class Forms_frmRollBackForm : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadOrderBooker();
            this.LoadLegend();
            Distributor_closing();
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
    }
    
    /// <summary>
    /// Loads Principals To Principal Comob
    /// </summary>
    private void LoadPrincipal()
    {
        //SKUPriceDetailController PController = new SKUPriceDetailController();
        //DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }
    
    /// <summary>
    /// Loads Legends To Legend Combo
    /// </summary>
    private void LoadLegend()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable m_dt = or.SelectLegend(); 
        clsWebFormUtil.FillDropDownList(this.DrpLenged, m_dt, 0, 2, true);
    }
    
    /// <summary>
    /// Load OrderBookers To OrderBooker Combo
    /// </summary>
    private void LoadOrderBooker()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(int.Parse(DrpDocumentType.SelectedValue.ToString()),Constants.IntNullValue,int.Parse(drpDistributor.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpOrderBooker, m_dt, 0, 1, true);
        }
        else
        {
            DrpOrderBooker.Items.Clear();
        }

    }

    /// <summary>
    /// Loads Rollback Order, Invoice And Sale Return Data To Grid
    /// </summary>
    private void LoadRollbackDocument()
    {
        try
        {
            if (D_Date.Text != "")
            {
                int PrincipalID = Constants.IntNullValue;
                if (hdfDisTypeID.Value != "6")//Location Type Distributor
                {
                    PrincipalID = Constants.IntNullValue;
                }
                OrderEntryController or = new OrderEntryController();
                //DataTable dtOrder = or.SelectRollBackDocument(int.Parse(drpDistributor.SelectedValue.ToString()), PrincipalID,
                //    int.Parse(DrpOrderBooker.SelectedValue.ToString()), int.Parse(DrpDocumentType.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                DataTable dtOrder = or.SelectRollBackDocument(int.Parse(drpDistributor.SelectedValue.ToString()), PrincipalID,
                  int.Parse(DrpOrderBooker.SelectedValue.ToString()), int.Parse(DrpDocumentType.SelectedValue.ToString()), DateTime.Parse(D_Date.Text));

                GrdOrder.DataSource = dtOrder;
                GrdOrder.DataBind();
            }
            else
            {

                GrdOrder.DataSource = null;
                GrdOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }


    }
    /// <summary>
    /// Loads Rollback Cheques Data To Grid
    /// </summary>
    private void LoadRollbackCheque()
    {
        if (D_Date.Text != "")
        {
            try
            {
                ChequeEntryController CController = new ChequeEntryController();
                //   DataTable dt = CController.SelectChequeEntry(Constants.Cheque_Clear, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), 0);
                DataTable dt = CController.SelectChequeEntry(Constants.Cheque_Clear, DateTime.Parse(D_Date.Text), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, 0);

                GrdCheque.DataSource = dt;
                GrdCheque.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
            }
        }else
        {
            GrdCheque.DataSource = null;
            GrdCheque.DataBind();
        }
    }
    
    /// <summary>
    /// Loads OrderBookers To OrderBooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
    }

    /// <summary>
    /// Loads OrderBookers To OrderBooker Combo And Principals To Principal Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadPrincipal();
        this.LoadOrderBooker();
        drpDistributor.Focus();
        Distributor_closing();
    }

    /// <summary>
    /// Rollbacks Order, Invoice, Sale Return And Realized Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPost_Click(object sender, EventArgs e)
    {
        //if (IsDayClosed())
        //{
        //    UserController UserCtl = new UserController();

        //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
        //    this.Session.Clear();
        //    System.Web.Security.FormsAuthentication.SignOut();
        //    Response.Redirect("../Login.aspx");
        //}
        //else
        //{
        if (D_Date.Text != "")
        {
            if (DrpDocumentType.SelectedIndex == 3)
            {
                foreach (GridViewRow dr in GrdCheque.Rows)
                {
                    CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                    if (ChbInvoice.Checked == true)
                    {
                        ChequeEntryController CController = new ChequeEntryController();
                        DataControl dc = new DataControl();
                        CController.RollbackChequeEntry(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[6].Text, long.Parse(dr.Cells[0].Text), long.Parse(dr.Cells[5].Text), decimal.Parse(dr.Cells[8].Text));
                    }
                }
                LoadRollbackCheque();
            }
            else
            {
                foreach (GridViewRow dr in GrdOrder.Rows)
                {
                    CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                    if (ChbInvoice.Checked == true)
                    {
                        OrderEntryController ORD = new OrderEntryController();
                        DataControl dc = new DataControl();
                        ORD.UpdateRollBackDocument(Convert.ToInt64(GrdOrder.DataKeys[dr.RowIndex].Values["Document_ID"]), int.Parse(DrpDocumentType.SelectedValue.ToString()), int.Parse(DrpLenged.SelectedValue.ToString()));
                    }
                }
                this.LoadRollbackDocument();
            }
        }
    }

    /// <summary>
    /// Loads Order, Invoice, Sale Return And Realized Cheque Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        //if (IsDayClosed())
        //{
        //    UserController UserCtl = new UserController();

        //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
        //    this.Session.Clear();
        //    System.Web.Security.FormsAuthentication.SignOut();
        //    Response.Redirect("../Login.aspx");
        //}
        //else
        //{

            if (DrpDocumentType.SelectedIndex == 3)
            {
                this.LoadRollbackCheque();
                GrdOrder.Visible = false;
                GrdCheque.Visible = true;
            }
            else
            {
                GrdOrder.Visible = true;
                GrdCheque.Visible = false;
                this.LoadRollbackDocument();

            }
       // }
    }

    /// <summary>
    /// Loads OrderBookers To OrderBooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
        //DrpPrincipal.Focus();
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
    protected void Distributor_closing()
    {
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(drpDistributor.SelectedValue), 3);
        if (dtDayClose != null && dtDayClose.Rows.Count > 0)
        {

            D_Date.Text = Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]).ToString("dd-MMM-yyyy");
            hdfDisTypeID.Value = Convert.ToInt32(dtDayClose.Rows[0]["SUBZONE_ID"]).ToString();
        }
        else
        {
            D_Date.Text = "";
            hdfDisTypeID.Value = "0";
        }
    }
}
