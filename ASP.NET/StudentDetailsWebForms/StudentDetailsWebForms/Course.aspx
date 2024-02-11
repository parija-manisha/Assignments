<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="Course.aspx.cs" Inherits="StudentDetailsWebForms.Course" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <asp:DataList ID="DataList1" runat="server" DataKeyField="CourseID" DataSourceID="SqlDataSource1" RepeatColumns="2" RepeatDirection="Horizontal" >
     <ItemTemplate>
         <div id="myrow" runat="server" style="border-style: solid; color: black; width: 300px;">
             <div style="padding: 5px;">
                 Course ID:
                 <asp:Label Text='<%# Eval("CourseID") %>' runat="server" ID="CourseIDLabel"/><br />
                 Course Name:
                 <asp:Label Text='<%# Eval("CourseName" ) %>' runat="server" ID="CourseNameLabel"/><br />
             </div>
         </div>
     </ItemTemplate>
 </asp:DataList><asp:SqlDataSource runat="server" ID="SqlDataSource1" 
     ConnectionString="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString %>"
     ProviderName="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString.ProviderName %>" 
     SelectCommand="SELECT * FROM [Subjects] "></asp:SqlDataSource>

</asp:Content>
