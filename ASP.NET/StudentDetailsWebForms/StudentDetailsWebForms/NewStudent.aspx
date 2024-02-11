<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NewStudent.aspx.cs" Inherits="StudentDetailsWebForms.NewStudent" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="NameLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Name"></asp:Label>
    <br />
    <asp:TextBox ID="NameText" ClientIDMode="Static" Width="800px"
        runat="server" ></asp:TextBox>
    <br /><br />
    <asp:Label ID="GenderLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Gender"></asp:Label>
    <br />
    <asp:TextBox ID="GenderText" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>
    <br /><br />
    <asp:Label ID="DateOfBirthLabel" ClientIDMode="Static"
        runat="server" Text="Enter your Date of Birth"></asp:Label>
    <br />
    <asp:TextBox ID="DateOfBirthText" TextMode="Date" ClientIDMode="Static" Width="800px"
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
</asp:Content>
