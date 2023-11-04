<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddChild.aspx.cs" Inherits="Admin_AddChild" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/CheckAdmin.ascx" TagName="CheckAdmin" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/AdminTask.ascx" TagName="AdminTask" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="~/styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <uc1:CheckAdmin id="CheckAdmin1" runat="server" />
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="../img/header.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">VIP Administration</h2>
    </header>
    <form id="form1" runat="server">
        <label>Task
            <uc1:AdminTask ID="AdminTask1" runat="server" /></label>
        <p>Child Account Information</p>
        <div>
            <label>Name
                <asp:TextBox runat="server" ID="txtName" CssClass="TextField" MaxLength="50" /></label>
            <label>NPI
                <asp:TextBox runat="server" ID="txtNPI" CssClass="TextField" MaxLength="80" /></label>
            <label>Email Address
                <asp:TextBox runat="server" id="txtEmailAddress" CssClass="TextField" MaxLength="255" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnSave" CssClass="Button" Text="Save" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
   </form>
</body>
</html>