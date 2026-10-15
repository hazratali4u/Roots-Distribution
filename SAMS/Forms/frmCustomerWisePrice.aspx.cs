using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

using System.Collections.Generic;

public partial class Forms_frmCustomerWisePrice : System.Web.UI.Page
{
    #region Variables

    DataTable PurchaseSKU;
    readonly DataControl dc = new DataControl();
    private static int RowId;

    #endregion

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            LoadGroup();
            LoadSKUDetail();
            CreatTable();
            // LoadDistributor();
            txtDateEfected.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
        }
    }

    /// <summary>
    /// Creates Datatable For Order, Invoice And Sale Return
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("SKU_ARTICAL_NO", typeof(string));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("DATE_EFFECTED", typeof(string));

        Session.Add("PurchaseSKU", PurchaseSKU);
    }

    /// <summary>
    /// Loads SKU Data To SKU ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        try
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, Constants.DateNullValue);
            clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, 0, 3, true);

            Session.Add("Dtsku_Price", Dtsku_Price);
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        // txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtUnitPrice.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        hfBatchValue.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtArticalNO.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtGst.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
    
        txtDateEfected.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        //txtskuCode.Enabled = false;
        hfSKUID.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        btnSave.Text = "Update SKU";
    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        Session.Add("PurchaseSKU", PurchaseSKU);
        LoadGird();
    }

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSKU()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;
    }

  

    /// <summary>
    /// Clears Session Variables And Redircts To Step1
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        ClearAll();
        Session.Remove("PurchaseSKU");
        txtGroupPrice.Text = "";
        txtDateEfected.Text = "";

    }

    /// <summary>
    /// Adds/Updates Row To Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        

        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            PurchaseSKU = (DataTable)Session["PurchaseSKU"];

            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["UNIT_PRICE"] = decimal.Parse(dc.chkNull_0(txtUnitPrice.Text));
                    dr["SKU_ARTICAL_NO"] = int.Parse(dc.chkNull_0(txtArticalNO.Text));
                    dr["GST_RATE"] = decimal.Parse(dc.chkNull_0(txtGst.Text));
                    dr["DATE_EFFECTED"] = txtDateEfected.Text;
                    PurchaseSKU.Rows.Add(dr);

                    Session.Add("PurchaseSKU", PurchaseSKU);
                    LoadGird();
                    ClearAll();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU Already Exists')", true);
                }
            }
            else
            {
                DataRow dr = PurchaseSKU.Rows[RowId];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["UNIT_PRICE"] = decimal.Parse(dc.chkNull_0(txtUnitPrice.Text));
                dr["SKU_ARTICAL_NO"] = int.Parse(dc.chkNull_0(txtArticalNO.Text));
                dr["GST_RATE"] = decimal.Parse(dc.chkNull_0(txtGst.Text));
                dr["DATE_EFFECTED"] = txtDateEfected.Text;
                Session.Add("PurchaseSKU", PurchaseSKU);
                LoadGird();
                ClearAll();
            }

           

        }
        ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {

        
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];

        OrderEntryController mOrderController = new OrderEntryController();
        bool IsValidInsert = true;
        if (PurchaseSKU.Rows.Count > 0)
        {

            if (btnSaveOrder.ToolTip.Equals("Save Group"))
            {
                IsValidInsert = mOrderController.Add_CustomerWisePrice(PurchaseSKU, int.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtGroupPrice.Text, Constants.DateNullValue, ChbIsActive.Checked);
            }
            else if (btnSaveOrder.ToolTip.Equals("Update Group"))
            {
                //edit code Update_CustomerWisePrice
                IsValidInsert = mOrderController.Update_CustomerWisePrice(PurchaseSKU, int.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtGroupPrice.Text, Convert.ToInt32(drpAddEditGroup.SelectedValue), Constants.DateNullValue, ChbIsActive.Checked);

            }
            if (IsValidInsert)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Updated !')", true);

                ClearMasterALL();
                LoadGroup();
                txtGroupPrice.Text = "";
                txtDateEfected.Text = "";
                txtGst.Text = "";
                btnSaveOrder.ToolTip = "Save Group";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred !')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No Detail Found !')", true);
        }
    }

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {
        //txtskuCode.Text = "";
        //txtskuName.Text = "";
        txtUnitPrice.Text = "";
        btnSave.Text = "Add Sku";
        txtArticalNO.Text = "";
    //    txtGst.Text = "";
     //   txtDateEfected.Text = "";
      //  txtGroupPrice.Text = "";

        //txtskuCode.Enabled = true;

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        Session.Remove("PurchaseSKU");
        ChbIsActive.Checked = false;
        CreatTable();
        LoadGird();
    }

    protected void drpAddEditGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (long.Parse(drpAddEditGroup.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            DataTable dtGroup = (DataTable)Session["dtGroup"];
            DataRow[] foundRows = dtGroup.Select("CUST_WISE_PRISE_MASTER_ID   = '" + drpAddEditGroup.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
                if (bool.Parse(foundRows[0]["IS_DELETED"].ToString()))
                {
                    ChbIsActive.Checked = true;
                }
                else
                {
                    ChbIsActive.Checked = false;
                }
            }
            ClearAll();
            string strtemp = drpAddEditGroup.SelectedItem.ToString();
            string[] words = strtemp.Split('-');
            txtGroupPrice.Text = words[1];
            btnSaveOrder.ToolTip = "Update Group";
            int temp = Convert.ToInt32(drpAddEditGroup.SelectedValue);
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectGroupDetail(Convert.ToInt32(drpAddEditGroup.SelectedValue));
            //DataTable dt = (DataTable)Session["dtOrder"];
            // DataRow[] foundRows = dt.Select("SKU_ID   = '" + drpAddEditGroup.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {

                GrdPurchase.DataSource = dt;
                GrdPurchase.DataBind();

                Session.Add("PurchaseSKU", dt);
                txtDateEfected.Text = Convert.ToDateTime(dt.Rows[0]["DATE_EFFECTED"]).ToString("dd-MMM-yyyy");
                
              
                ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
                // //RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
            }
        }
        else
        {
            CreatTable();
            LoadGird();
            btnSaveOrder.ToolTip = "Save Group";
            ClearAll();
            ClearMasterALL();
            txtDateEfected.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        }


    }

    private void LoadGroup()
    {
        drpAddEditGroup.Items.Clear();
        DistributorController DController = new DistributorController();
        try
        {
            DataTable dt = DController.SelectGroup();
            drpAddEditGroup.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(drpAddEditGroup, dt, 0, 1);
            Session.Add("dtGroup", dt);
        }
        catch (Exception)
        {
            
            throw;
        }
    }

}
