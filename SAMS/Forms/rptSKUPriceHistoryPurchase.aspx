<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="rptSKUPriceHistoryPurchase.aspx.cs" Inherits="Forms_rptSKUPriceHistoryPurchase" Title="SAMS :: SKU Price History Purchase Wise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

         function SelectAll() {
            var chkBoxList = document.getElementById('<%= ListSKUs.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllSKUs.ClientID %>');
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
            var chkBox = document.getElementById('<%= ChbAllSKUs.ClientID %>');
            var chkBoxList = document.getElementById('<%= ListSKUs.ClientID %>');
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
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" colspan="3">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="70px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong> </td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="78px" Text="SKU" CssClass="lblbox"></asp:Label></strong> </td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:CheckBox ID="ChbAllSKUs" onclick="SelectAll()" runat="server" Text="All SKUs"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left"></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:Panel ID="Panel1" runat="server" Width="490px" Height="310px" ScrollBars="Vertical" Style="border: groove;" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListSKUs" onclick="UnCheckSelectAll()" runat="server" AutoPostBack="True"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="76px" Height="13px" Text="From Date"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">&nbsp;<asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">&nbsp;<asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px" align="left"></td>
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
                    &nbsp; &nbsp;
        <asp:Button ID="btnViewPDF" Style="margin-left: 95px;" runat="server" CssClass="Button" Width="90" Text="View PDF" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Width="90" CssClass="Button" Text="View Excel" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
        &nbsp;
        
    </div>
</asp:Content>

