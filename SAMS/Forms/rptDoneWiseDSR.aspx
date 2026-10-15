<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDoneWiseDSR.aspx.cs" Inherits="Forms_rptDoneWiseDSR"
    MasterPageFile="~/Forms/PageMaster.master" Title="SAMS :: Zone Wise DSR" %>

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
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" colspan="1">
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 49px; height: 28px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Region" Width="47px"></asp:Label></strong>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="height: 16px" align="left">
                                            <asp:DropDownList ID="drpRegionTerritory" runat="server" Width="200px" AutoPostBack="True"
                                                CssClass="DropList" OnSelectedIndexChanged="drpRegionTerritory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px; height: 16px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left" style="width: 49px; height: 28px">
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Text="Zone" Width="47px"></asp:Label></strong>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DrpZone" runat="server" Width="200px" AutoPostBack="True" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px; height: 8px">
                                        </td>
                                    </tr>
                                    
                                  
                                    <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Principal" Width="61px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                  
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="59px" Text="From Date" CssClass="lblbox"></asp:Label></strong>
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
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="57px" Text="To  Date" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="153px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblSortOrder" runat="server" CssClass="lblbox" Text="Sort Order" Width="78px"
                                                    Visible="false"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rbtSortOrder" runat="server" RepeatDirection="Horizontal"
                                                Visible="false" Width="192px">
                                                <asp:ListItem Selected="True" Text="Ascending"></asp:ListItem>
                                                <asp:ListItem Text="Descending"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="Button"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
