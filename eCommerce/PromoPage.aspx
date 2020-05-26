<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromoPage.aspx.cs" Inherits="Erline_eCommerce.PromoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/general.css" rel="stylesheet" />
    <link href="Styles/promo.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Label ID="lblWelcome" CssClass="welcome" runat="server" Text="Welcome to our new store!"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblMSG" CssClass="message" runat="server" Text="We invite you to deram with us and make a wish for your next wish"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblGreating" CssClass="greating" runat="server" Text="Have a MAGICAL day :)"></asp:Label>
        </p>
    </form>
</body>
</html>
