<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="web_progress_report.Default" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>KUET Resource Sharing Club</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap">
            <div class="header">
                <h1>Welcome to KUET Resource Sharing Club</h1>
                <p>KUET Resource Sharing Club is a dedicated platform for students to collaborate and share valuable academic resources. We provide hand notes, lecture notes, and research resources to help students succeed in their studies.</p>
                <asp:Button ID="RegisterButton" runat="server" CssClass="register-btn" Text="Register" OnClick="RegisterButton_Click" />
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
                    <h2>Search Notes</h2>
                </div>
                <div class="search-form">
                    <div>
                        <label for="department">Department</label>
                        <input type="text" id="department" name="department" placeholder="e.g. CSE" />
                    </div>
                    <div>
                        <label for="course">Course</label>
                        <input type="text" id="course" name="course" placeholder="e.g. Data Structures" />
                    </div>
                    <div>
                        <label for="chapter">Chapter</label>
                        <input type="text" id="chapter" name="chapter" placeholder="e.g. Graph Theory" />
                    </div>
                    <div>
                        <label for="topic">Topic</label>
                        <input type="text" id="topic" name="topic" placeholder="e.g. BFS / DFS" />
                    </div>
                    <button type="button">Search</button>
                </div>
            </div>

            <div class="recent-files-section">
                <div class="section-title">
                    <h2>Recent Files</h2>
                </div>
                <div class="recent-files-card">
                    <ul>
                        <li>
                            <strong>Lecture Notes: Operating Systems</strong>
                            <span>Uploaded 2 hours ago • CSE</span>
                        </li>
                        <li>
                            <strong>Handwritten Notes: Linear Algebra</strong>
                            <span>Uploaded yesterday • EEE</span>
                        </li>
                        <li>
                            <strong>Project Guide: Web Development</strong>
                            <span>Uploaded 3 days ago • CSE</span>
                        </li>
                        <li>
                            <strong>Research Paper Summary: AI Ethics</strong>
                            <span>Uploaded 4 days ago • BBA</span>
                        </li>
                        <li>
                            <strong>Sample Questions: Mathematics</strong>
                            <span>Uploaded 1 week ago • CSE</span>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
