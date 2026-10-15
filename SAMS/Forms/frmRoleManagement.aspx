<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRoleManagement.aspx.cs" Inherits="frmRoleManagement" Title="SAMS :: Role Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 82px; height: 17px">
                                            <asp:Label ID="lblmsg" runat="server" Visible="False" Width="114px" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="107px" Text="Role Description" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddRole" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="ddRole_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="195px" CssClass="txtBox "
                                                Enabled="False"></asp:TextBox>
                                            
                                        </td>
                                        <td style="height: 17px; text-align:left;">
                                            <asp:Button ID="btnNew" Style="margin-left:-150px;" OnClick="btnNew_Click" runat="server" Width="55px" Font-Size="8pt"
                                                Text="New" CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Width="112px" Text="Module 1st Layer" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpModule1stLayer" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpModule1stLayer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="112px" Text="Module 2nd Layer" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpModule2ndLayer" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpModule2ndLayer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 16px">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="112px" Text="Module 3rd Layer" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpModule3rdLayer" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpModule3rdLayer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="height: 170px" colspan="2">
                                        <fieldset style="border-width:thin; border-color:Snow">
                                        <legend style="text-align:center;">Module Assignment</legend>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100px" rowspan="4">
                                                            <asp:ListBox ID="lstUnAssignModule" runat="server" Width="180px" Height="200px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 102px" rowspan="4">
                                                            <asp:ListBox ID="lstAssignModule" runat="server" Width="180px" Height="200px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">>"
                                                                OnClick="btnAssign_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnUnAssign" runat="server" Width="30px" Font-Size="8pt" Text="<<"
                                                                OnClick="btnUnAssign_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 75px">
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>
                                                        <td rowspan="1">
                                                        </td>
                                                        <td style="width: 75px; height: 24px">
                                                        </td>
                                                        <td rowspan="1">
                                                        </td>
                                                    </tr>--%>
                                                </tbody>
                                            </table>
                                            </fieldset>
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" width="10%">
                    <asp:Button ID="btnReport" runat="server" Text="View Report" OnClick="btnReport_Click"
                        Width="110px" CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
