<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmtesting.aspx.cs" Inherits="Forms_frmtesting" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="width:100%; margin-left: auto; margin-right: auto;">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" ToolPanelView="None" DisplayPage="true" EnableDrillDown ="true" EnableToolTips ="true" HasExportButton="True" HasGotoPageButton ="True" HasPrintButton ="True" />
</div>
    
    </form>
</body>
</html>




           
           
           
           