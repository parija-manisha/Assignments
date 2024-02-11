<%@ Page Title="Student List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Student.aspx.cs" Inherits="StudentDetailsWebForms.Student" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DataList ID="DataList1" runat="server" DataKeyField="StudentID" DataSourceID="SqlDataSource1" RepeatColumns="4" RepeatDirection="Horizontal" >
        <ItemTemplate>
            <div id="myrow" runat="server" style="border-style: solid; color: black; width: 300px;">
                <div style="padding: 5px;">
                    StudentID:
                    <asp:Label Text='<%# Eval("StudentID") %>' runat="server" ID="StudentIDLabel" /><br />
                    StudentName:
                    <asp:Label Text='<%# Eval("StudentName") %>' runat="server" ID="StudentNameLabel" /><br />
                    Gender:
                    <asp:Label Text='<%# Eval("Gender") %>' runat="server" ID="GenderLabel" /><br />
                    DateOfBirth:
                    <asp:Label Text='<%# Eval("DateOfBirth") %>' runat="server" ID="DateOfBirthLabel" /><br />
                    PhoneNumber:
                    <asp:Label Text='<%# Eval("PhoneNumber") %>' runat="server" ID="PhoneNumberLabel" /><br />
                    <br />
                    <asp:View></asp:View>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList><asp:SqlDataSource runat="server" ID="SqlDataSource1" 
        ConnectionString="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [Student]"></asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" DataKeyNames="StudentID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField ="StudentID" HeaderText="StudentID" ReadOnly="true" InsertVisible="false" SortExpression="StudentID" ControlStyle-Width="400px" />
            <asp:BoundField DataField ="StudentName" HeaderText="StudentName" ReadOnly="true" InsertVisible="false" ControlStyle-Width="400px" />
            <asp:BoundField DataField ="Gender" HeaderText="Gender" ReadOnly="true" InsertVisible="false" ControlStyle-Width="400px" />
            <asp:BoundField DataField ="DateOfBirth" HeaderText="DateOfBirth" ReadOnly="true" InsertVisible="false" ControlStyle-Width="400px" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField ="PhoneNumber" HeaderText="PhoneNumber" ReadOnly="true" InsertVisible="false" ControlStyle-Width="400px" />
            <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
