using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace web_progress_report
{
    public partial class UserLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string rollNumber = IDTextBox.Text.Trim();
            string password = PasswordTextBox.Text;

            if (!string.IsNullOrEmpty(rollNumber) && !string.IsNullOrEmpty(password))
            {
                bool isAuthenticated = false;
                string hashedPassword = HashPassword(password);

                try
                {
                    string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "SELECT COUNT(1) FROM Users WHERE UserId = @UserId AND PasswordHash = @PasswordHash AND UserType = 'Student'";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", rollNumber);
                            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count == 1)
                            {
                                isAuthenticated = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Database error: " + ex.Message;
                    StatusLabel.CssClass = "status-label error";
                    return;
                }

                if (isAuthenticated)
                {
                    Session["UserId"] = rollNumber;
                    Session["UserType"] = "Student";
                    StatusLabel.Text = "Login successful for Student: " + rollNumber;
                    StatusLabel.CssClass = "status-label success";
                    // Response.Redirect("ManageUsers.aspx");
                }
                else
                {
                    StatusLabel.Text = "Invalid roll number or password.";
                    StatusLabel.CssClass = "status-label error";
                }
            }
            else
            {
                StatusLabel.Text = "Please enter both roll number and password.";
                StatusLabel.CssClass = "status-label error";
            }
        }
    }
}
