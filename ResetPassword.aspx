<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Reset Password</h2>
    </header>
    <form id="form1" runat="server">
    
        <div>
            <asp:Panel runat="server" ID="pnlStart" Visible="true">
                <p>Please provide a new password below.</p>
                <p>Password Requirements</p>
                <ul>
                    <li>6 characters minimum</li>
                    <li>1 uppercase letter</li>
                    <li>1 lowercase letter</li>
                    <li>1 number</li>
                    <li>1 symbol</li>
                </ul>
                <label>Password
                    <asp:TextBox runat="server" ID="txtPassword1" MaxLength="50" CssClass="TextField" TextMode="Password" /></label>
                <label>Confirm Password
                    <asp:TextBox runat="server" ID="txtPassword2" MaxLength="50" CssClass="TextField" TextMode="Password" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnReset" CssClass="Button" Text="Reset" OnClick="btnReset_Click" />
                    <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
           
        </div>
        <div>
             <asp:Panel runat="server" ID="pnlComplete" Visible="false">
                <p>Your password has successfully been reset.  Please logon with your new password.</p>
                <asp:Button runat="server" ID="btnLogon" Text="Logon" CssClass="Button" OnClick="btnLogon_Click" />
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>
