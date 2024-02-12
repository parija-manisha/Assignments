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
            <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="UserID" OnSorting="GridViewUsers_Sorting"
                PageSize="10" AllowSorting="True" OnPageIndexChanging="GridViewUsers_PageIndexChanging" OnRowEditing="GridViewUsers_RowEditing" OnRowDataBound="GridViewUsers_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" SortExpression="Email" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
