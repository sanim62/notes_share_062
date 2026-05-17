using System;
using System.Web.UI;

namespace web_progress_report
{
    public partial class UserLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string rollNumber = IDTextBox.Text.Trim();
            string password = PasswordTextBox.Text;

            if (!string.IsNullOrEmpty(rollNumber) && !string.IsNullOrEmpty(password))
            {
                StatusLabel.Text = "Login successful for Student: " + rollNumber;
                StatusLabel.CssClass = "status-label success";
            }
            else
            {
                StatusLabel.Text = "Please enter both roll number and password.";
                StatusLabel.CssClass = "status-label error";
            }
        }
    }
}
