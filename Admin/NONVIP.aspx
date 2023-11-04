<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NONVIP.aspx.cs" Inherits="Admin_NONVIP" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/CheckAdmin.ascx" TagName="CheckAdmin" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/AdminTask.ascx" TagName="AdminTask" TagPrefix="uc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="viewpoint" content="width=device-width, initial-scale=1.0" />
    <link href="~/styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <uc1:CheckAdmin ID="CheckAdmin1" runat="server" />
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="../img/header.png" alt="eye health partners/vision america portal" width="397" height="44" />
        <h2 class="PageTitle">NON-VIP Administration</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <label>Task
                <uc1:AdminTask ID="AdminTask1" runat="server" /></label>
            <label>Non-VIP Account
                <asp:DropDownList runat="server" ID="cboNonVIPAccount" CssClass="TextField" AutoPostBack="true" /></label>
        
            <!-- Edit Panel -->
            <asp:Panel runat="server" ID="pnlEdit" Visible="false">
                <label>UserID: <asp:Label runat="server" ID="lblUserID" /></label>
                <label>NPI Number: <asp:TextBox runat="server" ID="txtNPINumber" CssClass="TextField" /></label>
                <label>Email Address: <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="TextField" /></label>
                <label>Password: <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="TextField" /> (Please leave blank to keep the current password) </label>
                <label>Last Logon: <asp:Label runat="server" ID="lblLastLogon" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="Button" />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="Button" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlConfirmDelete" Visible="false">
                <label>Are you sure you want to delete this NON-VIP Account?</label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnYes" Text="Yes" CssClass="Button" />
                    <asp:Button runat="server" ID="btnNo" Text="No" CssClass="Button" />
                </div>
            </asp:Panel>
        </div>
        <!-- Error Messages -->
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>