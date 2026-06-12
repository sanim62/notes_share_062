<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="web_progress_report.AdminLogin" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Admin Login - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css?v=3" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap" style="max-width: 600px; padding-top: 64px;">
            <div class="header">
                <h1>Admin Login</h1>
                <p>Please enter your administrative credentials to continue.</p>
            </div>

            <div class="card">
                <div class="register-form">
                    <div>
                        <label for="AdminIdTextBox">Admin ID</label>
                        <asp:TextBox ID="AdminIdTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div>
                        <label for="PasswordTextBox">Password</label>
                        <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="text-input" TextMode="Password" Required="true" />
                    </div>
                    <asp:Button ID="LoginButton" runat="server" CssClass="register-btn" Text="Login" style="width: 100%; margin-top: 12px;" OnClick="LoginButton_Click" />
                    <asp:Label ID="StatusLabel" runat="server" CssClass="status-label" />
                </div>
            </div>

            <div class="card" style="text-align: center;">
                <a class="register-btn" href="Default.aspx">Back to Welcome</a>
            </div>
        </div>
    </form>
    <button id="theme-toggle" class="theme-toggle" aria-label="Toggle dark mode" title="Toggle dark mode">🌙</button>
    <script src="theme.js"></script>
</body>
</html>

