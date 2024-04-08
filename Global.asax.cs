using System;
using System.Web;

namespace OptBaseNew
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Path.ToLower();

            if (path == "/" || path == "/default.aspx")
            {
                HttpContext.Current.Response.Redirect("~/Index.aspx");
            }
        }
    }
}