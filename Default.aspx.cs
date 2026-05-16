using System;

namespace web_progress_report
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}
