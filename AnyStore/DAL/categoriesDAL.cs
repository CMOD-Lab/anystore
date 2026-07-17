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
    class categoriesDAL
    {
        //Static String Method for Database Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
            //Creating Database Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Writing SQL Query to get all the data from Database
                string sql = "SELECT * FROM tbl_categories";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                //Open Database Connection
                conn.Open();
                //Adding the value from adapter to DataTable dt
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
        #region Insert New Category
        public bool Insert(categoriesBLL c)
        {
            //Creating A Boolean Variable and set its default value to false
            bool isSucces = false;

            //Connecting to Database
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Writing Query to Add New Category
                string sql = "INSERT INTO tbl_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";

                //Creating NpgsqlCommand to pass values in our query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing Values through parameter
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                //Open Database Connection
                conn.Open();

                //Creating the int variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then its value will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSucces = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSucces = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Database Connection
                conn.Close();
            }

            return isSucces;
        }
        #endregion
        #region Update Method
        public bool Update(categoriesBLL c)
        {
            //Creating Boolean variable and set its default value to false
            bool isSuccess = false;

            //Creating NpgsqlConnection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //NpgsqlCommand to Pass the Value on Sql Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Passing Value using cmd
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open Database Connection
                conn.Open();

                //Create Int Variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is successfully executed then the value will be greater than zero 
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
        #region Delete Category Method
        public bool Delete(categoriesBLL c)
        {
            //Create a Boolean variable and set its value to false
            bool isSuccess = false;

            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //SQL Query to Delete from Database
                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open NpgsqlConnection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then the value of rows will be greater than zero else it will be less than 0
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
        #region Method for Search Functionality
        public DataTable Search(string keywords)
        {
            //NpgsqlConnection For Database Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //Creating DataTable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                // PostgreSQL: CAST id to TEXT for LIKE comparison (SQL Server allows LIKE on int directly)
                String sql = "SELECT * FROM tbl_categories WHERE CAST(id AS TEXT) LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%'";
                //Creating NpgsqlCommand to Execute the Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Getting Data From Database
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();
                //Passing values from adapter to DataTable dt
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
