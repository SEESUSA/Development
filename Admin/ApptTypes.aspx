<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApptTypes.aspx.cs" Inherits="Admin_ApptTypes" %>
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
            <a href="Facilities.aspx" class="LinkButton">Facility</a> <%=API.Session.AdminEHPFacility %> | <a href="Doctors.aspx" class="LinkButton">Doctor</a> <%=API.Session.AdminEHPDoctor %>
            <label>EHP Appointment Type
                <asp:DropDownList runat="server" ID="cboEHPApptType" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPApptType_SelectedIndexChanged" /></label>
            <div class="input-links">
                <asp:LinkButton runat="server" ID="lbNew" CssClass="LinkButton" Text="New" Visible="false" OnClick="lbNew_Click" /> 
                <asp:LinkButton runat="server" ID="lbEdit" CssClass="LinkButton" Text="Edit" Visible="false" OnClick="lbEdit_Click" /> 
                <asp:LinkButton runat="server" ID="lbDelete" CssClass="LinkButton" Text="Delete" Visible="false" OnClick="lbDelete_Click" /> 
                <asp:LinkButton runat="server" ID="lbGEApptType" CssClass="LinkButton" Text="GE Appt Types" Visible="false" OnClick="lbGEApptType_Click" />
            </div>
            <asp:Panel runat="server" ID="pnlApptTypeInfo" Visible="false" >
                <label>Friendly Name
                    <asp:Label runat="server" ID="lblEHPName" /></label>
                <label>Enabled
                    <asp:Label runat="server" ID="lblEnabled" /></label>
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>