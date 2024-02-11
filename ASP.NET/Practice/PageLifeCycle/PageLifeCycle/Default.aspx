<%@ Page Title="Home Page" Language="C#"  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PageLifeCycle._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASP.NET Page Life Cycle Example</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtInput" runat="server" />
            <asp:Button ID="btnClickMe" runat="server" Text="Click Me" OnClick="btnClickMe_Click" />
            <br />
            <asp:CheckBox ID="chkToggle" runat="server" Text="Toggle Controls" OnCheckedChanged="chkToggle_CheckedChanged" />
            <br />
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </form>
</body>
</html>