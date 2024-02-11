<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DeleteBranch.aspx.cs" Inherits="RegistrationFormASP.DeleteStudent" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="DeleteBranchIdLabel" ClientIDMode="Static" runat="server" Text="Enter the Branch ID you want to delete"></asp:Label>
    <br />
    <asp:TextBox ID="DeleteIdText" runat="server" ClientIDMode="Static" Width="800px" TextMode="Number"></asp:TextBox>
    <br /><br />
    <asp:Label ID="DeleteBrLabel" ClientIDMode="Static"
        runat="server" Text="Enter Branch Name"></asp:Label>
    <br />
    <asp:TextBox ID="DeleteNameText" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>
    <br /><br />

    <br /><br />
    <asp:Button ID="DeleteStudentButton" runat="server" OnClick="DeleteStudentButton_Click"
        Text="Submit" />

    <asp:Label ID="DeleteErrorMessage" runat="server"></asp:Label>
    
</asp:Content>