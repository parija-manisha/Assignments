<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NewBranch.aspx.cs" Inherits="RegistrationFormASP.NewBranch" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <asp:Label ID="BranchLabel" ClientIDMode="Static"
        runat="server" Text="Enter Branch Name"></asp:Label>
    <br />
    <asp:TextBox ID="BranchText" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>
    <br /><br />

    <br /><br />
    <asp:Button ID="BranchButton" runat="server" ClientIDMode="Static" OnClick="BranchButton_Click"
        Text="Submit" />

    <asp:Label ID="ErrorMessage" ClientIDMode="Static" runat="server"></asp:Label>

</asp:Content>