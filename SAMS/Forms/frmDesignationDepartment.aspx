<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDesignationDepartment.aspx.cs" Inherits="Forms_frmDesignationDepartment"
    Title="Designation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="25%">
                    <tbody>
                        <tr>
                            <td style="height: 8px" align="left" colspan="2">
                                <asp:Label ID="lblErrorMsgDivsion" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="left">
                                <strong>
                                    <asp:Label ID="Label12" runat="server" Width="52px" Text="Code"></asp:Label></strong>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtbustypeCode" runat="server" Width="100px" CssClass="txtBox "
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <strong>
                                    <asp:Label ID="Label21" runat="server" Width="53px" Text="Name"></asp:Label></strong>
                            </td>
                            <td style="width: 100px; height: 29px">
                                <asp:TextBox ID="txtbustypeName" runat="server" Width="194px" CssClass="txtBox "
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSaveBusType" OnClick="btnSaveBusType_Click" runat="server" Width="85px"
                                    Font-Size="8pt" Text="New" CssClass="Button"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="250px" ScrollBars="Vertical" Width="70%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdDesignation" runat="server" Width="99.5%" ForeColor="SteelBlue"
                                    CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnRowEditing="GrdBusType_RowEditing" OnRowDeleting="GrdBusType_RowDeleting">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black"></RowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="REF_ID" ReadOnly="true" HeaderText="Id">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SLASH_CODE" ReadOnly="true" HeaderText="Code">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SLASH_DESC" ReadOnly="true" HeaderText="Name">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:CommandField>--%>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                    Text="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White"></FooterStyle>
                                    <PagerStyle BackColor="Transparent"></PagerStyle>
                                    <HeaderStyle BackColor="#007395" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        ForeColor="White"></HeaderStyle>
                                    <AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
