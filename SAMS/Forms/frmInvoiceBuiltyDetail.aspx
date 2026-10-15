<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmInvoiceBuiltyDetail.aspx.cs" Inherits="Forms_frmInvoiceBuiltyDetail"
    Title="SAMS :: Freight Entry" %>

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
            $("#<%=DrpTransporter.ClientID %>").searchable();
            $("#<%=DrpCustomer.ClientID %>").searchable();
            $("#<%=DrpInvoiceNo.ClientID %>").searchable();
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
               <%-- <tr>
                    <td>
                        <asp:Panel ID="PGetData" runat="server" Visible="true">
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="height: 20px" align="left">
                                            <strong>Location</strong>
                                        </td>
                                        <td style="width: 100px">
                                            <asp:DropDownList ID="DrpLocationData" runat="server" Width="250px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 90px" align="left"></td>
                                        <td style="height: 20px" align="left" colspan="1">
                                            <asp:Button ID="btnGetData" OnClick="btnGetData_Click" runat="server"
                                                Width="95px" Font-Size="8pt" Text="Get Data" CssClass="Button" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <div style="z-index: 101; left: 346px; width: 100px; position: absolute; top: 246px; height: 100px">
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
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                    TargetControlID="txtAmount" FilterType="Custom">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddNew">
                                    <table>
                                        <tbody>
                                            <%--<tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        Principal</strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="DrpPrincipal" runat="server" Width="250px" CssClass="DropList"
                                                        AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>

                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Location</strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" CssClass="DropList"
                                                        AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Transporter</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:DropDownList ID="DrpTransporter" runat="server" Width="250px" CssClass="DropList">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Customer</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:DropDownList ID="DrpCustomer" runat="server" Width="250px" CssClass="DropList"
                                                        AutoPostBack="True" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Invoice No</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:DropDownList ID="DrpInvoiceNo" runat="server" Width="250px" CssClass="DropList">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>As On Date</strong>
                                                </td>


                                                <td align="left" style="width: 100px">
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox" OnTextChanged="txtEndDate_TextChanged" AutoPostBack="true" MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                                        Width="191px"></asp:TextBox>
                                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                        Width="16px" />
                                                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                        TargetControlID="txtEndDate">
                                                    </cc1:CalendarExtender>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Builty Date</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:TextBox ID="txtBuiltyDate" runat="server" CssClass="txtBox" MaxLength="10" Width="191px"></asp:TextBox>
                                                    <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                                        Format="dd/MMM/yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtBuiltyDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Builty No</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:TextBox ID="txtRemarks" runat="server" Width="167px" CssClass="txtBox "></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Builty Carton</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:TextBox ID="txtBuiltyCtns" runat="server" Width="167px" CssClass="txtBox "></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        TargetControlID="txtBuiltyCtns" ValidChars="0123456789" FilterType="Custom">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>Freight</strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:TextBox ID="txtAmount" runat="server" Width="167px" CssClass="txtBox "></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 90px" align="left"></td>
                                                <td style="height: 20px" align="left" colspan="1">
                                                    <asp:Button AccessKey="S" ID="btnAddNew" OnClick="btnAddNew_Click" runat="server"
                                                        Width="95px" Font-Size="8pt" Text="Save" CssClass="Button" />
                                                </td>
                                            </tr>
                                        </tbody>
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
                                <table style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid; width: 650px; border-bottom: silver thin solid">
                                    <tbody>
                                        <tr>
                                            <td style="height: 20px" align="left" colspan="5">
                                                <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        OnRowDeleting="GrdOrder_RowDeleting" HorizontalAlign="Center" BorderColor="SteelBlue"
                                                        BackColor="White" AutoGenerateColumns="False" OnRowEditing="GrdOrder_RowEditing">

                                                        <Columns>
                                                            <asp:BoundField DataField="CUSTOMER_ID" ReadOnly="true" HeaderText="CUSTOMER_ID">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRINCIPAL_ID" ReadOnly="true" HeaderText="PRINCIPAL_ID">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="voucher_type_id" ReadOnly="true" HeaderText="voucher_type_id">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_HIE_NAME" ReadOnly="true" HeaderText="Principal">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" ReadOnly="true" HeaderText="Customer">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Document_no" ReadOnly="true" HeaderText="Invoice No">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Ledger_date" ReadOnly="true" HeaderText="Invoice Date">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Voucher_no" ReadOnly="true" HeaderText="Voucher No">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataFormatString="{0:F2}" ReadOnly="true" DataField="Balance" HeaderText="Freight">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BUILTY_NO" ReadOnly="true" HeaderText="Builty No">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="distributor_id" ReadOnly="true" HeaderText="distributor_id">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="payee_name" ReadOnly="true" HeaderText="transporter">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Document_No" ReadOnly="true" HeaderText="InvoiceNo">
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_NO" ReadOnly="true" HeaderText="Builty Ctns">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:CommandField>--%>
                                                            <%--<asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" VerticalAlign="Middle"></HeaderStyle>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:HiddenField ID="hfdocument_No" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hfDocument_type" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hfVoucherNo" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hfTotalAmount" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
