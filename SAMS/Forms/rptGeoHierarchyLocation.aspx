<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptGeoHierarchyLocation.aspx.cs" Inherits="Forms_rptGeoHierarchyLocation" Title="SAMS :: Geo Hierarchy Report" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    
   <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                      <tr >
                                        <td style="height: 25px" align="left"></td>
                                        <td style="width: 90px; height: 25px" align="left"><strong>
                                                <asp:Label ID="Label1" runat="server" Width="48px" Text="Region" CssClass="lblbox"></asp:Label></strong></td>
                                            <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left"><asp:DropDownList ID="DrpTownRegion" runat="server" AutoPostBack="True" CssClass="DropList"
                                                                                    OnSelectedIndexChanged="DrpTownRegion_SelectedIndexChanged" Width="200px">
                                                                                </asp:DropDownList></td>
                                             <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                      <tr >
                                        <td style="height: 25px" align="left"></td>
                                        <td style="width: 90px; height: 25px" align="left"><strong>
                                                <asp:Label ID="Label3" runat="server" Width="48px" Text="Zone" CssClass="lblbox"></asp:Label></strong></td>
                                            <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left"><asp:DropDownList ID="DrpTownZone" runat="server" AutoPostBack="True" CssClass="DropList"
                                                                                    OnSelectedIndexChanged="DrpTownZone_SelectedIndexChanged" Width="200px">
                                                                                </asp:DropDownList></td>
                                             <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                      <tr >
                                        <td style="height: 25px" align="left"></td>
                                        <td style="width: 90px; height: 25px" align="left"><strong>
                                                <asp:Label ID="Label4" runat="server" Width="48px" Text="Territory" CssClass="lblbox"></asp:Label></strong></td>
                                            <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left"><asp:DropDownList ID="DrpTerritory" runat="server" Width="200px" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="DrpTerritory_SelectedIndexChanged" CssClass="DropList">
                                                                                </asp:DropDownList></td>
                                             <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                      <tr >
                                        <td style="height: 25px" align="left"></td>
                                        <td style="width: 90px; height: 25px" align="left"><strong>
                                                <asp:Label ID="Label5" runat="server" Width="48px" Text="Town" CssClass="lblbox"></asp:Label></strong></td>
                                            <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left"><asp:DropDownList ID="drpCIty" runat="server" Width="200px" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="drpCIty_SelectedIndexChanged" CssClass="DropList">
                                                                                </asp:DropDownList></td>
                                             <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged"></asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    
                                  
                                    
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Route" Width="52px"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpRoute" runat="server" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="true" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Market" Width="52px"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpMarket" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                               
                                </tbody>
                            </table>
                          
                          
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button"
                        Text="View PDF" Width="90" Style="margin-left:106px;" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />

              
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
