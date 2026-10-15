using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Linq;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmDistributorCustomer : System.Web.UI.Page
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
            this.LoadTown();
            this.LoadRoute();
            this.LoadMarket();
            this.LoadChannelType();
            this.LoadBusinessType();
            this.LoadVolumeType();
            LoadGroup();
            Designation();
            Employees();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            btnSearch.Attributes.Add("onclick", "return SearchRecord()");
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtRegdate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadGroup()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectGroup();
        drpPriceGroup.Items.Add(new clsListItems("Default", "0"));
        clsWebFormUtil.FillDropDownList(drpPriceGroup, dt, 0, 1, false);
    }

    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Towns To Town Combo, Routes To Routes Comb And Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
        this.LoadRoute();
        this.LoadMarket();
        this.SetTableSorter();
        DrpDistributor.Focus();
    }

    /// <summary>
    /// Loads Towns To Town Combo
    /// </summary>
    protected void LoadTown()
    {
        if (DrpDistributor.Items.Count > 0)
        {
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(DrpDistributor.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 1, true);
        }
    }

    /// <summary>
    /// Loads Routes To Routes Comb And Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadRoute();
        this.LoadMarket();
        this.SetTableSorter();
        DrpTown.Focus();
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), null, null);
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        }
    }

    /// <summary>
    /// Loads Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadMarket();
        this.SetTableSorter();
        DrpRoute.Focus();
    }

    /// <summary>
    /// Loads Markets To Market Combo
    /// </summary>
    private void LoadMarket()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            DistributorRouteController gController = new DistributorRouteController();
            DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), long.Parse(DrpRoute.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpMarket, dt, 0, 8, true);
        }
        else
        {
            DrpMarket.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Channel Types To Channel Type Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Business Types To Business Type Combo
    /// </summary>
    private void LoadBusinessType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerTypeBusiness, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpBusinessType, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Promotion Classess To Promotion Class Combo
    /// </summary>
    private void LoadVolumeType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerVolumeClassType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpVolumeClass, dt, 0, 2, true);
        this.DrpVolumeClass.SelectedValue = "88";
    }

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadCustomer()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpMarket.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.UspSelectCustomer(int.Parse(DrpDistributor.SelectedValue.ToString()), ddSearchType.SelectedValue.ToString(), txtSeach.Text);
            this.Grid_users.DataSource = dt;
            this.Grid_users.DataBind();
        }
    }


    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        this.LoadCustomer();
        this.SetTableSorter();
    }

    /// <summary>
    /// Enables GST No TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbIsRegister_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbIsRegister.Checked == true)
        {
            txtIsRegister.Text = "";
            txtIsRegister.Enabled = true;
        }
        else
        {
            txtIsRegister.Text = "";
            txtIsRegister.Enabled = false;
        }
        this.SetTableSorter();
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerDataController mController = new CustomerDataController();

        if (btnSave.Text == "Save")
        {
            SETTINGS_TABLE_Controller mSettingsTableControl = new SETTINGS_TABLE_Controller();
            DataTable dtSettingsTable = mSettingsTableControl.Select_SETTINGS_TABLE("CUSTOMER", "CUSTOMER_ID", int.Parse(DrpDistributor.SelectedValue.ToString()));

            if (dtSettingsTable.Rows.Count > 0)
            {
                long CustomerId = long.Parse(dtSettingsTable.Rows[0]["Value"].ToString()) + 1;
                string StrCode = "";


                if (CustomerId.ToString().Length == 1)
                {
                    StrCode = "OT0000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 2)
                {
                    StrCode = "OT000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 3)
                {
                    StrCode = "OT00" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 4)
                {
                    StrCode = "OT0" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 5)
                {
                    StrCode = "OT" + CustomerId.ToString();
                }
                string TERM_AMOUNT = "";
                //if (txtPaymentTermAmount.Text.Equals(""))
                //{
                //    TERM_AMOUNT = "";
                //}
                //else
                //{
                //    TERM_AMOUNT = txtPaymentTermAmount.Text;
                //}

                mController.InsertCustomer(CustomerId, ChbIsRegister.Checked, chkIsActive.Checked,
                int.Parse(drpChannelType.SelectedValue.ToString()), int.Parse(DrpVolumeClass.SelectedValue.ToString()),
                int.Parse(DrpBusinessType.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(DrpMarket.SelectedValue.ToString()),
                int.Parse(DrpTown.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), txtIsRegister.Text, txtContactPerson.Text,
                txtPhoneNo.Text, "", StrCode, txtCustomerName.Text, txtAddress.Text, DateTime.Parse(txtRegdate.Text), 1, int.Parse(drpPriceGroup.SelectedValue), txtCNIC.Text, txtNTN.Text, int.Parse(DrpDesignation.SelectedValue.ToString()), int.Parse(DrpEmployee.SelectedValue.ToString())
                //, int.Parse(TERM_AMOUNT)
                );
                this.Session.Add("CustomerId", CustomerId);
            }
        }
        else
        {
            //string TERM_AMOUNT = "";
            //if (txtPaymentTermAmount.Text.Equals(""))
            //{
            //    TERM_AMOUNT = "";
            //}
            //else
            //{
            //    TERM_AMOUNT = txtPaymentTermAmount.Text;
            //}
            mController.UpdateCustomer(long.Parse(this.Session["CustomerId"].ToString()), ChbIsRegister.Checked, chkIsActive.Checked,
            int.Parse(drpChannelType.SelectedValue.ToString()), int.Parse(DrpVolumeClass.SelectedValue.ToString()),
            int.Parse(DrpBusinessType.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(DrpMarket.SelectedValue.ToString()),
            int.Parse(DrpTown.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), txtIsRegister.Text, txtContactPerson.Text,
            txtPhoneNo.Text, "", null, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue, 1, int.Parse(drpPriceGroup.SelectedValue), txtCNIC.Text, txtNTN.Text, int.Parse(DrpDesignation.SelectedValue.ToString()), int.Parse(DrpEmployee.SelectedValue.ToString())
            //,int.Parse(TERM_AMOUNT)
            );
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Update');", true);
        this.ClearAll();
    }

    /// <summary>
    /// Clears All Controls Through ClearAll() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    /// <summary>
    /// Filters Customer From Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadCustomer();
        this.SetTableSorter();
    }

    /// <summary>
    /// Set Customer Grid For JQuery Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (Grid_users.Rows.Count > 1)
        {
            Grid_users.UseAccessibleHeader = true;
            Grid_users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    /// <summary>
    /// Clears All  Constrols
    /// </summary>
    private void ClearAll()
    {
        txtCustomerName.Text = "";
        txtContactPerson.Text = "";
        txtAddress.Text = "";
        txtPhoneNo.Text = "";
        txtSeach.Text = "";
        txtIsRegister.Text = "";
        txtNTN.Text = string.Empty;
        txtCNIC.Text = string.Empty;
        btnSave.Text = "Save";
        Designation();
        Employees();
        Grid_users.DataSource = null;
        Grid_users.DataBind();

    }
    public void Designation()
    {
        Session["dt"] = null;
        Distributor_UserController _uController = new Distributor_UserController();
        if (DrpDistributor.Items.Count <= 0) return;
        DataTable dt = _uController.SelectDistributorUser(Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        Session.Add("dt", dt);
        var qry = from rows in dt.AsEnumerable().Where(x => x.Field<int>("DISTRIBUTOR_ID") == int.Parse(DrpDistributor.SelectedValue))
                  group rows by new { REF_ID = rows["REF_ID"], DESIGNATION_NAME = rows["SLASH_DESC"] } into grp
                  select grp.First();
        if (qry.Any())
        {
            var Blocks = qry.CopyToDataTable();
            DrpDesignation.DataSource = Blocks;
            DrpDesignation.DataTextField = "SLASH_DESC";
            DrpDesignation.DataValueField = "REF_ID";
            DrpDesignation.DataBind();
            ListItem myDefaultItem = new ListItem("--Select Designation--", "0");
            myDefaultItem.Selected = true;
            DrpDesignation.Items.Insert(0, myDefaultItem);
            //DrpDesignation.SelectedIndex = 0;
        }
        else
        {
            DrpDesignation.Items.Clear();
            DrpDesignation.Items.Add(new ListItem("--Select Designation--", "0"));
            DrpDesignation.SelectedIndex = 0;
        }
    }
    public void Employees()
    {
        DataTable dt = (DataTable)this.Session["dt"];
        var qry = from rows in dt.AsEnumerable().Where(x => x.Field<int>("DISTRIBUTOR_ID") == int.Parse(DrpDistributor.SelectedValue) && x.Field<int>("REF_ID") == int.Parse(DrpDesignation.SelectedValue))
                  group rows by new { USER_ID = rows["USER_ID"], USER_NAME = rows["USER_NAME"] } into grp
                  select grp.First();
        if (qry.Any())
        {
            var Blocks = qry.CopyToDataTable();
            DrpEmployee.DataSource = Blocks;
            DrpEmployee.DataTextField = "USER_NAME";
            DrpEmployee.DataValueField = "USER_ID";
            DrpEmployee.DataBind();
            ListItem myDefaultItem = new ListItem("--Select Employee--", "0");
            myDefaultItem.Selected = true;
            DrpEmployee.Items.Insert(0, myDefaultItem);
            //DrpEmployee.SelectedIndex = 0;
        }
        else
        {
            DrpEmployee.Items.Clear();
            DrpEmployee.Items.Add(new ListItem("--Select Employee--", "0"));
            DrpEmployee.SelectedIndex = 0;
        }
    }
    protected void DrpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Employees();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        GridViewRow Rows = (GridViewRow)(sender as LinkButton).NamingContainer;

        this.Session.Add("CustomerId", long.Parse(Rows.Cells[0].Text));

        DrpDistributor.SelectedValue = Rows.Cells[1].Text;
        DrpBusinessType.SelectedValue = Rows.Cells[2].Text;
        DrpVolumeClass.SelectedValue = Rows.Cells[3].Text;
        drpChannelType.SelectedValue = Rows.Cells[4].Text;
        this.LoadTown();
        DrpTown.SelectedValue = Rows.Cells[5].Text;
        this.LoadRoute();
        DrpRoute.SelectedValue = Rows.Cells[6].Text;
        this.LoadMarket();
        DrpMarket.SelectedValue = Rows.Cells[7].Text;
        txtCustomerName.Text = Rows.Cells[9].Text.Replace("amp;", "");
        txtContactPerson.Text = Rows.Cells[10].Text.Replace("&nbsp;", "");
        txtPhoneNo.Text = Rows.Cells[11].Text.Replace("&nbsp;", "");
        txtAddress.Text = Rows.Cells[13].Text.Replace("&nbsp;", "");
        txtIsRegister.Text = Rows.Cells[14].Text.Replace("&nbsp;", "");
        if (Rows.Cells[14].Text.Trim() == "&nbsp;")
        {
            txtIsRegister.Text = "";
            ChbIsRegister.Checked = false;
        }
        else
        {
            ChbIsRegister.Checked = true;
            txtIsRegister.Text = Rows.Cells[14].Text;
        }
        chkIsActive.Checked = bool.Parse(Rows.Cells[19].Text);
        txtRegdate.Text = Rows.Cells[20].Text.Replace("&nbsp;", "");
        txtCNIC.Text = Rows.Cells[23].Text.Replace("&nbsp;", "");
        txtNTN.Text = Rows.Cells[24].Text.Replace("&nbsp;", "");
        LoadGroup();
        drpPriceGroup.SelectedValue = Rows.Cells[22].Text.Replace("&nbsp;", "0");
        Designation();
        DrpDesignation.SelectedValue = Rows.Cells[25].Text.Replace("&nbsp;", "");
        Employees();
        DrpEmployee.SelectedValue = Rows.Cells[26].Text.Replace("&nbsp;", "");

        btnSave.Text = "Update";
        this.SetTableSorter();
    }
}
