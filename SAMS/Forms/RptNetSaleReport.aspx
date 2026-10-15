<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptNetSaleReport.aspx.cs" Inherits="Forms_RptNetSaleReport" Title="SAMS :: Purchase Document" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td align="left">
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
                                            <asp:Label ID="Label2" runat="server" Height="13px" Text="Report Type" Width="70px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height:25px">
                                        <asp:RadioButtonList ID="RbtType" runat="server" RepeatDirection="Horizontal" Width="245px">
                                            <asp:ListItem Selected="True" Value="1"> Customer Wise</asp:ListItem>
                                            <asp:ListItem Value="2"> Principal Wise</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="94px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="230px" CssClass="DropList">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="Channel Type" Width="94px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="ddlChannelType" runat="server" Width="230px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="height: 2px" align="left"></td>
                                    <td style="width: 90px; height: 2px" align="left">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Width="48px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                    <td style="width: 1px; height: 2px" align="left"></td>
                                    <td style="width: 204px; height: 2px" align="left">
                                        <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="All Principals" AutoPostBack="True" OnCheckedChanged="ChbAllCatagories_CheckedChanged"></asp:CheckBox></td>
                                    <td style="width: 1px; height: 2px" align="left"></td>
                                </tr>
                                <tr>
                                    <td style="height: 2px" align="left"></td>
                                    <td style="height: 2px" align="left"></td>
                                    <td style="height: 2px" align="left" colspan="2">
                                        <asp:Panel ID="Panel1" runat="server" Width="225px" Height="150px" ScrollBars="Vertical" Style="margin-left: 6px; border: groove;" BorderWidth="1px">
                                            <asp:CheckBoxList ID="ListCatagory" runat="server" AutoPostBack="True"></asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 1px; height: 2px" align="left"></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="76px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                        onkeyup="BlockStartDateKeyPress()" Width="205px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" /></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                        onkeyup="BlockEndDateKeyPress()" Width="205px"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" /></td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left"></td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy"
                                            PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
        <asp:Button ID="btnViewPDF" Style="margin-left:100px;" runat="server" CssClass="button"
            Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="button"
                        Text="View Excel" Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
