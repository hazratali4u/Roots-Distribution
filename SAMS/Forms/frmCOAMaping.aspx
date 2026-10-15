<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCOAMaping.aspx.cs" Inherits="frmCOAMaping" Title="SAMS :: COA Mapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=DrpAccountDetail.ClientID %>").searchable();
            $("#<%=DrpCustomer.ClientID %>").searchable();
            $("#<%=DrpPrincipal.ClientID %>").searchable();
            $("#<%=drpDistributor.ClientID %>").searchable();
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
                                <tr style="display: none;">
                                    <td></td>
                                    <td><strong>Type</strong></td>
                                    <td></td>
                                    <td>
                                        <asp:RadioButtonList ID="rbType" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rbType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Selected="True" Text="Customer Mapping"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Principal Mapping"></asp:ListItem>
                                        </asp:RadioButtonList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left" style="width: 80px;">
                                        <strong>
                                            <asp:Label ID="lblPrincipal" runat="server" Text="Principal"></asp:Label></strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label></strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" CssClass="DropList" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblAccount" runat="server" Text="Account"></asp:Label></strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left">
                                        <asp:DropDownList ID="DrpAccountDetail" runat="server" AutoPostBack="True" CssClass="DropList"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="lbl" runat="server" Width="100px" Text=" "></asp:Label>
                    <asp:Button ID="btnSave" runat="server" CssClass="Button" Text="Assign" Width="120"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnViewReport" runat="server" CssClass="Button" Text="View Report" Width="120"
                        OnClick="btnViewReport_Click" />

                </td>
            </tr>
        </table>

    </div>
</asp:Content>
