<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIP2.aspx.cs" Inherits="Admin_VIP" %>
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
        <div>
            <label>Task
                <uc1:AdminTask ID="AdminTask1" runat="server" /></label>

            <!-- Master Account -->
            <label>Master Account
                <asp:DropDownList runat="server" id="cboMasterAccount" cssclass="TextField" AutoPostBack="true" /></label>
            <label>Email Address
                <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="TextField" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnSend" Text="Send Credentials" CssClass="Button" Enabled="false" />
                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="Button" />
            </div>

            <!-- Convert nrml to VIP account prompt panel -->
            <asp:Panel runat="server" ID="pnlConvert" visible="false">
                <p>The specified email address already exists as a normal account.</p>
                <p>Do you want to convert to a master account?</p>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnConvYes" Text="Yes" CssClass="Button" />
                    <asp:Button runat="server" id="btnConvNo" Text="No" css="Button" />
                </div>
            </asp:Panel>

            <!-- Childen account panel -->
            <asp:Panel runat="server" ID="pnlChildren" Visible="false">
                <p>Options | Name | NPI</p>
                <asp:Literal runat="server" ID="litChildren" />
                <div class="buttons">
                    <asp:Button runat="server" ID="btnAdd" CssClass="Button" Text="Add Child" />
                </div>
            </asp:Panel>

            <!-- Child Panel -->
            <asp:Panel runat="server" ID="pnlChild" Visible="false">
                <label>Name
                    <asp:TextBox runat="server" ID="txtName" CssClass="TextField" /></label>
                <label>NPI
                    <asp:TextBox runat="server" ID="txtNPI" CssClass="TextField" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnSaveChild" CssClass="Button" Text="Save" />
                </div>
            </asp:Panel>
        </div>

        <!-- ERROR MSGS -->
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
   </form>
</body>
</html>