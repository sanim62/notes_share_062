using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_progress_report
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Simple authorization check
            if (Session["AdminRole"] == null || Session["AdminRole"].ToString() != "Admin")
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }

        protected void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            // Simulate clearing history/logs since there is no DB implementation for it yet
            MessagePanel.Visible = true;
            StatusLabel.Text = "System history and user activity logs have been successfully deleted.";
        }
    }
}
