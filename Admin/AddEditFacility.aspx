<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditFacility.aspx.cs" Inherits="Admin_AddEditFacility" %>
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
            <a href="Facilities.aspx" class="LinkButton">Facility</a> <%=API.Session.AdminEHPFacility %>
            <label>Friendly Name
                <asp:TextBox runat="server" ID="txtEHPName" CssClass="TextField" MaxLength="50" /></label>
            <label>Centricity Name
                <asp:DropDownList runat="server" ID="cboGEName" CssClass="TextField" /></label>
            <label>Enabled
                <asp:CheckBox runat="server" ID="chkEnabled" Checked="true" /></label>
            <table border="0">
                <tr>
                    <td colspan="3">Contacts</td>
                </tr>
                <tr>
                    <td>Options</td>
                    <td>Name</td>
                    <td>Address</td>
                </tr>
                <asp:Literal runat="server" ID="litContacts" />
            </table>
            
            <asp:LinkButton runat="server" ID="lbNewContact" CssClass="LinkButton" Text="New Contact" Visible="false" OnClick="lbNewContact_Click" />
            <div class="buttons">
                <asp:Button runat="server" id="btnSave" CssClass="Button" Text="Save" OnClick="btnSave_Click" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>