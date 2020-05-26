<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Erline_eCommerce.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Products</title>    
    <link href="Styles/general.css" type="text/css" rel="stylesheet" />
    <link href="Styles/products.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
            <div id="form_holder">
                <table class="form_table">
                    <!-- product id -->
                    <tr>
                        <td><asp:Label ID="lblProductId" CssClass="form_label" runat="server" Text="Product #"></asp:Label></td>
                        <td><asp:TextBox ID="txtProductId" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                    </tr>
                    <!-- manufacturer code -->
                    <tr>
                        <td><asp:Label ID="lblManufacCode" class="form_label" runat="server" Text="Manufacturer Code"></asp:Label></td>
                        <td><asp:TextBox ID="txtManufacCode" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                    </tr>
                    <!-- product description -->
                    <tr>
                        <td><asp:Label ID="lblDescription" class="form_label" runat="server" Text="Description"></asp:Label></td>
                        <td><asp:TextBox ID="txtDescription" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                    </tr>
                     <!-- product picture -->
                    <tr>
                        <td><asp:Label ID="lblProductPic" class="form_label" runat="server" Text="Picture"></asp:Label></td>
                        <td><asp:TextBox ID="txtProductPic" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                    </tr>
                     <!-- product QOH -->
                    <tr>
                        <td><asp:Label ID="lblQOH" class="form_label" runat="server" Text="Quantity on Hand"></asp:Label></td>
                        <td><asp:TextBox ID="txtQOH" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                    </tr>
                    <!-- product price -->
                    <tr>
                        <td><asp:Label ID="lblPrice" class="form_label" runat="server" Text="Price"></asp:Label></td>
                        <td><asp:TextBox ID="txtPrice" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                    </tr>
                </table>

                <asp:Image ID="imgProductPic" runat="server" />

                <asp:ListBox ID="lstPictures" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstPictures_SelectedIndexChanged"></asp:ListBox>
            </div>
            <asp:Label ID="lblProductIdNotifications" runat="server" Text=""></asp:Label>
            <div id="buttonContainer">
                <asp:Button ID="btnNew" CssClass="generalButton" runat="server" Text="New" OnClick="btnNew_Click" />
                <asp:Button ID="btnAdd" CssClass="generalButton" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" CssClass="generalButton" runat="server" Text="Update" Enabled="false" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" CssClass="generalButton" runat="server" Text="Delete" Enabled="false" OnClick="btnDelete_Click" />
                <asp:Button ID="btnFind" CssClass="generalButton" runat="server" Text="Find" OnClick="btnFind_Click" />
            </div>
    </form>
</body>
</html>
