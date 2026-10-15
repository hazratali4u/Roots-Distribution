<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKU_Price.aspx.cs" Inherits="SKU_Price" Title="SAMS :: SKU Price" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
   <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddskuPrincipal.ClientID %>").searchable();
            $("#<%=ddskuDivision.ClientID %>").searchable();
            $("#<%=ddskuCategory.ClientID %>").searchable();
            $("#<%=ddskuBrand.ClientID %>").searchable();
            $("#<%=ddskuName.ClientID %>").searchable();
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=ddskuName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select SKU Name');
                return false;
            }
            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Price Efftive Date');
                return false;
            }
            str = document.getElementById('<%=txtTaxPrices.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Tax Price');
                return false;
            }
            str = document.getElementById('<%=txtTradePrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Trade Price');
                return false;
            }
            str = document.getElementById('<%=txtRetailPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Retail Price');
                return false;
            }
            str = document.getElementById('<%=txtDistributorPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Factory Pricet');
                return false;
            }
            return true;
        }
        function CheckBoxListSelect() {
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
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
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True" ></asp:Label>
                                </td>
                                <td colspan="1">
                                </td>
                                <td colspan="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px;"  align="left">
                                    <strong>
                                        Principal Name</strong>
                                </td>
                                <td style="width: 298px;"  align="left">
                                    <asp:DropDownList ID="ddskuPrincipal" runat="server" Width="200px" 
                                        CssClass="DropList" OnSelectedIndexChanged="ddskuPrincipal_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="width:200px; border-right: darkgray 1px ridge; border-top: darkgray 1px ridge;
                                    border-left: darkgray 1px ridge; border-bottom: darkgray 1px ridge" align="left"
                                    rowspan="11">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="tblhead" width="200">
                                                    <strong>
                                                        <asp:Label ID="Label11" runat="server" Width="140px" ForeColor="White" Font-Bold="True"
                                                            Text="Location Name"></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="280">
                                                    <asp:Panel ID="Panel1" runat="server" Width="250px" Height="220px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Solid" BorderColor="Silver" BackColor="White">
                                                        <asp:CheckBoxList ID="ChbDistributorList" runat="server" Width="230px" Font-Size="8pt">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="height: 18px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px" align="left">
                                    <strong>
                                        Division Name</strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:DropDownList ID="ddskuDivision" runat="server" Width="200px" 
                                        CssClass="DropList" OnSelectedIndexChanged="ddskuDivision_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px" align="left">
                                    <strong>
                                       SKU Category</strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddskuCategory" runat="server" Width="200px" 
                                        CssClass="DropList" OnSelectedIndexChanged="ddskuCategory_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px" align="left">
                                    <strong>
                                        SKU Brand</strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddskuBrand" runat="server" Width="200px" 
                                        CssClass="DropList" OnSelectedIndexChanged="ddskuBrand_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 20px" align="left">
                                    <strong>
                                        SKU Name</strong>
                                </td>
                                <td style="height: 20px" align="left">
                                    <asp:DropDownList ID="ddskuName" runat="server" Width="200px" 
                                        CssClass="DropList" OnSelectedIndexChanged="ddskuName_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 201px; height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px" align="left">
                                    <strong>
                                        Date Effected</strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox Style="text-align: justify" ID="txtFromdate" TabIndex="2" runat="server"
                                        Width="148px"  CssClass="txtBox" ValidationGroup="MKE"
                                        MaxLength="1"></asp:TextBox>
                                    <asp:ImageButton ID="ImgBntFromCalc" runat="server"  ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        CausesValidation="False"></asp:ImageButton><cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server"  Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc"
                                            TargetControlID="txtFromdate">
                                        </cc1:CalendarExtender>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        Factory Price</strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox ID="txtDistributorPrice" runat="server" Width="193px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        Trade Price</strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox ID="txtTradePrice" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px" align="left">
                                    <strong>
                                        Retail Price</strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRetailPrice" runat="server" Width="192px"
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 12px" align="left">
                                    <strong>
                                        G.S.T (%)</strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTaxPrices" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px; height: 12px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 12px" align="left">
                                    <strong>
                                        S.E.D (%)</strong>
                                </td>
                                <td style="width: 298px; height: 12px" align="left">
                                    <asp:TextBox ID="txtSADTax" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px; height: 12px" align="left">
                                </td>
                            </tr>
                             </tbody>
                    </table>
                    <table>
                            <tr>
                                <td style="width: 110px">
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" Font-Size="8pt"
                                        Text="Save" ValidationGroup="vg" CssClass="Button" />
                                    &nbsp;
                                    
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                        TargetControlID="txtTaxPrices" ValidChars="0123456789." FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                        TargetControlID="txtTradePrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                                        TargetControlID="txtRetailPrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                                        TargetControlID="txtDistributorPrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                       </table>
                    <div style="z-index: 101; left: 530px; width: 100px; position: absolute; top: 440px;
                        height: 100px">
                        &nbsp;
                        <asp:Panel ID="Panel21" runat="server" >
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"  AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="23px" Height="26px" 
                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_pricedetails" runat="server" Width="100%" ForeColor="SteelBlue"
                            CssClass="gridRow2" BorderColor="SteelBlue" BackColor="White"
                            AutoGenerateColumns="False" >
                            
                            <Columns>
                                <asp:BoundField DataField="distributor_name" ReadOnly="true" HeaderText="Location">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="SKU Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="SKU Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" ReadOnly="true" HeaderText="GST">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_PRICE" ReadOnly="true" HeaderText="Factory Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TRADE_PRICE" ReadOnly="true" HeaderText="Trade Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RETAIL_PRICE" ReadOnly="true" HeaderText="Retail Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TAX_PRICE" ReadOnly="true" HeaderText="GST (%)" DataFormatString="{0:F2}">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle BorderColor="DarkGray" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SED_TAX" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="SED (%)">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE_EFFECTED" ReadOnly="true" HeaderText="Date Effected">
                                    <ItemStyle BackColor="Transparent" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
