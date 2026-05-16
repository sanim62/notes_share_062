using System;
using System.IO;

namespace web_progress_report
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegistrationPanel.Visible = false;
            }
        }

        protected void UserTypeRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userType = UserTypeRadioButtonList.SelectedValue;
            RegistrationPanel.Visible = true;
            StudentPanel.Visible = (userType == "Student");
            IDLabel.InnerText = (userType == "Student") ? "Roll Number / University ID" : "Faculty ID";
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrEmpty(FirstNameTextBox.Text) || string.IsNullOrEmpty(LastNameTextBox.Text) ||
                string.IsNullOrEmpty(IDTextBox.Text) || string.IsNullOrEmpty(GenderDropDownList.SelectedValue) ||
                string.IsNullOrEmpty(DepartmentDropDownList.SelectedValue) || string.IsNullOrEmpty(DOBTextBox.Text))
            {
                StatusLabel.Text = "Please fill in all required fields.";
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

            // Handle photo upload
            if (PhotoFileUpload.HasFile)
            {
                string fileName = Path.GetFileName(PhotoFileUpload.PostedFile.FileName);
                string filePath = Server.MapPath("~/Uploads/") + fileName;
                Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                PhotoFileUpload.SaveAs(filePath);
            }

            // Simulate registration success
            StatusLabel.Text = "Registration successful! Welcome to KUET Resource Sharing Club.";
            StatusLabel.CssClass = "status-label success";
        }
    }
}
