<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Erline_eCommerce.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Details</title>
    <link href="Styles/general.css" rel="stylesheet" />
    <link href="Styles/checkout.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table ID="tblCheckoutDetails" CssClass="cartTable" runat="server"></asp:Table>
        <br/>
        <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
        <p>
            <asp:CheckBox ID="cbMailingList" runat="server" text="Please add me to mailing list"/>
        </p>
        <asp:Button ID="btnPay" CssClass="generalButton" runat="server" Text="Pay Here" />
        <p>
            <asp:Label ID="lblSalesNotifications" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
