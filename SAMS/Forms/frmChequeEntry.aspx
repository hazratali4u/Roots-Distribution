<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChequeEntry.aspx.cs" Inherits="Forms_frmChequeEntry" Title="SAMS :: Cheque Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=drpDistributor.ClientID %>").searchable();
            <%--$("#<%=DrpPrincipal.ClientID %>").searchable();--%>
            $("#<%=DrpStatus.ClientID %>").searchable();
            $("#<%=DrpBankAccount.ClientID %>").searchable();
            $("#<%=DrpDeliveryMan.ClientID %>").searchable();
            $("#<%=DrpRoute.ClientID %>").searchable();
            $("#<%=DrpCustomer.ClientID %>").searchable();

        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;

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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <div style="z-index: 101; left: 350px; width: 100px; position: absolute; top: 150px; height: 100px">
                                    <asp:Panel ID="Panel21" runat="server">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                    Width="23px" />
                                                Wait Update
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </asp:Panel>
                                </div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 100px" align="left" colspan="1"></td>
                                            <td style="width: 70px" align="left" colspan="1"></td>
                                            <td style="width: 201px" align="left" colspan="1"></td>
                                            <td align="left" colspan="1"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 17px" align="left">
                                                <strong>Cheque Type</strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="DrpChequeType" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpChequeType_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>Cheque Realized</asp:ListItem>
                                                    <asp:ListItem>Cheque Advance</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px; height: 17px" align="left"></td>
                                            <td style="height: 17px" align="left">
                                                <strong>Sale Force</strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 17px" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 17px" align="left">
                                                <strong>Location</strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px; height: 17px" align="left"></td>
                                            <td style="height: 17px" align="left">
                                                <strong>Customer</strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>


                                            <td style="height: 17px" align="left"></td>
                                        </tr>
                                        <tr>
                                            <%--<td style="height: 17px" align="left">
                                                <strong>
                                                    Principal</strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>--%>
                                            <td align="left">
                                                <strong>Status</strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpStatus" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px; height: 17px" align="left"></td>

                                            <td style="height: 17px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" Visible="false" runat="server" Width="58px" Text="Route      " CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px; height: 17px; display: none;" align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>



                                            <td style="height: 17px" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>Deposit Account</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpBankAccount" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="25px" Text="            " CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left" colspan="2" rowspan="11">
                                                <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" BorderColor="Silver"
                                                    BorderStyle="Solid" BorderWidth="1px">
                                                    <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="304px" DataKeyNames="SALE_INVOICE_ID">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" Width="14px" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                            <td align="left" rowspan="11"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Chque No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="94px" Text="Bank Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtBankName" runat="server" Width="192px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 100px"></td>
                                        </tr>
                                        <%-- <tr>
                                            
                                            <td valign="top" align="left">
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="width: 100px;" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label13" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="298px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="74px" Text="Chq Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="75px" Text="Cheque Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtStartDate" runat="server" Width="132px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="96px" Text="Slip No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="100px" Text="Recevied Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtSlipNo" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtReceivedDate" runat="server" Width="128px" CssClass="txtBox "
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                                    Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    ValidChars="0123456789." TargetControlID="txtAmount">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                    Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                                <asp:HiddenField ID="HFChqueProcessId" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                                <table style="border-right: silver thin Solid; border-top: silver thin Solid; border-left: silver thin Solid; width: 650px; border-bottom: silver thin Solid">
                                    <tbody>
                                        <tr>
                                            <td style="height: 20px" align="left" colspan="5">
                                                <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical">
                                                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset; border-bottom: silver thin inset; background-color: silver"
                                                        width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="height: 21px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label110" runat="server" Width="154px" Text="Select Searching Type"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 170px; height: 21px" align="left">
                                                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                                        <asp:ListItem Value="CHEQUE_NO">All Records</asp:ListItem>
                                                                        <asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
                                                                        <asp:ListItem Value="BANK_NAME">Bank Name</asp:ListItem>
                                                                        <asp:ListItem Value="SlipNo">Slip No</asp:ListItem>
                                                                        <asp:ListItem Value="account_name">Deposit Account </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 224px; height: 21px" align="left">
                                                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 250px; height: 21px" align="left">
                                                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                                        OnClick="btnFilter_Click"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        BorderColor="White" HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False"
                                                        OnRowEditing="GrdOrder_RowEditing" OnRowDeleting="GrdOrder_RowDeleting">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CHEQUE_PROCESS_ID" ReadOnly="true" HeaderText="CHEQUE_PROCESS_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_ID" ReadOnly="true" HeaderText="CUSTOMER_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" ReadOnly="true" HeaderText="Customer">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_NO" ReadOnly="true" HeaderText="Chq. No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BANK_NAME" ReadOnly="true" HeaderText="Bank Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_DATE" ReadOnly="true" HeaderText="Chq.Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RECEIVED_DATE" ReadOnly="true" HeaderText="Received Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DEPOSIT_DATE" ReadOnly="true" HeaderText="Deposit Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_AMOUNT" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="Chq.Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SlipNo" ReadOnly="true" HeaderText="Slip No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" ReadOnly="true" HeaderText="Remarks">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="area_id" ReadOnly="true" HeaderText="area_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="account_name" ReadOnly="true" HeaderText="Deposit Account">
                                                                <ControlStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="account_head_id" ReadOnly="true" HeaderText="account_head_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DeliveryManID" ReadOnly="true" HeaderText="DeliveryManID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:CommandField>--%>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
