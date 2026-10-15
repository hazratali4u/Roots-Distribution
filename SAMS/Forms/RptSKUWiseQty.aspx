<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptSKUWiseQty.aspx.cs" Inherits="Forms_RptSKUWiseQty" Title="SAMS :: SKU Wise Net Sales / Sales Return Qty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ID" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" language="javascript">
        function SelectAll() {
            var chkBoxList = document.getElementById('<%= ListCatagory.ClientID %>');
                    var chkBox = document.getElementById('<%= ChbAllCatagories.ClientID %>');
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
                    var chkBox = document.getElementById('<%= ChbAllCatagories.ClientID %>');
            var chkBoxList = document.getElementById('<%= ListCatagory.ClientID %>');
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

        <%--function pageLoad() {
            var $checkBoxAll = $('[id$="ChbAllCatagories"]'); // assign main checkbox into a variable
            var $ChkBoxlst = $("table.checkboxlst input:checkbox"); // assign all checkboxes in to a variable
            $checkBoxAll.click(function () { // trigger click event when main checkbox will be clicked
                $ChkBoxlst
                .attr('checked', $checkBoxAll // attr() method checks all checkboxes
                .is(':checked'));
            });
            $ChkBoxlst.click( // trigger an event click
            function (e) {
                if (!$(this)[0].checked) { // check if all checkboxes is checked
                    $checkBoxAll.attr("checked", false); // un check the all checkbxes using attr() method, passing false as a second parameter
                }
            });

            jQuery('#<%=txtFromDate.ClientID %>').keydown(MonthTextBox);
            jQuery('#<%=txtToDate.ClientID %>').keydown(MonthTextBox);
        }--%>

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr style="display:none;">
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Text="Report Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpReportType" runat="server" Width="200px">
                                                <asp:ListItem Value="1" Text="Sales" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Sales Return"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpLocation" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="200px"></asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="78px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpArea" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Customer Route"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label11" runat="server" Width="94px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList"></asp:DropDownList>
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                    <tr style="display: none;">
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Width="80px" Text="Channel Type"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlChannelType" runat="server" Width="200px"></asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="48px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 100px" align="left">
                                            <strong>SKU Type</strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td align="left">
                                            <asp:DropDownList ID="DrpType" runat="server" Width="200px" CssClass="DropList">
                                                <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Gold"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Bronze"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Silver"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="width: 90px; height: 2px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="48px" Text="Catagory" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                        <td style="width: 204px; height: 2px" align="left">
                                            <asp:CheckBox ID="ChbAllCatagories" runat="server" onclick="SelectAll()" Text="All Catagories"></asp:CheckBox></td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="height: 2px;" align="left" colspan="2">
                                            <asp:Panel ID="Panel1" runat="server" Style="margin-left: 6px;" Width="295px" Height="305px" ScrollBars="Vertical" BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListCatagory" runat="server" onclick="UnCheckSelectAll()" class="checkboxlst"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="100px" Height="9px" Text="From Date"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="55px" Text="To Date"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromDate"
                                TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate"
                                TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            &nbsp; 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" Style="margin-left: 113px;" runat="server" CssClass="Button"
                        Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>

    </div>
</asp:Content>
