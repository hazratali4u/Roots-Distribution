<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorCustomer.aspx.cs" Inherits="Forms_frmDistributorCustomer"
    Title="SAMS :: New Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function NameValidation(event) {
            // Allow: backspace, delete, tab and escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
                // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
                // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
                // Allow: Dash, Underscoor
            (event.keyCode == 189) ||
                //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
                //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function AddressValidation(event) {
            // Allow: backspace, delete, tab , escape and space bar
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
                // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
                // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
                // Allow: Dash, Underscoor
            (event.keyCode == 189) ||
                // Allow: Open bracket, Close bracket
            ((event.keyCode == 57 || event.keyCode == 48) && event.shiftKey === true) ||
                //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
                //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            (event.keyCode >= 96 && event.keyCode <= 105) || //Keypad numbers
                //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function PhoneValidation(event) {
            // Allow: backspace, delete, tab , escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
                // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
                // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
                // Allow: Dash
            (event.keyCode == 189 && event.shiftKey === false) ||
                //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
                //Keypad numbers
            (event.keyCode >= 96 && event.keyCode <= 105)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function pageLoad() {
            $('#<%=txtCNIC.ClientID %>').mask("99999-9999999-9");
            $('#<%=txtNTN.ClientID %>').mask("9999999-9");
            $('#<%=txtIsRegister.ClientID %>').mask("99-99-9999-999-99");

            $('#<%=Grid_users.ClientID %>').tablesorter(
	     {
	         headers: {
	             25: {
	                 sorter: false
	             }
	         }
	     }
	     );
            $("#<%=DrpDistributor.ClientID %>").searchable();
            $("#<%=DrpTown.ClientID %>").searchable();
            $("#<%=DrpRoute.ClientID %>").searchable();
            $("#<%=DrpMarket.ClientID %>").searchable();

            $("#<%=DrpBusinessType.ClientID %>").searchable();

        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
        }


        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCustomerName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Customer Name');
                return false;
            }
            str = document.getElementById('<%=txtContactPerson.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Contact Person Name');
                return false;
            }
            str = document.getElementById('<%=txtAddress.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Address');
                return false;
            }

            str = document.getElementById('<%=txtPhoneNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Phone No');
                return false;
            }
            str = document.getElementById('<%=txtRegdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Register Date');
                return false;
            }

            str = document.getElementById('<%=txtCNIC.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter CNIC #');
                return false;
            }

            str = document.getElementById('<%=txtNTN.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter NTN #');
                return false;
            }

            if ($('#<%= ChbIsRegister.ClientID %>').is(':checked')) {
                str = document.getElementById('<%=txtIsRegister.ClientID%>').value;
                if (str == null || str.length == 0) {
                    alert('Must enter STRN #');
                    return false;
                }
            }

            return true;
        }
        function SearchRecord() {
            var str;
            str = document.getElementById('<%=txtSeach.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Key Word for Searching');
                return false;
            }
            return true;
        }

    </script>
    <div id="right_data">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 100px">
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>--%>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 110px" align="left"></td>
                                            <td style="width: 175px">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td align="left"></td>
                                            <td style="width: 219px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Location</strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td align="left">
                                                <strong>Town</strong>
                                            </td>
                                            <td style="width: 219px">
                                                <asp:DropDownList ID="DrpTown" runat="server" Width="205px" CssClass="DropList" OnSelectedIndexChanged="DrpTown_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Route</strong>
                                            </td>
                                            <td style="width: 175px; height: 12px">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="205px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 12px"></td>
                                            <td style="height: 12px" align="left">
                                                <strong>Market</strong>
                                            </td>
                                            <td style="width: 219px; height: 12px">
                                                <asp:DropDownList ID="DrpMarket" runat="server" Width="206px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Channel Type</strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:DropDownList ID="drpChannelType" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblMobileNo" runat="server" Width="96px" Text="Business Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 219px">
                                                <asp:DropDownList ID="DrpBusinessType" runat="server" Width="206px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Promotion Class</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:DropDownList ID="DrpVolumeClass" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <strong>Contact Person</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:TextBox ID="txtContactPerson" runat="server" Width="200px" CssClass="txtBox"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Name</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtCustomerName" runat="server" Width="200px" CssClass="txtBox"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblAddress1" runat="server" Width="44px" Text="Address " CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:TextBox ID="txtAddress" runat="server" Width="200px" CssClass="txtBox " MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Phone No</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px" CssClass="txtBox" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td align="left">
                                                <strong>Register Date</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:TextBox ID="txtRegdate" runat="server" Width="200px" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>CNIC #</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtCNIC" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td align="left">
                                                <strong>NTN #</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:TextBox ID="txtNTN" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <asp:CheckBox ID="ChbIsRegister" runat="server" Width="96px" Text="Is Register" AutoPostBack="True"
                                                    OnCheckedChanged="ChbIsRegister_CheckedChanged" Font-Bold="true"></asp:CheckBox>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtIsRegister" runat="server" Width="200px" CssClass="txtBox " Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td align="left">
                                                <strong>Price Group</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:DropDownList ID="drpPriceGroup" runat="server" Width="205">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Designation</strong>
                                            </td>
                                            <td style="width: 175px; height: 12px">
                                                <asp:DropDownList ID="DrpDesignation" runat="server" Width="205px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDesignation_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 12px"></td>
                                            <td style="height: 12px" align="left">
                                                <strong>Employee</strong>
                                            </td>
                                            <td style="width: 219px; height: 12px">
                                                <asp:DropDownList ID="DrpEmployee" runat="server" Width="206px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="left">
                                                <strong>Payment Term Amount(%)</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtPaymentTermAmount" runat="server" Width="200px" CssClass="txtBox " Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5" rowspan="3"></td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="width: 175px">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="80px"
                                                    Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"
                                                    Font-Bold="true"></asp:CheckBox>
                                            </td>
                                            <td style="width: 219px"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                                <%--<div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 244px; height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>--%>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset; width: 100%; border-bottom: silver thin inset; background-color: silver">
                        <tbody>
                            <tr>
                                <td style="height: 21px" align="left">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                        <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                        <asp:ListItem Value="ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
                                        <asp:ListItem Value="GEO_NAME">Town Name</asp:ListItem>
                                        <asp:ListItem Value="AREA_NAME">Route Name</asp:ListItem>
                                        <asp:ListItem Value="ROUTE_NAME">Market Name</asp:ListItem>
                                        <asp:ListItem Value="SLASH_DESC">Channel Type</asp:ListItem>
                                        <asp:ListItem>CNIC</asp:ListItem>
                                        <asp:ListItem>NTN</asp:ListItem>
                                        <asp:ListItem Value="GST_NUMBER">STRN</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="height: 21px" align="left" width="250">
                                    <asp:Button ID="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue">
                            <RowStyle ForeColor="Black"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="CUSTOMER_ID" ReadOnly="true" HeaderText="Customer Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" ReadOnly="true" HeaderText="DISTRIBUTOR_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BUSINESS_TYPE_ID" ReadOnly="true" HeaderText="BUSINESS_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROMOTION_CLASS" ReadOnly="true" HeaderText="PROMOTION_CLASS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TOWN_ID" ReadOnly="true" HeaderText="TOWN_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_ID" ReadOnly="true" HeaderText="AREA_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_ID" ReadOnly="true" HeaderText="ROUTE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_CODE" ReadOnly="true" HeaderText="Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_NAME" ReadOnly="true" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NUMBER" ReadOnly="true" HeaderText="Contact Number">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ADDRESS" ReadOnly="true" HeaderText="Email">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" ReadOnly="true" HeaderText="ADDRESS">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_NUMBER" ReadOnly="true" HeaderText="Gst No">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ChannelType" ReadOnly="true" HeaderText="Channel Type">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GEO_NAME" ReadOnly="true" HeaderText="Town">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_NAME" ReadOnly="true" HeaderText="Route">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_NAME" ReadOnly="true" HeaderText="Market">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" ReadOnly="true" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGDATE" ReadOnly="true" DataFormatString="{0:dd-MMM-yyyy}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_STAND" ReadOnly="true" HeaderText="Stand">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_COOLER" ReadOnly="true" HeaderText="Cooler">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CNIC" ReadOnly="true" HeaderText="CNIC">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NTN" ReadOnly="true" HeaderText="NTN">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DESIGNATION_ID" ReadOnly="true" HeaderText="Designation">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPLOYEE_ID" ReadOnly="true" HeaderText="Employee">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("CUSTOMER_ID")%>' OnClick="btnEdit_Click" ToolTip="Edit">
                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:CommandField>--%>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                                Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333"></AlternatingRowStyle>
                        </asp:GridView>
                    </asp:Panel>
                    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
