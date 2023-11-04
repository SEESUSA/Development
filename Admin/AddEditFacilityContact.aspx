<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditFacilityContact.aspx.cs" Inherits="Admin_AddEditFacilityContact" %>
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
    <uc1:CheckAdmin ID="CheckAdmin1" runat="server" />
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="../img/header.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Administration</h2>
    </header>
    <form id="form2" runat="server">
        <div>
            <label>Task
                <uc1:AdminTask ID="AdminTask1" runat="server" /></label>
            <label>Name
                <asp:TextBox runat="server" ID="txtName" CssClass="TextField" maxlenth="100" width="200" /></label>
            <label>Email Address
                <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="TextField" MaxLength="255" Width="250" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnSave" CssClass="Button" Text="Save" OnClick="btnSave_Click" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>