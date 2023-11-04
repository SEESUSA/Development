<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application["Title"].ToString() %></title>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Logon</h2>
    </header>
    <form id="form2" runat="server">
        <div>
            <label>Email Address
                <asp:TextBox runat="server" ID="txtEmail" MaxLength="255" CssClass="TextField" /></label>
            <label>Password
                <asp:TextBox runat="server" ID="txtPassword"  MaxLength="50" textmode="Password" CssClass="TextField" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnLogon" Text="Logon" CssClass="Button" OnClick="btnLogon_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="Button" onclick="btnCancel_Click" />
            </div>
            <div class="link-buttons">
               <%-- <asp:LinkButton runat="server" ID="lbSignUp" Text="Sign Up" CssClass="LinkButton" OnClick="lbSignUp_Click" /> 
                | --%>
                <asp:LinkButton runat="server" ID="lbForgot" Text="Forgot Password" CssClass="LinkButton" OnClick="lbForgot_Click" />
                <%--| <asp:LinkButton runat="server" ID="lbResend" Text="Resend Validation" CssClass="LinkButton" OnClick="lbResend_Click" />--%>
            </div>
            <br />
            <div style="text-align:center;font-size:14px;">
                If you are having problem logging-in, please email us at RPPSupport@theseesgroup.com
            </div>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
        <div class="buttons">
                <%--<asp:Button runat="server" ID="btnCareCloudLogon" Text="Logon With Care Cloud" CssClass="Button" OnClick="btnCareCloudLogon_Click" />--%>
                
            </div>
    </form>
</body>
</html>