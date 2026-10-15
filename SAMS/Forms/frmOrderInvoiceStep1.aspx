<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOrderInvoiceStep1.aspx.cs" Inherits="Forms_frmOrderInvoiceStep1" Title="SAMS :: Order/Invoice Step 1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
        function endRequest(sender, e) {
            var oControl = args.get_postBackElement();
            oControl.disabled = false;
        }
        function pageLoad() {
            $("#<%=DrpDocumentType.ClientID %>").searchable();
            $("#<%=drpDistributor.ClientID %>").searchable();
            $("#<%=DrpRoute.ClientID %>").searchable();
            $("#<%=DrpOrderBooker.ClientID %>").searchable();
            $("#<%=DrpDeliveryMan.ClientID %>").searchable();
        }
    </script>
    <div id="right_data">
        <div>
            <span class="heading">Order/Invoice Step 1</span>
        </div>
        <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 150px; height: 100px">
            <asp:Panel ID="Panel2" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="27px" />
                        Wait Update.......
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <table width="100%">
            <tr>
                <td align="left">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" style="width: 110px">
                                            <strong>Transaction Type</strong>
                                        </td>
                                        <td style="width: 238px;" align="left">
                                            <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                                                <%--<asp:ListItem Value="0">Order Entry</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">Spot Sale</asp:ListItem>
                                                <asp:ListItem Value="2">Sale Return</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Location</strong>
                                        </td>
                                        <td style="width: 238px; height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Customer Route</strong>
                                        </td>
                                        <td style="width: 238px;" align="left">
                                            <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Order Booker</strong>
                                        </td>
                                        <td style="width: 238px;" align="left">
                                            <asp:DropDownList ID="DrpOrderBooker" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Sale Force</strong>
                                        </td>
                                        <td style="width: 238px;" align="left">
                                            <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Working Date</strong>
                                        </td>
                                        <td align="left" style="width: 238px;">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox" MaxLength="10" Width="177px"></asp:TextBox><asp:ImageButton
                                                ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="94px" Text="Principal" CssClass="lblbox"
                                                    Visible="false"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged"
                                                Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="94px" Text="Quantity Type" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 238px; height: 25px" align="left">
                                            <asp:RadioButtonList ID="RbUnitType" runat="server" Width="225px" RepeatDirection="Horizontal"
                                                Visible="false">
                                                <asp:ListItem Selected="True">Quantity In Unit</asp:ListItem>
                                                <asp:ListItem>Quantity In Ctns</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnGetOrder" OnClick="btnGetOrder_Click" runat="server" Width="100px"
                                                Font-Size="8pt" Text="Get Order" CssClass="Button" />
                                        </td>
                                        <td style="width: 238px; height: 25px" align="left">
                                            <asp:Button ID="btnNext" OnClick="btnNext_Click" runat="server" Width="100px" Font-Size="8pt"
                                                Text="Next" CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:GridView ID="GrdFreeSKU" runat="server" Visible="False" Width="100%" Height="57px"
                                                ForeColor="Red" CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" HorizontalAlign="Center">
                                                <RowStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" />
                                                <Columns>
                                                    <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Order Id">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                            Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Order Qty">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BALANCE" HeaderText="Stock">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                            Width="50px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="White" />
                                                <PagerStyle BackColor="Transparent" />
                                                <HeaderStyle BackColor="Red" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                    ForeColor="Black" />
                                            </asp:GridView>
                                            <asp:GridView ID="gvRateDifference" runat="server" Visible="False" Width="100%" Height="57px"
                                                ForeColor="Red" CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" HorizontalAlign="Center">
                                                <PagerSettings PreviousPageText="Previous" Mode="NextPrevious" LastPageText="" FirstPageText=""
                                                    NextPageText="Next"></PagerSettings>
                                                <FooterStyle BackColor="White"></FooterStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Order Id">
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                        <ItemStyle BorderStyle="Solid" Width="80px" BorderWidth="1px" BorderColor="Silver"
                                                            HorizontalAlign="Left"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Order_Rate" HeaderText="Order Rate">
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Center"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SSR_Rate" HeaderText="SSR Rate">
                                                        <ItemStyle BorderStyle="Solid" Width="50px" BorderWidth="1px" BorderColor="Silver"
                                                            HorizontalAlign="Center"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" BorderColor="Silver"></RowStyle>
                                                <PagerStyle BackColor="Transparent"></PagerStyle>
                                                <HeaderStyle BackColor="Red" BorderStyle="Solid" ForeColor="Black" BorderWidth="1px"
                                                    BorderColor="Black"></HeaderStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
                <td></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 100px;" align="left" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width: 700px;">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table style="border: 1px solid silver; width: 100%; background-color: whitesmoke;">
                                                <tr>
                                                    <td style="width: 10%;">
                                                        <strong>
                                                            <asp:CheckBox ID="cbAll" runat="server" Width="103px" Text="Select All" AutoPostBack="True"
                                                                OnCheckedChanged="cbAll_CheckedChanged"></asp:CheckBox>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 9%;">
                                                        <strong>
                                                            <asp:Label ID="Label3" runat="server" Width="100px" Text="Payment Mode"></asp:Label></strong>
                                                    </td>
                                                    <td style="width: 30%;">
                                                        <asp:DropDownList ID="ddSearchType" runat="server" Width="176px" CssClass="DropList">
                                                            <asp:ListItem Value="214">Cash</asp:ListItem>
                                                            <asp:ListItem Value="215">Credit</asp:ListItem>
                                                            <asp:ListItem Value="216">Advance</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 51%;">
                                                        <asp:Button ID="btnPost" OnClick="btnPost_Click" runat="server" Width="128px" Font-Size="8pt"
                                                            Text="Convert to Invoice" CssClass="Button" />
                                                        <asp:Button ID="btnPrint" runat="server" Width="104px" Font-Size="8pt" Text="Print Invoice"
                                                            CssClass="Button" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 500px; height: 21px" colspan="5">
                                            <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                Width="770px" Height="180px" ScrollBars="Vertical">
                                                <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Sale Order Id">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Order Date">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SCHEME_AMOUNT" DataFormatString="{0:F2}" HeaderText="Scheme">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="GST Amount">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TOTAL_NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MANUAL_ORDER_ID" HeaderText="Bill Book No">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BUILTY_NO">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BUILTY_AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TRANSPORTER_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EXTRA_DISCOUNT_AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EXPIRY_DISCOUNT">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REMARKS">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PURCHASE_AVG_AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
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
    <asp:UpdatePanel ID="PopupUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblCancel" runat="server"></asp:Label>
            <asp:LinkButton runat="server" ID="hbutton"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="PopupExtender" runat="server" PopupControlID="pnlParameters" TargetControlID="hbutton" BehaviorID="ModelPopup" BackgroundCssClass="modal-background" CancelControlID="lblCancel"></cc1:ModalPopupExtender>
            <asp:Panel ID="pnlParameters" runat="server" Style="display: none; border: 0px solid silver;" ScrollBars="Auto" DefaultButton="BtnYes">
                <div class="modal-dialog" style="width: 400px; margin-left: 0px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="btnClose" class="close" runat="server" visible="false"></button>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table style="border: 1px solid silver; width: 100%; background-color: whitesmoke;">
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="../images/warning_256.png" width="25" alt="" style="margin-left: 10px;" />
                                        </td>
                                        <td style="font-family: Verdana; color: red;">Are you sure you want to convert the invoice?
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="BtnYes" Style="margin-right: 12px; margin-bottom: 7px;" runat="server" Text="Yes" CssClass="Button" Width="70px" OnClick="BtnYes_Click" />
                                            <asp:Button ID="BtnNo" Style="margin-right: 20px; margin-bottom: 7px;" runat="server" Text="No" CssClass="Button" Width="70px" OnClick="BtnNo_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
