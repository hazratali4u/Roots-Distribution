using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmSkipCheque : System.Web.UI.Page
{
    readonly DistributorController _dController = new DistributorController();
    readonly BankController _bnkController = new BankController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadChequBooks();
            LoadCheques(2);
            LoadGridCountry();
            btnSaveCountry.Attributes.Add("onclick", "return ValidateForm()");
            RemoveTextboxSuggessions();
        }
    }
    public void RemoveTextboxSuggessions()
    {
        try
        {
            txtremarks.Attributes.Add("autocomplete", "off");
        }
        catch (Exception)
        {
        }
    }
    public void clearControls()
    {
        try
        {
            drpBankAccount.Focus();
            txtremarks.Text = "";
            drpCheque.Enabled = true;
            drpBankAccount.Enabled = true;
            btnSaveCountry.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    private void LoadGridCountry()
    {
        try
        {
            Grid_Country.DataSource = null;
            Grid_Country.DataBind();

            DataTable dt = new DataTable();
            // dt = _bnkController.SelectCheckCancel(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue, 6);
            if (ddSearchType.SelectedValue == "CHEQUE_NO")
            {
                dt.DefaultView.RowFilter = "CHEQUE_NO LIKE '%" + txtSeach.Text + "%'";
            }
            else if (ddSearchType.SelectedValue == "REMARKS")
            {
                dt.DefaultView.RowFilter = "REMARKS LIKE '%" + txtSeach.Text + "%'";
            }
            Grid_Country.DataSource = dt;
            Grid_Country.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void LoadChequBooks()
    {
        try
        {
            Configuration.GetAccountHead();
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(drpBankAccount, dt, 0, 4, true);
            if (dt.Rows.Count > 0)
            {
                drpBankAccount.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void LoadCheques(int Type)
    {
        try
        {


            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Convert.ToInt32(drpBankAccount.SelectedItem.Value), null, null, null, true, Constants.DateNullValue, Type);
            clsWebFormUtil.FillDropDownList(this.drpCheque, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                drpCheque.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void drpBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCheques(2);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnSaveCountry_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSaveCountry.Text == "Save")
            {
                _bnkController.UpdateChequeStatus(Constants.IntNullValue, drpCheque.SelectedItem.Text, txtremarks.Text, 5);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record saved successfully');", true);
                clearControls();
            }
            else if (btnSaveCountry.Text == "Reactive")
            {
                _bnkController.UpdateChequeStatus(Constants.IntNullValue, drpCheque.SelectedItem.Text, txtremarks.Text, 2);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Cheque reactive successfully');", true);
                clearControls();
                btnSaveCountry.Text = "Save";
            }
            LoadChequBooks();
            LoadCheques(2);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
        LoadGridCountry();
    }

    protected void btnCancelCountry_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();
            LoadChequBooks();
            LoadCheques(2);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Grid_Country_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            drpCheque.Enabled = true;
            drpBankAccount.Enabled = true;
            GridViewRow gvr = Grid_Country.Rows[e.NewEditIndex];
            drpBankAccount.SelectedValue = gvr.Cells[3].Text;
            LoadCheques(3);
            drpCheque.SelectedValue = gvr.Cells[1].Text;
            txtremarks.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
            drpCheque.Enabled = false;
            drpBankAccount.Enabled = false;
            btnSaveCountry.Text = "Reactive";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Grid_Country_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_Country.PageIndex = e.NewPageIndex;
            LoadGridCountry();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnSearchGrid_Click(object sender, EventArgs e)
    {
        LoadGridCountry();
    }
}