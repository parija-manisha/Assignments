<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserControlAssignment._Default" %>

<%@ Register Src="~/AddNote.ascx" TagPrefix="uc1" TagName="AddNote" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">ASP.NET</h1>
            <uc1:AddNote runat="server" id="AddNote" PageName="Default"/>
        </section>
       
    </main>

</asp:Content>
