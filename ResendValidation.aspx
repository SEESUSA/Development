<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResendValidation.aspx.cs" Inherits="ResendValidation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <title><%=Application.Get("Title") %></title>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Confirmation</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <asp:Panel runat="server" ID="pnlValidation" Visible="false">
                <label>Email Address
                    <asp:TextBox runat="server" ID="txtEmailAddress" MaxLength="255" CssClass="TextField" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnSend" CssClass="Button" Text="Send" OnClick="btnSend_Click" />
                    <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlValidateResent" Visible="false">
                The validation email has been re-sent. Please check your email.<br><br>
                This window can be closed now.
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlPwdReset" Visible="false">
                A password reset email has been sent to your email address on file. Please check your email and follow further instructions.<br><br>
                This window can be closed now.
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>
