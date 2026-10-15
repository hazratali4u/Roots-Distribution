<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseDocuments.aspx.cs" Inherits="Forms_frmPurchaseDocuments" Title="SAMS :: Stock Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddlSkus.ClientID %>").searchable();
            $("#<%=DrpDocumentType.ClientID %>").searchable();
            $("#<%=drpDocumentNo.ClientID %>").searchable();
            $("#<%=drpDistributor.ClientID %>").searchable();
            $("#<%=drpPrincipal.ClientID %>").searchable();
            $("#<%=DrpTransferFor.ClientID %>").searchable();
            $("#<%=DrpAccountDetail.ClientID %>").searchable();
            $("#<%=DrpExpenseAccountHead.ClientID %>").searchable();
            
        }

        function ValidateForm() {
            var str;


            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var str2 = document.getElementById('<%=txtCtn.ClientID%>').value;
            //if (str == null || str.length == 0) {
            if ((str == null || str.length == 0) && (str2 == null || str2.length == 0 || str2 == "0")) {

                alert('Must Enter Qty');

                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }

            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Invoice/DC No');
                document.getElementById('<%=txtDocumentNo.ClientID%>').focus();
                return false;
            }

            str = document.getElementById('<%=txtBuiltyNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Builty No');
                document.getElementById('<%=txtBuiltyNo.ClientID%>').focus();
                return false;
            }

            return true;
        }
        <%-- 
   function SearchList() {
            var l = document.getElementById('<%= lstCode.ClientID %>');
            var tb = document.getElementById('<%= txtskuCode.ClientID %>');

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
            var l = document.getElementById('<%= lstCode.ClientID %>');
            var str;
            for (var i = 0; i < l.options.length; i++) {
                if (l.options[i].selected) {
                    str = l.options[i].value;
                    ClearSelection(l);
                    break;
                }
                else {
                    str = "";
                }
            }
            var stroption = document.getElementById("<%= txtskuCode.ClientID %>").value;
            if (str.length > 0) {
                document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
                document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1);
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= lstCode.ClientID %>").focus();
            }

        }
        function SelectCode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var str = document.getElementById("<%= lstCode.ClientID %>").value;
                document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
                document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1);
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
                document.getElementById("<%= txtQuantity.ClientID %>").focus();
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 24px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblbox" Height="14px" Text="Transaction Type"
                                                        Width="102px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 24px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="2">Purchase</asp:ListItem>
                                                    <asp:ListItem Value="5">Transfer Out</asp:ListItem>
                                                    <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                                    <%--<asp:ListItem Value="4">Transfer In</asp:ListItem>--%>
                                                    <asp:ListItem Value="6">Damage(Expense)</asp:ListItem>
                                                    <asp:ListItem Value="10">Damage(Claimable)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Purchase For"
                                                        Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <asp:Panel ID="PanelPrincipal" runat="server">
                                                <td align="left" style="height: 25px">
                                                    <strong>
                                                        <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Principal" Width="94px"></asp:Label></strong>
                                                </td>
                                                <td style="height: 25px">
                                                    <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" CssClass="DropList"
                                                        OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelTransfer" runat="server">
                                                <td style="height: 25px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Transfer To" Visible="False"
                                                            Width="82px"></asp:Label></strong>
                                                </td>
                                                <td style="height: 25px">
                                                    <asp:DropDownList ID="DrpTransferFor" runat="server" CssClass="DropList" Visible="False"
                                                        Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </asp:Panel>


                                        </tr>
                                        <tr>

                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblAccount" runat="server" Text="Account head"></asp:Label></strong>
                                            </td>

                                            <td align="left">
                                                <asp:DropDownList ID="DrpAccountDetail" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Expense Head" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpExpenseAccountHead" runat="server" OnSelectedIndexChanged="DrpExpenseAccountHead_SelectedIndexChanged" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>

                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Text="Expense Amount"></asp:Label></strong>
                                            </td>

                                            <td align="left">
                                                <asp:TextBox ID="txtExpenseAmount" runat="server" ReadOnly="true" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="INV/DC  No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>


                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Builty No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Remarks" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 25px" valign="top"></td>
                                            <td style="height: 25px" valign="top" align="left">
                                                <asp:CheckBox ID="ChbBatchNo" runat="server" AutoPostBack="True" OnCheckedChanged="ChbBatchNo_CheckedChanged"
                                                    Text="Batch No" Width="77px" />
                                                <asp:CheckBox ID="ChbFreeSKU" runat="server" Style="margin-left: 15px;" Width="121px" Text="Apply Free SKU"
                                                    AutoPostBack="True" OnCheckedChanged="ChbFreeSKU_CheckedChanged"></asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 25px" valign="top"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    TargetControlID="txtExpenseAmount" ValidChars="0123456789" FilterType="Custom">
                                </cc1:FilteredTextBoxExtender>--%>
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
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
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
                                                    Free SKU
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    Batch No
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    Price
                                                </label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--    <asp:TextBox ID="txtskuCode" onkeyup="SearchList()" runat="server" Width="83px" CssClass="txtBox"></asp:TextBox>
                                            
                                                <asp:TextBox ID="txtskuName" runat="server" Width="200px" Font-Bold="True" CssClass="txtBox"
                                                    Enabled="False"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlSkus" runat="server" Width="300px" OnSelectedIndexChanged="ddlSkus_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCtn" runat="server" Width="70px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <%--    <asp:TextBox ID="txtQuantity" onfocus="SearchedCode()" runat="server" Width="70px"
                                                    CssClass="txtBox "></asp:TextBox>--%>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFreeSKU" runat="server" Width="70px" CssClass="txtBox" Enabled="False">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBatchNo" runat="server" Width="76px" CssClass="txtBox" Enabled="False">N/A</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtprice" runat="server" Width="76px" CssClass="txtBox">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="7">
                                                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="215px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="100%">

                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="SKU Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="215px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_CTN" ReadOnly="true" HeaderText="Ctn">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" ReadOnly="true" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FREE_SKU" ReadOnly="true" HeaderText="Free SKU">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" ReadOnly="true" HeaderText="BatchNo">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="75px" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Price" ReadOnly="true" HeaderText="Price" DataFormatString="{0:0.00}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="75px" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <%-- <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="35px" HorizontalAlign="Center" />
                                                            </asp:CommandField>--%>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="40px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                <strong>
                                    <asp:Label ID="Label7" runat="server" Width="100px" Height="16px" Text="Total Ctns/Units"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalCtn" runat="server" Width="60px" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtTotalUnit" runat="server" Width="60px" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                                <strong>
                                    <asp:Label ID="Label5" runat="server" Width="103px" Height="16px" Text="Total Amount"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalAmount" runat="server" Width="88px"
                                    CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="hfTotalWeight" runat="server"/>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    TargetControlID="txtprice" ValidChars=".0123456789" FilterType="Custom">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    TargetControlID="txtCtn" ValidChars="0123456789" FilterType="Custom">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    TargetControlID="txtQuantity" ValidChars="0123456789" FilterType="Custom">
                                </cc1:FilteredTextBoxExtender>
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveDocument" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

