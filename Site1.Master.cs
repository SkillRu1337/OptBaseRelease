using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace OptBaseNew
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] != null && Session["UserGroup"] != null)
            {
                UserLabel.Text = Session["UserLogin"].ToString() + " (Группа: " + Session["UserGroup"] + ") <a href=\"#\" onclick=\"logout(); return false;\">Выйти</a>";
            }
            else
            {
                UserLabel.Text = "<a href=\"/Login.aspx\">Авторизация</a>";
            }
        }
    }
}