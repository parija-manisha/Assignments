<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNoteControl.ascx.cs" Inherits="DemoUserManagement.ucNoteControl" %>

<div class="container mt-3">
    <div class="row">
        <div>
            <div class="row">
                <asp:TextBox ID="AddNoteText" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Enter your note"></asp:TextBox>
            </div>
            <br />
            <asp:Button ID="SaveNoteButton" runat="server" Text="Add" OnClick="SaveNoteButton_Click" CssClass="btn btn-primary" />
            <asp:Label ID="AddingSuccess" runat="server" CssClass="text-success mt-2"></asp:Label>
        </div>
    </div>
    <div class="row mt-3">
        <div class="col-md-12">
            <asp:GridView ID="NoteListGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records has been added" AllowSorting="true" CssClass="table table-striped" OnPageIndexChanging="NoteListGridView_PageIndexChanging" AllowCustomPaging="true" OnSorting="NoteListGridView_Sorting" OnRowDataBound="NoteListGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ObjectID" HeaderText="Object ID" ItemStyle-Width="200" SortExpression="ObjectID" />
                    <asp:BoundField DataField="ObjectType" HeaderText="Page Name" ItemStyle-Width="200" SortExpression="ObjectID" />
                    <asp:BoundField DataField="NoteText" HeaderText="Note" ItemStyle-Width="200" SortExpression="ObjectID" />
                    <asp:BoundField DataField="TimeStamp" HeaderText="Entered At" ItemStyle-Width="200" SortExpression="ObjectID" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
