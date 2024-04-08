using System;

namespace OptBaseNew
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserLogin"] = null;
            Session["UserID"] = null;
            Session["UserGroup"] = null;

            Response.Write("Success");
            Response.End();
        }
    }
}