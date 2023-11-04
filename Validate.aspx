<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Validate.aspx.cs" Inherits="Validate" %>

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
        <h2 class="PageTitle">Email Validation</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <asp:Panel runat="server" id="pnlValidated" Visible="false">
                <p>Your email address has been validated. Now, please create your new password.</p>
                <p>Password Requirements</p>
                <ul>
                    <li>6 characters minimum</li>
                    <li>1 uppercase letter</li>
                    <li>1 lowercase letter</li>
                    <li>1 symbol</li>
                    <li>1 number</li>
                </ul>
                <label>Password
                    <asp:TextBox runat="server" ID="txtPassword1" CssClass="TextField" MaxLength="50" TextMode="Password" /></label>
                <label>Confirm Password
                    <asp:TextBox runat="server" ID="txtPassword2" CssClass="TextField" MaxLength="50" TextMode="Password" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnChange" Text="Change Password" CssClass="Button" OnClick="btnChange_Click" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlInvalid" Visible="false" >
                <p>We are sorry but unfortunately, the information you provided could not be validated.</p>
                <p>You can also try the "Signup" or "Resend Validation Email" links to try your registration again.</p>
                <div class="link-buttons">
                    <asp:LinkButton runat="server" ID="lbSignup" Text="Signup" CssClass="LinkButton" OnClick="lbSignup_Click" />
                    <asp:LinkButton runat="server" ID="lbResend" Text="Resend Validation Email" CssClass="LinkButton" OnClick="lbResend_Click" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlComplete" runat="server" Visible="false">
                <p>Your account has now been validated and you have set your password.</p>
                <p>You can now logon to the portal and begin using it.</p>
                <div class="link-buttons">
                    <asp:LinkButton runat="server" ID="lbLogon" CssClass="LinkButton" Text="Logon" OnClick="lbLogon_Click" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAlreadyValidated" Visible="false">
                <p>Your account has already been validated. Please logon.</p>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnLogon" CssClass="Button" Text="Logon" OnClick="btnLogon_Click" />
                </div>
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>
