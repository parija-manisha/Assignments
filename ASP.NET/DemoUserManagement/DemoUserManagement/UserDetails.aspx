<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DemoUserManagement.UserDetails" %>

<%@ Register Src="~/AddNote.ascx" TagPrefix="uc1" TagName="AddNote" %>


<asp:Content ClientIDMode="static" ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div id="UserInformation" class="d-flex">
            <asp:Panel ClientIDMode="static" ID="pnlPersonalDetails" runat="server" CssClass="w-50">
                <h3>PERSONAL DETAILS</h3>
                <hr />
                <div>
                    <div id="DivTxtFirstName" class="pb-3">
                        <asp:Label ClientIDMode="static" ID="LblFirstName" runat="server" AssociatedControlID="TxtFirstName" CssClass="pe-2 w-25">First Name</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtFirstName" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivMiddleName" class="pb-3">
                        <asp:Label ClientIDMode="static" ID="LblMiddleName" runat="server" AssociatedControlID="TxtMiddleName" CssClass="pe-2 w-25">Middle Name</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtMiddleName" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivLastName" class="pb-3">
                        <asp:Label ClientIDMode="static" ID="LblLastName" runat="server" AssociatedControlID="TxtLastName" CssClass="pe-2 w-25">Last Name</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtLastName" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivGender" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblGender" runat="server" AssociatedControlID="TxtGender">Gender</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtGender" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivEmailID" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblEmailID" runat="server" AssociatedControlID="TxtEmailID">Email ID</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtEmailID" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivPhoneNumber" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPhoneNumber" runat="server" AssociatedControlID="TxtPhoneNumber">Phone Number</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtPhoneNumber" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivDateOfBirth" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblDateOfBirth" runat="server" AssociatedControlID="TxtDateOfBirth">Date of Birth</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtDateOfBirth" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivFatherName" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblFatherName" runat="server" AssociatedControlID="TxtFatherName">Father Name</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtFatherName" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivTxtMotherName" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblTxtMotherName" runat="server" AssociatedControlID="TxtMotherName">Mother Name</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtMotherName" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div class="pt-5">
                    <asp:Button runat="server" ClientIDMode="static" ID="SaveUserButton" Text="Save User" CssClass="w-25" OnClick="SaveUserButton_Click" />
                    <asp:Button runat="server" ClientIDMode="static" ID="AddUserButton" Text="Add User" CssClass="w-25" />
                    <asp:Button runat="server" ClientIDMode="static" ID="UpdateUserButton" Text="Update User" CssClass="w-25" />
                </div>

                <asp:Label ClientIDMode="static" ID="ErrorMessage" runat="server"></asp:Label>
            </asp:Panel>

            <asp:Panel ClientIDMode="static" ID="pnlAddress" runat="server" CssClass="w-50">
                <div>
                    <h3>PERMANENT ADDRESS</h3>
                    <hr />

                    <div>
                        <div id="divPermanentCountry" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="lblPermanentCountry" runat="server" AssociatedControlID="PermanentCountry">Nationality</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="PermanentCountry" runat="server" CssClass="w-100">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="India">India</asp:ListItem>
                                <asp:ListItem Value="USA">USA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentState" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentState" runat="server" AssociatedControlID="PermanentState">State</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="PermanentState" runat="server" CssClass="w-100">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Mumbai">Mumbai</asp:ListItem>
                                <asp:ListItem Value="Odisha">Odisha</asp:ListItem>
                                <asp:ListItem Value="Florida">Florida</asp:ListItem>
                                <asp:ListItem Value="NewYork">NewYork</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentCity" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentCity" runat="server" AssociatedControlID="PermanentCity">City</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="PermanentCity" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentPincode" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentPincode" runat="server" AssociatedControlID="PermanentPincode">Pincode</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="PermanentPincode" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentAddressLine" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentAddressLine" runat="server" AssociatedControlID="PermanentAddressLine">Address</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="PermanentAddressLine" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div>
                    <h3>PRESENT ADDRESS</h3>
                    <hr />
                    <div>
                        <div id="divPresentCountry" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="lblPresentCountry" runat="server" AssociatedControlID="PresentCountry">Nationality</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="PresentCountry" runat="server" CssClass="w-100">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="India">India</asp:ListItem>
                                <asp:ListItem Value="USA">USA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentState" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentState" runat="server" AssociatedControlID="PresentState">State</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="PresentState" runat="server" CssClass="w-100">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Mumbai">Mumbai</asp:ListItem>
                                <asp:ListItem Value="Odisha">Odisha</asp:ListItem>
                                <asp:ListItem Value="Florida">Florida</asp:ListItem>
                                <asp:ListItem Value="NewYork">NewYork</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentCity" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentCity" runat="server" AssociatedControlID="PresentCity">City</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="PresentCity" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentPincode" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentPincode" runat="server" AssociatedControlID="PresentPincode">Pincode</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="PresentPincode" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentAddressLine" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="Label1" runat="server" AssociatedControlID="PresentAddressLine">Address</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="PresentAddressLine" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <uc1:AddNote runat="server" ID="AddNote" Visible="false" />
        </div>
    </main>
</asp:Content>
