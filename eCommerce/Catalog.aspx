<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="Erline_eCommerce.Catalog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catalog</title>
    <link href="Styles/catalog.css" rel="stylesheet" />
    <link href="Styles/general.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" class="noScrollForm" runat="server">
        <asp:Table ID="tblCatalog" CssClass="cartTable" runat="server"></asp:Table>
        <asp:ImageButton ID="imgBtnTemplate" runat="server" Visible="false" OnClick="btnTemplate_Click"/>
        <br />
    </form>
</body>
</html>