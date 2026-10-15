using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;  
public partial class Forms_frmDesignationDepartment : System.Web.UI.Page
{
    private static int RefId;
   
    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Designation_Id, null, Constants.IntNullValue, bool.Parse("True"));
        GrdDesignation.DataSource = dt.DefaultView;
        GrdDesignation.DataBind();
    }
   
    
   
    private string GetAutoCode(string PreeFix, int CodeType, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, CValue);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
            this.LoadDesignation();
            
           
        }   
    }
    protected void btnSaveBusType_Click(object sender, EventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();

        if (btnSaveBusType.Text == "New")
        {
            //txtbustypeCode.Text = this.GetAutoCode("DS", 0, Constants.LongNullValue);
            txtbustypeName.Enabled = true;
            txtbustypeName.Focus();
            btnSaveBusType.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtbustypeName);
        }
        else if (btnSaveBusType.Text == "Save")
        {
            if (txtbustypeName.Text.Length == 0)
            {
                lblErrorMsgDivsion.Text = "Must Entry Designation";
                return;
            }
           mController.InsertSlashCodes(txtbustypeCode.Text, Constants.Employee_Designation_Id, txtbustypeName.Text, 1, true);
           //this.GetAutoCode("DS", 1, long.Parse(txtbustypeCode.Text.Substring(2)));
            
            this.LoadDesignation();
            txtbustypeCode.Text = "";
            txtbustypeName.Text = "";
            txtbustypeName.Enabled = false;
            btnSaveBusType.Text = "New";
        }
        else if (btnSaveBusType.Text == "Update")
        {
            mController.UpdateSlashCodes(RefId, null, Constants.Employee_Designation_Id, txtbustypeName.Text, 1, true, Constants.DateNullValue);
            this.LoadDesignation();
            txtbustypeCode.Text = "";
            txtbustypeName.Text = "";
            txtbustypeName.Enabled = false;
            btnSaveBusType.Text = "New";
        }
    }
    protected void GrdBusType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RefId = int.Parse(GrdDesignation.Rows[e.NewEditIndex].Cells[0].Text);
        txtbustypeCode.Text = GrdDesignation.Rows[e.NewEditIndex].Cells[1].Text;
        txtbustypeName.Text = GrdDesignation.Rows[e.NewEditIndex].Cells[2].Text;
        txtbustypeName.Enabled = true;
        btnSaveBusType.Text = "Update";

    }
    protected void GrdBusType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();
        RefId = int.Parse(GrdDesignation.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateSlashCodes(RefId, null, Constants.Employee_Designation_Id, null, 1, false, Constants.DateNullValue);
        this.LoadDesignation();
    }
   
    
}
