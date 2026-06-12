<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="web_progress_report.AdminDashboard" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Admin Dashboard - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css?v=3" />
    <style>
        .dashboard-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-top: 24px;
        }
        .dashboard-card {
            background: var(--card);
            border: 1px solid var(--border);
            border-radius: 16px;
            padding: 24px;
            text-align: center;
            box-shadow: 0 10px 30px rgba(15, 23, 42, 0.05);
        }
        .dashboard-card h3 {
            margin-top: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap" style="max-width: 900px;">
            <div class="header">
                <h1>Admin Dashboard</h1>
                <p>Monitor system activity, manage users, and maintain the platform.</p>
                <div class="header-actions">
                    <a href="Default.aspx" class="register-btn" style="background-color: #d32f2f;">Logout</a>
                </div>
            </div>

            <asp:Panel ID="MessagePanel" runat="server" Visible="false" CssClass="card" style="background-color: #e8f5e9; border-color: #a5d6a7;">
                <asp:Label ID="StatusLabel" runat="server" style="color: #2e7d32; font-weight: 600;"></asp:Label>
            </asp:Panel>

            <div class="dashboard-grid">
                <div class="dashboard-card">
                    <h3>User Management</h3>
                    <p>Monitor users, delete fake accounts, or remove users with multiple accounts.</p>
                    <a href="ManageUsers.aspx" class="register-btn">Manage Users</a>
                </div>
                
                <div class="dashboard-card">
                    <h3>System History</h3>
                    <p>Clear old system logs, upload history, and cached user activity data.</p>
                    <asp:Button ID="ClearHistoryButton" runat="server" CssClass="register-btn" Text="Delete History" style="background-color: #d32f2f;" OnClick="ClearHistoryButton_Click" OnClientClick="return confirm('Are you sure you want to delete system history? This action cannot be undone.');" />
                </div>
            </div>
        </div>
    </form>
    <button id="theme-toggle" class="theme-toggle" aria-label="Toggle dark mode" title="Toggle dark mode">🌙</button>
    <script src="theme.js"></script>
</body>
</html>

