using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
// Replaced Microsoft.Data.SqlClient with Npgsql for PostgreSQL 16 compatibility
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class productsDAL
    {
        //Creating STATIC String Method for DB Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module
        public DataTable Select()
        {
            //Create NpgsqlConnection to connect Database
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //DataTable to hold the data from database
            DataTable dt = new DataTable();

            try
            {
                //Writing the Query to Select all the products from database
                String sql = "SELECT * FROM tbl_products";

                //Creating NpgsqlCommand to Execute Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //NpgsqlDataAdapter to hold the value from database temporarily
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region Method to Insert Product in database
        public bool Insert(productsBLL p)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //NpgsqlConnection for Database
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                //Creating NpgsqlCommand to pass the values
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                //Opening the Database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Method to Update Product in Database
        public bool Update(productsBLL p)
        {
            //create a boolean variable and set its initial value to false
            bool isSuccess = false;

            //Create NpgsqlConnection for Database
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //SQL Query to Update Data in database
                String sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //Create NpgsqlCommand to pass the value to query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing the values using parameters and cmd
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open the Database connection
                conn.Open();

                //Create Int Variable to check if the query is executed successfully or not
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the value of rows will be greater than 0 else it will be less than zero
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Method to Delete Product from Database
        public bool Delete(productsBLL p)
        {
            //Create Boolean Variable and Set its default value to false
            bool isSuccess = false;

            //NpgsqlConnection for DB connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Write Query to Delete Product from Database
                String sql = "DELETE FROM tbl_products WHERE id=@id";

                //NpgsqlCommand to Pass the Value
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //If the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region SEARCH Method for Product Module
        public DataTable Search (string keywords)
        {
            //NpgsqlConnection for DB Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);
            //Creating DataTable to hold value from database
            DataTable dt = new DataTable();

            try
            {
                //SQL query to search product
                string sql = "SELECT * FROM tbl_products WHERE CAST(id AS TEXT) LIKE '%"+keywords+"%' OR name LIKE '%"+keywords+"%' OR category LIKE '%"+keywords+"%'";
                //NpgsqlCommand to execute Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //NpgsqlDataAdapter to hold the data from database temporarily
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE
        public productsBLL GetProductsForTransaction(string keyword)
        {
            //Create an object of productsBLL and return it
            productsBLL p = new productsBLL();
            //NpgsqlConnection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);
            //Datatable to store data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write the Query to Get the details
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE CAST(id AS TEXT) LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
                //Create NpgsqlDataAdapter to Execute the query
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);

                //Open Database Connection
                conn.Open();

                //Pass the value from adapter to dt
                adapter.Fill(dt);

                //If we have any values on dt then set the values to productsBLL
                if(dt.Rows.Count>0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return p;
        }
        #endregion
        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME
        public productsBLL GetProductIDFromName(string ProductName)
        {
            //First Create an Object of productsBLL and Return it
            productsBLL p = new productsBLL();

            //NpgsqlConnection here
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);
            //DataTable to Hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_products WHERE name='" + ProductName + "'";
                //Create the NpgsqlDataAdapter to Execute the Query
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);

                conn.Open();

                //Passing the Value from Adapter to DataTable
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to productsBLL p
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return p;
        }
        #endregion
        #region METHOD TO GET CURRENT QUANTITY from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
            //NpgsqlConnection First
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);
            //Create a Decimal Variable and set its default value to 0
            decimal qty = 0;

            //Create DataTable to save the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query to Get Quantity from Database
                string sql = "SELECT qty FROM tbl_products WHERE id = "+ProductID;

                //Create a NpgsqlCommand
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Create a NpgsqlDataAdapter to Execute the query
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Pass the value from Data Adapter to DataTable
                adapter.Fill(dt);

                //Lets check if the datatable has value or not
                if(dt.Rows.Count>0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return qty;
        }
        #endregion
        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            //Create a Boolean Variable and Set its value to false
            bool success = false;

            //NpgsqlConnection to Connect Database
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

                //Create NpgsqlCommand to Pass the value into Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing the Value through parameters
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is executed Successfully or not
                int rows = cmd.ExecuteNonQuery();
                //Lets check if the query is executed Successfully or not
                if(rows>0)
                {
                    //Query Executed Successfully
                    success = true;
                }
                else
                {
                    //Failed to Execute Query
                    success = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion
        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
            //Create a Boolean Variable and Set its value to False
            bool success = false;

            //Create NpgsqlConnection To Connect Database
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Get the Current Qty From database based on id
                decimal currentQty = GetProductQty(ProductID);

                //Increase the Current Quantity by the qty purchased from Dealer
                decimal NewQty = currentQty + IncreaseQty;

                //Update the Product Quantity Now
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            //Create Boolean Variable and Set its Value to false
            bool success = false;

            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Get the Current product Quantity
                decimal currentQty = GetProductQty(ProductID);

                //Decrease the Product Quantity based on product sales
                decimal NewQty = currentQty - Qty;

                //Update Product in Database
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region DISPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
            //NpgsqlConnection First
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Display Product Based on Category
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"'";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection Here
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
    }
}
