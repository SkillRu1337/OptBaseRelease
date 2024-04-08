using System;
using System.Data.SqlClient;
using System.Web.Services;

namespace OptBaseNew
{
    public partial class Index : System.Web.UI.Page
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

                    string query = "SELECT MatID, MatName, MatDescription, MatWeight, MatSizes, MatBuyPrice, MatSellPrice, MatOptSellPrice, MatRemainder FROM BaseMaterials ORDER BY MatID ASC";

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
        public static string AddMaterial(string matName, string matDesc, string matWeight, string matSizes, string matBuyPrice, string matSellPrice, string matOptSellPrice)
        {
            string result = "false";
            if (!string.IsNullOrEmpty(matName) && !string.IsNullOrEmpty(matDesc) && !string.IsNullOrEmpty(matWeight) && !string.IsNullOrEmpty(matSizes) && !string.IsNullOrEmpty(matBuyPrice) && !string.IsNullOrEmpty(matSellPrice) && !string.IsNullOrEmpty(matOptSellPrice))
            {
                if (double.TryParse(matWeight, out double newMatWeight) && double.TryParse(matBuyPrice, out double newMatBuyPrice) && double.TryParse(matSellPrice, out double newMatSellPrice) && double.TryParse(matOptSellPrice, out double newMatOptSellPrice))
                {
                    string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;";
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        string countQuery = "SELECT COUNT(*) FROM BaseMaterials";
                        int rowCount = 0;
                        using (SqlCommand countCmd = new SqlCommand(countQuery, sqlCon))
                        {
                            rowCount = (int)countCmd.ExecuteScalar();
                        }

                        // Получение нового ID
                        int newMatID = rowCount + 1;

                        string query = "INSERT INTO BaseMaterials (MatID, MatName, MatDescription, MatWeight, MatSizes, MatBuyPrice, MatSellPrice, MatOptSellPrice, MatRemainder) VALUES (@MatID, @MatName, @MatDescription, @MatWeight, @MatSizes, @MatBuyPrice, @MatSellPrice, @MatOptSellPrice, 0)";
                        using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                        {
                            cmd.Parameters.AddWithValue("@MatID", newMatID);
                            cmd.Parameters.AddWithValue("@MatName", matName);
                            cmd.Parameters.AddWithValue("@MatDescription", matDesc);
                            cmd.Parameters.AddWithValue("@MatWeight", newMatWeight);
                            cmd.Parameters.AddWithValue("@MatSizes", matSizes);
                            cmd.Parameters.AddWithValue("@MatBuyPrice", newMatBuyPrice);
                            cmd.Parameters.AddWithValue("@MatSellPrice", newMatSellPrice);
                            cmd.Parameters.AddWithValue("@MatOptSellPrice", newMatOptSellPrice);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                result = "true";
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}