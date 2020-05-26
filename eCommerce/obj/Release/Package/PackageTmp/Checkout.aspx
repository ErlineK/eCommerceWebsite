<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Erline_eCommerce.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checkout</title>
    <link href="Styles/general.css" rel="stylesheet" />
    <link href="Styles/checkout.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="frameContainer">
            <iframe id="customerFrame" src="Customers.aspx" runat="server"></iframe>
        </div>
        <div class="frameContainer">
            <iframe id="detailsFrame" src="Details.aspx" runat="server"></iframe>
        </div>
    </form>
</body>
</html>
