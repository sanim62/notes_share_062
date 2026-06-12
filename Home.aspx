<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="web_progress_report.Home" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css?v=3" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap">
            <div class="header">
                <h1>Welcome to KUET Resource Sharing Club</h1>
                <p>KUET Resource Sharing Club is a dedicated platform for students to collaborate and share valuable academic resources. We provide hand notes, lecture notes, and research resources to help students succeed in their studies.</p>
                <div class="button-group" style="margin-top: 24px;">
                    <a href="UserLogin.aspx" class="register-btn secondary-btn">Student Login</a>
                    <a href="FacultyLogin.aspx" class="register-btn secondary-btn">Faculty Login</a>
                    <asp:Button ID="RegisterButton" runat="server" CssClass="register-btn" Text="Register" OnClick="RegisterButton_Click" />
                </div>
            </div>

            <div class="card">
                <div class="section-title">
                    <h2>Actions</h2>
                </div>
                <div class="action-buttons">
                    <button type="button">View Notes</button>
                    <button type="button">Upload File</button>
                </div>
            </div>

            <div class="card">
                <div class="section-title">
                    <h2>Resources Hub</h2>
                </div>
                <p style="margin-bottom: 16px;">Please log in as a Student or Faculty member to search, upload, and download academic resources.</p>
            </div>
        </div>
    </form>
    <button id="theme-toggle" class="theme-toggle" aria-label="Toggle dark mode" title="Toggle dark mode">🌙</button>
    <script src="theme.js"></script>
</body>
</html>

