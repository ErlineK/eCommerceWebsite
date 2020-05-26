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
    public partial class Default : System.Web.UI.Page
    {
        public static string productsDataPath = HttpContext.Current.Server.MapPath(".") + @"\Data\Products\";
        public static string dbConnect = @"integrated security = True; data source=(localdb)\ProjectsV13;Initial Catalog=eCommerce;persist security info=False;";

        // cart params
        public static CartItem[] cartItems;
        public static int numItems = 0;

        // existing order customer
        public static string customerNum = "-1";
        public static Boolean checkoutActive = false;

        // db connection params
        private SqlConnection connectCmd = null;
        private SqlCommand cmd = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                resetArrays();
            }
        }

        protected void btnPromo_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "PromoPage.aspx");
        }

        protected void btnCatalog_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Catalog.aspx");
        }

        public void btnCart_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Cart.aspx");
        }

        protected void btnWeather_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "https://www.theweathernetwork.com/ca/weather/ontario/london");
        }

        protected void btnCustomers_Click(object sender, EventArgs e)
        {
            Default.checkoutActive = false;
            MyFrame.Attributes.Add("src", "Customers.aspx");
        }

        protected void btnProducts_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Products.aspx");
        }

        protected void btnSales_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "SalesReport.aspx");
        }

        public static void resetArrays()
        {
            SqlConnection connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            // get amount of products
            int numOfProducts = 0;
            string countProducts = "SELECT COUNT(*) FROM Products";
            SqlCommand cmd = new SqlCommand(countProducts, connectCmd);
            numOfProducts = Convert.ToInt32(cmd.ExecuteScalar());

            cartItems = new CartItem[numOfProducts];

            disposeResources(ref connectCmd, ref cmd);
        }

        public static void disposeResources(ref SqlConnection iConnectCmd, ref SqlCommand iCmd, ref SqlDataAdapter dataAdapter, ref DataSet data)
        {
            if (dataAdapter != null)
            {
                dataAdapter.Dispose();
            }
            if (data != null)
            {
                data.Dispose();
            }
            disposeResources(ref iConnectCmd, ref iCmd);
        }

        public static void disposeResources(ref SqlConnection iConnectCmd, ref SqlCommand iCmd)
        {
            if (iConnectCmd != null)
            {
                iConnectCmd.Dispose();
            }
            if (iCmd != null)
            {
                iCmd.Dispose();
            }
        }

    }
}