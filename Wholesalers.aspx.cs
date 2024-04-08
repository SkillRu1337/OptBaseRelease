using System;
using System.Data.SqlClient;
using System.Web.Services;

namespace OptBaseNew
{
    public partial class Wholesalers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userGroup = Session["UserGroup"] as string;
            if (userGroup != "Admin")
            {
                form1.Visible = false;
                Label_Warning.Visible = true;
            }
            else
            {
                form1.Visible = true;
                Label_Warning.Visible = false;

                string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;";
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    // Открываем соединение
                    sqlCon.Open();

                    string query = "SELECT UserID, UserLogin, UserFullName, UserPicUrl, CONVERT(varchar, UserBirthdate, 105) AS FormattedUserBirthdate FROM BaseAuth WHERE UserGroup = 'Wholesaler' ORDER BY UserID ASC";

                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Заполняем GridView данными
                        GridView1.DataSource = reader;
                        GridView1.DataBind();

                        reader.Close();
                    }
                }
            }
        }

        [WebMethod]
        public static string AddUser(string userLogin, string userPassword, string userFullName, string userPicUrl, string userBirthdate)
        {
            string result = "false";
            if (!string.IsNullOrEmpty(userLogin) && !string.IsNullOrEmpty(userPassword) && !string.IsNullOrEmpty(userFullName) && !string.IsNullOrEmpty(userPicUrl) && !string.IsNullOrEmpty(userBirthdate))
            {
                string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;";
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    string countQuery = "SELECT COUNT(*) FROM BaseAuth";
                    int rowCount = 0;
                    using (SqlCommand countCmd = new SqlCommand(countQuery, sqlCon))
                    {
                        rowCount = (int)countCmd.ExecuteScalar();
                    }

                    // Получение нового ID
                    int newUserID = rowCount + 1;

                    string query = "INSERT INTO BaseAuth (UserID, UserLogin, UserPassword, UserGroup, UserFullName, UserPicUrl, UserBirthdate) VALUES (@UserID, @UserLogin, @UserPassword, 'Wholesaler', @UserFullName, @UserPicUrl, @UserBirthdate)";
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@UserID", newUserID);
                        cmd.Parameters.AddWithValue("@UserLogin", userLogin);
                        cmd.Parameters.AddWithValue("@UserPassword", userPassword);
                        cmd.Parameters.AddWithValue("@UserFullName", userFullName);
                        cmd.Parameters.AddWithValue("@UserPicUrl", userPicUrl);
                        cmd.Parameters.AddWithValue("@UserBirthdate", userBirthdate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result = "true";
                        }
                    }
                }
            }
            return result;
        }
    }
}