<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptCustomerStatusReport.aspx.cs" Inherits="Forms_RptCustomerStatusReport" Title="SAMS :: Customer Status Report" %>
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
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="61px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Town" Width="48px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpTown" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Route" Width="52px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Channel Type" Width="100px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpChannelType" runat="server" AutoPostBack="True" CssClass="DropList"
                                            Width="200px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Status" Width="100px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpStatus" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="0">All </asp:ListItem>
                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                    <asp:ListItem Value="2">In Active</asp:ListItem>
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Principal" Width="100px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left"></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Is Registered Customer" Width="174px" /></td>
                                </tr>
                            </table>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" OnClick="btnViewPDF_Click" CssClass="Button" Width="90" />
                    <asp:Button ID="btnViewExcel" runat="server"
                        Text="View Excel" OnClick="btnViewExcel_Click" CssClass="Button" Width="90" /></td>
            </tr>
        </table>
    </div>
</asp:Content>