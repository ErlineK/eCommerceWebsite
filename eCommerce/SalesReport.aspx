<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesReport.aspx.cs" Inherits="Erline_eCommerce.SalesReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Report</title>
    <link href="Styles/catalog.css" rel="stylesheet" />
    <link href="Styles/general.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="gridSales" HeaderStyle-BackColor="#E9E6E0" CssClass="salesGrid" runat="server"></asp:GridView>
         <asp:GridView ID="gridSalesTotals" CssClass="totalSalesGrid" 
            HeaderStyle-BackColor="#9ACFDA" runat="server"></asp:GridView>
    </form>
</body>
</html>
