using Erline_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Erline_eCommerce
{
    public partial class Details : System.Web.UI.Page
    {
        const int TABLE_COLUMNS = 3;
        const int DESCRIP_COLUMN_INDEX = 0;
        const int PRICE_COLUMN_INDEX = 1;
        const int QTY_COLUMN_INDEX = 2;

        private SqlConnection connectCmd = null;
        private SqlCommand cmd = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnPay.Click += new EventHandler(payForOrder_click);

            cretaeDetailsTable();
            calculateTotal();
        }

        private void cretaeDetailsTable()
        {
            tblCheckoutDetails.Rows.Clear();

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
                        case DESCRIP_COLUMN_INDEX:
                            tblCell.Text = item.Description;
                            break;

                        case PRICE_COLUMN_INDEX:
                            tblCell.Text = "$" + item.Price;
                            break;

                        case QTY_COLUMN_INDEX:
                            tblCell.Text = item.Qty.ToString();
                            break;
                    }

                    tblRow.Cells.Add(tblCell);
                }

                tblCheckoutDetails.Rows.Add(tblRow);
            }
        }

        private void calculateTotal()
        {
            decimal orderTotal = 0;

            for (int row = 0; row < Default.numItems; row++)
            {
                TableRow itemRow = tblCheckoutDetails.Rows[row];
                decimal itemPrice = 0;

                for (int cell = 0; cell < TABLE_COLUMNS; cell++)
                {
                    TableCell itemCell = itemRow.Cells[cell];
                    if (cell == PRICE_COLUMN_INDEX)
                    {
                        // get row price
                        string price = itemCell.Text;
                        price = price.Replace("$", string.Empty);
                        itemPrice = decimal.Parse(price);
                    }
                    else if (cell == QTY_COLUMN_INDEX)
                    {
                        // calc row total
                        decimal rowTotal = itemPrice * int.Parse(itemCell.Text);
                        orderTotal += rowTotal;
                    }
                }
            }

            lblTotal.Text = "Total: " + orderTotal.ToString("$##,##0.##");
            lblTotal.ForeColor = System.Drawing.Color.Green;
        }

        protected void payForOrder_click(object sender, EventArgs e)
        {
            // open db connection
            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            string addSaleQuery = "INSERT INTO Sales(ProductID, CustId, QtySold, SellingPrice, OrderDate)"
                                     + " VALUES(@ProductID, @CustId, @QtySold, @sellingPrice, @OrderDate)";

            // get all cart items and add them to sales table
            for (int i = Default.numItems; i >= 0; i--) {
                CartItem item = Default.cartItems[i];
                if (item != null)
                {
                    try
                    {
                        cmd = new SqlCommand(addSaleQuery, connectCmd);
                        cmd.Parameters.AddWithValue("@ProductID", item.ProdId);
                        cmd.Parameters.AddWithValue("@CustId", Default.customerNum);
                        cmd.Parameters.AddWithValue("@QtySold", item.Qty);
                        cmd.Parameters.AddWithValue("@sellingPrice", item.Price);
                        cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        lblSalesNotifications.Text = ex.Message;
                        lblSalesNotifications.ForeColor = System.Drawing.Color.Red;

                        Default.disposeResources(ref connectCmd, ref cmd);
                        return;
                    }

                    lblSalesNotifications.Text = "Sale completed successfully!";
                    lblSalesNotifications.ForeColor = System.Drawing.Color.Green;

                    //remove item from cart
                    // shift down other items on the list
                    for (int j = i; j < Default.numItems; j++)
                    {
                        Default.cartItems[j] = Default.cartItems[j + 1];
                    }

                    // update number of items in cart
                    Default.numItems--;

                    // if cart empty - release customer. 
                    if (Default.numItems <= 0)
                    {
                        Default.customerNum = "-1";

                        // disable payment button
                        btnPay.Enabled = false;
                    }
                }
               
            }
        }
    }
}