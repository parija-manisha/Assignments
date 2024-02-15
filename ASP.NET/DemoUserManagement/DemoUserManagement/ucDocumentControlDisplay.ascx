<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDocumentControlDisplay.ascx.cs" Inherits="DemoUserManagement.ucDocumentControlDisplay" %>

<div class="row mt-3">
    <div class="col-md-12">
        <asp:GridView ID="DocumentListGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records has been added" AllowSorting="true" CssClass="table table-striped" AllowCustomPaging="true">
            <Columns>
                <asp:TemplateField HeaderText="Sl.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ObjectID" HeaderText="Object ID" ItemStyle-Width="200" />
                <asp:BoundField DataField="ObjectType" HeaderText="Object Type" ItemStyle-Width="200" />
                <asp:BoundField DataField="DocumentType" HeaderText="Documnet Type" ItemStyle-Width="200" />
                <asp:BoundField DataField="DocumentNameOnDisk" HeaderText="Document Name On Disk" ItemStyle-Width="200" />
                <asp:BoundField DataField="OriginalDocumentName" HeaderText="Original Document Name" ItemStyle-Width="200" />
            </Columns>
        </asp:GridView>
    </div>
</div>
