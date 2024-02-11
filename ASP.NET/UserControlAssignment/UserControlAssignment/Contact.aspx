<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="UserControlAssignment.Contact" %>

<%@ Register Src="~/AddNote.ascx" TagPrefix="uc1" TagName="AddNote" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="row" aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <uc1:AddNote runat="server" ID="AddNote" PageName="Contact"/>
    </main>
</asp:Content>
