
<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptClaim.aspx.cs" Inherits="Forms_RptClaim"
    Title="SAMS :: Claim Voucher" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }
        function pageLoad() {
            $("#<%=DrpLocation.ClientID %>").searchable();
            $("#<%=DrpCustomer.ClientID %>").searchable();
            $("#<%=DrpRoute.ClientID %>").searchable();
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
                                    <td align="left" colspan="3">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="" Width="94px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="height: 25px" align="left">
                                       <asp:RadioButtonList ID="RbdClaimType" runat="server" Width="215px" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="94px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpLocation" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                    <%--<tr>
                                        <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Town"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                        <td align="left">
                                    </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="94px" Text="Town"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                        <td align="left">
                                    </td>
                                            <td style="height: 25px" align="left">
                                               <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                                <%-- <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>--%>
                                            </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Customer" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="76px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        &nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                            onkeyup="BlockStartDateKeyPress()" Width="175px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        &nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                            onkeyup="BlockEndDateKeyPress()" Width="175px"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
                    <asp:Button ID="btnViewPDF" runat="server" Style="margin-left:109px;" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Width="90" Text="View Excel"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>