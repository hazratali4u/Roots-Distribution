<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSkipCheque.aspx.cs" Inherits="Forms_frmSkipCheque" Title="SAMS :: Cancel Cheque" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">

    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function Change(obj, evt) {
            if (evt.type == "focus") {
                obj.style.border = "1px solid black";
            }
            else if (evt.type == "blur") {
                obj.style.border = "0px";
            }
        }
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=drpBankAccount.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select bank account');
                return false;
            }
            str = document.getElementById('<%=drpCheque.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select cheque no.');
                return false;
            }
            return true;
        }

    </script>

    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Bank Account</strong>
                                            </td>
                                            <td style="width: 175px; height: 12px">
                                                <asp:DropDownList ID="drpBankAccount" runat="server" Width="250px" CssClass="DropList" OnSelectedIndexChanged="drpBankAccount_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Cheque No.</strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:DropDownList ID="drpCheque" runat="server" Width="250px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Remarks</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtremarks" Width="350px" Height="40px" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="500"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 175px; height: 26px" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="width: 175px">
                                                <asp:Button ID="btnSaveCountry" runat="server" CssClass="Button" Font-Size="8pt" Width="80px" onblur="Change(this, event)"
                                                    OnClick="btnSaveCountry_Click" onfocus="Change(this, event)" Text="Save" />
                                                <asp:Button ID="btnCancelCountry" OnClick="btnCancelCountry_Click" Font-Size="8pt" Width="80px" runat="server" CssClass="Button" onblur="Change(this, event)"
                                                    onfocus="Change(this, event)" Text="Cancel" />
                                                <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="A1"
                                                    runat="server">dummy</a>
                                                <asp:HiddenField runat="server" ID="hfStatus" Value="0" />
                                            </td>
                                            <%--<td style="width: 1px"></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"
                                                    Font-Bold="true"></asp:CheckBox>
                                            </td>
                                            <td style="width: 219px"></td>--%>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 244px; height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset; width: 70%; border-bottom: silver thin inset; background-color: silver">
                        <tbody>
                            <tr>
                                <td style="height: 21px" align="left">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="CHEQUE_NO">Cheque Number</asp:ListItem>
                                        <asp:ListItem Value="REMARKS">REMARKS</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="height: 21px" align="left" width="250">
                                    <asp:Button ID="btnSearchGrid" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnSearchGrid_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel2" Style="margin-top: -10px;" runat="server" Width="70%" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_Country" runat="server" ForeColor="SteelBlue" CssClass="tablesorter"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue"
                            OnRowEditing="Grid_Country_RowEditing" OnPageIndexChanging="Grid_Country_PageIndexChanging" PageSize="8" AllowPaging="true">
                            <RowStyle ForeColor="Black"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="ACCOUNT_NAME" ReadOnly="true" HeaderText="Bank Name" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="20%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHEQUE_ID" ReadOnly="true" HeaderText="CHEQUE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHEQUE_NO" ReadOnly="true" HeaderText="Cheque No" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BANK_ACCOUNT_ID" ReadOnly="true" HeaderText="BANK_ACCOUNT_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARKS" ReadOnly="true" HeaderText="Remarks" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="23%"></HeaderStyle>
                                </asp:BoundField>
                                <%--<asp:CommandField ShowEditButton="True" HeaderText="Action" EditText="Reactive" ButtonType="Link" HeaderStyle-Width="2%" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px" ItemStyle-HorizontalAlign="Center"></asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <ItemStyle Width="2%"
                                        HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Edit" OnClientClick="javascript:return confirm('Are you sure you want to Reactive this cheque?');return false;"
                                            Text="Reactive"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                                Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333"></AlternatingRowStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
