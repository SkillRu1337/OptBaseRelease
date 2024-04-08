using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OptBaseNew
{
    public partial class Remnant : System.Web.UI.Page
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

                    string query = "SELECT MatID, MatName, MatDescription, MatRemainder FROM BaseMaterials ORDER BY MatID ASC";

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