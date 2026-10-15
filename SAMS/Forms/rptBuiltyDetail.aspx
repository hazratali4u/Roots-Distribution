<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptBuiltyDetail.aspx.cs" Inherits="Forms_rptBuiltyDetail" Title="SAMS :: Builty Detail Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script type="text/jscript">
        function pageLoad() {
            $("select").searchable();
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
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>Location</strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>Town</strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddlTown" runat="server" AutoPostBack="True" CssClass="DropList"
                                            OnSelectedIndexChanged="ddlTown_SelectedIndexChanged" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>Customer</strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>Transporter</strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddlTransporter" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>From Date</strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" Width="176px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>To Date</strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" Width="176px"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                    </td>
                                </tr>
                                <tr>

                                    <td>&nbsp;
                                        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                            TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;<asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
