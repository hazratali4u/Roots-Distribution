<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptDeliveryChallan.aspx.cs" Inherits="Forms_rptDeliveryChallan" Title="SAMS :: Delivery Challan By Order" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="78px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpArea" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
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
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
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
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label>
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
                                        </tr>
                                        <tr>
                                            <td>
                                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                    TargetControlID="txtStartDate">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                    TargetControlID="txtEndDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp; &nbsp;
                        <asp:Button ID="btnViewPDF" runat="server" Width="90" Text="View PDF" OnClick="btnViewPDF_Click"
                            CssClass="Button" />
                        <asp:Button ID="btnViewExcel" runat="server" Width="90" Text="View Excel" OnClick="btnViewExcel_Click"
                            CssClass="Button" />
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
