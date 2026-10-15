using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmChequeBook : System.Web.UI.Page
{
    readonly DistributorController _dController = new DistributorController();
    readonly BankController _bnkController = new BankController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAccounts();
            LoadGridCountry();
            LoadAccountHead();
            btnSaveCountry.Attributes.Add("onclick", "return ValidateForm()");
            RemoveTextboxSuggessions();
            txtIssueDate.Attributes.Add("readonly", "readonly");
            txtIssueDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
        }
    }
    public void clearControls()
    {
        try
        {
            drpAccountno.Focus();
            txtCheckfrom.Text = "";
            txtcheckto.Text = "";
            txtremarks.Text = "";
            txtBranchCode.Text = "";
            txtBranchNo.Text = "";
            txtCPNumber.Text = "";
            txtAccountTitle.Text = "";
            txtIBAN.Text = "";
            txtPrefix.Text = "";
            btnSaveCountry.Text = "Save";
            chkIsActive.Checked = true;
            txtCheckfrom.ReadOnly = false;
            txtcheckto.ReadOnly = false;
            hfStatus.Value = "0";
        }
        catch (Exception)
        {
        }
    }
    private void LoadAccountHead()
    {
        try
        {
            Configuration.GetAccountHead();
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(drpAccountno, dt, 0, 4, true);
            if (dt.Rows.Count > 0)
            {
                drpAccountno.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void RemoveTextboxSuggessions()
    {
        try
        {
            txtCheckfrom.Attributes.Add("autocomplete", "off");
            txtcheckto.Attributes.Add("autocomplete", "off");
            txtremarks.Attributes.Add("autocomplete", "off");
            //txtSearch.Attributes.Add("autocomplete", "off");
            txtBranchCode.Attributes.Add("autocomplete", "off");
            txtBranchNo.Attributes.Add("autocomplete", "off");
            txtCPNumber.Attributes.Add("autocomplete", "off");
            txtAccountTitle.Attributes.Add("autocomplete", "off");
            txtIBAN.Attributes.Add("autocomplete", "off");
        }
        catch (Exception)
        {
        }
    }
    public void LoadAccounts()
    {
        try
        {
            //DataTable dt = _bnkController.SelectBankAccount(Constants.IntNullValue, null, null, null, null, null, Constants.DecimalNullValue, null, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue);
            //clsWebFormUtil.FillDropDownList(this.drpAccountno, dt, 0, 15, true);
        }
        catch (Exception)
        {
        }
    }
    private void LoadGridCountry()
    {
        try
        {
            Grid_Country.DataSource = null;
            Grid_Country.DataBind();

            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue, 1);
            //if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            //{
            //    dt.DefaultView.RowFilter = "ACCOUNT_DETAIL LIKE '%" + txtSearch.Text + "%' OR CHECK_FROM LIKE '%" + txtSearch.Text + "%' OR CHECK_TO LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '%" + txtSearch.Text + "%'";
            //}
            Grid_Country.DataSource = dt;
            Grid_Country.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        LoadGridCountry();
    }


    public string ExtractNumber(string original)
    {
        return new string(original.Where(c => Char.IsDigit(c)).ToArray());
    }
    protected void btnSaveCountry_Click(object sender, EventArgs e)
    {
        try
        {
            string ChqAlpha = txtPrefix.Text;

            //string Checkfrom = "";
            //Checkfrom = String.Join(String.Empty, txtCheckfrom.Text.Select(c => ((int)c).ToString()).ToArray());
            //string Checkto = "";
            //Checkto = String.Join(String.Empty, txtcheckto.Text.Select(c => ((int)c).ToString()).ToArray());

            //string str = txtCheckfrom.Text;
            //char ch = ' ';
            //int Checkfrom = 0;
            //for (int i = 0; i < str.Length; i++)
            //{
            //    ch = str[i];
            //    Checkfrom = (int)ch;
            //    //Console.WriteLine(Checkfrom);
            //}
            //string str2 = txtcheckto.Text;
            //char ch2 = ' ';
            //int Checkto = 0;
            //for (int i = 0; i < str2.Length; i++)
            //{
            //    ch2 = str2[i];
            //    Checkto = (int)ch2;
            //    //Console.WriteLine(Checkfrom);
            //}

            long ChqFromNo = Convert.ToInt64(ExtractNumber(txtCheckfrom.Text));
            long ChqToNo = Convert.ToInt64(ExtractNumber(txtcheckto.Text));
            if (ChqFromNo > ChqToNo)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Invalid cheque range');", true);
                return;
            }
            if (btnSaveCountry.Text == "Save")
            {
                _bnkController.InsertCheckBook(Convert.ToInt32(drpAccountno.SelectedItem.Value), ChqFromNo, ChqToNo, txtremarks.Text, chkIsActive.Checked, DateTime.Parse(txtIssueDate.Text), txtBranchCode.Text, txtBranchNo.Text, txtIBAN.Text, txtAccountTitle.Text, txtCPNumber.Text, ChqAlpha);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record saved successfully');", true);
                clearControls();
            }
            else if (btnSaveCountry.Text == "Update")
            {
                _bnkController.UpdateCheckBook(Convert.ToInt32(Session["BK_ID"]), Convert.ToInt32(drpAccountno.SelectedItem.Value), ChqFromNo, ChqToNo, txtremarks.Text, chkIsActive.Checked, DateTime.Parse(txtIssueDate.Text), 1, Convert.ToInt32(hfStatus.Value), ChqAlpha, txtBranchCode.Text, txtBranchNo.Text, txtIBAN.Text, txtAccountTitle.Text, txtCPNumber.Text);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully');", true);
                clearControls();
                btnSaveCountry.Text = "Save";
            }
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
            LoadAccountHead();
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
            hfStatus.Value = "0";
            txtCheckfrom.ReadOnly = false;
            txtcheckto.ReadOnly = false;
            GridViewRow gvr = Grid_Country.Rows[e.NewEditIndex];
            Session.Add("BK_ID", Convert.ToInt32(gvr.Cells[2].Text));
            drpAccountno.SelectedValue = gvr.Cells[3].Text;

            txtBranchCode.Text = gvr.Cells[10].Text.Replace("&nbsp;", "");
            txtBranchNo.Text = gvr.Cells[11].Text.Replace("&nbsp;", "");
            txtCPNumber.Text = gvr.Cells[13].Text.Replace("&nbsp;", "");
            txtAccountTitle.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            txtIBAN.Text = gvr.Cells[12].Text.Replace("&nbsp;", "");
            txtCheckfrom.Text = ExtractNumber(gvr.Cells[5].Text);
            txtcheckto.Text = ExtractNumber(gvr.Cells[6].Text);
            txtIssueDate.Text = Convert.ToDateTime(gvr.Cells[7].Text).ToString("dd-MMM-yyyy");
            txtremarks.Text = gvr.Cells[8].Text;
            txtPrefix.Text = gvr.Cells[15].Text.Replace("&nbsp;", ""); 
            if (gvr.Cells[9].Text == "Active")
            {
                chkIsActive.Checked = true;
            }
            else
            {
                chkIsActive.Checked = false;
            }
            if (gvr.Cells[14].Text != "0")
            {
                txtCheckfrom.ReadOnly = true;
                txtcheckto.ReadOnly = true;
                hfStatus.Value = "1";
            }
            btnSaveCountry.Text = "Update";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
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
        LoadGridForSearch();
    }
    private void LoadGridForSearch()
    {
        try
        {
            Grid_Country.DataSource = null;
            Grid_Country.DataBind();

            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue, 1);
            if (ddSearchType.SelectedValue == "ACCOUNT_TITLE")
            {
                dt.DefaultView.RowFilter = "ACCOUNT_TITLE LIKE '%" + txtSeach.Text + "%'";
                //OR CHECK_FROM LIKE '%" + txtSeach.Text + "%' OR CHECK_TO LIKE '%" + txtSeach.Text + "%' OR IS_ACTIVE LIKE '%" + txtSeach.Text + "%'";
            }
            else if (ddSearchType.SelectedValue == "ACCOUNT_DETAIL")
            {
                dt.DefaultView.RowFilter = "ACCOUNT_DETAIL LIKE '%" + txtSeach.Text + "%'";
            }
            else if (ddSearchType.SelectedValue == "CP_NUMBER")
            {
                dt.DefaultView.RowFilter = "CP_NUMBER LIKE '%" + txtSeach.Text + "%'";
            }
            else if (ddSearchType.SelectedValue == "IBAN")
            {
                dt.DefaultView.RowFilter = "IBAN LIKE '%" + txtSeach.Text + "%'";
            }
            Grid_Country.DataSource = dt;
            Grid_Country.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
}