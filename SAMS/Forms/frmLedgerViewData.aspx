<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmLedgerViewData.aspx.cs" Inherits="Forms_frmLedgerViewData" Title="SAMS :: Voucher Editing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    
    
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="lbltoLocation" runat="server" Width="67px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"></asp:DropDownList></td>
                                <td style="height: 25px;"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Voucher Type" Width="104px"></asp:Label></strong></td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpVoucherType" runat="server" AutoPostBack="true" Width="200px" CssClass="DropList" OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="height: 25px;"></td>
                            </tr>

                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Voucher Sub Type" Width="104px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpVoucherSubType" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px;">&nbsp;
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td align="left" style="width: 100px; display: none;" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Principal" Width="104px"></asp:Label></strong></td>
                                <td align="left" style="width: 100px; display: none;">
                                    <asp:DropDownList ID="DrpPrincipal" runat="server" CssClass="DropList" Width="200px">
                                    </asp:DropDownList></td>
                                <td align="left" style="height: 25px"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="User Name" Width="104px"></asp:Label></strong></td>
                                <td align="left" style="width: 100px">
                                    <asp:DropDownList ID="DrpUser" runat="server" CssClass="DropList" Width="200px">
                                    </asp:DropDownList></td>
                                <td align="left" style="height: 25px"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong></td>
                                <td style="width: 100px" align="left">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                        Width="190px"></asp:TextBox></td>
                                <td align="left" style="height: 25px;">
                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong></td>
                                <td align="left" style="width: 100px">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                        Width="191px"></asp:TextBox></td>
                                <td align="left" style="height: 25px">
                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" /></td>
                            </tr>
                            <tr style="display: none;">
                                <td align="left" style="width: 100px; height: 12px;" valign="top">&nbsp;<strong><asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Status " Width="90px"></asp:Label></strong></td>
                                <td style="width: 100px; height: 12px;" align="left">
                                    <asp:RadioButtonList ID="RbdList" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="RbdList_SelectedIndexChanged">
                                        <asp:ListItem Value="FALSE">Un Post Voucher</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="TRUE">Post Voucher</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td align="left" style="height: 25px;"></td>
                            </tr>
                        </table>
                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                            TargetControlID="txtStartDate">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                            TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>

        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table width="300">
                            <tr>
                                <td style="width: 100px; height: 21px;">
                                    <asp:CheckBox ID="ChbSelect" runat="server" Text="All Select" Width="115px" AutoPostBack="True" OnCheckedChanged="ChbSelect_CheckedChanged" /></td>
                                <td style="width: 100px; height: 21px;">
                                    <asp:Button ID="btnView" runat="server" Font-Size="8pt" OnClick="btnView_Click" Text="View" CssClass="Button"
                                        Width="107px" /></td>
                                <td style="width: 100px; height: 21px;"></td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnDelete" runat="server" Font-Size="8pt" Text="Cancel Voucher" CssClass="Button"
                                        Width="108px" OnClick="btnDelete_Click" /></td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnPrintVoucher" runat="server" Font-Size="8pt" Text="Print Voucher" CssClass="Button"
                                        Width="108px" OnClick="btnPrintVoucher_Click" />
                                </td>
                            </tr>
                        </table>
                        <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset; width: 650px; border-bottom: silver thin inset; height: 251px;">
                            <tbody>
                                <tr>
                                    <td align="left" colspan="5" style="height: 21px">
                                        <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical" Width="750px">
                                            <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                Width="728px" OnRowEditing="GrdLedger_RowEditing" OnRowDeleting="GrdLedger_RowDeleting">
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Voucher" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChbSelect" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VOUCHER_NO" ReadOnly="true" HeaderText="Voucher No" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Ledger_date" ReadOnly="true" HeaderText="Voucher Date" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" ReadOnly="true" HeaderText="Remarks" HeaderStyle-Width="50%">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_TYPE_ID" ReadOnly="true" HeaderText="VOUCHER_TYPE_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_DATE" ReadOnly="true" HeaderText="VOUCHER_DATE">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Print"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%--<asp:CommandField HeaderText="Print" ShowEditButton="True" EditText="Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:CommandField>--%>
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Edit?');return false;"
                                                                Text="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="tblhead" HorizontalAlign="Center"
                                                    VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

