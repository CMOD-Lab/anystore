using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class loginDAL
    {
        // Static String to Connect Database
        // ConfigurationManager is provided by System.Configuration.ConfigurationManager NuGet package in .NET 8
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
            // Create a boolean variable and set its value to false and return it
            bool isSuccess = false;

            // Connecting To Database using Npgsql (PostgreSQL ADO.NET provider replacing Microsoft.Data.SqlClient)
            using NpgsqlConnection conn = new NpgsqlConnection(myconnstrng);

            try
            {
                // SQL Query to check login
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                // Creating SQL Command to pass value
                using NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                // Checking The rows in DataTable
                if (dt.Rows.Count > 0)
                {
                    // Login Successful
                    isSuccess = true;
                }
                else
                {
                    // Login Failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return isSuccess;
        }
    }
}
