<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNote.ascx.cs" Inherits="DemoUserManagement.AddNote" %>

<asp:TextBox ID="AddNoteText" runat="server" TextMode="MultiLine" Rows="6" Columns="100"></asp:TextBox>
<asp:Button ID="SaveNoteButton" runat="server" Text="Add" OnClick="SaveNoteButton_Click"/>
<asp:Label ID="AddingSuccess" runat="server"></asp:Label>
<br />
<asp:GridView ID="NoteListGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records has been added" AllowSorting="true">
    <Columns>
        <asp:BoundField DataField="UserID" HeaderText="ID" ItemStyle-Width="200"/>
        <asp:BoundField DataField="ObjectType" HeaderText="Page Name" ItemStyle-Width="200"/>
        <asp:BoundField DataField="Text" HeaderText="Note" ItemStyle-Width="200"/>
        <asp:BoundField DataField="Date" HeaderText="Entered At" ItemStyle-Width="200"/>
    </Columns>
</asp:GridView>