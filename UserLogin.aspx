<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="web_progress_report.UserLogin" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Student Login - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap">
            <div class="header">
                <h1>Student Login</h1>
                <p>Welcome back! Please enter your credentials to login.</p>
            </div>

            <div class="card">
                <div class="section-title">
                    <h2>Login Details</h2>
                </div>
                <div class="register-form">
                    <div>
                        <label for="IDTextBox">Roll Number</label>
                        <asp:TextBox ID="IDTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div>
                        <label for="PasswordTextBox">Password</label>
                        <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="text-input" TextMode="Password" Required="true" />
                    </div>
                    <asp:Button ID="LoginButton" runat="server" CssClass="register-btn" style="position: relative; top: auto; right: auto; margin-top: 16px;" Text="Login" OnClick="LoginButton_Click" />
                    <asp:Label ID="StatusLabel" runat="server" CssClass="status-label" />
                </div>
            </div>

            <div class="card">
                <a class="register-btn" style="position: relative; top: auto; right: auto;" href="Default.aspx">Back to Home</a>
            </div>
        </div>
    </form>
</body>
</html>
