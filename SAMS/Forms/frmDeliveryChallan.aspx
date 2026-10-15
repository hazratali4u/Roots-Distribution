<%@ Page Title="SAMS :: Delivery Challan" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDeliveryChallan.aspx.cs" Inherits="Forms_frmDeliveryChallan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
    <script language="JavaScript" type="text/javascript">

        function SelectAll() {
            var chkBoxList = document.getElementById('<%= grdInvoices.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
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
    </script>
    <div id="right_data">
     <%--Wait Update Panel Start --%>
                <div style="z-index: 5000; left: 612px; width: 100px; position: absolute; top: 50px;
                    height: 100px">
                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="31px" />
                                Wait....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
                <%--Wait Update Panel End --%>
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
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="70px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChbAllCategory" onclick="SelectAll()" runat="server" Text="All">
                                                </asp:CheckBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="300px" ScrollBars="Vertical"
                                    BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray">
                                    <asp:GridView ID="grdInvoices" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="SteelBlue" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                        Width="99.9%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DocumentNo" HeaderText="DC No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer Name">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="18%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Phone No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ADDRESS" HeaderText="Town">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="27%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        &nbsp; &nbsp;
                        <asp:Button ID="btnViewPDF" runat="server" Width="90" Text="View PDF" OnClick="btnViewPDF_Click"
                            CssClass="Button" />
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>

