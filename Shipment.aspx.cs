using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OptBaseNew
{
    public partial class Shipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userGroup = Session["UserGroup"] as string;
            if (userGroup != "Wholesaler")
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

                    string query = "SELECT MatID, MatName, MatDescription, MatWeight, MatSizes, MatRemainder FROM BaseMaterials ORDER BY MatID ASC";

                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Заполняем GridView данными
                        GridView1.DataSource = reader;
                        GridView1.DataBind();

                        reader.Close();
                    }
                }

                FillDropDownList();
            }
        }

        [WebMethod]
        public static string ShipMaterial(string shipMaterial, string shipCount, string shipPartner)
        {
            string result = "error1"; // Заполнены не все поля
            bool updateSuccess = false; // Статус обновления количества
            int intShipMaterial = Convert.ToInt32(shipMaterial) + 1;
            // Проверяем, все ли поля заполнены
            if (!string.IsNullOrEmpty(shipMaterial) && !string.IsNullOrEmpty(shipCount) && !string.IsNullOrEmpty(shipPartner))
            {
                // Проверяем, чтобы количество отгружаемого не было <= 0
                if(!(Convert.ToInt32(shipCount) <= 0))
                {
                    string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "SELECT MatRemainder FROM BaseMaterials WHERE MatID = @MatID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MatID", intShipMaterial);
                            connection.Open();
                            object resultExec = command.ExecuteScalar();

                            // Проверяем, чтобы запрос выполнился и запрошенное количество не превышало остаток на базе
                            if (resultExec != null && (Convert.ToInt32(resultExec) >= Convert.ToInt32(shipCount)))
                            {
                                int matRemainder = Convert.ToInt32(resultExec);

                                // Изменение количества на остатках базы
                                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                                {
                                    sqlCon.Open();

                                    string queryExec = "UPDATE BaseMaterials SET MatRemainder = @MatRemainder WHERE MatID = @MatID";
                                    using (SqlCommand cmd = new SqlCommand(queryExec, sqlCon))
                                    {
                                        cmd.Parameters.AddWithValue("@MatRemainder", matRemainder - Convert.ToInt32(shipCount));
                                        cmd.Parameters.AddWithValue("@MatID", intShipMaterial);

                                        int rowsAffected = cmd.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            updateSuccess = true;
                                        }
                                    }
                                }

                                // Если удалось обновить количество на базе, то записываем транзакцию в таблицу
                                if (updateSuccess == true)
                                {
                                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                                    {
                                        sqlCon.Open();

                                        string countQuery = "SELECT COUNT(*) FROM BaseShipment";
                                        int rowCount = 0;
                                        using (SqlCommand countCmd = new SqlCommand(countQuery, sqlCon))
                                        {
                                            rowCount = (int)countCmd.ExecuteScalar();
                                        }

                                        // Получение нового ID
                                        int newShipID = rowCount + 1;

                                        // Получение текущего времени и приведение его к формату SQL
                                        DateTime currentDateTime = DateTime.Now;
                                        string sqlFormattedDateTime = currentDateTime.ToString("yyyy-dd-MM HH:mm:ss");

                                        string queryExec = "INSERT INTO BaseShipment (ShipID, MatID, UserID, ShipCount, ShipPartner, ShipDateTime) VALUES (@ShipID, @MatID, @UserID, @ShipCount, @ShipPartner, @ShipDateTime)";
                                        using (SqlCommand cmd = new SqlCommand(queryExec, sqlCon))
                                        {
                                            cmd.Parameters.AddWithValue("@ShipID", newShipID);
                                            cmd.Parameters.AddWithValue("@MatID", intShipMaterial);
                                            cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"]);
                                            cmd.Parameters.AddWithValue("@ShipCount", shipCount);
                                            cmd.Parameters.AddWithValue("@ShipPartner", shipPartner);
                                            cmd.Parameters.AddWithValue("@ShipDateTime", sqlFormattedDateTime);

                                            int rowsAffected = cmd.ExecuteNonQuery();

                                            if (rowsAffected > 0)
                                            {
                                                result = "true";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result = "error2"; // Остаток на складе меньше, чем запрошено в форме
                            }
                        }
                    }
                }
                else
                {
                    result = "error3"; // В форме запрошено 0 или меньше
                }
            }
            return result;
        }

        private void FillDropDownList()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;";

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                string query = "SELECT MatID, MatName, MatWeight, MatSizes, MatRemainder FROM BaseMaterials ORDER BY MatID ASC";

                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string listItemText = string.Format("[ID: {0}] {1}, {2} кг, {3} см [Кол-во на складе: {4}]",
                            reader["MatID"],
                            reader["MatName"],
                            reader["MatWeight"],
                            reader["MatSizes"],
                            reader["MatRemainder"]);

                        string listItemValue = reader["MatID"].ToString();

                        DropDownList1.Items.Add(new ListItem(listItemText, listItemValue));
                    }

                    reader.Close();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int remainder = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MatRemainder"));

                if (remainder == 0)
                {
                    e.Row.CssClass = "ship-row-zero";
                }
            }
        }
    }
}