using System;
using System.Data;
using System.Data.SqlClient;

namespace OptBaseNew
{
    public partial class Archive : System.Web.UI.Page
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
            }
        }

        protected void ArchiveRequest_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(DateFrom.Text) && !string.IsNullOrEmpty(DateTo.Text))
            {
                DateTime dateFromBox = DateTime.Parse(DateFrom.Text);
                DateTime dateToBox = DateTime.Parse(DateTo.Text);
                if (dateFromBox <= dateToBox)
                {
                    ErrorMessageBox.Visible = false;
                    GridView1.Visible = true;

                    string connectionString = @"Data Source=localhost\SQLEXPRESS;initial Catalog=OptBase;Integrated Security=True;";
                    string query = @"SELECT 'Приемка' AS OperationType, 
                                       FORMAT(ba.AcceptDateTime, 'dd.MM.yyyy HH:mm') AS OperationDate, 
                                       ba.AcceptProvider AS Partner, 
                                       bm.MatName AS MaterialName, 
                                       ba.AcceptCount AS Count, 
                                       ba.UserID AS UserID, 
                                       bu.UserFullName AS UserName
                                FROM dbo.BaseAcceptance ba
                                INNER JOIN dbo.BaseMaterials bm ON ba.MatID = bm.MatID
                                INNER JOIN dbo.BaseAuth bu ON ba.UserID = bu.UserID
                                WHERE CONVERT(date, ba.AcceptDateTime) BETWEEN @DateFrom AND @DateTo

                                UNION

                                SELECT 'Отгрузка' AS OperationType, 
                                       FORMAT(bs.ShipDateTime, 'dd.MM.yyyy HH:mm') AS OperationDate, 
                                       bs.ShipPartner AS Partner, 
                                       bm.MatName AS MaterialName, 
                                       bs.ShipCount AS Count, 
                                       bs.UserID AS UserID, 
                                       bu.UserFullName AS UserName
                                FROM dbo.BaseShipment bs
                                INNER JOIN dbo.BaseMaterials bm ON bs.MatID = bm.MatID
                                INNER JOIN dbo.BaseAuth bu ON bs.UserID = bu.UserID
                                WHERE CONVERT(date, bs.ShipDateTime) BETWEEN @DateFrom AND @DateTo";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@DateFrom", DateTime.Parse(DateFrom.Text).ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@DateTo", DateTime.Parse(DateTo.Text).ToString("yyyy-MM-dd"));

                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    GridView1.Visible = false;
                    ErrorMessage.Text = "Дата начала не может быть больше даты окончания.";
                    ErrorMessageBox.Visible = true;
                }
            }
            else
            {
                GridView1.Visible = false;
                ErrorMessage.Text = "Укажите полностью период для выгрузки архива.";
                ErrorMessageBox.Visible = true;
            }
        }
    }
}