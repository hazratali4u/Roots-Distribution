<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOpeningStock.aspx.cs" Inherits="Forms_frmOpeningStock" Title="SAMS :: Stock Adjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddlSkus.ClientID %>").searchable();
        }

        function ConfirmDelete() {
            if (confirm("Do you want to Cancel this record?") == true)
                return true;

            else {
                return false;
            }
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
        <%--  function SearchList() {
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
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td align="left" colspan="1"></td>
                                            <td align="left">
                                                <%--   <div style="left: 15px; position: absolute; top: 325px;">
                                                    <asp:Panel ID="Panel1" runat="server" Width="280px" Height="190px" CssClass="HidePanel"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="Silver">
                                                        <strong>
                                                            <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                                                border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                                            &nbsp;<strong>Select SKU from List </strong>
                                                                        </td>
                                                                        <td style="border-bottom: black thin solid" valign="top" align="right">
                                                                            <asp:Button AccessKey="S" ID="Button4" runat="server" Width="25px" Height="23px"
                                                                                Font-Size="8pt" Text="x" BorderWidth="1px" BorderStyle="Groove" CssClass="Button" />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:ListBox ID="lstCode" runat="server" Height="154px" onkeydown="SelectCode(event)"
                                                                SelectionMode="Multiple" Width="275px"></asp:ListBox>
                                                    </asp:Panel>
                                                    </div>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="102px" Height="14px" Text="Transaction Type"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                                                    <asp:ListItem Value="7">Opening Stock</asp:ListItem>
                                                    <asp:ListItem Value="8">Short</asp:ListItem>
                                                    <asp:ListItem Value="9">Excess</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>Document No</strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <%--<tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    Principal</strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" TextMode="MultiLine" Height="25px" Width="435px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 25px" valign="top"></td>
                                            <td style="height: 25px" valign="top" align="left">
                                                <asp:CheckBox ID="ChbBatchNo" runat="server" Width="77px" Text="Batch No" AutoPostBack="True"></asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 25px; display: none;" valign="top">
                                                <asp:CheckBox ID="ChbFreeSKU" runat="server" Width="146px" Text="Apply Free SKU"
                                                    AutoPostBack="True"></asp:CheckBox>
                                            </td>

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
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                            <%-- <td >
                                                <label >
                                                   Free SKU
                                                </label>
                                            </td>--%>
                                            <td>
                                                <label>
                                                    Batch No
                                                </label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSkus" runat="server" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCtn" runat="server" Width="70px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px"
                                                    CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <%--<td>
                                                <asp:TextBox ID="txtFreeSKU" runat="server" Width="70px" CssClass="txtBox" Enabled="False">0</asp:TextBox>
                                            </td>--%>
                                            <td>
                                                <asp:TextBox ID="txtBatchNo" runat="server" Width="76px" CssClass="txtBox" Enabled="False">N/A</asp:TextBox>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="645px" Height="170px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="100%" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="White" BackColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" HorizontalAlign="Center" AutoGenerateColumns="False">

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
                                                                    Width="205px" />
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
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" ReadOnly="true" HeaderText="BatchNo">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="80px" HorizontalAlign="Center" />
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
                                <asp:Button AccessKey="S" ID="btnSaveDocument" OnClick="btnSaveDocument_Click" runat="server"
                                    Width="119px" Font-Size="8pt" Text="Save Document" UseSubmitBehavior="False"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" OnClick="btnCancel_Click" runat="server"
                                    Width="120px" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" CssClass="Button" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
