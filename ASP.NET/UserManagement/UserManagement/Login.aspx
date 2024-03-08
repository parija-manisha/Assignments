<%@ Page Title="Login_" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserManagement.Login" MasterPageFile="~/Site.Master" %>

<asp:Content ID="LoginPage" runat="server" ContentPlaceHolderID="MainContent">
    <div class="login-container">
        <h2>Login</h2>
        <label id="LblMessage" class="pt-2 pb-2"></label>
        <div class="form-group">
            <label for="TxtUserName">Username</label>
            <input type="text" class="form-control" id="TxtUserName" placeholder="Enter your username" required>
        </div>
        <div class="form-group">
            <label for="TxtPassword">Password</label>
            <input type="password" class="form-control" id="TxtPassword" placeholder="Enter your password" required>
        </div>
        <button type="button" class="btn btn-primary mt-4" onclick="loginUser(); return false;">Login</button>
        <button type="submit" class="btn btn-secondary mt-4" onclick="newUser(); return false;">New User</button>
    </div>
</asp:Content>

