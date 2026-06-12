using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_progress_report
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string adminId = AdminIdTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();

            // Simple hardcoded check for demonstration purposes
            if (adminId == "admin" && password == "admin123")
            {
                Session["AdminRole"] = "Admin";
                Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                StatusLabel.Text = "Invalid Admin ID or Password.";
                StatusLabel.CssClass = "status-label error";
            }
        }
    }
}
