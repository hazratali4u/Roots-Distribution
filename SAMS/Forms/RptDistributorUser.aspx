<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptDistributorUser.aspx.cs" Inherits="Forms_RptDistributorUser" Title="SAMS :: Employee List" %>

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
                                                <asp:Label ID="lblUser" runat="server" Width="76px" Height="13px" Text="Location"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="76px" Height="13px" Text="Status"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:RadioButtonList ID="drpStatus" runat="server" RepeatDirection="Horizontal" Width="170px" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="true">Active</asp:ListItem>
                                            <asp:ListItem Value="false">InActive</asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="76px" Height="13px" Text="Designation"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlDesignation" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="76px" Height="13px" Text="Employee"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpEmployee" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>

                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnPDF" runat="server" Style="margin-left:91px;" Text="View PDF" Width="90" OnClick="btnViewPDF_Click" CssClass="Button" />
                    <asp:Button ID="btnExcel" runat="server" Text="View Excel" Width="90" OnClick="btnViewExcel_Click" CssClass="Button" />
                    &nbsp;
                    &nbsp; &nbsp;&nbsp; &nbsp;
                </td>
            </tr>
        </table>
        &nbsp;
        
    </div>
</asp:Content>

