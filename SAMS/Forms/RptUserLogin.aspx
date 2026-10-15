<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptUserLogin.aspx.cs" Inherits="Forms_RptUserLogin" Title="SAMS :: User Login History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
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
                                                <asp:Label ID="lblUser" runat="server" Width="76px" Height="13px" Text="User"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlUser" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>

                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="76px" Height="13px" Text="From Date"></asp:Label></strong></td>
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

                    <asp:Button ID="btnPDF" runat="server" Text="View PDF" Width="90" OnClick="btnPDF_Click" CssClass="Button" />
                    <asp:Button ID="btnExcel" runat="server" Text="View Excel" Width="90" OnClick="btnExcel_Click" CssClass="Button" />
                    &nbsp;
                    &nbsp; &nbsp;&nbsp; &nbsp;
                </td>
            </tr>
        </table>
        &nbsp;
        
    </div>
</asp:Content>

