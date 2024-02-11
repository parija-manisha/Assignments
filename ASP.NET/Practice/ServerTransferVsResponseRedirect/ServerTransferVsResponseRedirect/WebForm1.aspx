<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ServerTransferVsResponseRedirect.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="serverTransfer" runat="server" Text="ServerTransfer" OnClick="ServerTransfer_Click"/>
            <asp:Button ID="responseRedirect" runat="server" Text="ResponseRedirect" OnClick="ResponseRedirect_Click" />
        </div>
    </form>
</body>
</html>
