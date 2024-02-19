<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login_v2.aspx.cs" Inherits="DemoUserManagement.Login_v2" %>

<asp:Content ID="Login" ContentPlaceHolderID="MainContent" runat="server">
    <div>
         <h2>Login</h2>
        <label id="lblMessage"></label>
        <br />
        <input type="text" id="txtUsername" placeholder="Username" />
        <br />
        <br />
        <input type="password" id="txtPassword" placeholder="Password" />
        <br />
        <br />
        <button type="button" id="btnLogin" onclick="loginUser(); return false;">Login</button>
        <button type="button" id="btnSignUp" onclick="newUser(); return false;">New User</button>
    </div>
</asp:Content>