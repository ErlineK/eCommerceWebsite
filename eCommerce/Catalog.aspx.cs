using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Erline_eCommerce.Models;

namespace Erline_eCommerce
{
    public partial class Catalog : System.Web.UI.Page
    {
        const int TABLE_COLUMNS = 4;
        const int PIC_COLUMN_INDEX = 0;
        const int DESCRIP_COLUMN_INDEX = 1;
        const int PRICE_COLUMN_INDEX = 2;
        const int BTN_COLUMN_INDEX = 3;

        private SqlConnection connectCmd = null;
        private SqlCommand cmd = null;

        DataSet prods;
        private CartItem[] tempProducts; // holds catalog items before adding to cart


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Default.resetArrays();
            //}

            prods = getProductsData();
            populateProductsTable();
        }

        private void populateProductsTable()
        {
            // reset tempProducts array
            int numOfItems = prods.Tables["Products"].Rows.Count;
            tempProducts = new CartItem[numOfItems];

            for (int i = 0; i < numOfItems; i++)
            //foreach (DataRow product in prods.Tables["Products"].Rows)
            {
                DataRow product = prods.Tables["Products"].Rows[i];

                TableRow tblRow = new TableRow();
                CartItem item = new CartItem();

                int productId = int.Parse(product["ProductId"].ToString());
                item.ProdId = productId;

                for (int tblCol = 0; tblCol < TABLE_COLUMNS; tblCol++)
                {
                    TableCell tblCell = new TableCell();
                    tblCell.CssClass = "tableCell";
                    switch (tblCol)
                    {
                        case PIC_COLUMN_INDEX:
                            Image prodImg = new Image();
                            prodImg.ImageUrl = "Pictures/" + product["pic"].ToString();
                            prodImg.Height = 100;

                            tblCell.Controls.Add(prodImg);
                            tblCell.CssClass = "imgCell";

                            item.Pic = product["pic"].ToString();
                            break;

                        case DESCRIP_COLUMN_INDEX:
                            tblCell.Text = product["description"].ToString();
                            item.Description = product["description"].ToString();
                            break;

                        case PRICE_COLUMN_INDEX:
                            tblCell.Text = "$" + product["price"].ToString();
                            item.Price = decimal.Parse(product["price"].ToString());
                            break;

                        case BTN_COLUMN_INDEX:
                            ImageButton btnAddToCart = new ImageButton();
                            btnAddToCart.ID = "btnSelect_" + product["ProductId"].ToString();
                            btnAddToCart.AlternateText = "Add to Cart";
                            btnAddToCart.ImageUrl = "../images/cart/add_shopping_cart.svg";
                            btnAddToCart.CssClass = "addCartButton";

                            btnAddToCart.Click += new ImageClickEventHandler(btnTemplate_Click);

                            tblCell.Controls.Add(btnAddToCart);
                            tblCell.CssClass = "buttonCell";
                            break;
                    }

                    tblRow.Cells.Add(tblCell);
                }
                tempProducts[i] = item;
                tblCatalog.Rows.Add(tblRow);
            }
        }

        protected void btnTemplate_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton addBtn = (ImageButton)sender;
            addBtn.Enabled = false;
            string id = addBtn.ID;

            string[] idParts = id.Split('_');

            int ProductId = int.Parse(idParts[1]);

            // add item to cart
            if (Default.numItems > 0)
            {
                bool itemInCart = false;
                for (int i = 0; i < Default.numItems; i++)
                {
                    if (ProductId == Default.cartItems[i].ProdId)
                    {
                        itemInCart = true; //item exist in cart. do not add again
                    }
                }

                if (!itemInCart) // item does not exist in cart
                {
                    CartItem itemToCart = getItemById(ProductId);
                    Default.cartItems[Default.numItems] = itemToCart;
                    Default.numItems++;
                }
            }
            else // add first item to cart
            {
                CartItem itemToCart = getItemById(ProductId);
                Default.cartItems[Default.numItems] = itemToCart;
                Default.numItems++;
            }
        }

        private CartItem getItemById(int productId)
        {
            foreach (CartItem product in tempProducts)
            {
                if (product.ProdId == productId)
                {
                    return product;
                }
            }
            return null;
        }

        protected DataSet getProductsData()
        {
            SqlDataAdapter dataAdapter = null;
            DataSet data = new DataSet();

            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            string findProductQuery = "SELECT * FROM Products";

            try
            {
                cmd = new SqlCommand(findProductQuery, connectCmd);

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(data, "Products");
            }
            catch (Exception ex)
            {
                Default.disposeResources(ref connectCmd, ref cmd);
            }

            return data;
        }

    }

}