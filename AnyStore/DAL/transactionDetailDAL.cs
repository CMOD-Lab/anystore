using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
// Replaced Microsoft.Data.SqlClient with Npgsql for PostgreSQL 16 compatibility
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {
        //Create Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(transactionDetailBLL td)
        {
            //Create a boolean value and set its default value to false
            bool isSuccess = false;

            //Create a database connection here
            NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                //Sql Query to Insert Transaction details
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //Passing the value to the SQL Query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                //Open Database connection
                conn.Open();

                //declare the int variable and execute the query
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
                //Close Database Connection
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
    }
}
