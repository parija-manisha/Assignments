<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="DemoUserManagement.Users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Users</h2>
            <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                PageSize="10" AllowSorting="True" OnPageIndexChanging="GridViewUsers_PageIndexChanging"
                OnSorting="GridViewUsers_Sorting">
                <Columns>
                    <asp:BoundField DataField="UserId" HeaderText="User ID" SortExpression="UserId" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" SortExpression="Email" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
