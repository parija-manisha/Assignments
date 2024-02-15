<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDocumentUserControl.ascx.cs" Inherits="DemoUserManagement.ucDocumentUserControl" %>

<asp:Label CssClass="pe-2 w-25" ClientIDMode="Static" ID="LblDocumentType" runat="server">Choose Document</asp:Label>
<asp:DropDownList ID="DocumentDropDown" runat="server" CssClass="w-100" OnSelectedIndexChanged="DocumentDropDown_SelectedIndexChanged">
    <asp:ListItem Text="Select" Value=""></asp:ListItem>
</asp:DropDownList>
<br />
<br />
<asp:Label CssClass="pe-2 w-25" ClientIDMode="Static" ID="LblFileUpload" runat="server" AssociatedControlID="DdlFileUpload">Upload documents</asp:Label>
<asp:FileUpload runat="server" ID="DdlFileUpload" ClientIDMode="Static"/>
<asp:Label runat="server" ID="ErrorMessage" CssClass="text-danger"></asp:Label>