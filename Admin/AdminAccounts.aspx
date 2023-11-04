<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminAccounts.aspx.cs" Inherits="Admin_Accounts" %>
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
        <h2 class="PageTitle">Admin Accounts Administration</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <label>Task
                <uc1:AdminTask ID="AdminTask1" runat="server" /></label>
            <label>Account: 
                <asp:DropDownList runat="server" ID="cboAccounts" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboAccounts_SelectedIndexChanged" /></label>
            <div class="input-links">
                <asp:LinkButton runat="server" ID="lbNew" Text="New" CssClass="LinkButton" Visible="false" OnClick="lbNew_Click" /> 
                <asp:LinkButton runat="server" ID="lbDelete" Text="Delete" CssClass="LinkButton" Visible="false"  OnClick="lbDelete_Click" /> 
            </div>
        </div>
        <div>
            <asp:Panel runat="server" ID="pnlAccountDetails" Visible="false" >
                <label>Name <asp:TextBox runat="server" ID="txtName" CssClass="TextField" MaxLength="50" /></label>
                <label>Email Address <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="TextField" MaxLength="255" /></label>
                <label>Password: <asp:TextBox runat="server" ID="txtPassword" CssClass="TextField" MaxLength="50" TextMode="Password" /> 
                    <asp:LinkButton runat="server" ID="lnkReset" Text="Reset Password" OnClick="lnkReset_Click" />
                </label>
                <label>Last Logon: <asp:Label runat="server" ID="lblLastLogon" /></label>
                <label>Active: <asp:CheckBox runat="server" ID="chkActive" /></label>
                <label>IsAdmin: <asp:CheckBox runat="server" ID="chkIsAdmin" /></label>
                <label>IsSetupAdmin: <asp:CheckBox runat="server" ID="chkIsSetupAdmin" /></label>
                <br />
                <asp:Button runat="server" ID="btnSave" CssClass="Button" Text="Save" OnClick="btnSave_Click" />
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>