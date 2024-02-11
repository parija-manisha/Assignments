<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Student.aspx.cs" Inherits="RegistrationFormASP.Student" %>

<asp:Content ID="StudentList" runat="server" ContentPlaceHolderID="MainContent">
    <asp:GridView ID="gridServiceStudent" runat="server" AutoGenerateColumns="false" EmptyDataText="No records have been added" AllowSorting="true" AllowPaging="true" OnSorting="gridServiceStudent_Sorting" OnPageIndexChanging="gridServiceStudent_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="Student ID" HeaderText="Sl.No" ItemStyle-Width="200" SortExpression="Student ID" />
            <asp:BoundField DataField="Student Name" HeaderText="Student Name" ItemStyle-Width="200" SortExpression="Student Name" />
            <asp:BoundField DataField="Phone Number" HeaderText="Phone Number" ItemStyle-Width="200" SortExpression="Phone Number" />
            <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="200" SortExpression="Email" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" ItemStyle-Width="200" SortExpression="Gender" />
            <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
