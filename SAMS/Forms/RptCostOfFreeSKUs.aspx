<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptCostOfFreeSKUs.aspx.cs" Inherits="Forms_RptCostOfFreeSKUs" Title="SAMS :: Cost Of Free SKUs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ID" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            $("select").searchable();
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
                                            <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="All Catagories"></asp:CheckBox></td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="height: 2px" align="left" colspan="2">
                                            <asp:Panel ID="Panel1" runat="server" Width="295px" Height="305px" ScrollBars="Vertical" BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListCatagory" runat="server" class="checkboxlst"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    <tr>
                                            <td align="left">
                                            </td>
                                           
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="90px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left" style="height: 25px">
                                                &nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                                    onkeyup="BlockStartDateKeyPress()" Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                         <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left" style="height: 25px">
                                                &nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                                    onkeyup="BlockEndDateKeyPress()" Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate"></cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate" TargetControlID="txtEndDate"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                </tbody>
                            </table>
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
