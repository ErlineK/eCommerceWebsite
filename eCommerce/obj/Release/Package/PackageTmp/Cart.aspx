<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Erline_eCommerce.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cart</title>
    <link href="Styles/general.css" rel="stylesheet" />
    <link href="Styles/cart.css" rel="stylesheet" />
    <link href="Styles/catalog.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table ID="tblCart" CssClass="cartTable" runat="server"></asp:Table>
        <asp:ImageButton ID="btnTemplateRemove" runat="server" AlternateText="Remove" Visible="false" />
        <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
        <asp:Button ID="btnRecalculate" CssClass="generalButton" runat="server" Text="Recalculate" />
        <asp:Button ID="btnCheckout" CssClass="generalButton" runat="server" Text="Checkout" />

    </form>
</body>
</html>
