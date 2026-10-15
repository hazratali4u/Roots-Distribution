<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptDailySales.aspx.cs" Inherits="Forms_RptDailySales" Title="SAMS :: Daily Sales" %>

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

            <%--jQuery('#<%=txtFromDate.ClientID %>').keydown(MonthTextBox);
            jQuery('#<%=txtToDate.ClientID %>').keydown(MonthTextBox);--%>
        }

        //function onCalendarShown1() {
        //    var cal = $find("calendar1");
        //    cal._switchMode("months", true);
        //    if (cal._monthsBody) {
        //        for (var i = 0; i < cal._monthsBody.rows.length; i++) {
        //            var row = cal._monthsBody.rows[i];
        //            for (var j = 0; j < row.cells.length; j++) {
        //                Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call1);
        //            }
        //        }
        //    }
        //}

        //function onCalendarHidden1() {
        //    var cal = $find("calendar1");

        //    if (cal._monthsBody) {
        //        for (var i = 0; i < cal._monthsBody.rows.length; i++) {
        //            var row = cal._monthsBody.rows[i];
        //            for (var j = 0; j < row.cells.length; j++) {
        //                Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call1);
        //            }
        //        }
        //    }
        //}

        //function call1(eventElement) {
        //    var target = eventElement.target;
        //    switch (target.mode) {
        //        case "month":
        //            var cal = $find("calendar1");
        //            cal._visibleDate = target.date;
        //            cal.set_selectedDate(target.date);
        //            cal._switchMonth(target.date);
        //            cal._blur.post(true);
        //            cal.raiseDateSelectionChanged();
        //            break;
        //    }
        //}

        //function onCalendarShown2() {
        //    var cal = $find("calendar2");
        //    cal._switchMode("months", true);
        //    if (cal._monthsBody) {
        //        for (var i = 0; i < cal._monthsBody.rows.length; i++) {
        //            var row = cal._monthsBody.rows[i];
        //            for (var j = 0; j < row.cells.length; j++) {
        //                Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call2);
        //            }
        //        }
        //    }
        //}

        //function onCalendarHidden2() {
        //    var cal = $find("calendar2");

        //    if (cal._monthsBody) {
        //        for (var i = 0; i < cal._monthsBody.rows.length; i++) {
        //            var row = cal._monthsBody.rows[i];
        //            for (var j = 0; j < row.cells.length; j++) {
        //                Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call2);
        //            }
        //        }
        //    }
        //}

        //function call2(eventElement) {
        //    var target = eventElement.target;
        //    switch (target.mode) {
        //        case "month":
        //            var cal = $find("calendar2");
        //            cal._visibleDate = target.date;
        //            cal.set_selectedDate(target.date);
        //            cal._switchMonth(target.date);
        //            cal._blur.post(true);
        //            cal.raiseDateSelectionChanged();
        //            break;
        //    }
        //}

        //function MonthTextBox(event) {
        //    event.preventDefault();
        //}

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
                                                <asp:Label ID="Label6" runat="server" Text="Report Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" Width="250px">
                                                <%--<asp:ListItem Value="1" Text="Units"></asp:ListItem>--%>
                                                <asp:ListItem Value="2" Text="Carton Wise" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Value Wise"></asp:ListItem>
                                            </asp:RadioButtonList>
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
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="200px"></asp:DropDownList>
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
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="100px" Height="9px" Text="From Month"></asp:Label></strong></td>
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
                                                <asp:Label ID="Label3" runat="server" Width="55px" Text="To Month"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromDate" TargetControlID="txtFromDate"></cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate"></cc1:CalendarExtender>
                            <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False" PopupButtonID="ImgBntFromDate" TargetControlID="txtFromDate" BehaviorID="calendar1" OnClientShown="onCalendarShown1" OnClientHidden="onCalendarHidden1" Format="MMM-yyyy"></cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate" BehaviorID="calendar2" OnClientShown="onCalendarShown2" OnClientHidden="onCalendarHidden2" Format="MMM-yyyy"></cc1:CalendarExtender>--%>
                            &nbsp; 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" Style="margin-left: 113px;" runat="server" CssClass="Button" Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel" Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
