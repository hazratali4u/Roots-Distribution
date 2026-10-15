using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;


public partial class Forms_frmTransporter : Page
{
    readonly DistributorController _dController = new DistributorController();
   
     static int _userId = Constants.IntNullValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
            LoadGrid();
            
        }

    }

    protected void LoadGrid()
    {        
        var dt = _dController.SelectTranspoter();
        Grid_users.DataSource = dt;
        Grid_users.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (_userId <= 0)
            {
                _userId = Constants.IntNullValue;
            }
            if (_dController.InsertTransporter(_userId, txtName.Text, txtEmail.Text, txtPhoneNo.Text, txtAddress.Text,chkIsActive.Checked))
            {
                LoadGrid();
                ClearControls();
                btnSave.Text = "Save";
            }
            else
            {
               
                lblErrorMsg.Text = "Plz Try Again!!!";
            }
        }

        catch (Exception ex)
        {
            lblErrorMsg.Text = "Plz Try Again!!!";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGrid();
        ClearControls();
        btnSave.Text = "Save";
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        var gvr = Grid_users.Rows[e.NewEditIndex];
        _userId = int.Parse(gvr.Cells[0].Text);

        txtName.Text = gvr.Cells[1].Text;

        txtPhoneNo.Text = Convert.ToString(gvr.Cells[2].Text.Replace("&nbsp;", ""));
        txtEmail.Text = Convert.ToString(gvr.Cells[3].Text.Replace("&nbsp;", ""));
        txtAddress.Text = Convert.ToString(gvr.Cells[4].Text.Replace("&nbsp;", ""));

        // chkIsActive.Checked = bool.Parse(gvr.Cells[10].Text);

        btnSave.Text = "Update";

    }

    protected void ClearControls()
    {

        txtName.Text = null;
        txtPhoneNo.Text = null;
        txtAddress.Text = null;
        txtEmail.Text = null;
        _userId = Constants.IntNullValue;
        lblErrorMsg.Text = null;
    }
}