<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="web_progress_report.Default" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Welcome - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css?v=3" />
    <style>
        .role-cards {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            gap: 24px;
            margin-top: 32px;
        }
        .role-card {
            background: var(--card);
            border: 1px solid var(--border);
            border-radius: 24px;
            padding: 32px;
            text-align: center;
            box-shadow: 0 18px 40px rgba(15, 23, 42, 0.05);
            transition: transform 200ms ease, box-shadow 200ms ease;
            text-decoration: none;
            color: var(--text);
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        .role-card:hover {
            transform: translateY(-4px);
            box-shadow: 0 24px 48px rgba(15, 23, 42, 0.1);
        }
        .role-icon {
            font-size: 3rem;
            margin-bottom: 16px;
            background: var(--primary-soft);
            width: 80px;
            height: 80px;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
        }
        .role-card h2 {
            margin: 0 0 12px 0;
            font-size: 1.5rem;
        }
        .role-card p {
            margin: 0;
            color: var(--muted);
            line-height: 1.5;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap" style="max-width: 1000px; padding-top: 64px;">
            <div class="header" style="text-align: center; padding: 48px 32px;">
                <h1>KUET Resource Sharing Club</h1>
                <p style="margin: 0 auto;">Select your role to continue to the platform.</p>
            </div>

            <div class="role-cards">
                <a href="Home.aspx" class="role-card">
                    <div class="role-icon">&#128100;</div>
                    <h2>User</h2>
                    <p>Already a member? Log in as a student or faculty member to access resources.</p>
                </a>
                
                <a href="Register.aspx" class="role-card">
                    <div class="role-icon">&#128075;</div>
                    <h2>New Comer</h2>
                    <p>Just joined? Register for a new account to start sharing and learning.</p>
                </a>
                
                <a href="AdminLogin.aspx" class="role-card">
                    <div class="role-icon">&#128737;</div>
                    <h2>Admin</h2>
                    <p>Access the administrative dashboard to manage users and system settings.</p>
                </a>
            </div>
        </div>
    </form>
</body>
</html>

