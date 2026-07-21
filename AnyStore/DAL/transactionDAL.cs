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
    class transactionDAL
    {
        // Create a connection string variable
        // ConfigurationManager is provided by System.Configuration.ConfigurationManager NuGet package in .NET 8
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method
        public bool Insert_Transaction(transactionsBLL t, out int transactionID)
        {
            bool isSuccess = false;
            transactionID = -1;

            try
            {
                // SQL Query to Insert Transactions; SELECT SCOPE_IDENTITY() is preferred over @@IDENTITY in .NET 8
                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT SCOPE_IDENTITY();";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                conn.Open();
                object? o = cmd.ExecuteScalar();

                if (o != null && o != DBNull.Value)
                {
                    transactionID = int.Parse(o.ToString() ?? "-1");
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return isSuccess;
        }
        #endregion

        #region METHOD TO DISPLAY ALL THE TRANSACTIONS
        public DataTable DisplayAllTransactions()
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_transactions";

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

        #region METHOD TO DISPLAY TRANSACTION BASED ON TRANSACTION TYPE
        public DataTable DisplayTransactionByType(string type)
        {
            DataTable dt = new DataTable();

            try
            {
                // Using parameterized query to prevent SQL injection
                string sql = "SELECT * FROM tbl_transactions WHERE type=@type";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", type);

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
