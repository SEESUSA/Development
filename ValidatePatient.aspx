<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidatePatient.aspx.cs" Inherits="ValidatePatient" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Validate Patient</h2>
    </header>
    <form class="validate-patient" id="form1" runat="server">
    <p>We found a patient in our system that matches the data you submitted. Please verify this patient by entering his/her complete social security number.</p>
        <label>Social Security Number<label>
            <asp:TextBox runat="server" ID="txtSSN1" CssClass="TextField" MaxLength="3" Width="40" />
            - <asp:TextBox runat="server" ID="txtSSN2" CssClass="TextField" MaxLength="2" Width="30" />
            - <asp:TextBox runat="server" ID="txtSSN3" CssClass="TextField" MaxLength="4" Width="50" />
        <div class="buttons">
            <asp:Button runat="server" ID="btnVerify" CssClass="Button" Text="Verify" OnClick="btnVerify_Click" />
            <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>
