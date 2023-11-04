<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logoff.aspx.cs" Inherits="Logoff" %>
<%@ Register Src="CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="img/header.png" alt="eye health partners/vision america portal" width="397" height="44">
    </header>
    <form id="form1" runat="server">
    </form>
</body>
</html>
