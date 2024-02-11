<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewStudent.aspx.cs" Inherits="RegistrationFormASP.NewStudent" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <asp:Label ID="NameLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Name"></asp:Label>
    <br />
    <asp:TextBox ID="NameText" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>
    <br /><br />
    <asp:Label ID="GenderLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Gender"></asp:Label>
    <br />
    <asp:TextBox ID="GenderText" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>
    <br /><br />
    <asp:Label ID="EmailLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Email"></asp:Label>
    <br />
    <asp:TextBox ID="EmailText" TextMode="Email" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>
    <br /><br />
    <asp:Label ID="PhoneLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Phone Number"></asp:Label>
    <br />
    <asp:TextBox ID="PhoneText" ClientIDMode="Static" TextMode="Phone" Width="800px"
        runat="server"></asp:TextBox>

    <br /><br />
    <asp:Button ID="StudentButton" runat="server" OnClick="StudentButton_Click"
        Text="Submit" />
    <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
    <asp:Label ID="SessionMessage" runat="server"></asp:Label>

</asp:Content>