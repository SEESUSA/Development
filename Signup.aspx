<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Signup" %>

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
        <h2 class="PageTitle">Sign Up</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <asp:Panel runat="server" ID="pnlSignup" Visible="true">
                <label>First Name
                    <asp:TextBox runat="server" ID="txtFirstName" MaxLength="50" CssClass="TextField" /></label>
                <label>Last Name
                    <asp:TextBox runat="server" ID="txtLastName" MaxLength="50" CssClass="TextField" /></label>
                <label>Office Phone
                    <asp:TextBox runat="server" ID="txtPhoneNumber" MaxLength="15" CssClass="TextField" /></label>
                <label>Zip Code
                    <asp:TextBox runat="server" ID="txtZipCode" MaxLength="10" CssClass="TextField" /></label>
                <label>NPI #
                    <asp:TextBox runat="server" ID="txtNPI" MaxLength="80" CssClass="TextField" /></label>
                <label>Email Address
                    <asp:TextBox runat="server" id="txtEmailAddress" MaxLength="255" CssClass="TextField" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnSignup" Text="Sign Up" CssClass="Button" OnClick="btnSignup_Click" />
                    <asp:button runat="server" ID="btnCancel" Text="Cancel" CssClass="Button" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlComplete" Visible="false">
                You have signed up for the <%=Application.Get("PageTitle") %> web site. Before you can use it, you will need to validate
                your email account. Please check your email and follow the instructions in it to get started.
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>