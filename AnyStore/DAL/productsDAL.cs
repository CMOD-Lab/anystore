using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class productsDAL
    {
        // Creating Static String Method for DB Connection
        // ConfigurationManager is provided by System.Configuration.ConfigurationManager NuGet package in .NET 8
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module
        public DataTable Select()
        {
            // DataTable to hold the data from database
            DataTable dt = new DataTable();

            try
            {
                // Writing the Query to Select all the products from database
                string sql = "SELECT * FROM tbl_products";

                // Using Microsoft.Data.SqlClient (replaces System.Data.SqlClient in .NET 8)
                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }
        #endregion

        #region Method to Insert Product in database
        public bool Insert(productsBLL p)
        {
            bool isSuccess = false;

            try
            {
                string sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return isSuccess;
        }
        #endregion

        #region Method to Update Product in Database
        public bool Update(productsBLL p)
        {
            bool isSuccess = false;

            try
            {
                string sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return isSuccess;
        }
        #endregion

        #region Method to Delete Product from Database
        public bool Delete(productsBLL p)
        {
            bool isSuccess = false;

            try
            {
                string sql = "DELETE FROM tbl_products WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", p.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return isSuccess;
        }
        #endregion

        #region SEARCH Method for Product Module
        public DataTable Search(string keywords)
        {
            DataTable dt = new DataTable();

            try
            {
                // Using parameterized query to prevent SQL injection
                string sql = "SELECT * FROM tbl_products WHERE CAST(id AS NVARCHAR) LIKE @kw OR name LIKE @kw OR category LIKE @kw";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keywords + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }
        #endregion

        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE
        public productsBLL GetProductsForTransaction(string keyword)
        {
            productsBLL p = new productsBLL();
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE CAST(id AS NVARCHAR) LIKE @kw OR name LIKE @kw";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString() ?? string.Empty;
                    p.rate = decimal.Parse(dt.Rows[0]["rate"]?.ToString() ?? "0");
                    p.qty = decimal.Parse(dt.Rows[0]["qty"]?.ToString() ?? "0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return p;
        }
        #endregion

        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME
        public productsBLL GetProductIDFromName(string ProductName)
        {
            productsBLL p = new productsBLL();
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM tbl_products WHERE name=@name";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", ProductName);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    p.id = int.Parse(dt.Rows[0]["id"]?.ToString() ?? "0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return p;
        }
        #endregion

        #region METHOD TO GET CURRENT QUANTITY from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
            decimal qty = 0;
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT qty FROM tbl_products WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", ProductID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"]?.ToString() ?? "0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return qty;
        }
        #endregion

        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            bool success = false;

            try
            {
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                success = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return success;
        }
        #endregion

        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
            bool success = false;

            try
            {
                decimal currentQty = GetProductQty(ProductID);
                decimal NewQty = currentQty + IncreaseQty;
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return success;
        }
        #endregion

        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            bool success = false;

            try
            {
                decimal currentQty = GetProductQty(ProductID);
                decimal NewQty = currentQty - Qty;
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return success;
        }
        #endregion

        #region DISPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_products WHERE category=@category";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", category);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }
        #endregion
    }
}
