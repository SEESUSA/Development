<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<!DOCTYPE html>
<style>
    .loader-container {
            background: #07253121;
            top: 0;
            left: 0;
            z-index: 50;
            width: 100%;
            height: 100%;
            position: fixed;
        }

        .load {
            width: 100px;
            height: 100px;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
        }
    .lds-dual-ring {
  display: inline-block;
  width: 80px;
  height: 80px;
}
.lds-dual-ring:after {
  content: " ";
  display: block;
  width: 64px;
  height: 64px;
  margin: 8px;
  border-radius: 50%;
  border: 6px solid #fff;
  border-color: #5c9692 transparent #5c9692 transparent;
  animation: lds-dual-ring 1.2s linear infinite;
}
@keyframes lds-dual-ring {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnCancel").click(function () {
                $(".loader-container").show();
                setTimeout(function () {
                    $(".loader-container").hide();
                }, 45000);
            });

        });
    </script>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Reset Password</h2>
    </header>
    <form id="form1" runat="server">
    
        <div>
            <asp:Panel runat="server" ID="pnlStart" Visible="true">
                <p>Please provide a new password below.</p>
                <p>Password Requirements</p>
                <ul>
                    <li>6 characters minimum</li>
                    <li>1 uppercase letter</li>
                    <li>1 lowercase letter</li>
                    <li>1 number</li>
                    <li>1 symbol</li>
                </ul>
                <label>Password
                    <asp:TextBox runat="server" ID="txtPassword1" MaxLength="50" CssClass="TextField" TextMode="Password" /></label>
                <label>Confirm Password
                    <asp:TextBox runat="server" ID="txtPassword2" MaxLength="50" CssClass="TextField" TextMode="Password" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnReset" CssClass="Button" Text="Reset" OnClick="btnReset_Click" />
                    <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
           
        </div>
        <div>
             <asp:Panel runat="server" ID="pnlComplete" Visible="false">
                <p>Your password has successfully been reset.  Please logon with your new password.</p>
                <asp:Button runat="server" ID="btnLogon" Text="Logon" CssClass="Button" OnClick="btnLogon_Click" />
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>
