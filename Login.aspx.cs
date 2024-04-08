using System;
using System.Web;
using System.Data.SqlClient;
using System.Web.Services;

namespace OptBaseNew
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] != null)
                Response.Redirect("Index.aspx");
        }

        [WebMethod]
        public static string CheckLogin(string username, string password)
        {
            string result = "false";

            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;"))
            {
                sqlCon.Open();
                string sqlQuery = "SELECT UserID, UserGroup FROM BaseAuth WHERE UserLogin=@username COLLATE SQL_Latin1_General_CP1_CS_AS AND UserPassword=@password COLLATE SQL_Latin1_General_CP1_CS_AS";
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Получаем значения UserID и UserGroup из результата запроса
                        int userID = reader.GetInt32(0);
                        string userGroup = reader.GetString(1);

                        // Сохраняем значения в сессию
                        HttpContext.Current.Session["UserLogin"] = username;
                        HttpContext.Current.Session["UserID"] = userID;
                        HttpContext.Current.Session["UserGroup"] = userGroup;
                    }

                    result = "true";
                }
            }

            return result;
        }
    }
}