<%@ Page Title="Users List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Users.aspx.cs" Inherits="DemoUserManagement.Users" %>

<asp:Content ID="UserListGridView" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Users</h2>
    <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="UserID" OnSorting="GridViewUsers_Sorting"
        PageSize="10" AllowSorting="True" OnPageIndexChanging="GridViewUsers_PageIndexChanging" OnRowDataBound="GridViewUsers_RowDataBound" CssClass="table table-striped table-bordered table-hover">
        <HeaderStyle BackColor="LightBlue" Height="50" ForeColor="white" />
        <Columns>
            <asp:TemplateField HeaderText="Sl.No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" SortExpression="PhoneNumber" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDownload" runat="server" Text='<%#Eval("FileName") %>' CommandArgument='<%#Eval("UserID") %>' OnCommand="LinkButtonDownload_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" runat="server" Text="Edit" NavigateUrl='<%# "~/UserDetails.aspx?" + DemoUserManagement.Util.Constants.ObjectIDName.UserID + "=" + Eval("UserID") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="SuccessMessage" runat="server" ClientIDMode="Static" ForeColor="red"></asp:Label>
</asp:Content>
