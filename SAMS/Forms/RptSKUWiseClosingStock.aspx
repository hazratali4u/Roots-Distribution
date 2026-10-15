<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptSKUWiseClosingStock.aspx.cs" Inherits="Forms_RptSKUWiseClosingStock" Title="SAMS :: SKU Wise Closing Stock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
        }
        function SelectAll() {
            var chkBoxList = document.getElementById('<%= LstCategory.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }

        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstCategory.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="Principal"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="250px" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 90px; height: 2px" align="left">
                                                <strong>
                                                    Category</strong>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                            </td>
                                            <td style="width: 204px; height: 2px" align="left">
                                                <asp:CheckBox ID="ChbAllCategory" onclick="SelectAll()" runat="server" Text="All">
                                                </asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                             <td></td>
                                            <td style="height: 2px" align="left" colspan="3">
                                                <asp:Panel ID="Panel1" runat="server" Width="250px" Height="140px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove">
                                                    <asp:CheckBoxList ID="LstCategory" onclick="UnCheckSelectAll()" runat="server">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                         <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="Report Type" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddlType" runat="server" Width="250px">
                                            <asp:ListItem Value="0" Text="Units"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Carton"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                        <tr>
                                            <td>
                                                <strong>Sort By</strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlSortBy" Width="250px">
                                                    <asp:ListItem Text="Item Name" Value="SKU_NAME" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Item Market Name" Value="SKU_NAME2"></asp:ListItem>
                                                    <asp:ListItem Text="Closing Stock" Value="CLOSING_STOCK"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Order By</strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlOrderBy" Width="250px">
                                                    <asp:ListItem Selected="True" Text="Ascending" Value="ASC"></asp:ListItem>
                                                    <asp:ListItem Text="Descending" Value="DESC"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="80px" Height="13px" Text="Stock Date"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="222px"
                                                    CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            &nbsp;
                                            </td>
                                        </tr>
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" Width="90" CssClass="Button"
                        OnClick="btnViewPDF_Click" Style="margin-left:95px;" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" Width="90" CssClass="Button"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
