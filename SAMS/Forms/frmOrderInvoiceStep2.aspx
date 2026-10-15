<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOrderInvoiceStep2.aspx.cs" Inherits="Forms_frmOrderInvoiceStep2" Title="SAMS :: Order/Invoice Step 2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function pageLoad() {
            $("#<%=ddlSkus.ClientID %>").searchable();
            ////////////////////////////////////////////

            var Str = $('#<%=ddlSkus.ClientID %> option:selected').text();

            var Detail = Str.split(':');
            var unitprice = Detail[1];

            document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;

            //

            $('#<%=ddlSkus.ClientID %>').change(function () {

                var Str = $('#<%=ddlSkus.ClientID %> option:selected').text();

                var Detail = Str.split(':');
                var unitprice = Detail[1];


                document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
            });

            ////////////////////////////////////////////
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
        }


        function ValidateForm() {


            var str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var str2 = document.getElementById('<%=txtCtn.ClientID%>').value;
            //if (str == null || str.length == 0) {
            if ((str == null || str.length == 0 || str == "0") && (str2 == null || str2.length == 0 || str2 == "0")) {

                alert('Must Enter Cotton or Unit');

                document.getElementById('<%=txtQuantity.ClientID%>').focus();

                return false;
            }

            return true;
        }

        function SearchList() {
            var l = document.getElementById('<%= ListCustomer.ClientID %>');
            var tb = document.getElementById('<%= txtOutletCode.ClientID %>');


            if (tb.value == "") {
                ClearSelection(l);
            }
            else {
                for (var i = 0; i < l.options.length; i++) {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                        l.options[i].selected = true;
                        ClearSelection(l);
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
            str = document.getElementById("<%= ListCustomer.ClientID %>").value;
            stroption = document.getElementById("<%= txtOutletCode.ClientID %>").value;

            if (str.length > 0) {
                //   document.getElementById("<%= txtOutletCode.ClientID %>").value = 'OT' + str.substring(str.indexOf('| ') + 2);
                //  document.getElementById("<%= txtOutletName.ClientID %>").value = str.substring(0, str.indexOf('|'));

                var split = str.split('|');
                document.getElementById("<%= txtOutletName.ClientID %>").value = split[0];
                document.getElementById("<%= txtOutletCode.ClientID %>").value = 'OT' + split[1];
                document.getElementById("<%= txtPriceGroup.ClientID %>").value = split[2];

                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";

            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= ListCustomer.ClientID %>").focus();
                ClearSelection(l);
            }
        ClearSelection(document.getElementById('<%= ListCustomer.ClientID %>'));
        }

        function SelectCode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var str = document.getElementById("<%= ListCustomer.ClientID %>").value;
                //  document.getElementById("<%= txtOutletCode.ClientID %>").value = "OT" + str.substring(str.indexOf('|') + 2);
                // document.getElementById("<%= txtOutletName.ClientID %>").value = str.substring(0, str.indexOf('|'));

                var split = str.split('|');
                document.getElementById("<%= txtOutletName.ClientID %>").value = split[0];
                document.getElementById("<%= txtOutletCode.ClientID %>").value = 'OT' + split[1];

                document.getElementById("<%= txtPriceGroup.ClientID %>").value = split[2];

                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";

                document.getElementById('<%= btnOutletCode.ClientID %>').click();

                document.getElementById("<%= ddlSkus.ClientID %>").focus();
                ClearSelection(l);
            }
        }
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
            document.getElementById("<%= txtOutletName.ClientID %>").value = "";
            document.getElementById("<%= txtOutletCode.ClientID %>").value = "";
        }

    </script>
    <div id="right_data">
        <div>
            <span class="heading">Order/Invoice Step 2</span>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnOutletCode" runat="server" Style="display: none;" OnClick="btnOutletCode_Click" />
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <div style="left: 280px; position: absolute; top: 190px;">
                                                    <asp:Panel ID="Panel1" runat="server" Width="300px" Height="230px" BorderWidth="1px"
                                                        BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                                        <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove; border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="border-bottom: black thin solid" align="left" colspan="2">&nbsp;<strong>Select Customer from List Press Enter</strong>
                                                                    </td>
                                                                    <td style="border-bottom: black thin solid" valign="top" align="right">
                                                                        <asp:Button AccessKey="S" ID="Button4" runat="server" Width="27px" Height="24px"
                                                                            Font-Size="8pt" Text="X" BorderWidth="1px" BorderStyle="Groove" CssClass="Button" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <asp:ListBox ID="ListCustomer" onkeydown="SelectCode(event)" runat="server" Width="99%"
                                                            Height="86%"></asp:ListBox>
                                                    </asp:Panel>
                                                </div>
                                                <div style="z-index: 101; left: 820px; width: 100px; position: absolute; top: 369px; height: 100px">
                                                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                            <ProgressTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                    Width="31px" />
                                                                Wait Update
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </asp:Panel>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tbody>
                                                                <%--<tr>
                                                                    <td align="left" style="width: 100px;">
                                                                        <strong><asp:Label ID="lblPrincipal" runat="server" Text="Principal"></asp:Label></strong>
                                                                    </td>
                                                                    <td style="width: 238px; height: 25px" align="left" colspan="3">
                                                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="231px" CssClass="DropList"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>

                                                                </tr>--%>
                                                                <tr>
                                                                    <td valign="top" align="left">
                                                                        <strong>Order No</strong>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <asp:DropDownList ID="drpDocumentNo" runat="server" Width="231px" CssClass="DropList"
                                                                            OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <strong>
                                                                            <asp:Label ID="lblBillBook" TabIndex="1" runat="server" Text="Bill Book No:" CssClass="lblbox"></asp:Label></strong>
                                                                        <asp:TextBox ID="txtBillBookNo" runat="server" Width="100px"
                                                                            CssClass="txtBox "></asp:TextBox>

                                                                        <%--<asp:TextBox ID="" runat="server" CssClass="uppercase" Width="105px" MaxLength="10"></asp:TextBox>--%>
                                                                        <asp:CheckBox ID="ChbDiscount" runat="server" Visible="False" Width="90px" Text="Promotion"
                                                                            AutoPostBack="True" Checked="True"></asp:CheckBox>
                                                                    </td>
                                                                    <td>
                                                                        <strong>
                                                                            <asp:Label ID="lblGSTInv" TabIndex="1" runat="server" Text="GST Invoice #:" CssClass="lblbox"></asp:Label></strong>
                                                                        <asp:TextBox ID="txtGSTInv" runat="server" Width="94px"
                                                                            CssClass="txtBox "></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="left">
                                                                        <strong>Order Type</strong>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <asp:RadioButtonList ID="RblPayMode" runat="server" Width="228px" Height="1px" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="214">Cash</asp:ListItem>
                                                                            <asp:ListItem Value="215" Selected="True">Credit</asp:ListItem>
                                                                            <asp:ListItem Value="216">Advance</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="1">
                                                                        <asp:CheckBox ID="ChbBatchNo" runat="server" Width="94px" Text="Batch No" AutoPostBack="True"
                                                                            OnCheckedChanged="ChbBatchNo_CheckedChanged"></asp:CheckBox>
                                                                    </td>
                                                                    <td style="display: none">
                                                                        <strong>Builty #</strong>
                                                                        <asp:TextBox ID="txtBuiltyNo" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtBuiltyAmount" runat="server" placeHolder="Amount" Width="94px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="left">
                                                                        <strong>Route/Saleman</strong>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <asp:TextBox ID="txtprincipal" runat="server" Width="277px" CssClass="txtBox "
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="1">
                                                                        <asp:TextBox ID="txtDeliveryMan" runat="server" Width="180px" CssClass="txtBox"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="display: none;">
                                                                        <strong>Transporter</strong>
                                                                        <asp:DropDownList ID="DrpTransporter" runat="server" Width="180px" CssClass="DropList">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 23px" align="left">
                                                                        <strong>Customer</strong>
                                                                    </td>
                                                                    <td style="height: 23px" align="left">
                                                                        <asp:TextBox ID="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px"
                                                                            CssClass="txtBox "></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 23px" align="left">
                                                                        <asp:TextBox ID="txtOutletName" onfocus="SearchedCode()" runat="server" Width="192px"
                                                                            CssClass="txtBox "></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 23px" align="left">
                                                                        <asp:TextBox ID="txtPriceGroup" runat="server" CssClass="txtBox" Width="180px"
                                                                            placeHolder="Price Group" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 23px" align="left">
                                                                        <asp:TextBox ID="txtDiscountType" onkeyup="SearchList()" runat="server" Width="180px"
                                                                            CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="height: 126px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr class="detailClass">
                                                                    <td>
                                                                        <label>
                                                                            SKU Description
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Ctn
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Unit
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Discount
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Unit Price
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Batch No
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Amount
                                                                        </label>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSkus" runat="server" Width="260px" Style="z-index: inherit;">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCtn" runat="server" Width="70px"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCtn"
                                                                            ValidChars="0123456789" runat="server">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtBox" Width="55px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="txtBox " Enabled="false" Width="55px"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbe_discount" TargetControlID="txtDiscount"
                                                                            ValidChars="0123456789." runat="server">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtUnitRate" runat="server" Width="56px" CssClass="txtBox" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBatchNo" runat="server" CssClass="txtBox " Enabled="False" Width="62px">N/A</asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="txtBoxnNum" Enabled="False" Width="72px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add"
                                                                            ValidationGroup="vg" Width="95px" AccessKey="A" CssClass="Button" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="8">
                                                                        <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="800px"
                                                                            BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                                            <asp:GridView ID="GrdPurchase" runat="server" BackColor="White" AutoGenerateColumns="False"
                                                                                BorderColor="White" OnRowEditing="GrdPurchase_RowEditing" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting"
                                                                                OnRowCommand="GrdPurchase_RowCommand" Width="100%">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="SKU Code">
                                                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="75px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="SKU Name">
                                                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="200px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="QUANTITY_CTN" ReadOnly="true" HeaderText="Ctn">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="QUANTITY_UNIT" ReadOnly="true" HeaderText="Quantity">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="STANDARD_DISCOUNT" ReadOnly="true" HeaderText="Discount" DataFormatString="{0:F2}">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="UNIT_PRICE" ReadOnly="true" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="BATCH_NO" ReadOnly="true" HeaderText="Batch No">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Amount" ReadOnly="true" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                                            Width="80px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="ORG_QUANTITY_CTN" ReadOnly="true">
                                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                                        <ItemStyle CssClass="HidePanel" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="ORG_QUANTITY_UNIT" ReadOnly="true">
                                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                                        <ItemStyle CssClass="HidePanel" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Edit">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:CommandField>--%>
                                                                                    <%--<asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" class="fa fa-pencil" CommandArgument='<%#Eval("SKU_ID")%>' CommandName="Edit" ToolTip="Edit">
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Delete">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                                                CommandName="Delete"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px"
                                                                                            HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:HiddenField ID="hfBillBookNo" runat="server" />
                                                <asp:HiddenField ID="txtTotalPurchaseAmount" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU" CssClass="lblbox"></asp:Label></strong>
                                                            <asp:Panel
                                                                ID="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                                                BorderWidth="1px" ScrollBars="Auto">
                                                                <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                                    BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                        PreviousPageText="Previous"></PagerSettings>
                                                                    <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black"></RowStyle>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                                            <ItemStyle Width="80px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                                            <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                                            <ItemStyle Width="50px" HorizontalAlign="Center" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ORG_QUANTITY" HeaderText="ORG_QUANTITY">
                                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="Label2" runat="server" Width="60px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                                            <asp:Panel
                                                                ID="Panel3" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                                                BorderWidth="1px" ScrollBars="Auto">
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox" Width="343px" Height="82px" TextMode="MultiLine"></asp:TextBox>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td></td>
                                                        <td style="height: 20px; width: 177px;" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblDocumentNo1" runat="server" Width="94px" Text="Gross Sale"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 7px; height: 20px" align="right">
                                                            <asp:TextBox ID="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="height: 18px; width: 177px;" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label51" runat="server" Width="110px" Text="Standard Discount" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 7px; height: 18px" align="right">
                                                            <asp:TextBox ID="numTxtTotalStndrdDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                                Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="numtxtTotalExtraDiscntpr" runat="server" Width="0px" ForeColor="Black"
                                                                Font-Bold="True" CssClass="txtBoxnNum" Visible="false" Text="0"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 177px" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblfromLocation1" runat="server" Width="94px" Text="Extra Discount"
                                                                    CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td align="right">
                                                            <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                                Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="numtxtSpecialDiscPr" runat="server" Width="0px" ForeColor="Black"
                                                                Font-Bold="True" Text="0" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 177px" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblSpecialDiscount" runat="server" Width="120px" Text="Special Discount"></asp:Label></strong>
                                                        </td>
                                                        <td align="right">
                                                            <asp:TextBox ID="numtxtSpecialDiscValue" runat="server" Width="150px" ForeColor="Black"
                                                                Font-Bold="True" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="numtxtExpiryDiscPr" runat="server" Width="0px" ForeColor="Black"
                                                                Font-Bold="True" Text="0" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 177px" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label1" runat="server" Width="120px" Text="Expiry Discount"></asp:Label></strong>
                                                        </td>
                                                        <td align="right">
                                                            <asp:TextBox ID="numtxtExpiryDiscValue" runat="server" Width="150px" ForeColor="Black"
                                                                Font-Bold="True" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="height: 20px; width: 177px;" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label8" runat="server" Width="116px" Text="Claimable  Discount" Visible="false"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 7px; height: 20px" align="right">
                                                            <asp:TextBox ID="numtxtUnClaimabledist" runat="server" Width="150px" Visible="false"
                                                                ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="height: 18px; width: 177px;" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label81" runat="server" Width="93px" Text="GST Amount" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 7px; height: 18px" align="right">
                                                            <asp:TextBox ID="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="height: 18px; width: 177px;" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label9" runat="server" Width="93px" Text="TST Amount" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 7px; height: 18px" align="right">
                                                            <asp:TextBox ID="numTxtTotalTST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="height: 18px; width: 177px;" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label91" runat="server" Width="105px" Text="Net Amount" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 7px; height: 18px" align="right">
                                                            <asp:TextBox ID="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="C" ID="btnCalculate" TabIndex="100" OnClick="btnCalculate_Click"
                                                                    runat="server" Width="100px" Font-Size="8pt" Text="Calculate" Enabled="False"
                                                                    CssClass="Button" />
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="S" ID="btnSaveOrder" TabIndex="101" OnClick="btnSaveOrder_Click"
                                                                    runat="server" Width="110px" Font-Size="8pt" Text="Save Order" Enabled="False"
                                                                    CssClass="Button" />
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="H" ID="btnCancel" TabIndex="102" runat="server" Width="110px"
                                                                    Font-Size="8pt" Text="Home" OnClick="btnCancel_Click" CssClass="Button" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 177px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="105px" Text="Cash Received" CssClass="lblbox"
                                                        Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px" align="right">
                                                <asp:TextBox ID="txtCashReceived" runat="server" Width="150px" Visible="false" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveOrder" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:TextBox ID="numTxtTotalSED" runat="server" CssClass="txtBox " Font-Bold="False"
                            ForeColor="Black" Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <asp:UpdatePanel ID="PopupUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblCancel" runat="server"></asp:Label>
            <asp:LinkButton runat="server" ID="hbutton"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="PopupExtender" runat="server" PopupControlID="pnlParameters" TargetControlID="hbutton" BehaviorID="ModelPopup" BackgroundCssClass="modal-background" CancelControlID="lblCancel"></cc1:ModalPopupExtender>
            <asp:Panel ID="pnlParameters" runat="server" Style="display: none; border: 0px solid silver;" ScrollBars="Auto" DefaultButton="BtnYes">
                <div class="modal-dialog" style="width: 400px; margin-left: 0px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="btnClose" class="close" runat="server" visible="false"></button>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table style="border: 1px solid silver; width: 100%; background-color: whitesmoke;">
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="../images/warning_256.png" width="25" alt="" style="margin-left: 10px;" />
                                        </td>
                                        <td style="font-family: Verdana; color: red;">Are you sure you want to
                                    <asp:Label ID="lblbtnName" runat="server"></asp:Label>?
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="BtnYes" Style="margin-right: 12px; margin-bottom: 7px;" runat="server" Text="Yes" CssClass="Button" Width="70px" OnClick="BtnYes_Click" />
                                            <asp:Button ID="BtnNo" Style="margin-right: 20px; margin-bottom: 7px;" runat="server" Text="No" CssClass="Button" Width="70px" OnClick="BtnNo_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
