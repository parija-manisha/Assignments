<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserControlDemo.aspx.cs" Inherits="UserControlPractice.UserControlDemo" %>

<%@ Register Src="~/WebUserControlPractice.ascx" TagPrefix="uc1" TagName="WebUserControlPractice" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:WebUserControlPractice runat="server" ID="WebUserControlPractice" />
        </div>
    </form>
</body>
</html>
