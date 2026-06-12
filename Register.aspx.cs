using System;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace web_progress_report
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegistrationPanel.Visible = false;
                InitializeDatabase();
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                string connStrMaster = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStrMaster))
                {
                    conn.Open();
                    string checkDbQuery = "SELECT database_id FROM sys.databases WHERE Name = 'ProgressReportDB'";
                    using (SqlCommand cmd = new SqlCommand(checkDbQuery, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            string createDbQuery = "CREATE DATABASE ProgressReportDB";
                            using (SqlCommand createCmd = new SqlCommand(createDbQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
                        CREATE TABLE Users (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            UserType NVARCHAR(50),
                            FirstName NVARCHAR(100),
                            LastName NVARCHAR(100),
                            UserId NVARCHAR(100) UNIQUE,
                            Gender NVARCHAR(50),
                            Department NVARCHAR(100),
                            DOB DATE,
                            PasswordHash NVARCHAR(255),
                            CurrentYear NVARCHAR(50),
                            CurrentSemester NVARCHAR(50),
                            PhotoPath NVARCHAR(255)
                        );

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Resources' and xtype='U')
                        CREATE TABLE Resources (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            UserId NVARCHAR(100),
                            FileName NVARCHAR(255),
                            FilePath NVARCHAR(500),
                            Department NVARCHAR(100),
                            Course NVARCHAR(100),
                            Chapter NVARCHAR(100),
                            Topic NVARCHAR(255),
                            UploadDate DATETIME DEFAULT GETDATE()
                        );

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Downloads' and xtype='U')
                        CREATE TABLE Downloads (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            UserId NVARCHAR(100),
                            ResourceId INT,
                            DownloadDate DATETIME DEFAULT GETDATE()
                        );";
                    using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DB Init Error: " + ex.Message);
            }
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

        protected void UserTypeRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userType = UserTypeRadioButtonList.SelectedValue;
            RegistrationPanel.Visible = true;
            StudentPanel.Visible = (userType == "Student");
            IDLabel.InnerText = (userType == "Student") ? "Roll Number" : "University ID";
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrEmpty(FirstNameTextBox.Text) || string.IsNullOrEmpty(LastNameTextBox.Text) ||
                string.IsNullOrEmpty(IDTextBox.Text) || string.IsNullOrEmpty(GenderDropDownList.SelectedValue) ||
                string.IsNullOrEmpty(DepartmentDropDownList.SelectedValue) || string.IsNullOrEmpty(DOBTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text) || string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
            {
                StatusLabel.Text = "Please fill in all required fields.";
                StatusLabel.CssClass = "status-label error";
                return;
            }

            if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                StatusLabel.Text = "Passwords do not match.";
                StatusLabel.CssClass = "status-label error";
                return;
            }

            if (UserTypeRadioButtonList.SelectedValue == "Student" &&
                (string.IsNullOrEmpty(YearDropDownList.SelectedValue) || string.IsNullOrEmpty(SemesterDropDownList.SelectedValue)))
            {
                StatusLabel.Text = "Please fill in year and semester.";
                StatusLabel.CssClass = "status-label error";
                return;
            }

            DateTime dob;
            if (!DateTime.TryParse(DOBTextBox.Text, out dob) || dob.Year < 1753)
            {
                StatusLabel.Text = "Please enter a valid Date of Birth.";
                StatusLabel.CssClass = "status-label error";
                return;
            }

            string filePath = "";
            if (PhotoFileUpload.HasFile)
            {
                string fileName = Path.GetFileName(PhotoFileUpload.PostedFile.FileName);
                string uploadsFolder = Server.MapPath("~/Uploads/");
                filePath = "~/Uploads/" + fileName;
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                PhotoFileUpload.SaveAs(Server.MapPath(filePath));
            }

            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO Users (UserType, FirstName, LastName, UserId, Gender, Department, DOB, PasswordHash, CurrentYear, CurrentSemester, PhotoPath) 
                                           VALUES (@UserType, @FirstName, @LastName, @UserId, @Gender, @Department, @DOB, @PasswordHash, @CurrentYear, @CurrentSemester, @PhotoPath)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserType", UserTypeRadioButtonList.SelectedValue);
                        cmd.Parameters.AddWithValue("@FirstName", FirstNameTextBox.Text);
                        cmd.Parameters.AddWithValue("@LastName", LastNameTextBox.Text);
                        cmd.Parameters.AddWithValue("@UserId", IDTextBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", GenderDropDownList.SelectedValue);
                        cmd.Parameters.AddWithValue("@Department", DepartmentDropDownList.SelectedValue);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@PasswordHash", HashPassword(PasswordTextBox.Text));
                        cmd.Parameters.AddWithValue("@CurrentYear", UserTypeRadioButtonList.SelectedValue == "Student" ? YearDropDownList.SelectedValue : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CurrentSemester", UserTypeRadioButtonList.SelectedValue == "Student" ? SemesterDropDownList.SelectedValue : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhotoPath", string.IsNullOrEmpty(filePath) ? (object)DBNull.Value : filePath);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Auto-login and redirect to dashboard
                Session["UserId"] = IDTextBox.Text.Trim();
                Session["UserType"] = UserTypeRadioButtonList.SelectedValue;
                Response.Redirect("UserDashboard.aspx");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint error
                {
                    StatusLabel.Text = "This ID is already registered.";
                }
                else
                {
                    StatusLabel.Text = "Database error: " + ex.Message;
                }
                StatusLabel.CssClass = "status-label error";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "An error occurred: " + ex.Message;
                StatusLabel.CssClass = "status-label error";
            }
        }
    }
}
