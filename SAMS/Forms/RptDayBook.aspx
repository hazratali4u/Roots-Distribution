<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptDayBook.aspx.cs" Inherits="Forms_RptDayBook" Title="SAMS :: Day Book" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td align="left" colspan="4">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                 <tr style="display:none;">
                                     <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Height="14px" Text="Report Type"
                                                Width="98px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:RadioButtonList ID="RbtReportType" runat="server" Width="265px" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">Sales / Purchase</asp:ListItem>
                                            <asp:ListItem Value="2">Receipts / Payments</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Report Type" Width="94px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="ddlReportType" runat="server" Width="240px" CssClass="DropList" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem value="1" text="Sales"></asp:ListItem>
                                            <asp:ListItem value="2" text="Purchases"></asp:ListItem>
                                            <asp:ListItem value="3" text="Receipts"></asp:ListItem>
                                            <asp:ListItem value="4" text="Payments"></asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Report Type" Width="94px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem value="2" text="Purchase"></asp:ListItem>
                                            <asp:ListItem value="3" text="Purchase Return"></asp:ListItem>
                                            <asp:ListItem value="8" text="Short"></asp:ListItem>
                                            <asp:ListItem value="9" text="Excess"></asp:ListItem>
                                            <asp:ListItem value="6" text="Damage"></asp:ListItem>
                                            <asp:ListItem value="4" text="Transfer In"></asp:ListItem>
                                            <asp:ListItem value="5" text="Transfer Out"></asp:ListItem>
                                            <asp:ListItem value="7" text="Opening Stock"></asp:ListItem>
                                            <%--<asp:ListItem value="15" text="Stock Issuance"></asp:ListItem>
                                            <asp:ListItem value="16" text="Stock Return"></asp:ListItem>
                                            <asp:ListItem value="17" text="Physical Stock Taking"></asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="94px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="240px" CssClass="DropList">
                                        </asp:DropDownList></td>
                                </tr>
                                <asp:Panel ID="PanelSupplier" runat="server" Visible="false">
                                <tr style="display:none;">
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Principal" Width="78px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList></td>
                                </tr>
                                    </asp:Panel>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="76px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                        onkeyup="BlockStartDateKeyPress()" Width="215px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" /></td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                        onkeyup="BlockEndDateKeyPress()" Width="215px"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" /></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left"></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy"
                                            PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
        <asp:Button ID="btnViewPDF" Style="margin-left: 97px;" runat="server" CssClass="button"
            Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="button"
                        Text="View Excel" Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>

