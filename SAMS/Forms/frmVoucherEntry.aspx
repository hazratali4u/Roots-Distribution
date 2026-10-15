<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVoucherEntry.aspx.cs" Inherits="Forms_frmVoucherEntry" Title="SAMS :: Voucher Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function pageLoad() {
            $("#<%=drpDistributor.ClientID %>").searchable();
            $("#<%=ddlAccounts.ClientID %>").searchable();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

        }
        <%--function ValidateVoucherDate(txtVoucher) {
            var voucherDt = document.getElementById("<%=txtVoucherDate.ClientID %>").value;
            var year = voucherDt.split("/")[2];
            var month = voucherDt.split("/")[1];
            var date = voucherDt.split("/")[0];
            if (year <= 2012) {
                alert('Voucher Date must be greater than 31-May-2013');
                setTimeout(function () { txtVoucher.focus() }, 10);
            }
            else {
                if (year == 2013) {
                    if (month < 6) {
                        alert('Voucher Date must be greater than 31-May-2013');
                        setTimeout(function () { txtVoucher.focus() }, 10);
                    }
                    else if (month > 12) {
                        alert('Voucher date is invalid.');
                    }
                }
            }

        }--%>

        function ValidateForm() {
            <%--var voucherDt = document.getElementById("<%=txtVoucherDate.ClientID %>").value;
            var year = voucherDt.split("/")[2];
            var month = voucherDt.split("/")[1];
            var date = voucherDt.split("/")[0];
            if (year <= 2012) {
                alert('Voucher Date must be greater than 31-May-2013');
                return false;
            }
            else {
                if (year == 2013) {
                    if (month < 6) {
                        alert('Voucher Date must be greater than 31-May-2013');
                        return false;
                    }
                    else if (month > 12) {
                        alert('Voucher date is invalid.');
                    }
                }
            }--%>

            var str;

            str = document.getElementById("<%= txtSlipNo.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must Enter Slip No');
                document.getElementById("<%= txtSlipNo.ClientID %>").focus();
                return false;
            }
            str = document.getElementById("<%= txtpayeesName.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must Enter Payee Name');
                document.getElementById("<%= txtpayeesName.ClientID %>").focus();
                return false;
            }
            str = document.getElementById("<%= txtVDate.ClientID %>").value;
            if (str == null || str.length < 9) {
                alert('Must Enter Voucher Date');
                document.getElementById("<%= txtVDate.ClientID %>").focus();
                return false;
            }
            str = document.getElementById("<%= txtRemarks.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must Enter Narration');
                document.getElementById("<%= txtRemarks.ClientID %>").focus();
                return false;
            }
            var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
            var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;

            if (Debit.length == 0 && Credit.length == 0) {
                alert('Debit or Credit Must Enter');
                return false;
            }

            if (Debit > 0 && Credit > 0) {
                alert('Only Debit or Credit Can Enter');
                return false;
            }
            return true;
        }
        <%-- function SearchList() {
            var l = document.getElementById('<%= LstAccountHead.ClientID %>');
            var tb = document.getElementById('<%= txtAccountCode.ClientID %>');

            if (tb.value == "") {
                ClearSelection(l);
            }
            else {
                for (var i = 0; i < l.options.length; i++) {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                        l.options[i].selected = true;
                        return false;
                    }
                    else {
                        ClearSelection(l);

                    }
                }
            }
        }
        function SearchedCode() {

            var str
            var stroption
            str = document.getElementById("<%= LstAccountHead.ClientID %>").value;
            stroption = document.getElementById("<%= txtAccountName.ClientID %>").value;

            if (str.length > 0) {
                document.getElementById("<%= txtAccountCode.ClientID %>").value = str.substring(str.indexOf('~') + 1);
                document.getElementById("<%= txtAccountName.ClientID %>").value = str.substring(0, str.indexOf('~'));
                document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";


            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= LstAccountHead.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= LstAccountHead.ClientID %>'));

        }

        function SelectCode(e) {
            if (e.keyCode == 13) {
                var str = document.getElementById("<%= LstAccountHead.ClientID %>").value;
                document.getElementById("<%= txtAccountCode.ClientID %>").value = str.substring(0, str.indexOf('~'));
                document.getElementById("<%= txtAccountName.ClientID %>").value = str.substring(str.indexOf('~') + 1);
                document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
                document.getElementById("<%= txtAccountDes.ClientID %>").focus();
            }
        }
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
        --%>   
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
                                            <td style="width: 99px; height: 25px" align="left">
                                                <strong>Location</strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="206px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left"></td>

                                        </tr>
                                        <tr>
                                            <td style="width: 99px" align="left">
                                                <strong>Voucher Type</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpVoucherType" runat="server" Width="205px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left"></td>
                                            <td style="width: 99px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblSubType" runat="server" Width="120px" Text="Voucher Sub Type"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpVoucherSubType" runat="server" Width="205px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherSubType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Payment Mode</strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:DropDownList ID="DrpPaymentMode" runat="server" Width="206px" CssClass="DropList" OnSelectedIndexChanged="DrpPaymentMode_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="19">Cash</asp:ListItem>
                                                    <asp:ListItem Value="18">Cheque</asp:ListItem>
                                                    <asp:ListItem Value="21">Pay Order</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 18px" align="left"></td>
                                            <td style="width: 99px; height: 18px" align="left">
                                                <strong>Bank/Cash Head</strong>
                                            </td>
                                            <td style="height: 18px" align="left">
                                                <asp:DropDownList ID="drpBanks" runat="server" Width="206px" CssClass="DropList" OnSelectedIndexChanged="drpBanks_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Voucher Date</strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:TextBox ID="txtVDate" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                <%--<asp:TextBox ID="txtVoucherDate" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>--%>
                                                <%--onblur="ValidateVoucherDate(this);"--%>
                                            </td>
                                            <td align="left"></td>
                                            <td style="width: 99px" align="left">
                                                <strong>Slip No</strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:TextBox ID="txtSlipNo" onkeyup="SearchList()" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblChequeNo" runat="server" Width="76px" Text="Cheque No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="200px" CssClass="txtBox " Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="drpCheque" runat="server" Width="202px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 20px" align="left"></td>
                                            <td style="width: 99px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblChequedate" runat="server" Width="78px" Text="Cheque Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:TextBox ID="txtChequeDate" onkeyup="SearchList()" runat="server" Width="200px"
                                                    CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="91px" Text="Payee's Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:TextBox ID="txtpayeesName" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label14" runat="server" Width="10px" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 99px; height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="85px" Text="Due Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:TextBox ID="txtDueDate" onkeyup="SearchList()" runat="server" Width="200px"
                                                    CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Narration</strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="200px" CssClass="txtBox " MaxLength="250"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px" align="left"></td>
                                            <td style="width: 99px; height: 25px" align="left"></td>
                                            <td style="height: 25px" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtChequeDate">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtVDate">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtDueDate">
                                </cc1:MaskedEditExtender>

                                <asp:TextBox ID="txtTotalDebit" runat="server" Width="100px" CssClass="HidePanel"></asp:TextBox><asp:TextBox
                                    ID="txtTotalCredit" runat="server" Width="73px" CssClass="HidePanel"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table style="border-right: #007395 1px solid; border-top: #007395 1px solid; border-left: #007395 1px solid; border-bottom: #007395 1px solid"
                                    cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td style="height: 16px">


                                                <asp:Label ID="Label13" runat="server" Width="260px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Account Description" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblskuname" runat="server" Width="190px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Remarks" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px; display: none;">
                                                <asp:Label ID="Label15" runat="server" Width="135px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Principal" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label11" runat="server" Width="89px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Debit" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label12" runat="server" Width="87px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Credit" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px"></td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Width="69px" Height="16px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px">
                                                <%-- <asp:TextBox ID="txtAccountCode" onkeyup="SearchList()" runat="server" Width="76px"
                                                    CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td colspan="1" rowspan="1">
                                                <asp:TextBox ID="txtAccountName" runat="server" Width="156px" ForeColor="Black" Font-Bold="False"
                                                    CssClass="txtBox " Enabled="False"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlAccounts" runat="server" Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 12px">
                                                <asp:TextBox ID="txtAccountDes" runat="server" Width="185px"
                                                    CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 12px; display: none;">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="133px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 12px">
                                                <asp:TextBox ID="txtDebitAmount" runat="server" Width="80px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 12px">
                                                <asp:TextBox ID="txtCreditAmount" runat="server" Width="80px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 12px"></td>
                                            <td style="width: 79px; height: 12px">
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="70px"
                                                    Font-Size="8pt" Text="Add New" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="8" rowspan="1">
                                                <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                                    BackColor="White" AutoGenerateColumns="False" BorderColor="White" CaptionAlign="Left"
                                                    HorizontalAlign="Center" OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting"
                                                    OnRowEditing="GrdOrder_RowEditing" ShowFooter="True" ShowHeader="False">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Account_Head_Id" ReadOnly="true" HeaderText="Account Head Id">
                                                            <FooterStyle CssClass="HidePanel" />
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Account_Code" ReadOnly="true" HeaderText="Account Code">
                                                            <FooterStyle BorderColor="Silver" Width="30%" BorderStyle="Solid" BorderWidth="1px" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="30%" BorderWidth="1px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Account_Name" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            <FooterStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REMARKS" ReadOnly="true" HeaderText="Account Description">
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Principal" ReadOnly="true" HeaderText="Principal">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                            <FooterStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="debit" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="Debit">
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="credit" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="Credit">
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Principal_id" ReadOnly="true" HeaderText="Principal">
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="35px"
                                                                HorizontalAlign="Center" />
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        </asp:CommandField>--%>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="35px"
                                                                HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="tblhead" />
                                                </asp:GridView>
                                                <%--  <div style="left: 55px; width: 325px; position: absolute; top: 350px; height: 200px">
                                                    <asp:Panel ID="Panel3" runat="server" Width="325px" Height="190px" CssClass="HidePanel"
                                                        BackColor="Silver" BorderColor="White" BorderStyle="Inset" BorderWidth="1px">
                                                        <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                                            border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                                        &nbsp;Select A<strong>ccount Head from List</strong>
                                                                    </td>
                                                                    <td style="border-bottom: black thin solid" valign="top" align="right">
                                                                        <asp:Button ID="Button5" runat="server" Width="21px" Height="16px" Font-Size="8pt"
                                                                            Text="X" BorderStyle="Groove" BorderWidth="1px"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <asp:ListBox ID="LstAccountHead" onkeydown="SelectCode(event)" runat="server" Width="325px"
                                                            Height="200px" CssClass="DropList"></asp:ListBox>
                                                    </asp:Panel>
                                                    &nbsp; &nbsp;&nbsp;
                                                </div>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="8"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCreditAmount"
                                    FilterType="Custom" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                                <div style="z-index: 101; left: 706px; width: 100px; position: absolute; top: 364px; height: 100px">
                                    <asp:Panel ID="Panel21" runat="server">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="35px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                    Width="39px" />
                                                Wait Update
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </asp:Panel>
                                </div>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDebitAmount"
                                    FilterType="Custom" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save"
                CssClass="Button" />
        </div>
        <br />
    </div>
</asp:Content>
