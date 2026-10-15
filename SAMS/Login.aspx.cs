using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Login : System.Web.UI.Page
{
    UserController mController = new UserController();
    RoleManagementController ObjRole = new RoleManagementController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToInt32(Session["UserID"]) > 0)
                {
                    Response.Redirect("Forms/Home.aspx");
                }
            }
            catch (Exception ex)
            {
                txtLogin.Focus();
            }
            
        }
    }
    
    public void ValidateUser()
    {

        if (txtLogin.Text == "" && txtPassword.Text == "")
        {
            return;
        }
        else
        {
            DataTable dt = mController.SelectSlashUser(txtLogin.Text, txtPassword.Text,3);
            this.Session.Clear();
            if (dt.Rows.Count > 0)
            {
                this.Session.Add("UserID", Convert.ToInt32(dt.Rows[0]["USER_ID"].ToString()));
                this.Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                this.Session.Add("DISTRIBUTOR_ID", Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("CompanyId", Convert.ToInt32(dt.Rows[0]["COMPANY_ID"].ToString()));
                this.Session.Add("RoleID", Convert.ToInt32(dt.Rows[0]["ROLE_ID"]));
                LastClosedDay(Convert.ToInt32(dt.Rows[0]["USER_ID"]), Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("UserName2", dt.Rows[0]["USER_NAME"].ToString());
                this.Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                this.Session.Add("DistTypeID", dt.Rows[0]["DistTypeID"].ToString());
                long User_Log_ID = mController.InsertUserLoginTime(Convert.ToInt32(dt.Rows[0]["USER_ID"]));
                this.Session.Add("CodeFile", DISTRIBUTOR_CHECK());
                this.Session.Add("User_Log_ID", User_Log_ID);
                Response.Redirect("Forms/Home.aspx");
            }
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Wrong User Id/Password";
                return;
            }
        }
    }

    private void LastClosedDay(int UserId, int p_Distributor)
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(UserId, p_Distributor);
        if (dt.Rows.Count > 0)
        {
            this.Session.Add("CurrentWorkDate", DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()));

        }
        else
        {
            this.Session.Add("CurrentWorkDate", DateTime.Now);
        }        
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        ValidateUser();
    }

    private string DISTRIBUTOR_CHECK()
    {
        #region DISTRIBUTOR CHECK
        CustomerDataController cd = new CustomerDataController();
        string CodeFileName = "";
        DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);
        if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
        {
            CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
        }
        else
        {
            CodeFileName = "";
        }
        return CodeFileName;
        #endregion
    }
}