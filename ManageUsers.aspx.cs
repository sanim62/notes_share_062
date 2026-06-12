using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace web_progress_report
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, UserId, FirstName, LastName, UserType, Department FROM Users", conn))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        UsersGridView.DataSource = dt;
                        UsersGridView.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error loading users: " + ex.Message;
                StatusLabel.CssClass = "status-label error";
            }
        }

        protected void UsersGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UsersGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void UsersGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UsersGridView.EditIndex = -1;
            BindGrid();
        }

        protected void UsersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(UsersGridView.DataKeys[e.RowIndex].Value);
                GridViewRow row = UsersGridView.Rows[e.RowIndex];

                string firstName = (row.Cells[2].Controls[0] as TextBox).Text;
                string lastName = (row.Cells[3].Controls[0] as TextBox).Text;
                string userType = (row.Cells[4].Controls[0] as TextBox).Text;
                string department = (row.Cells[5].Controls[0] as TextBox).Text;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, UserType=@UserType, Department=@Department WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@UserType", userType);
                        cmd.Parameters.AddWithValue("@Department", department);
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                UsersGridView.EditIndex = -1;
                BindGrid();
                StatusLabel.Text = "User updated successfully.";
                StatusLabel.CssClass = "status-label success";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error updating user: " + ex.Message;
                StatusLabel.CssClass = "status-label error";
            }
        }

        protected void UsersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(UsersGridView.DataKeys[e.RowIndex].Value);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Users WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                BindGrid();
                StatusLabel.Text = "User deleted successfully.";
                StatusLabel.CssClass = "status-label success";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error deleting user: " + ex.Message;
                StatusLabel.CssClass = "status-label error";
            }
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = UserIdTextBox.Text.Trim();
                string firstName = FirstNameTextBox.Text.Trim();
                string lastName = LastNameTextBox.Text.Trim();
                string userType = UserTypeTextBox.Text.Trim();
                string department = DepartmentTextBox.Text.Trim();

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(userType))
                {
                    StatusLabel.Text = "Please fill in all required fields for the new user.";
                    StatusLabel.CssClass = "status-label error";
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO Users (UserId, FirstName, LastName, UserType, Department) 
                                     VALUES (@UserId, @FirstName, @LastName, @UserType, @Department)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@UserType", userType);
                        cmd.Parameters.AddWithValue("@Department", string.IsNullOrEmpty(department) ? (object)DBNull.Value : department);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                UserIdTextBox.Text = string.Empty;
                FirstNameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                UserTypeTextBox.Text = string.Empty;
                DepartmentTextBox.Text = string.Empty;

                BindGrid();
                StatusLabel.Text = "User added successfully.";
                StatusLabel.CssClass = "status-label success";
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    StatusLabel.Text = "A user with this User ID already exists.";
                }
                else
                {
                    StatusLabel.Text = "Database error: " + sqlEx.Message;
                }
                StatusLabel.CssClass = "status-label error";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Error adding user: " + ex.Message;
                StatusLabel.CssClass = "status-label error";
            }
        }
    }
}
