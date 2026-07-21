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
    class categoriesDAL
    {
        // Static String Method for Database Connection String
        // ConfigurationManager is provided by System.Configuration.ConfigurationManager NuGet package in .NET 8
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_categories";

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

        #region Insert New Category
        public bool Insert(categoriesBLL c)
        {
            bool isSuccess = false;

            try
            {
                string sql = "INSERT INTO tbl_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

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

        #region Update Method
        public bool Update(categoriesBLL c)
        {
            bool isSuccess = false;

            try
            {
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

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

        #region Delete Category Method
        public bool Delete(categoriesBLL c)
        {
            bool isSuccess = false;

            try
            {
                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                using SqlConnection conn = new SqlConnection(myconnstrng);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", c.id);

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

        #region Method for Search Functionality
        public DataTable Search(string keywords)
        {
            DataTable dt = new DataTable();

            try
            {
                // Using parameterized query to prevent SQL injection
                string sql = "SELECT * FROM tbl_categories WHERE CAST(id AS NVARCHAR) LIKE @kw OR title LIKE @kw OR description LIKE @kw";

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
    }
}
