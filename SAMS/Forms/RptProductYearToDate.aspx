<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptProductYearToDate.aspx.cs" Inherits="Forms_RptProductYearToDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }
        function onCalendarShown() {
            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
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
                                <tr>
                                    <td align="left" colspan="4">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="73px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Principal" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Width="79px" Text="Report Type" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:RadioButtonList ID="rblUnitType" runat="server" Width="199px" CssClass="DropList" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Unit" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Carton" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblSKU" runat="server" Width="79px" Text="SKU" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpSKU" runat="server" Width="199px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Height="13px" Text="Year" Width="70px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="4" onkeyup="BlockStartDateKeyPress()"
                                            Width="60px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                    </td>
                                </tr>
                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                <cc1:CalendarExtender ID="CEStartDate" runat="server" BehaviorID="calendar1" Format="yyyy"
                                    PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate" OnClientShown="onCalendarShown"
                                    OnClientHidden="onCalendarHidden" />
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="right_data">
                        <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                            OnClick="btnViewPDF_Click" />
                        <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                            Width="90" OnClick="btnViewExcel_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
