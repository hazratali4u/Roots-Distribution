<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTransferOutIn.aspx.cs" Inherits="Forms_frmTransferOutIn" Title="SAMS :: Transfer In" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>

                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="100px" Text="Document No" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="94px" Text="Transfer From" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpTransferFor" runat="server" Width="200px" CssClass="DropList"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Transfer To" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td style="height: 25px" align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                             <strong>
                                                <asp:Label ID="Label1" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="height: 25px" align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="63px" Text="Order No" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="94px" Text="Builty No" CssClass="lblbox"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" Width="195px" CssClass="txtBox"></asp:TextBox>
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
        <div>
            <table width="100%">
                <tr>

                    <td align="left" style="height: 220px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="640px" Height="140px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="620px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="205px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_CTN" HeaderText="Cartons">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY" HeaderText="Units">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="40px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnTransferIn" runat="server" Width="100px" Font-Size="8pt" CssClass="Button"
                                    Text="Transfer In" UseSubmitBehavior="False" OnClick="btnTransferIn_Click"></asp:Button>
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" CssClass="Button"></asp:Button>&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 100px;"></td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
