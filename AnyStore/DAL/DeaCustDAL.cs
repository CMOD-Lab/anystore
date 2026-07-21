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
    class DeaCustDAL
    {
        // Static String Method for Database Connection
        // ConfigurationManager is provided by System.Configuration.ConfigurationManager NuGet package in .NET 8
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT Method for Dealer and Customer
        public DataTable Select()
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_dea_cust";

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

        #region INSERT Method to Add details of Dealer or Customer
        public bool Insert(DeaCustBLL dc)
        {
            bool isSuccess = false;

            try
            {
                string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

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

        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBLL dc)
        {
            bool isSuccess = false;

            try
            {
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

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

        #region DELETE Method for Dealer and Customer Module
        public bool Delete(DeaCustBLL dc)
        {
            bool isSuccess = false;

            try
            {
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", dc.id);

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

        #region SEARCH METHOD for Dealer and Customer Module
        public DataTable Search(string keyword)
        {
            DataTable dt = new DataTable();

            try
            {
                // Using parameterized query to prevent SQL injection
                string sql = "SELECT * FROM tbl_dea_cust WHERE CAST(id AS NVARCHAR) LIKE @kw OR type LIKE @kw OR name LIKE @kw";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

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

        #region METHOD TO SEARCH DEALER OR CUSTOMER FOR TRANSACTION MODULE
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            DeaCustBLL dc = new DeaCustBLL();
            DataTable dt = new DataTable();

            try
            {
                // Using parameterized query to prevent SQL injection
                string sql = "SELECT name, email, contact, address FROM tbl_dea_cust WHERE CAST(id AS NVARCHAR) LIKE @kw OR name LIKE @kw";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"]?.ToString() ?? string.Empty;
                    dc.email = dt.Rows[0]["email"]?.ToString() ?? string.Empty;
                    dc.contact = dt.Rows[0]["contact"]?.ToString() ?? string.Empty;
                    dc.address = dt.Rows[0]["address"]?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dc;
        }
        #endregion

        #region METHOD TO GET ID OF THE DEALER OR CUSTOMER BASED ON NAME
        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
            DeaCustBLL dc = new DeaCustBLL();
            DataTable dt = new DataTable();

            try
            {
                // Using parameterized query to prevent SQL injection
                string sql = "SELECT id FROM tbl_dea_cust WHERE name=@name";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", Name);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dc.id = int.Parse(dt.Rows[0]["id"]?.ToString() ?? "0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dc;
        }
        #endregion
    }
}
