using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmStockRegisterAdjustment : System.Web.UI.Page
{
    static string location = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            
            LoadSKU();

            string strProcessScript = "this.value='Wait...';this.disabled=true;";
            btnViewPDF.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnViewPDF, "").ToString());
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }

   
    private void LoadSKU()
    {
        ddlSKU.Items.Clear();
        SkuController mSKUController = new SkuController();
        DataTable m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        ddlSKU.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlSKU, m_SKUDt, "SKU_ID", "skuDetail", false);

    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, false);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                location += dr[0].ToString() + ',';
            }
        }
    }

    /// <summary>
    /// Shows Stock Reconciliation in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    public DateTime Lastdate;
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        int Distributor_ID = int.Parse(drpDistributor.SelectedValue);
        int SKU_ID = int.Parse(ddlSKU.SelectedValue);
        DataTable dt = SKUList(Distributor_ID, SKU_ID);
        try
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int SKUID = int.Parse(row["SKU_ID"].ToString());
                    DataTable dtopening = Opening(Distributor_ID, SKUID);
                    if (dtopening.Rows.Count > 0)
                    {
                        int Opeing_QTY = int.Parse(dtopening.Rows[0]["QTY"].ToString());
                        DateTime StartDate = DateTime.Parse(dtopening.Rows[0]["TIME_STAMP"].ToString());
                        DateTime EndDate = DateTime.Parse(txtEndDate.Text);
                        DataTable Stock = Stock_Resig(Distributor_ID, SKUID, StartDate, EndDate);
                        bool First = true;
                        DateTime date;

                        int QTY;
                        int LastClosing = 0;
                        foreach (DataRow StckRow in Stock.Rows)
                        {
                            if (First)
                            {
                                date = StartDate;
                                QTY = Opeing_QTY;
                                First = false;
                            }
                            else
                            {
                                date = DateTime.Parse(StckRow["STOCK_DATE"].ToString());
                                QTY = LastClosing;
                            }
                            int Val = RptInventoryCtl.UpdateStockData(Distributor_ID, SKUID, date, QTY);
                            LastClosing = Val;
                            Lastdate = DateTime.Parse(StckRow["STOCK_DATE"].ToString());
                        }
                        UpdateClosing(Distributor_ID, SKUID, Lastdate, 0);
                    }
                    else
                    {
                        DataTable dtClosing = Closing(Distributor_ID, SKUID);
                        if (dtClosing.Rows.Count > 0)
                        {
                            DateTime StartDate = DateTime.Parse(dtClosing.Rows[0]["STOCK_DATE"].ToString());
                            DateTime EndDate = DateTime.Parse(txtEndDate.Text);
                            DataTable Stock = Stock_Resig(Distributor_ID, SKUID, StartDate, EndDate);
                            bool First = true;
                            DateTime date;
                            int QTY = 0;
                            int LastClosing = 0;
                            foreach (DataRow StckRow in Stock.Rows)
                            {
                                if (First)
                                {
                                    date = StartDate;
                                    int Value = RptInventoryCtl.UpdateStockData(Distributor_ID, SKUID, date, QTY);
                                    LastClosing = Value;
                                    First = false;
                                }
                                else
                                {
                                    date = DateTime.Parse(StckRow["STOCK_DATE"].ToString());
                                    QTY = LastClosing;
                                    int Val = RptInventoryCtl.UpdateStockData(Distributor_ID, SKUID, date, QTY);
                                    LastClosing = Val;
                                    Lastdate = DateTime.Parse(StckRow["STOCK_DATE"].ToString());
                                }
                            }
                            UpdateClosing(Distributor_ID, SKUID, Lastdate, 0);
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Stock Register updated successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message + "');", true);
        }
        
    }
    public DataTable SKUList(int Distributor_ID, int SKU_ID)
    {
        DataTable dt = new DataTable();
        StringBuilder qry = new StringBuilder();
        int rows_returned;
        if (SKU_ID == Constants.IntNullValue)
        {
            qry.Append(@"SELECT SKU_ID FROM SKU_STOCK_REGISTER
                    WHERE DISTRIBUTOR_ID = " + Distributor_ID + " GROUP BY SKU_ID ORDER BY SKU_ID ASC");
        }
        else
        {
            qry.Append(@"SELECT SKU_ID FROM SKU_STOCK_REGISTER
                    WHERE DISTRIBUTOR_ID = " + Distributor_ID + " AND (SKU_ID = " + SKU_ID + " OR " + SKU_ID + " IS NULL) GROUP BY SKU_ID ORDER BY SKU_ID ASC");
        }
        using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
        using (SqlCommand cmd = connection.CreateCommand())
        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        {
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            connection.Open();
            rows_returned = sda.Fill(dt);
            connection.Close();
        }
        return dt;
    }

    public DataTable Opening(int Distributor_ID, int SKU_ID)
    {
        DataTable dt = new DataTable();
        StringBuilder qry = new StringBuilder();
        int rows_returned;
        qry.Append(@"SELECT ISNULL(QUANTITY + (QUANTITY_CTN * S.UNITS_IN_CASE),0) AS QTY 
                    ,* FROM PURCHASE_DETAIL PD
                    INNER JOIN SKUS S ON PD.SKU_ID = S.SKU_ID
                    WHERE PD.DISTRIBUTOR_ID = " + Distributor_ID + " AND PD.TYPE_ID = 7 AND PD.SKU_ID = " + SKU_ID + "");
       
        using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
        using (SqlCommand cmd = connection.CreateCommand())
        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        {
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            connection.Open();
            rows_returned = sda.Fill(dt);
            connection.Close();
        }
        return dt;
    }

    public DataTable Stock_Resig( int Distributor_ID, int SKU_ID, DateTime StratDate, DateTime EndDate)
    {
        DataTable dt = new DataTable();
        StringBuilder qry = new StringBuilder();
        int rows_returned;

        qry.Append(@"SELECT * FROM SKU_STOCK_REGISTER
                    WHERE DISTRIBUTOR_ID = " + Distributor_ID + " AND SKU_ID = " + SKU_ID + " AND STOCK_DATE >= '" + StratDate + "' AND STOCK_DATE <= '" + EndDate + "' ORDER BY STOCK_DATE ASC");

        using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
        using (SqlCommand cmd = connection.CreateCommand())
        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        {
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            connection.Open();
            rows_returned = sda.Fill(dt);
            connection.Close();
        }
        return dt;
    }

    public void UpdateOpening(int Distributor_ID, int SKU_ID, DateTime StratDate, int Qty)
    {
        StringBuilder qry = new StringBuilder();
        //qry.Append(@"UPDATE SKU_STOCK_REGISTER SET OPENING_STOCK = " + Qty + " WHERE DISTRIBUTOR_ID = " + Distributor_ID + " AND SKU_ID = " + SKU_ID + " AND STOCK_DATE = '"+ StratDate + "'");
        using (SqlConnection con = new SqlConnection(Configuration.ConnectionString))
        {
            con.Open();
            using (SqlCommand command = new SqlCommand("UPDATE SKU_STOCK_REGISTER SET OPENING_STOCK = " + Qty + " WHERE DISTRIBUTOR_ID = " + Distributor_ID + " AND SKU_ID = " + SKU_ID + " AND STOCK_DATE = '"+ StratDate + "'", con))
            {
                command.ExecuteNonQuery();
            }
            
            con.Close();
        }

        //using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
        //using (SqlCommand cmd = connection.CreateCommand())
        //using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //{
        //    cmd.CommandText = qry.ToString();
        //    cmd.CommandType = CommandType.Text;
        //    connection.Open();
        //    rows_returned = sda.Fill(dt);
        //    connection.Close();
        //}
    }

    public void UpdateClosing(int Distributor_ID, int SKU_ID, DateTime LastDate, int Qty)
    {
        StringBuilder qry = new StringBuilder();
        using (SqlConnection con = new SqlConnection(Configuration.ConnectionString))
        {
            con.Open();
            using (SqlCommand command = new SqlCommand("UPDATE SKU_STOCK_REGISTER SET CLOSING_STOCK = " + 0 + " WHERE DISTRIBUTOR_ID = " + Distributor_ID + " AND SKU_ID = " + SKU_ID + " AND STOCK_DATE = '" + LastDate + "'", con))
            {
                command.ExecuteNonQuery();
            }

            con.Close();
        }
    }

    //public void UpdateStockItems(int Distributor_ID, int SKU_ID, DateTime StratDate, int Qty)
    //{
    //    DataTable dt = new DataTable();
    //    StringBuilder qry = new StringBuilder();
    //    int rows_returned;

    //    qry.Append(@"UPDATE SKU_STOCK_REGISTER SET OPENING_STOCK = " + Qty + " WHERE DISTRIBUTOR_ID = " + Distributor_ID + " AND SKU_ID = " + SKU_ID + " AND STOCK_DATE = '" + StratDate + "'");

    //    using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
    //    using (SqlCommand cmd = connection.CreateCommand())
    //    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //    {
    //        cmd.CommandText = qry.ToString();
    //        cmd.CommandType = CommandType.Text;
    //        connection.Open();
    //        rows_returned = sda.Fill(dt);
    //        connection.Close();
    //    }
    //}

    public DataTable Closing(int Distributor_ID, int SKU_ID)
    {
        DataTable dt = new DataTable();
        StringBuilder qry = new StringBuilder();
        int rows_returned;
        qry.Append(@"SELECT S.SKU_CODE
                    ,R.SKU_ID
                    ,R.DISTRIBUTOR_ID
                    ,R.STOCK_DATE
                    ,R.OPENING_STOCK
                    ,R.PURCHASED
                    ,R.PURCHASE_RETURN
                    ,R.SALED
                    ,R.SALE_RETURN
                    ,R.TRANSFER_IN
                    ,R.TRANSFER_OUT
                    ,R.FREE_STOCK_IN
                    ,R.FREE_STOCK_OUT
                    ,R.DAMAGED_STOCK
                    ,R.DAMAGED_STOCK_RETURN
                    ,R.SHORT
                    ,R.EXCESS
                    ,R.CLOSING_STOCK
                    FROM SKU_STOCK_REGISTER R
                    INNER JOIN SKUS S ON R.SKU_ID = S.SKU_ID
                    WHERE R.DISTRIBUTOR_ID = " + Distributor_ID + " AND OPENING_STOCK = 0 AND CLOSING_STOCK > 0 AND R.SKU_ID = " + SKU_ID + "");

        using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
        using (SqlCommand cmd = connection.CreateCommand())
        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        {
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            connection.Open();
            rows_returned = sda.Fill(dt);
            connection.Close();
        }
        return dt;
    }
}