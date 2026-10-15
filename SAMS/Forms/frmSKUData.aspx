<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" Title="SAMS :: SKU Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddskuPrincipal.ClientID %>").searchable();
            $("#<%=ddskudivision.ClientID %>").searchable();
            $("#<%=ddskucategory.ClientID %>").searchable();
            $("#<%=ddskuBrand.ClientID %>").searchable();
            $("#<%=DrpSKUTaxType.ClientID %>").searchable();
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
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>SKU Type</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="1" Text="Gold" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Bronze"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Silver"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td align="left" rowspan="10"></td>
                                            <td style="width: 100px" align="left">
                                                <strong>SKU Principal</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskuPrincipal" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskuPrincipal_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td align="left" rowspan="10"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>SKU Division</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskudivision" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskudivision_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 100px" align="left">
                                                <strong>SKU Category</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskucategory" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>SKU Brand</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskuBrand" runat="server" Width="200px" AutoPostBack="True"
                                                    CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td align="left" style="width: 100px">
                                                <strong>GST On</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpSKUTaxType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="T">Trade Price</asp:ListItem>
                                                    <asp:ListItem Value="R">Retail Price</asp:ListItem>
                                                    <asp:ListItem Value="E">Exempted</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>UOM</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpUOM" runat="server" Width="198px" AutoPostBack="True" CssClass="DropList">
                                                    <asp:ListItem Value="1">Grams</asp:ListItem>
                                                    <asp:ListItem Value="2">Kilograms</asp:ListItem>
                                                    <asp:ListItem Value="3">Milliliters</asp:ListItem>
                                                    <asp:ListItem Value="4">Liters</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td style="width: 100px" align="left">
                                                <strong>SKU Code</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtskucode" runat="server" Width="196px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 20px" align="left">
                                                <strong>SKU Name</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtskuname" runat="server" Width="196px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 100px" align="left">
                                                <strong>Pack Size</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtpacksize" runat="server" Width="196px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>Weight</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtWeight" runat="server" Width="196px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 100px" align="left">
                                                <strong>Unit In Case</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtunitincase" runat="server" Width="196px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>Sort Order</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtSortOrder" runat="server" Width="196px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 100px" align="left"></td>
                                            <td style="width: 37px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 12px"></td>
                                            <td style="width: 37px; height: 12px">
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtunitincase"
                                                    FilterType="Numbers">
                                                </cc1:FilteredTextBoxExtender>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtSortOrder"
                                                    FilterType="Numbers">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td></td>
                                            <td style="width: 37px; height: 12px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 12px"></td>
                                            <td style="height: 12px" align="left" colspan="2">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="86px" Font-Size="8pt"
                                                    Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 37px; height: 12px" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>&nbsp; Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table class="tblhead" width="980px">
                        <tbody>
                            <tr>
                                <td style="color: White; font-weight: bold;">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 22px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                        <asp:ListItem Value="Principal">Principal</asp:ListItem>
                                        <asp:ListItem Value="SKU_TYPE_NAME">SKU Type</asp:ListItem>
                                        <asp:ListItem Value="Category">Category</asp:ListItem>
                                        <asp:ListItem>Brand</asp:ListItem>
                                        <asp:ListItem Value="SKU_CODE">Code</asp:ListItem>
                                        <asp:ListItem Value="SKU_NAME">Name</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 22px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="width: 350px; height: 22px" align="left">
                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnFilter_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="230px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Style="margin-top:5px;" ScrollBars="Vertical">
                        <asp:GridView ID="grdSKUData" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                            HorizontalAlign="Center" BorderColor="SteelBlue"
                            BackColor="White" AutoGenerateColumns="False" OnRowEditing="grdSKUData_RowEditing"
                            OnRowDeleting="grdSKUData_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="Principal_Id" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Division_Id" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Category_Id" ReadOnly="true" HeaderText="Category_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand_Id" ReadOnly="true" HeaderText="Brand_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Principal" ReadOnly="true" HeaderText="Principal">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_TYPE_NAME" ReadOnly="true" HeaderText="SKU Type">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" ReadOnly="true" HeaderText="Category">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand" ReadOnly="true" HeaderText="Brand">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PACKSIZE" ReadOnly="true" HeaderText="Pack Size">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UNITS_IN_CASE" ReadOnly="true" HeaderText="UIC">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SORTORDER" ReadOnly="true" HeaderText="SO">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" ReadOnly="true" HeaderText="GST">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_TYPE" ReadOnly="true" HeaderText="SKU_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM" ReadOnly="true" HeaderText="UOM">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_WEIGHT" ReadOnly="true" HeaderText="SKU_WEIGHT">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--<asp:CommandField ShowEditButton="True" >
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"></ItemStyle>
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
