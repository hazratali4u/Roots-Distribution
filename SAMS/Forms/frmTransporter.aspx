<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmTransporter.aspx.cs" Inherits="Forms_frmTransporter" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <div id="right_data">
        <div style="z-index: 101; left: 40%; width: 100px; position: absolute; top: 100px; height: 100px">
            <asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="23px" />
                        Wait Update..
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 143px" align="left"></td>
                                            <td style="width: 175px">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td align="left"></td>
                                            <td></td>
                                        </tr>
                                        <tr>

                                            <td style="height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="96px" Text="Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 15px" colspan="4">
                                                <asp:TextBox ID="txtName" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="txtNamer" runat="server"
                                                    ErrorMessage="Transporter Name?" ControlToValidate="txtName" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                                <%--  <cc1:ValidatorCalloutExtender ID="vctxtName" runat="server" TargetControlID="txtName" HighlightCssClass="highlight" CssClass="CustomValidatorCalloutStyle" />
                                                --%></td>
                                        </tr>

                                        <tr>

                                            <td style="height: 22px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblPhNo" runat="server" Width="90px" Text="Phone No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 22px">
                                                <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="lblEmail" runat="server" Width="87px" Text="Email" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>

                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="vg"
                                                ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"></asp:RegularExpressionValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="lblAddress" runat="server" Width="114px" Text="Address "
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtAddress" runat="server" Width="500px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 21px" align="left"></td>
                                            <td style="width: 175px; height: 21px">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="82px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                <asp:Button ID="btnCancel" runat="server" Width="73px" Font-Size="8pt" Text="Cancel"
                                                    OnClick="btnCancel_Click" CssClass="Button" />
                                            </td>
                                            <td style="width: 1px; height: 21px"></td>
                                            <td style="height: 21px">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"></asp:CheckBox>
                                            </td>
                                            <td style="height: 21px"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                        Height="250px" ScrollBars="Vertical" Width="70%">
                        <asp:GridView ID="Grid_users" runat="server" Width="99.9%" ForeColor="SteelBlue"
                            CssClass="gridRow2" BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False"
                            BackColor="White" OnRowEditing="Grid_users_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="TRASNPOTER_ID" ReadOnly="true" HeaderText="User Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="NAME" ReadOnly="true" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="PHONE" ReadOnly="true" HeaderText="Phone No">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="EMAIL" ReadOnly="true" HeaderText="Email">

                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="ADDRESS" ReadOnly="true" HeaderText="Address">

                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="ACTIVE" ReadOnly="true" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:CommandField>--%>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


