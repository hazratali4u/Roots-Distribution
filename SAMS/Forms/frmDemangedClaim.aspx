<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDemangedClaim.aspx.cs" Inherits="Forms_frmDemangedClaim" Title="SAMS :: Customer Claim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
     <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }


        function pageLoad() {
            <%--$("#<%=DrpPrincipal.ClientID %>").searchable();--%>
            $("#<%=drpDistributor.ClientID %>").searchable();
            $("#<%=DrpRoute.ClientID %>").searchable();
            $("#<%=DrpCustomer.ClientID %>").searchable();
            $("#<%=DrpAccountHead.ClientID %>").searchable();
        }

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }

        }
 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div style="z-index: 101; left: 487px; width: 100px; position: absolute; top: 200px;
                            height: 100px">
                            <asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    TargetControlID="txtAmount" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddNew">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Claim Type" Width="65px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RbdClaimType" runat="server" RepeatDirection="Horizontal"
                                                    Width="215px" AutoPostBack="True" OnSelectedIndexChanged="RbdClaimType_SelectedIndexChanged">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" CssClass="lblbox" Text="Principal" Width="68px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="250px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Location"
                                                        Width="65px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Route" Width="82px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="250px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Customer" Width="82px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="250px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Account Head" Width="82px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="DrpAccountHead" runat="server" Width="250px" CssClass="DropList"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="100px" Text="Transaction Date" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:TextBox ID="txtTransactionDate" runat="server" Width="210px" CssClass="txtBox" OnTextChanged="txtTransactionDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" Style="vertical-align:middle;" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txtTransactionDate"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Remarks" Width="71px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Width="243px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="63px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="107px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                            </td>
                                            <td align="left" colspan="1" style="height: 20px">
                                                <asp:Button ID="btnAddNew" runat="server" AccessKey="S" Font-Size="8pt" OnClick="btnAddNew_Click"
                                                    CssClass="Button" Text="Save" Width="95px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                &nbsp;&nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAddNew" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <strong>
                            <asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label></strong>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                    width: 650px; border-bottom: silver thin solid">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="5" style="height: 20px">
                                                <asp:Panel ID="Panel12" runat="server" Height="200px" ScrollBars="Vertical" Width="750px">
                                                    <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdOrder_RowDeleting" Width="728px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CUSTOMER_ID" ReadOnly="true" HeaderText="CUSTOMER_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRINCIPAL_ID" ReadOnly="true" HeaderText="PRINCIPAL_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="voucher_type_id" ReadOnly="true" HeaderText="voucher_type_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_HIE_NAME" ReadOnly="true" HeaderText="Principal">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" ReadOnly="true" HeaderText="Customer">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACCOUNT_NAME" ReadOnly="true" HeaderText="ACCOUNT HEAD">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Voucher_no" HeaderText="Voucher No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Balance" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" ReadOnly="true" HeaderText="Remarks">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                    &nbsp; &nbsp;
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
