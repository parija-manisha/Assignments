<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNote.ascx.cs" Inherits="DemoUserManagement.AddNote" %>

<asp:TextBox ID="AddNoteText" runat="server" TextMode="MultiLine" Rows="6" Columns="100"></asp:TextBox>
<asp:Button ID="SaveNoteButton" runat="server" Text="Add" OnClick="SaveNoteButton_Click"/>
<asp:Label ID="AddingSuccess" runat="server"></asp:Label>