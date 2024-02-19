<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DemoUserManagement.Login" %>

<asp:Content ID="Login" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Login</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:TextBox ID="txtUsername" ClientIDMode="Static" runat="server" placeholder="Username"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtPassword" ClientIDMode="Static" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        <asp:Button ID="btnSignUp" runat="server" Text="New User" OnClick="btnSignUp_Click" />
    </div>
</asp:Content>

