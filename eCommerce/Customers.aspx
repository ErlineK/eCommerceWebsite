<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="Erline_eCommerce.Customers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Maintenance</title>
    <link href="Styles/general.css" type="text/css" rel="stylesheet" />
    <link href="Styles/products.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
            <table class="form_table">
                <!-- customer id -->
                <tr>
                    <td><asp:Label ID="lblCustomerId" class="form_label" runat="server" Text="Customer ID"></asp:Label></td>
                    <td><asp:TextBox ID="txtCustomerId" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
                <!-- customer first name -->
                <tr>
                    <td><asp:Label ID="lblFirstName" class="form_label" runat="server" Text="First Name"></asp:Label></td>
                    <td><asp:TextBox ID="txtFirstName" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
                <!-- customer last name -->
                <tr>
                    <td><asp:Label ID="lblLastName" class="form_label" runat="server" Text="Last Name"></asp:Label></td>
                    <td><asp:TextBox ID="txtLastName" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
                 <!-- customer address -->
                <tr>
                    <td><asp:Label ID="lblAddress" class="form_label" runat="server" Text="Address"></asp:Label></td>
                    <td><asp:TextBox ID="txtAddress" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
                 <!-- customer city -->
                <tr>
                    <td><asp:Label ID="lblCity" class="form_label" runat="server" Text="City"></asp:Label></td>
                    <td><asp:TextBox ID="txtCity" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
                <!-- customer province -->
                <tr>
                    <td><asp:Label ID="lblProvince" class="form_label" runat="server" Text="Province"></asp:Label></td>
                    <td><asp:TextBox ID="txtProvince" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
                <!-- customer postal code -->
                <tr>
                    <td><asp:Label ID="lblPostalCode" class="form_label" runat="server" Text="Postal Code"></asp:Label></td>
                    <td><asp:TextBox ID="txtPostalCode" CssClass="form_textbox" runat="server"></asp:TextBox></td>
                </tr>
            </table>

            <asp:Label ID="lblCustomerIdNotifications" runat="server" Text=""></asp:Label>

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
