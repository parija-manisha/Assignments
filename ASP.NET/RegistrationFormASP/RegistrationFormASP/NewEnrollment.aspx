<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NewEnrollment.aspx.cs" Inherits="RegistrationFormASP.NewEnrollment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="EnrollmentStudentLabel" ClientIDMode="Static"
        runat="server" Text="Enter Student ID"></asp:Label>
    <br />
    <asp:TextBox ID="EnrollmentStudentText" TextMode="Number" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>

    <br />
    <br />

    <asp:Label ID="EnrollmentBranchLabel" ClientIDMode="Static"
        runat="server" Text="Enter Branch ID"></asp:Label>
    <br />
    <asp:TextBox ID="EnrollmentBranchText" TextMode="Number" ClientIDMode="Static" Width="800px" oninput="return enrollmentBranchTextInput()"
        runat="server"></asp:TextBox>

    <br />
    <br />

    <asp:Label ID="EnrollmentBranchNameLabel" ClientIDMode="Static" 
        runat="server" Text="Enter Branch Name"></asp:Label>
    <br />
    <asp:TextBox ID="EnrollmentBranchNameText" ClientIDMode="Static" Width="800px"
        runat="server"></asp:TextBox>

    <br />
    <br />
    <asp:Button ID="EnrollmentButton" runat="server" OnClick="EnrollmentButton_Click"
        Text="Submit" />

    <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
