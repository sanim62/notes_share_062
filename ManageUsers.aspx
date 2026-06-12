<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="web_progress_report.ManageUsers" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Manage Users - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css?v=3" />
    <style>
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .grid-view th, .grid-view td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .grid-view th {
            background-color: var(--border);
            color: var(--text);
        }
        .grid-view tr:nth-child(even) {
            background-color: transparent;
        }
        .grid-view tr:hover {
            background-color: var(--border);
        }
        .action-btn {
            padding: 5px 10px;
            text-decoration: none;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }
        .action-btn.delete {
            background-color: #f44336;
        }
        .form-table {
            width: 100%;
            max-width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        .form-table td {
            padding: 6px 8px;
        }
        .form-table input[type="text"],
        .form-table select {
            width: 100%;
            padding: 6px 8px;
            box-sizing: border-box;
            border: 1px solid #ccc;
            border-radius: 3px;
        }
        .form-title {
            margin-bottom: 12px;
            color: var(--text);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap" style="max-width: 1000px;">
            <div class="header">
                <h1>Manage Users</h1>
                <p>View, update, or delete registered users.</p>
            </div>

            <div class="card">
                <asp:Label ID="StatusLabel" runat="server" CssClass="status-label" />
                
                <div class="form-title">
                    <h2>Add New User</h2>
                    <p>Enter details for a new user and click Add User.</p>
                </div>
                <table class="form-table">
                    <tr>
                        <td><label for="UserIdTextBox">User ID</label></td>
                        <td><asp:TextBox ID="UserIdTextBox" runat="server" /></td>
                        <td><label for="FirstNameTextBox">First Name</label></td>
                        <td><asp:TextBox ID="FirstNameTextBox" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><label for="LastNameTextBox">Last Name</label></td>
                        <td><asp:TextBox ID="LastNameTextBox" runat="server" /></td>
                        <td><label for="UserTypeTextBox">Role</label></td>
                        <td><asp:TextBox ID="UserTypeTextBox" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><label for="DepartmentTextBox">Department</label></td>
                        <td colspan="3"><asp:TextBox ID="DepartmentTextBox" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:right; padding-top:12px;">
                            <asp:Button ID="AddUserButton" runat="server" Text="Add User" CssClass="action-btn" OnClick="AddUserButton_Click" />
                        </td>
                    </tr>
                </table>
                
                <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False" CssClass="grid-view" DataKeyNames="Id" 
                    OnRowEditing="UsersGridView_RowEditing" OnRowCancelingEdit="UsersGridView_RowCancelingEdit" 
                    OnRowUpdating="UsersGridView_RowUpdating" OnRowDeleting="UsersGridView_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="UserId" HeaderText="User ID" ReadOnly="True" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="UserType" HeaderText="Role" />
                        <asp:BoundField DataField="Department" HeaderText="Department" />
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ControlStyle-CssClass="action-btn" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="card">
                <a class="register-btn" href="AdminDashboard.aspx">Back to Dashboard</a>
            </div>
        </div>
    </form>
    <button id="theme-toggle" class="theme-toggle" aria-label="Toggle dark mode" title="Toggle dark mode">🌙</button>
    <script src="theme.js"></script>
</body>
</html>
