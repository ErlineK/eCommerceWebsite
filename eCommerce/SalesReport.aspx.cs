using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Erline_eCommerce
{
    public partial class SalesReport : System.Web.UI.Page
    {
        private SqlConnection connectCmd = null;
        private SqlCommand cmd = null;

        private DataSet sales;
        private DataTable totals;

        protected void Page_Load(object sender, EventArgs e)
        {
            sales = getSalesData();
            totals = getTotals();
            populateSalesGrid();
            populateTotalsGrid();
        }

        private void populateTotalsGrid()
        {
            // get DataTable and populate grid
            gridSalesTotals.DataSource = totals;
            gridSalesTotals.DataBind();
        }

        private DataTable getTotals()
        {
            SqlDataAdapter dataAdapter = null;
            DataSet data = new DataSet();

            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            string getTotalsQuery = "SELECT SUM(QtySold) AS 'Total Sales Quantity', SUM(QtySold*sellingPrice) AS 'Sum of Quantity Sold' FROM Sales";

            try
            {
                cmd = new SqlCommand(getTotalsQuery, connectCmd);

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(data, "Totals");
            }
            catch (Exception ex)
            {
                Default.disposeResources(ref connectCmd, ref cmd);
            }

            return data.Tables["Totals"];
        }

        private void populateSalesGrid()
        {
            // get DataTable and populate grid
            gridSales.DataSource = sales.Tables["Sales"];
            gridSales.DataBind();
        }

        private DataSet getSalesData()
        {
            SqlDataAdapter dataAdapter = null;
            DataSet data = new DataSet();

            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            string getSalesQuery = "SELECT s.Id AS 'Sales ID', c.FirstName + ' ' + c.LastName AS 'Customer Name', p.manufacCode AS 'Product Manufacturing Code', p.description AS 'Product Description', s.QtySold AS 'Quantity Sold', s.sellingPrice AS 'Selling Price', FORMAT (s.OrderDate , 'yyyy-MM-dd') AS 'Order Date' " +
                "FROM Sales s INNER JOIN Products p ON s.ProductID = p.ProductId INNER JOIN Customers c ON s.CustId = c.CustomerId";

            try
            {
                cmd = new SqlCommand(getSalesQuery, connectCmd);

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(data, "Sales");
            }
            catch (Exception ex)
            {
                Default.disposeResources(ref connectCmd, ref cmd);
            }

            return data;
        }
    }
}