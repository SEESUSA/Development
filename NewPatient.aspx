<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPatient.aspx.cs" Inherits="NewPatient" %>
<%@ Register Src="CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>

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
        <img src="img/header.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">New Patient</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <label>First Name
                <asp:TextBox runat="server" ID="txtFirstName" CssClass="TextField" MaxLength="35" /></label>
            <label>Last Name
                <asp:TextBox runat="server" ID="txtLastName" CssClass="TextField" MaxLength="60" /></label>
            <label>Gender
                <asp:DropDownList runat="server" ID="cboGender" CssClass="TextField" /></label>
            <label>Date of Birth
                <asp:TextBox runat="server" ID="txtDOB" CssClass="TextField" /></label>
            <label>Phone
                <asp:TextBox runat="server" ID="txtPhone" CssClass="TextField" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnSave" CssClass="Button" Text="Save" OnClick="btnSave_Click" />
                <asp:Button runat="server" id="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>