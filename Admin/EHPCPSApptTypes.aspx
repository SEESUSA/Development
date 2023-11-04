<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EHPCPSApptTypes.aspx.cs" Inherits="Admin_EHPCPSApptTypes" %>
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
            <a href="Facilities.aspx" class="LinkButton">Facility</a> <%=API.Session.AdminEHPFacility %> | <a href="Doctors.aspx" class="LinkButton">Doctor</a> <%=API.Session.AdminEHPDoctor %> | <a href="ApptTypes.aspx" class="LinkButton">EHP Appt Type</a> <%=API.Session.AdminEHPApptType %>
            <label>Centricity Appt Type
                <asp:DropDownList runat="server" ID="cboEHPCPSApptType" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPCPSApptType_SelectedIndexChanged" /></label>
            <div class="input-links">
                <asp:LinkButton runat="server" ID="lbNew" Text="New" CssClass="LinkButton" Visible="false" OnClick="lbNew_Click" /> 
                <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CssClass="LinkButton" Visible="false" OnClick="lbEdit_Click" /> 
                <asp:LinkButton runat="server" ID="lbDelete" Text="Delete" CssClass="LinkButton" Visible="false" OnClick="lbDelete_Click" />
            </div>
            <asp:Panel runat="server" ID="pnlEHPCPSApptTypeInfo" Visible="false">
                <label>Centricity Appt Type
                    <asp:Label runat="server" ID="lblGEName" /></label>
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>