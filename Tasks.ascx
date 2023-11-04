<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tasks.ascx.cs" Inherits="Tasks" %>
<label>Task
    <asp:DropDownList runat="server" ID="cboTask" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboTask_SelectedIndexChanged" /></label>
<br /><br />