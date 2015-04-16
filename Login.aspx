<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GSDC.Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Menu.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <webopt:BundleReference ID="BundleReference1" runat="server" Path="~/Content/css" />
    <link href="~/Images/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>

    <form id="form1" runat="server">
        <center>
            <table style="width: 20%;margin-top:12%">
                <tr>
                    <td colspan="2">
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; vertical-align: top;padding-top:8px">
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" CssClass="control-label" Style="width: 65px">User name</asp:Label>
                    </td>
                    <td style="vertical-align: middle">
                        <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                            CssClass="text-danger" ErrorMessage="The user name field is required." />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; vertical-align: top;padding-top:8px">
                        <asp:Label ID="Label2" runat="server" AssociatedControlID="Password" CssClass="control-label" Style="width: 65px">Password</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top;">
                        <div class="checkbox" style="margin-top: 0px;width: 120px">
                            <asp:CheckBox runat="server" ID="RememberMe"/>
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        
                            <asp:Button ID="Button1" runat="server" OnClick="Login1_Authenticate" Text="Log in" CssClass="btn btn-default" />
                        
                    </td>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
