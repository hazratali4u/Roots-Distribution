<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptDailyOrder.aspx.cs" Inherits="Forms_rptDailyOrder"
    Title="SAMS :: Daily Order Sheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 1px; height: 30px" align="left">
                                        </td>
                                        <td style="height: 30px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 30px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="210px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 30px" align="left">
                                        </td>
                                        <td style="height: 30px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="48px" Text="Principal"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 30px" align="left">
                                            <asp:DropDownList ID="ddlPrincipal" runat="server" Width="210px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="59px" Text="Date" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="153px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
