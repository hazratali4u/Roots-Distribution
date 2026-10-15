<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChequeBook.aspx.cs" Inherits="Forms_frmChequeBook" Title="SAMS :: Cheque Book" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../js/jquery-1.10.2.js" type="text/javascript"></script>
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
        function ErrorMessage() {
            alert("Can't inactive as its cheques are in use");
        }
        function ValidateForm() {
            var str;
            <%--str = document.getElementById('<%=txtBranchCode.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Branch code is required');
                return false;
            }--%>
            str = document.getElementById('<%=txtPrefix.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Cheque no. prefix is required');
                return false;
            }
            str = document.getElementById('<%=txtCheckfrom.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Cheque From Range is required');
                return false;
            }
            str = document.getElementById('<%=txtcheckto.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Cheque To Range is required');
                return false;
            }
            str = document.getElementById('<%=txtIssueDate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Issue date is required');
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
                                                <asp:DropDownList ID="drpAccountno" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 12px"></td>
                                            <td style="height: 12px" align="left">
                                                <strong>Branch Code</strong>
                                            </td>
                                            <td style="width: 219px; height: 12px">
                                                <asp:TextBox ID="txtBranchCode" Width="200px" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Branch No.</strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:TextBox ID="txtBranchNo" runat="server" Width="200px" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblMobileNo" runat="server" Width="96px" Text="CP Number" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 219px">
                                                <asp:TextBox ID="txtCPNumber" Width="200px" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Account Title</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtAccountTitle" Width="200px" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <strong>Prefix</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:TextBox ID="txtPrefix" Width="200px" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                                    TargetControlID="txtPrefix" ValidChars="QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklxcvbnm">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Cheque No. From</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtCheckfrom" Width="200px" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredtxtFaxNumber" runat="server" FilterType="Custom"
                                                    TargetControlID="txtCheckfrom" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td style="height: 26px" align="left">
                                                <strong>Cheque No. To</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">
                                                <asp:TextBox ID="txtcheckto" Width="200px" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                    TargetControlID="txtcheckto" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>IBAN</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtIBAN" Width="200px" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td align="left">
                                                <strong>Issue Date</strong>
                                            </td>
                                            <td style="width: 219px; height: 26px">

                                                <asp:TextBox ID="txtIssueDate" runat="server" CssClass="txtBox" MaxLength="10"
                                                    onkeyup="BlockStartDateKeyPress()" Width="182px"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" align="left">
                                                <strong>Remarks</strong>
                                            </td>
                                            <td style="width: 175px; height: 26px">
                                                <asp:TextBox ID="txtremarks" Width="200px" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="500"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 26px" valign="top"></td>
                                            <td align="left"></td>
                                            <td style="width: 219px; height: 26px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                    TargetControlID="txtIssueDate" OnClientShown="calendarShown">
                                                </cc1:CalendarExtender>

                                            </td>

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
                                            <td style="width: 1px"></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"
                                                    Font-Bold="true"></asp:CheckBox>
                                            </td>
                                            <td style="width: 219px"></td>
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
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset; width: 100%; border-bottom: silver thin inset; background-color: silver">
                        <tbody>
                            <tr>
                                <td style="height: 21px" align="left">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="ACCOUNT_TITLE">Account Title</asp:ListItem>
                                        <asp:ListItem Value="ACCOUNT_DETAIL">Branch Code</asp:ListItem>
                                        <asp:ListItem Value="CP_NUMBER">CP Number</asp:ListItem>
                                        <asp:ListItem Value="IBAN">IBAN</asp:ListItem>
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
                    <asp:Panel ID="Panel2" Style="margin-top: -10px;" runat="server" Width="100%" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_Country" runat="server" ForeColor="SteelBlue" CssClass="tablesorter"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue"
                            OnRowEditing="Grid_Country_RowEditing" OnPageIndexChanging="Grid_Country_PageIndexChanging" PageSize="8" AllowPaging="true">
                            <RowStyle ForeColor="Black"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="ACCOUNT_NAME" ReadOnly="true" HeaderText="Bank Name" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="30%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ACCOUNT_TITLE" ReadOnly="true" HeaderText="Account Title" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="15%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHECK_BOOK_ID" ReadOnly="true" HeaderText="CHECK_BOOK_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BANK_ACCOUNT_ID" ReadOnly="true" HeaderText="BANK_ACCOUNT_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ACCOUNT_DETAIL" ReadOnly="true" HeaderText="Branch Code" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHECK_FROM" ReadOnly="true" HeaderText="Cheque From" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CHECK_TO" ReadOnly="true" HeaderText="Cheque To" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIME_STAMP" ReadOnly="true" HeaderText="TIME_STAMP" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARKS" ReadOnly="true" HeaderText="Remarks" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px">
                                    <HeaderStyle Width="25%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" ReadOnly="true" HeaderText="Status" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="7%"></HeaderStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="BRANCH_CODE" ReadOnly="true" HeaderText="Status">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BRANCH_NO" ReadOnly="true" HeaderText="Status">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IBAN" ReadOnly="true" HeaderText="Status">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CP_NUMBER" ReadOnly="true" HeaderText="Status">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="STATUS" ReadOnly="true" HeaderText="Status">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PREFIX" ReadOnly="true" HeaderText="PREFIX">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit" HeaderStyle-Width="5%" HeaderStyle-Font-Size="9pt" HeaderStyle-Height="25px" ItemStyle-HorizontalAlign="Center">
                                </asp:CommandField>--%>
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
