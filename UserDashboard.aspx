<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="web_progress_report.UserDashboard" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>User Dashboard - KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css?v=4" />
    <style>
        .dashboard-grid {
            display: grid;
            grid-template-columns: 1fr;
            gap: 24px;
        }
        @media (min-width: 900px) {
            .dashboard-grid {
                grid-template-columns: 1fr 1fr;
            }
            .full-width {
                grid-column: 1 / -1;
            }
        }
        .form-group {
            margin-bottom: 16px;
        }
        .form-group label {
            display: block;
            margin-bottom: 8px;
            font-weight: 600;
        }
        .text-input, .file-input {
            width: 100%;
            padding: 10px 14px;
            border: 1px solid var(--border);
            border-radius: 12px;
            background: var(--card);
            color: var(--text);
            box-sizing: border-box;
        }
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 16px;
        }
        .grid-view th, .grid-view td {
            border: 1px solid var(--border);
            padding: 10px;
            text-align: left;
        }
        .grid-view th {
            background-color: var(--border);
            color: var(--text);
        }
        .action-link {
            color: var(--primary);
            text-decoration: none;
            font-weight: 600;
        }
        .action-link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap" style="max-width: 1200px;">
            <div class="header" style="display: flex; justify-content: space-between; align-items: center; padding: 32px;">
                <div>
                    <h1>Welcome, <asp:Label ID="UserNameLabel" runat="server"></asp:Label></h1>
                    <p>Manage your resources, uploads, and downloads from here.</p>
                </div>
                <div>
                    <asp:Button ID="LogoutButton" runat="server" CssClass="register-btn" Text="Log Out" OnClick="LogoutButton_Click" formnovalidate="formnovalidate" />
                </div>
            </div>

            <div class="dashboard-grid">
                <!-- Search Resources Section -->
                <div class="card full-width">
                    <div class="section-title">
                        <h2>Search Resources</h2>
                    </div>
                    <div class="search-form">
                        <div>
                            <label for="SearchDeptTextBox">Department</label>
                            <asp:TextBox ID="SearchDeptTextBox" runat="server" CssClass="text-input" placeholder="e.g. CSE" />
                        </div>
                        <div>
                            <label for="SearchCourseTextBox">Course</label>
                            <asp:TextBox ID="SearchCourseTextBox" runat="server" CssClass="text-input" placeholder="e.g. Data Structures" />
                        </div>
                        <div>
                            <label for="SearchTopicTextBox">Topic</label>
                            <asp:TextBox ID="SearchTopicTextBox" runat="server" CssClass="text-input" placeholder="e.g. BFS" />
                        </div>
                        <div style="display: flex; align-items: flex-end;">
                            <asp:Button ID="SearchButton" runat="server" CssClass="register-btn" Text="Search" OnClick="SearchButton_Click" formnovalidate="formnovalidate" />
                        </div>
                    </div>
                    <div style="margin-top: 20px;">
                        <asp:Label ID="SearchStatusLabel" runat="server" CssClass="status-label"></asp:Label>
                        <asp:GridView ID="SearchResultsGrid" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowCommand="SearchResultsGrid_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                <asp:BoundField DataField="Department" HeaderText="Department" />
                                <asp:BoundField DataField="Course" HeaderText="Course" />
                                <asp:BoundField DataField="Topic" HeaderText="Topic" />
                                <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" DataFormatString="{0:MMM dd, yyyy}" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DownloadBtn" runat="server" CommandName="DownloadFile" CommandArgument='<%# Eval("Id") + "|" + Eval("FilePath") %>' CssClass="action-link">Download</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <p>No resources found matching your search.</p>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Upload Section -->
                <div class="card">
                    <div class="section-title">
                        <h2>Upload Resource</h2>
                    </div>
                    <asp:Label ID="UploadStatusLabel" runat="server" CssClass="status-label"></asp:Label>
                    <div class="form-group">
                        <label>Department</label>
                        <asp:TextBox ID="UploadDeptTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div class="form-group">
                        <label>Course</label>
                        <asp:TextBox ID="UploadCourseTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div class="form-group">
                        <label>Topic</label>
                        <asp:TextBox ID="UploadTopicTextBox" runat="server" CssClass="text-input" Required="true" />
                    </div>
                    <div class="form-group">
                        <label>File</label>
                        <asp:FileUpload ID="ResourceFileUpload" runat="server" CssClass="file-input" Required="true" />
                    </div>
                    <asp:Button ID="UploadButton" runat="server" CssClass="register-btn" Text="Upload" OnClick="UploadButton_Click" />
                </div>

                <!-- My Uploads & Downloads -->
                <div style="display:flex; flex-direction:column; gap:24px;">
                    <div class="card" style="flex:1;">
                        <div class="section-title">
                            <h2>My Uploads</h2>
                        </div>
                        <asp:GridView ID="MyUploadsGrid" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowCommand="MyUploadsGrid_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                <asp:BoundField DataField="UploadDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DelBtn" runat="server" CommandName="DeleteFile" CommandArgument='<%# Eval("Id") + "|" + Eval("FilePath") %>' CssClass="action-link" style="color:#ef4444;">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <p>You haven't uploaded any resources yet.</p>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    
                    <div class="card" style="flex:1;">
                        <div class="section-title">
                            <h2>My Downloads</h2>
                        </div>
                        <asp:GridView ID="MyDownloadsGrid" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowCommand="MyDownloadsGrid_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                <asp:BoundField DataField="DownloadDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ReDownloadBtn" runat="server" CommandName="DownloadFile" CommandArgument='<%# Eval("ResourceId") + "|" + Eval("FilePath") %>' CssClass="action-link">Re-download</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <p>You haven't downloaded any resources yet.</p>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>
    </form>
    <button id="theme-toggle" class="theme-toggle" aria-label="Toggle dark mode" title="Toggle dark mode">🌙</button>
    <script src="theme.js"></script>
</body>
</html>
