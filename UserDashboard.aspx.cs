using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_progress_report
{
    public partial class UserDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                UserNameLabel.Text = Session["UserId"].ToString();
                BindMyUploads();
                BindMyDownloads();
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (!ResourceFileUpload.HasFile)
            {
                UploadStatusLabel.Text = "Please select a file to upload.";
                UploadStatusLabel.CssClass = "status-label error";
                return;
            }

            try
            {
                string fileName = Path.GetFileName(ResourceFileUpload.PostedFile.FileName);
                string uploadsFolder = Server.MapPath("~/Resources/");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                
                string uniqueFileName = Guid.NewGuid().ToString().Substring(0,8) + "_" + fileName;
                string filePath = "~/Resources/" + uniqueFileName;
                
                ResourceFileUpload.SaveAs(Server.MapPath(filePath));

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"INSERT INTO Resources (UserId, FileName, FilePath, Department, Course, Chapter, Topic) 
                                     VALUES (@UserId, @FileName, @FilePath, @Department, @Course, @Chapter, @Topic)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@Department", UploadDeptTextBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Course", UploadCourseTextBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Chapter", ""); // Optional
                        cmd.Parameters.AddWithValue("@Topic", UploadTopicTextBox.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                UploadStatusLabel.Text = "File uploaded successfully!";
                UploadStatusLabel.CssClass = "status-label success";
                
                UploadDeptTextBox.Text = "";
                UploadCourseTextBox.Text = "";
                UploadTopicTextBox.Text = "";
                
                BindMyUploads();
            }
            catch (Exception ex)
            {
                UploadStatusLabel.Text = "Error uploading file: " + ex.Message;
                UploadStatusLabel.CssClass = "status-label error";
            }
        }

        private void BindMyUploads()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Id, FileName, FilePath, UploadDate FROM Resources WHERE UserId = @UserId ORDER BY UploadDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        MyUploadsGrid.DataSource = dt;
                        MyUploadsGrid.DataBind();
                    }
                }
            }
        }

        private void BindMyDownloads()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT d.ResourceId, r.FileName, r.FilePath, MAX(d.DownloadDate) as DownloadDate 
                                 FROM Downloads d
                                 JOIN Resources r ON d.ResourceId = r.Id
                                 WHERE d.UserId = @UserId
                                 GROUP BY d.ResourceId, r.FileName, r.FilePath
                                 ORDER BY DownloadDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        MyDownloadsGrid.DataSource = dt;
                        MyDownloadsGrid.DataBind();
                    }
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string dept = SearchDeptTextBox.Text.Trim();
            string course = SearchCourseTextBox.Text.Trim();
            string topic = SearchTopicTextBox.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Id, FileName, FilePath, Department, Course, Topic, UploadDate FROM Resources WHERE 1=1";
                if (!string.IsNullOrEmpty(dept)) query += " AND Department LIKE @Dept";
                if (!string.IsNullOrEmpty(course)) query += " AND Course LIKE @Course";
                if (!string.IsNullOrEmpty(topic)) query += " AND Topic LIKE @Topic";
                query += " ORDER BY UploadDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(dept)) cmd.Parameters.AddWithValue("@Dept", "%" + dept + "%");
                    if (!string.IsNullOrEmpty(course)) cmd.Parameters.AddWithValue("@Course", "%" + course + "%");
                    if (!string.IsNullOrEmpty(topic)) cmd.Parameters.AddWithValue("@Topic", "%" + topic + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        SearchResultsGrid.DataSource = dt;
                        SearchResultsGrid.DataBind();
                    }
                }
            }
        }

        private void HandleFileDownload(string commandArgument)
        {
            string[] args = commandArgument.Split('|');
            if (args.Length == 2)
            {
                int resourceId = int.Parse(args[0]);
                string filePath = args[1];

                // Track download
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO Downloads (UserId, ResourceId) VALUES (@UserId, @ResourceId)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@ResourceId", resourceId);
                        cmd.ExecuteNonQuery();
                    }
                }

                BindMyDownloads();

                // Initiate download
                string fullPath = Server.MapPath(filePath);
                if (File.Exists(fullPath))
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(fullPath).Split('_')[1]);
                    Response.TransmitFile(fullPath);
                    Response.End();
                }
                else
                {
                    SearchStatusLabel.Text = "File not found on server.";
                    SearchStatusLabel.CssClass = "status-label error";
                }
            }
        }

        protected void SearchResultsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownloadFile")
            {
                HandleFileDownload(e.CommandArgument.ToString());
            }
        }

        protected void MyDownloadsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownloadFile")
            {
                HandleFileDownload(e.CommandArgument.ToString());
            }
        }

        protected void MyUploadsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFile")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                if (args.Length == 2)
                {
                    int resourceId = int.Parse(args[0]);
                    string filePath = args[1];

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        // Delete related downloads first
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Downloads WHERE ResourceId = @ResourceId", conn))
                        {
                            cmd.Parameters.AddWithValue("@ResourceId", resourceId);
                            cmd.ExecuteNonQuery();
                        }
                        
                        // Delete resource
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Resources WHERE Id = @Id AND UserId = @UserId", conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", resourceId);
                            cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                            int rows = cmd.ExecuteNonQuery();
                            
                            if (rows > 0)
                            {
                                string fullPath = Server.MapPath(filePath);
                                if (File.Exists(fullPath))
                                {
                                    File.Delete(fullPath);
                                }
                            }
                        }
                    }
                    BindMyUploads();
                }
            }
        }
    }
}
