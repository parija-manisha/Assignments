<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Enrollment.aspx.cs" Inherits="RegistrationFormASP.Enrollment" %>

<asp:Content ID="EnrollmentList" runat="server" ContentPlaceHolderID="MainContent">

    <asp:GridView ID="gridServiceEnrollment" runat="server"  CssClass="Grid" AutoGenerateColumns="false" EmptyDataText="No records has been added">
        <Columns>
            <asp:BoundField DataField="Enrollment ID" HeaderText="Sl.No" ItemStyle-Width="200" />
            <asp:BoundField DataField="Student ID" HeaderText="Student ID" ItemStyle-Width="200" />
            <asp:BoundField DataField="Branch ID" HeaderText="Branch ID" ItemStyle-Width="200" />
            <asp:BoundField DataField="Branch Name" HeaderText="Branch Name" ItemStyle-Width="200" />
            <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>

</asp:Content>
