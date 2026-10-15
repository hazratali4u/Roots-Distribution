<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmGiftSKU.aspx.cs" Inherits="Forms_frmGiftSKU" Title="SAMS :: Gift SKU Entry" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=drpDistributor.ClientID %>").searchable();
            $("#<%=DrpSaleForce.ClientID %>").searchable();
            $("#<%=ddlSkus.ClientID %>").searchable();


        }

        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }

            return true;
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <div style="z-index: 101; left: 477px; width: 100px; position: absolute; top: 229px; height: 100px">
                        &nbsp;<asp:Panel ID="Panel21" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td valign="top" align="left" style="width: 100px;">
                                            <strong>Location</strong>
                                        </td>
                                        <td valign="top" align="left" colspan="2">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="280px" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td style="height: 17px" valign="top" align="left">
                                            <strong>Principal</strong>
                                        </td>
                                        <td style="height: 17px" valign="top" align="left" colspan="2">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="280px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td style="height: 17px" valign="top" align="left">
                                            <strong>Sale Force</strong>
                                        </td>
                                        <td style="height: 17px" valign="top" align="left" colspan="2">
                                            <asp:DropDownList ID="DrpSaleForce" runat="server" Width="280px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpSaleForce_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 17px" valign="top" align="left">
                                            <strong>Invoice No</strong>
                                        </td>
                                        <td style="height: 17px" valign="top" align="left" colspan="2">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" Width="280px" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="left">
                                            <strong>Customer Code</strong>
                                        </td>
                                        <td valign="top" align="left">
                                            <asp:TextBox ID="txtOutletCode" runat="server" Width="76px" CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtOutletName" runat="server" Width="188px" CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td style="height: 34px" valign="top" align="left" colspan="3">
                                              <asp:ListBox ID="lstCode" onkeydown="SelectSkuCode(event)" runat="server" Width="374px"
                                                Height="130%" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>--%>
                                </tbody>
                            </table>
                            &nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <strong>
                        <asp:CheckBox ID="ChbAllTax" runat="server" Checked="True" Text="Is Apply All Tax" /></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="width: 27%; border: 1px solid Silver; margin: 0px !important; border-radius: 5px;">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 60%; font-size: 15px;">
                                        <strong>Closing Stock:</strong>
                                    </td>
                                    <td style="font-size: 15px; width: 40%" align="right">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblClosingStock" runat="server" Text="0.00"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr class="detailClass">
                                        <td>
                                            <label>
                                                SKU Description
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Quantity
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Unit Price
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                GST Rate    
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Amount
                                            </label>
                                        </td>


                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px">
                                            <asp:DropDownList ID="ddlSkus" runat="server" Width="300px" OnSelectedIndexChanged="ddlSkus_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtQuantity" runat="server" Width="55px" CssClass="txtBox "></asp:TextBox>
                                        </td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtUnitRate" runat="server" Width="56px" CssClass="txtBox" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtBatchNo" runat="server" Width="62px" CssClass="txtBoxnNum"></asp:TextBox>
                                        </td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="72px" CssClass="txtBoxnNum" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="height: 21px">
                                            <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="7">
                                            <asp:Panel ID="Panel2" runat="server" Width="680px" Height="130px" ScrollBars="Vertical"
                                                BorderWidth="1px" BorderStyle="Solid" BorderColor="Silver">
                                                <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" CssClass="gridRow2"
                                                    BackColor="White" OnRowDeleting="GrdPurchase_RowDeleting" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <RowStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SALE_INVOICE_PROMOTION_ID" ReadOnly="true" HeaderText="SALE_INVOICE_PROMOTION_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="SKU Code">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                Width="75px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="SKU Name">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="QUANTITY" ReadOnly="true" HeaderText="Quantity">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                Width="65px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UNIT_PRICE" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="PRICE">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                Width="65px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GST_RATE" ReadOnly="true" HeaderText="GST Rate">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Amount" DataFormatString="{0:F2}" ReadOnly="true" HeaderText="Amount">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" HorizontalAlign="Center" />
                                                        </asp:CommandField>--%>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px"
                                                                HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                    <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
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
</asp:Content>
