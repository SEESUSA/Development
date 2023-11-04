<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<%@ Register Src="CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/AdminTask.ascx" TagName="AdminTasks" TagPrefix="uc1" %>
<%@ Register Src="~/Tasks.ascx" TagName="Tasks" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%=Application.Get("Title").ToString() %></title>
    <link href="styles.css" rel="stylesheet" />
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/jquery-ui-1.13.2.min.js"></script>
     <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"/>
    <script type="text/javascript">
        // When the document is ready
        $(document).ready(function () {
           
            // When the user clicks the close button or outside the modal, close it
            $(".close, .modal").click(function () {
                $("#myModal").css("display", "none");
                window.location.href = '/Logoff.aspx';
            });

            // Prevent the modal from closing when clicking inside it
            $(".modal-content").click(function (e) {
                e.stopPropagation();
            });
        });

    </script>
  <style>
      /* Style for the modal container */
.modal {
    display: none;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.7);
}

/* Style for the modal content */
.modal-content {
    background-color: #fff;
    margin: 15% auto;
    padding: 0px;
    border: 1px solid #888;
    width: 80%;
    max-width: 400px;
    text-align: center;
    position: relative;
}

/* Style for the close button */
.close {
    position: absolute;
    top: 0;
    right: 0;
    padding: 10px;
    cursor: pointer;
}

  </style>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Change Password</h2>
    </header>
    <form id="form1" runat="server">
        <%--<asp:Panel runat="server" ID="pnlAdminTasks" Visible="false">
            <label>Task
                <uc1:AdminTasks runat="server" ID="AdminTasks1" /></label>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTasks" Visible="false">
            <uc1:Tasks runat="server" ID="Tasks1" />
        </asp:Panel>--%>
        
        <div>
            <label>Current Password
                <asp:TextBox runat="server" ID="txtCPassword" CssClass="TextField" MaxLength="50" TextMode="Password" /></label>
            <p>Password Requirements</p>
            <ul>
                <li>6 characters minimum</li>
                <li>1 uppercase letter</li>
                <li>1 lowercase letter</li>
                <li>1 number</li>
                <li>1 symbol</li>
            </ul>
            <label>New Password
                <asp:TextBox runat="server" ID="txtPass1" CssClass="TextField" MaxLength="50" TextMode="Password" /></label>
            <label>Confirm Password
                <asp:TextBox runat="server" ID="txtPass2" CssClass="TextField" MaxLength="50" TextMode="Password" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnChange" CssClass="Button" Text="Change" OnClick="btnChange_Click" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
        <div id="myModal" class="modal">
    <div class="modal-content">
        <span class="close">&times;</span>
        <p id="modal-message">Password changed successfully.</p>
    </div>
    </div>
    </form>
    
</body>
</html>