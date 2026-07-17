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
    class DeaCustDAL
    {
        //Static String Method for Database Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT Method for Dealer and Customer
        public DataTable Select()
        {
            //NpgsqlConnection for Database Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //DataTable to hold the value from database and return it
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query to Select all the Data from database
                string sql = "SELECT * FROM tbl_dea_cust";

                //Creating NpgsqlCommand to execute Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Creating NpgsqlDataAdapter to Store Data From Database Temporarily
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();
                //Passing the value from NpgsqlDataAdapter to DataTable
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
        #region INSERT Method to Add details of Dealer or Customer
        public bool Insert(DeaCustBLL dc)
        {
            //Creating NpgsqlConnection First
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //Create a Boolean value and set its default value to false
            bool isSuccess = false;

            try
            {
                //Write SQL Query to Insert Details of Dealer or Customer
                string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                //NpgsqlCommand to Pass the values to query and execute
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing the values using Parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                //Open Database Connection
                conn.Open();

                //Int variable to check whether the query is executed successfully or not
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
        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBLL dc)
        {
            //NpgsqlConnection for Database Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);
            //Create Boolean variable and set its default value to false
            bool isSuccess = false;

            try
            {
                //SQL Query to update data in database
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //Create NpgsqlCommand to pass the value in sql
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open the Database Connection
                conn.Open();

                //Int variable to check if the query executed successfully or not
                int rows = cmd.ExecuteNonQuery();
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
        #region DELETE Method for Dealer and Customer Module
        public bool Delete(DeaCustBLL dc)
        {
            //NpgsqlConnection for Database Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //Create a Boolean Variable and set its default value to false
            bool isSuccess = false;

            try
            {
                //SQL Query to Delete Data from database
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                //NpgsqlCommand to pass the value
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing the value
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open DB Connection
                conn.Open();
                //integer variable
                int rows = cmd.ExecuteNonQuery();
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
        #region SEARCH METHOD for Dealer and Customer Module
        public DataTable Search(string keyword)
        {
            //Create a NpgsqlConnection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //Creating a DataTable and returning its value
            DataTable dt = new DataTable();

            try
            {
                // PostgreSQL: CAST id to TEXT for LIKE comparison (SQL Server allows LIKE on int directly)
                string sql = "SELECT * FROM tbl_dea_cust WHERE CAST(id AS TEXT) LIKE '%"+keyword+"%' OR type LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //NpgsqlCommand to Execute the Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //NpgsqlDataAdapter to hold the data from database temporarily
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();
                //Pass the value from adapter to data table
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
        #region METHOD TO SEARCH DEALER OR CUSTOMER FOR TRANSACTION MODULE
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            //Create an object for DeaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();

            //Create a Database Connection
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            //Create a DataTable to hold the value temporarily
            DataTable dt = new DataTable();

            try
            {
                // PostgreSQL: CAST id to TEXT for LIKE comparison
                string sql = "SELECT name, email, contact, address from tbl_dea_cust WHERE CAST(id AS TEXT) LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //Create a NpgsqlDataAdapter to Execute the Query
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);

                //Open the Database Connection
                conn.Open();

                //Transfer the data from NpgsqlDataAdapter to DataTable
                adapter.Fill(dt);

                //If we have values on dt we need to save it in dealerCustomer BLL
                if(dt.Rows.Count>0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database connection
                conn.Close();
            }

            return dc;
        }
        #endregion
        #region METHOD TO GET ID OF THE DEALER OR CUSTOMER BASED ON NAME
        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
            //First Create an Object of DeaCust BLL and Return it
            DeaCustBLL dc = new DeaCustBLL();

            //NpgsqlConnection here
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);
            //DataTable to Hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_dea_cust WHERE name='"+Name+"'";
                //Create the NpgsqlDataAdapter to Execute the Query
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);

                conn.Open();

                //Passing the Value from Adapter to DataTable
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return dc;
        }
        #endregion

    }
}
