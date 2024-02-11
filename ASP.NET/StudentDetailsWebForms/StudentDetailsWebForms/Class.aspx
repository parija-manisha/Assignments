<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class.aspx.cs" MasterPageFile="~/Site.Master" Inherits="StudentDetailsWebForms.Class" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <asp:DataList ID="DataList1" runat="server" DataKeyField="ClassID" DataSourceID="SqlDataSource1" RepeatColumns="2" RepeatDirection="Horizontal" >
     <ItemTemplate>
         <div id="myrow" runat="server" style="border-style: solid; color: black; width: 300px;">
             <div style="padding: 5px;">
                 Class ID:
                 <asp:Label Text='<%# Eval("ClassID") %>' runat="server" ID="ClassIDLabel"/><br />
                 Class Name:
                 <asp:Label Text='<%# Eval("ClassName" ) %>' runat="server" ID="ClassNameLabel"/><br />
             </div>
         </div>
     </ItemTemplate>
 </asp:DataList><asp:SqlDataSource runat="server" ID="SqlDataSource1" 
     ConnectionString="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString %>"
     ProviderName="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString.ProviderName %>" 
     SelectCommand="SELECT * FROM [Class_Detail] "></asp:SqlDataSource>

</asp:Content>
