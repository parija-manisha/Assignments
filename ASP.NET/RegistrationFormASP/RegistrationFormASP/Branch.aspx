<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Branch.aspx.cs" Inherits="RegistrationFormASP.Branch" %>

<asp:Content ID="BranchList" runat="server" ContentPlaceHolderID="MainContent">
            <asp:GridView ID="gridServiceBranch" runat="server" AutoGenerateColumns="false" EmptyDataText="No records has been added">
                <Columns>
                   <asp:BoundField DataField="Branch ID" HeaderText="Sl.No" ItemStyle-Width="200" />
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name" ItemStyle-Width="200" />
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>
</asp:Content>
