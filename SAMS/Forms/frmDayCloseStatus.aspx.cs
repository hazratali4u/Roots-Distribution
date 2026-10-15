using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Day Close
/// </summary>
public partial class Forms_frmDayCloseStatus : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()));
            if (Grid_Hierarchy.Rows.Count > 1)
            {
                Grid_Hierarchy.UseAccessibleHeader = true;
                Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            btnDayClose.Visible = true;
        }
        else
        {
           this.Session.Add("CurrentWorkDate", DateTime.Now);
           rblDistributorTypes.Visible = true;
        }
        GetLastClosedDay(UserId, p_Distributor, 0);       
    }
    
    
    private void GetLastClosedDay(int UserId, int p_Distributor, int p_Status)
    {
        DataTable dtable = mController.MaxDayClose(int.Parse(this.Session["UserId"].ToString()), p_Status);
        Grid_Hierarchy.DataSource = dtable;
        Grid_Hierarchy.DataBind();
    }
    
    
    protected void rblDistributorTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()), Convert.ToInt32(rblDistributorTypes.SelectedValue));
        if (Grid_Hierarchy.Rows.Count > 1)
        {
            Grid_Hierarchy.UseAccessibleHeader = true;
            Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

 
    protected void btnDayClose_Click(object sender, EventArgs e)
    {
        if (btnDayClose.Visible == true)
        {
            DistributorController mDayClose = new DistributorController();
            PurchaseController mPurchase = new PurchaseController();
            bool flag = false;
            bool check = false;
            foreach (GridViewRow dr2 in Grid_Hierarchy.Rows)
            {
                var chRelized2 = (CheckBox)dr2.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized2.Checked)
                {
                    check = true;
                    break;
                }

            }
            if (!check)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select location first');", true);
                return;
            }
            foreach (GridViewRow dr in Grid_Hierarchy.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked)
                {
                    DataTable ChkTransfer = mPurchase.SelectPurchaseDocumentNo(Constants.Document_Transfer_Out, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0, int.Parse(dr.Cells[6].Text));
                    if (ChkTransfer.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Transfer in is pendding for " + dr.Cells[1].Text + ". Please perform the transection first.');", true);
                        flag = false;
                        return;
                    }
                    flag = true;
                }
            }
            foreach (GridViewRow dr in Grid_Hierarchy.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked)
                {
                    //DateTime Cdt = Convert.ToDateTime(dr.Cells[4].Text + "00:00:000");
                    string date = dr.Cells[4].Text.Replace("-", "/");
                    DateTime Date = DateTime.Parse(ConvertDate.British_To_American(date));
                    DateTime DateAdded = Date.AddDays(1);
                    bool dt = mDayClose.UspDayClose(DateAdded, int.Parse(dr.Cells[6].Text), int.Parse(this.Session["UserID"].ToString()));
                    if (dt == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in day Close Contact System Administrator');", true);
                    }
                    else
                    {
                        UserController mController = new UserController();
                        if (mController.InsertUserLogoutTime(Convert.ToInt64(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"])) == "Logout Time Inserted")
                        {
                            //this.Session.Clear();
                            //Response.Redirect("~/Login.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in day Close Contact System Administrator');", true);
                        }
                    }
                }
            }

            if (flag)
            {
                this.Session.Clear();
                Response.Redirect("~/Login.aspx");
            }


        }
    }
}
