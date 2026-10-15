<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPriceGroupsReport.aspx.cs" Inherits="Forms_frmPriceGroupsReport" Title="SAMS :: SKU Price Groups" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        
        <%--function pageLoad() {
            $("#<%=DrpGroup.ClientID %>").searchable();
            $("#<%=drpPrincipal.ClientID %>").searchable();
            $("#<%=DrpCatagory.ClientID %>").searchable();
        }--%>
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 273px; height: 68px" id="TABLE1" onclick="return TABLE1_onclick()">
                                <tbody>
                                    <tr>
                                        <td style="height: 15px" align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 1px; height: 15px" align="left" colspan="1"></td>
                                    </tr>
                                    <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpDistributor" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        </tr>
                                    
                                    <tr>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 29px" align="left">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Width="90px" Text="Group Name" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpGroup" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True">
                                            </asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="61px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="59px" Text="Catagory" CssClass="lblbox"></asp:Label></strong><br />
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpCatagory" runat="server" Width="200px" CssClass="DropList"></asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                            &nbsp;&nbsp; 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;  &nbsp; &nbsp;&nbsp; &nbsp;               
                    <asp:Button ID="btnViewPDF" Style="margin-left:77px;" runat="server" Text="View PDF" OnClick="btnViewPDF_Click" CssClass="Button" Width="90" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" OnClick="btnViewExcel_Click" CssClass="Button" Width="90" />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

