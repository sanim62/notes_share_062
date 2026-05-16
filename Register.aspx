<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="web_progress_report.Register" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registration Form - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap">
            <div class="header">
                <h1>Registration Form</h1>
                <p>Please fill in your details to register for KUET Resource Sharing Club.</p>
            </div>

            <div class="card">
                <div class="section-title">
                    <h2>Are you a student or faculty member?</h2>
                </div>
                <div class="user-type">
                    <asp:RadioButtonList ID="UserTypeRadioButtonList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UserTypeRadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                        <asp:ListItem Text="Faculty" Value="Faculty"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <asp:Panel ID="RegistrationPanel" runat="server" Visible="false" CssClass="card">
                <div class="section-title">
                    <h2>Registration Details</h2>
                </div>
                <div class="register-form">
                    <div>
                        <label for="FirstNameTextBox">First Name</label>
                        <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div>
                        <label for="LastNameTextBox">Last Name</label>
                        <asp:TextBox ID="LastNameTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div>
                        <label for="IDTextBox" id="IDLabel" runat="server">Roll Number / University ID</label>
                        <asp:TextBox ID="IDTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div>
                        <label for="GenderDropDownList">Gender</label>
                        <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="text-input">
                            <asp:ListItem Text="Select Gender" Value="" />
                            <asp:ListItem Text="Male" Value="Male" />
                            <asp:ListItem Text="Female" Value="Female" />
                            <asp:ListItem Text="Other" Value="Other" />
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label for="DepartmentDropDownList">Department</label>
                        <asp:DropDownList ID="DepartmentDropDownList" runat="server" CssClass="text-input">
                            <asp:ListItem Text="Select Department" Value="" />
                            <asp:ListItem Text="CSE" Value="CSE" />
                            <asp:ListItem Text="EEE" Value="EEE" />
                            <asp:ListItem Text="BBA" Value="BBA" />
                            <asp:ListItem Text="Other" Value="Other" />
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label for="DOBTextBox">Date of Birth</label>
                        <asp:TextBox ID="DOBTextBox" runat="server" CssClass="text-input" TextMode="Date" Required="true" />
                    </div>
                    <asp:Panel ID="StudentPanel" runat="server" Visible="false">
                        <div>
                            <label for="YearDropDownList">Current Year</label>
                            <asp:DropDownList ID="YearDropDownList" runat="server" CssClass="text-input">
                                <asp:ListItem Text="Select Year" Value="" />
                                <asp:ListItem Text="1st Year" Value="1" />
                                <asp:ListItem Text="2nd Year" Value="2" />
                                <asp:ListItem Text="3rd Year" Value="3" />
                                <asp:ListItem Text="4th Year" Value="4" />
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label for="SemesterDropDownList">Current Semester</label>
                            <asp:DropDownList ID="SemesterDropDownList" runat="server" CssClass="text-input">
                                <asp:ListItem Text="Select Semester" Value="" />
                                <asp:ListItem Text="1st Semester" Value="1" />
                                <asp:ListItem Text="2nd Semester" Value="2" />
                            </asp:DropDownList>
                        </div>
                    </asp:Panel>
                    <div>
                        <label for="PhotoFileUpload">Upload Photo</label>
                        <asp:FileUpload ID="PhotoFileUpload" runat="server" CssClass="text-input" />
                    </div>
                    <asp:Button ID="SubmitButton" runat="server" CssClass="register-btn" Text="Submit Registration" OnClick="SubmitButton_Click" />
                    <asp:Label ID="StatusLabel" runat="server" CssClass="status-label" />
                </div>
            </asp:Panel>

            <div class="card">
                <a class="register-btn" href="Default.aspx">Back to Home</a>
            </div>
        </div>
    </form>
</body>
</html>
