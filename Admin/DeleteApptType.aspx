<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteApptType.aspx.cs" Inherits="Admin_DeleteApptType" %>
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
    <form id="form1" runat="server">
        <div>
            <label>Task
                <uc1:AdminTask ID="AdminTask1" runat="server" /></label>
            <p>Are you sure you want to delete the EHP Appt Type (<%=API.Session.AdminEHPApptType %>)?</p>
            <div class="buttons">
                <asp:Button runat="server" ID="btnYes" Text="Yes" CssClass="Button" OnClick="btnYes_Click" />
                <asp:Button runat="server" ID="btnNo" Text="No" CssClass="Button" OnClick="btnNo_Click" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>