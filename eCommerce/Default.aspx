<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Erline_eCommerce.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Erline eCommerce</title>
    <link href="Styles/general.css" type="text/css" rel="stylesheet" />
    <link href="Styles/default.css" type="text/css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Indie+Flower&display=swap" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Erline Katz eCommerce</h1>
         <asp:Panel ID="panMain" runat="server" CssClass="MainPanel">
            <asp:Button ID="btnPromo" runat="server" Text="Promotions" CssClass="navButtons" OnClick="btnPromo_Click" />
            <asp:Button ID="btnCatalog" runat="server" Text="Catalog" CssClass="navButtons" OnClick="btnCatalog_Click" />
            <asp:Button ID="btnCart" runat="server" Text="Cart" CssClass="navButtons" OnClick="btnCart_Click" />
            <asp:Button ID="btnWeather" runat="server" Text="Weather Page" CssClass="navButtons" OnClick="btnWeather_Click" />
            <asp:Button ID="btnCustomers" runat="server" Text="Customers" CssClass="navButtons" OnClick="btnCustomers_Click" />
            <asp:Button ID="btnProducts" runat="server" Text="Products" CssClass="navButtons" OnClick="btnProducts_Click" />
            <asp:Button ID="btnSalesReport" runat="server" Text="Sales" CssClass="navButtons" OnClick="btnSales_Click" />
        </asp:Panel>
        <iframe id="MyFrame" src="" runat="server">
        </iframe>
    </form>
</body>
</html>
