<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptGlLogReport.aspx.cs" Inherits="Forms_RptGlLogReport" Title="SAMS :: GL Log Detail" %>

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
                                <tbody>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="73px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="104px" Text="Voucher Type" __designer:dtid="5066549580791825" CssClass="lblbox" __designer:wfdid="w4"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpVoucherType" runat="server" Width="200px" __designer:dtid="5066549580791827" CssClass="DropList" OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged" AutoPostBack="true" __designer:wfdid="w2">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px;">&nbsp;
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Voucher Sub Type" Width="104px"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px;">&nbsp;
                                        </td>
                                        <td style="width: 100px">
                                            <asp:DropDownList ID="DrpVoucherSubType" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="104px" Text="User Name" __designer:dtid="5066549580791837" CssClass="lblbox" __designer:wfdid="w3"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpUser" runat="server" Width="200px" __designer:dtid="4503599627370527" CssClass="DropList" __designer:wfdid="w1">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="70px" Height="13px" Text="Filter On" __designer:wfdid="w1"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:RadioButtonList ID="rblFilter" runat="server" Width="200px" CssClass="lblbox" __designer:wfdid="w2" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="0">Entry Date</asp:ListItem>
                                                <asp:ListItem Value="1">Voucher Date</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="70px" Height="13px" Text="From Date"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">&nbsp;<asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">&nbsp;<asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left"></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button"
                        Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button"
                        Text="View Excel" Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
