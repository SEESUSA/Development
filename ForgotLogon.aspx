<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotLogon.aspx.cs" Inherits="ForgotLogon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title") %></title>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Forgot Password</h2>
    </header>
    <form id="form1" runat="server">
        <div><span>Enter your email address to receive password reset link.</span></div>
        <div>
            
            <label>Email Address
                <asp:TextBox runat="server" ID="txtEmailAddress" MaxLength="255" CssClass="TextField" /></label>
            <div class="buttons">
                <asp:button runat="server" ID="btnRetrieve" CssClass="Button" Text="Send Email" OnClick="btnRetrieve_Click" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>