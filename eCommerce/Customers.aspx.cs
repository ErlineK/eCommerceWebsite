using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Erline_eCommerce
{
    public partial class Customers : System.Web.UI.Page
    {
        private SqlConnection connectCmd = null;
        private SqlCommand cmd = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getOpenOrderCustomer();
            }
        }

        protected void getOpenOrderCustomer()
        {
            string currentCustomerID = Default.customerNum;

            // check if there is an open order customer and chackout page is active
            if (currentCustomerID != "-1" && Default.checkoutActive)
            {
                findCustomer(currentCustomerID);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            lblCustomerIdNotifications.Text = "";
            flushData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblCustomerIdNotifications.Text = "";

            // open db connection
            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

           // add customer to db
            string addCustomerQuery = "INSERT INTO Customers(FirstName, LastName, Address, City, Province, PostalCode)" 
                                    + "VALUES(@fName, @lName, @address, @city, @province, @pCode)";
            try
            {
                cmd = new SqlCommand(addCustomerQuery, connectCmd);
                cmd.Parameters.AddWithValue("@fName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@city", txtCity.Text);
                cmd.Parameters.AddWithValue("@province", txtProvince.Text);
                cmd.Parameters.AddWithValue("@pCode", txtPostalCode.Text);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblCustomerIdNotifications.Text = ex.Message;
                lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Red;

                Default.disposeResources(ref connectCmd, ref cmd);
                return;
            }

            // get created cutomer id
            string getNewCustIdQuery = "SELECT IDENT_CURRENT('Customers') FROM Customers";
            cmd = new SqlCommand(getNewCustIdQuery, connectCmd);
            int newCustId = Convert.ToInt32(cmd.ExecuteScalar());

            txtCustomerId.Text = newCustId.ToString();
            Default.customerNum = newCustId.ToString();

            lblCustomerIdNotifications.Text = "Customer " + newCustId + " was added successfully!";
            lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Green;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblCustomerIdNotifications.Text = "";
            if (txtCustomerId.Text != "")
            {
                // open db connection
                connectCmd = new SqlConnection(Default.dbConnect);
                connectCmd.Open();

                // update customer in db
                string updateCustomerQuery = "Update Customers SET FirstName=@fName, LastName=@lName, Address=@address," +
                                        "City=@city, Province=@province, PostalCode=@pCode WHERE CustomerID = @custId";
                try
                {
                    cmd = new SqlCommand(updateCustomerQuery, connectCmd);
                    cmd.Parameters.AddWithValue("@custId", txtCustomerId.Text);
                    cmd.Parameters.AddWithValue("@fName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@lName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@city", txtCity.Text);
                    cmd.Parameters.AddWithValue("@province", txtProvince.Text);
                    cmd.Parameters.AddWithValue("@pCode", txtPostalCode.Text);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblCustomerIdNotifications.Text = ex.Message;
                    lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Red;

                    Default.disposeResources(ref connectCmd, ref cmd);
                    return;
                }

                lblCustomerIdNotifications.Text = "Customer was updated successfully!";
                lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Green;

                Default.disposeResources(ref connectCmd, ref cmd);
            }
            else
            {
                lblCustomerIdNotifications.Text = "Customer not found";
                lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblCustomerIdNotifications.Text = "";

            // open db connection
            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            // delete customer from db
            string deleteCustomerQuery = "DELETE FROM Customers WHERE CustomerID = @custId";
            try
            {
                cmd = new SqlCommand(deleteCustomerQuery, connectCmd);
                cmd.Parameters.AddWithValue("@custId", txtCustomerId.Text);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblCustomerIdNotifications.Text = ex.Message;
                lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Red;

                Default.disposeResources(ref connectCmd, ref cmd);
                return;
            }

            flushData();

            lblCustomerIdNotifications.Text = "Customer deleted successfully";
            lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Green;

            Default.disposeResources(ref connectCmd, ref cmd);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            lblCustomerIdNotifications.Text = "";
            findCustomer(txtCustomerId.Text);

            Default.customerNum = txtCustomerId.Text;
        }

        private void findCustomer(string customerID)
        {
            SqlDataAdapter dataAdapter = null;
            DataSet data = new DataSet();

            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            string findCustomerQuery = "SELECT * FROM Customers WHERE CustomerID = @custId";

            try
            {
                cmd = new SqlCommand(findCustomerQuery, connectCmd);
                cmd.Parameters.AddWithValue("@custId", customerID);

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(data, "Customers");
            }
            catch (Exception ex)
            {
                lblCustomerIdNotifications.Text = ex.Message;
                lblCustomerIdNotifications.ForeColor = System.Drawing.Color.Red;

                Default.disposeResources(ref connectCmd, ref cmd);
                return;
            }

            if (data.Tables["Customers"].Rows.Count == 1)
            {
                DataRow userData = data.Tables["Customers"].Rows[0];

                txtCustomerId.Text = customerID;
                txtFirstName.Text = userData["FirstName"].ToString();
                txtLastName.Text = userData["LastName"].ToString();
                txtAddress.Text = userData["Address"].ToString();
                txtCity.Text = userData["City"].ToString();
                txtProvince.Text = userData["Province"].ToString();
                txtPostalCode.Text = userData["PostalCode"].ToString();
            }

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            Default.disposeResources(ref connectCmd, ref cmd, ref dataAdapter, ref data);
        }

        private void flushData()
        {
            txtCustomerId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtProvince.Text = "";
            txtPostalCode.Text = "";

            lblCustomerIdNotifications.Text = "";

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

    }
}