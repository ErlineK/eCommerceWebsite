using Erline_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Erline_eCommerce
{
    public partial class Cart : System.Web.UI.Page
    {
        const int TABLE_COLUMNS = 5;
        const int PIC_COLUMN_INDEX = 0;
        const int DESCRIP_COLUMN_INDEX = 1;
        const int PRICE_COLUMN_INDEX = 2;
        const int BTN_REMOVE_COLUMN_INDEX = 3;
        const int QTY_COLUMN_INDEX = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnRecalculate.Click += new EventHandler(btnRecalculate_Click);
            btnCheckout.Click += new EventHandler(btnCheckout_Click);
            createCartGrid();
            recalculateTotal();
        }


        protected void btnTemplateRemove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton removeBtn = (ImageButton)sender;
            string id = removeBtn.ID;

            string[] idParts = id.Split('_');
            int productIndex = int.Parse(idParts[1]);

            // shift down other items on the list
            for (int i = productIndex; i < Default.numItems; i++)
            {
                Default.cartItems[i] = Default.cartItems[i + 1];
            }

            // update number of items in cart
            Default.numItems--;

            createCartGrid();
            recalculateTotal();
        }

        protected void tbQty_TextChanged(object sender, EventArgs e)
        {
            TextBox tbQty = (TextBox)sender;
            string id = tbQty.ID;

            string[] idParts = id.Split('_');
            int productndex = int.Parse(idParts[1]);

            // update in cart info
            Default.cartItems[productndex].Qty = int.Parse(tbQty.Text);
        }

        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            recalculateTotal();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            //replace current page with checkout page
            Server.Transfer("Checkout.aspx");
        }

        private void createCartGrid()
        {
            // empty table
            tblCart.Rows.Clear();

            for (int row = 0; row < Default.numItems; row++)
            {
                TableRow tblRow = new TableRow();
                for (int col = 0; col < TABLE_COLUMNS; col++)
                {
                    TableCell tblCell = new TableCell();
                    tblCell.CssClass = "tableCell";
                    CartItem item = Default.cartItems[row];
                    switch (col)
                    {
                        case PIC_COLUMN_INDEX:
                            Image prodImg = new Image();
                            prodImg.ImageUrl = "Pictures/" + item.Pic;
                            prodImg.Height = 100;

                            tblCell.Controls.Add(prodImg);
                            tblCell.CssClass = "imgCell";
                            break;

                        case DESCRIP_COLUMN_INDEX:
                            tblCell.Text = item.Description;
                            break;

                        case PRICE_COLUMN_INDEX:
                            tblCell.Text = "$" + item.Price;
                            break;

                        case BTN_REMOVE_COLUMN_INDEX:
                            ImageButton btnRemove = new ImageButton();
                            btnRemove.ID = "btnRemove_" + row;
                            btnRemove.ImageUrl = "../images/cart/remove_shopping_cart.svg";

                            btnRemove.Click += new ImageClickEventHandler(btnTemplateRemove_Click);
                            btnRemove.CssClass = "addCartButton";

                            tblCell.Controls.Add(btnRemove);
                            tblCell.CssClass = "buttonCell";
                            break;

                        case QTY_COLUMN_INDEX:
                            TextBox tbQty = new TextBox();
                            tbQty.ID = "itemQty_" + row;
                            tbQty.Text = item.Qty.ToString();
                            tbQty.AutoPostBack = true;
                            tbQty.TextChanged += new EventHandler(tbQty_TextChanged);
                            tbQty.CssClass = "textBoxInsert";

                            tblCell.CssClass = "buttonCell";
                            tblCell.Controls.Add(tbQty);
                            break;
                    }

                    tblRow.Cells.Add(tblCell);
                }

                tblCart.Rows.Add(tblRow);
            }
        }

        private void recalculateTotal()
        {
            decimal cartTotal = 0;

            for (int i = 0; i < Default.numItems; i++)
            {
                CartItem item = Default.cartItems[i];
                decimal itemTotal = item.Price * item.Qty;

                cartTotal += itemTotal;
            }

            lblTotal.Text = cartTotal.ToString("$##,##0.##");
        }

    }
}