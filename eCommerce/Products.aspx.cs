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
    public partial class Products : System.Web.UI.Page
    {
        public static string productPics = HttpContext.Current.Server.MapPath(".") + @"\Pictures";

        private SqlConnection connectCmd = null;
        private SqlCommand cmd = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get all files from pictures directory
                string[] picsList = Directory.GetFiles(productPics, "*.*");
                lstPictures.Items.Clear();

                // add all file names fromdirectory into pics names list
                for (int i = 0; i < picsList.Length; i++)
                {
                    string picName = Path.GetFileName(picsList[i]);
                    lstPictures.Items.Add(picName);
                }
            }
        }

        protected void lstPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgProductPic.ImageUrl = "pictures/" + lstPictures.SelectedItem;
            txtProductPic.Text = lstPictures.SelectedItem.Text;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            flushData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // open db connection
            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            // add product to db
            string addCustomerQuery = "INSERT INTO Products(manufacCode, description, pic, qtyOnHand, price)"
                                    + "VALUES(@manufCode, @descript, @pic, @qoh, @price)";
            try
            {
                cmd = new SqlCommand(addCustomerQuery, connectCmd);
                cmd.Parameters.AddWithValue("@manufCode", txtManufacCode.Text);
                cmd.Parameters.AddWithValue("@descript", txtDescription.Text);
                cmd.Parameters.AddWithValue("@pic", txtProductPic.Text);
                cmd.Parameters.AddWithValue("@qoh", txtQOH.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblProductIdNotifications.Text = ex.Message;
                lblProductIdNotifications.ForeColor = System.Drawing.Color.Red;

                Default.disposeResources(ref connectCmd, ref cmd);
                return;
            }

            // get created cutomer id
            string getNewProdIdQuery = "SELECT IDENT_CURRENT('Products') FROM Products";
            cmd = new SqlCommand(getNewProdIdQuery, connectCmd);
            int newProdId = Convert.ToInt32(cmd.ExecuteScalar());

            txtProductId.Text = newProdId.ToString();

            lblProductIdNotifications.Text = "Product " + newProdId + " was added successfully!";
            lblProductIdNotifications.ForeColor = System.Drawing.Color.Green;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblProductIdNotifications.Text = "";
            if (txtProductId.Text != "")
            {
                // open db connection
                connectCmd = new SqlConnection(Default.dbConnect);
                connectCmd.Open();

                // update product in db
                string updateProductQuery = "Update Products SET manufacCode=@manufCode, description=@descript, pic=@pic," +
                                        "qtyOnHand=@qoh, price=@price WHERE ProductID = @prodId";
                try
                {
                    cmd = new SqlCommand(updateProductQuery, connectCmd);
                    cmd.Parameters.AddWithValue("@prodId", txtProductId.Text);
                    cmd.Parameters.AddWithValue("@manufCode", txtManufacCode.Text);
                    cmd.Parameters.AddWithValue("@descript", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@pic", txtProductPic.Text);
                    cmd.Parameters.AddWithValue("@qoh", txtQOH.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblProductIdNotifications.Text = ex.Message;
                    lblProductIdNotifications.ForeColor = System.Drawing.Color.Red;

                    Default.disposeResources(ref connectCmd, ref cmd);
                    return;
                }

                lblProductIdNotifications.Text = "Product was updated successfully!";
                lblProductIdNotifications.ForeColor = System.Drawing.Color.Green;

                Default.disposeResources(ref connectCmd, ref cmd);
            }
            else
            {
                lblProductIdNotifications.Text = "Product not found";
                lblProductIdNotifications.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblProductIdNotifications.Text = "";

            // open db connection
            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            // delete product from db
            string deleteProdQuery = "DELETE FROM Products WHERE ProductID = @prodId";
            try
            {
                cmd = new SqlCommand(deleteProdQuery, connectCmd);
                cmd.Parameters.AddWithValue("@prodId", txtProductId.Text);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblProductIdNotifications.Text = ex.Message;
                lblProductIdNotifications.ForeColor = System.Drawing.Color.Red;

                Default.disposeResources(ref connectCmd, ref cmd);
                return;
            }

            flushData();

            lblProductIdNotifications.Text = "Product deleted successfully";
            lblProductIdNotifications.ForeColor = System.Drawing.Color.Green;

            Default.disposeResources(ref connectCmd, ref cmd);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            lblProductIdNotifications.Text = "";
            SqlDataAdapter dataAdapter = null;
            DataSet data = new DataSet();

            connectCmd = new SqlConnection(Default.dbConnect);
            connectCmd.Open();

            string findProductQuery = "SELECT * FROM Products WHERE ProductID = @prodId";

            try
            {
                cmd = new SqlCommand(findProductQuery, connectCmd);
                cmd.Parameters.AddWithValue("@prodId", txtProductId.Text);

                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(data, "Products");
            }
            catch (Exception ex)
            {
                lblProductIdNotifications.Text = ex.Message;
                lblProductIdNotifications.ForeColor = System.Drawing.Color.Red;

                Default.disposeResources(ref connectCmd, ref cmd);
                return;
            }

            if (data.Tables["Products"].Rows.Count == 1)
            {
                DataRow product = data.Tables["Products"].Rows[0];

                txtManufacCode.Text = product["manufacCode"].ToString();
                txtDescription.Text = product["description"].ToString();
                txtProductPic.Text = product["pic"].ToString();
                txtQOH.Text = product["qtyOnHand"].ToString();
                txtPrice.Text = product["price"].ToString();

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;

                imgProductPic.ImageUrl = "pictures/" + txtProductPic.Text;
            }
            else
            {
                lblProductIdNotifications.Text = "Product not found";
                lblProductIdNotifications.ForeColor = System.Drawing.Color.Red;
            }

            Default.disposeResources(ref connectCmd, ref cmd, ref dataAdapter, ref data);
        }

        private void flushData()
        {
            txtProductId.Text = "";
            txtManufacCode.Text = "";
            txtDescription.Text = "";
            txtProductPic.Text = "";
            txtQOH.Text = "";
            txtPrice.Text = "";

            lblProductIdNotifications.Text = "";

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }


    }
}