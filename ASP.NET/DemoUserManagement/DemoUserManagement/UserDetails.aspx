<%@ Page Title="User Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="DemoUserManagement._Default" %>

<%@ Register Src="~/ucNoteControl.ascx" TagPrefix="uc1" TagName="ucNoteControl" %>
<%@ Register Src="~/ucDocumentUserControl.ascx" TagPrefix="uc1" TagName="ucDocumentUserControl" %>



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
                        <asp:TextBox ClientIDMode="static" ID="TxtEmailID" runat="server" CssClass="w-100" onkeyup="chech"></asp:TextBox><br />
                        <asp:Label ID="LblEmailExists" ClientIDMode="Static" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div>
                    <div id="DivPassword" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPassword" runat="server" AssociatedControlID="TxtPassword">Password</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtPassword" runat="server" CssClass="w-100"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <div id="DivConfirmPassword" class="pb-3">
                        <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblConfirmPassword" runat="server" AssociatedControlID="TxtConfirmPassword">Confirm Password</asp:Label>
                        <asp:TextBox ClientIDMode="static" ID="TxtConfirmPassword" runat="server" CssClass="w-100"></asp:TextBox>
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

                <div id="AddRoleToUser" runat="server" class="pt-5">
                    <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblAddRole" runat="server" AssociatedControlID="DdlAddRole">Add Roles</asp:Label>
                    <asp:DropDownList ClientIDMode="static" ID="DdlAddRole" runat="server" CssClass="w-100" AutoPostBack="true" OnSelectedIndexChanged="AddRole">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                </div>

                <div class="pt-5">
                    <asp:Button runat="server" ClientIDMode="static" ID="SaveUserButton" Text="Save User" CssClass="w-25" OnClick="SaveUserButton_Click" />
                    <asp:Button runat="server" ClientIDMode="static" ID="DeleteUserButton" Text="Delete User" CssClass="w-25" />
                </div>

                <asp:Label ClientIDMode="static" ID="ErrorMessage" runat="server"></asp:Label>
            </asp:Panel>

            <asp:Panel ClientIDMode="static" ID="pnlAddress" runat="server" CssClass="w-50">
                <div>
                    <h3>PERMANENT ADDRESS</h3>
                    <hr />

                    <div>
                        <div id="divPermanentCountry" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentCountry" runat="server" AssociatedControlID="DdlPermanentCountry">Nationality</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="DdlPermanentCountry" runat="server" CssClass="w-100" AutoPostBack="true" OnSelectedIndexChanged="PermanentCountryState">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentState" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentState" runat="server" AssociatedControlID="DdlPermanentState">State</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="DdlPermanentState" runat="server" CssClass="w-100" DataTextField="StateName" DataValueField="StateId">
                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentCity" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentCity" runat="server" AssociatedControlID="DdlPermanentCity">City</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="DdlPermanentCity" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentPincode" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentPincode" runat="server" AssociatedControlID="DdlPermanentPincode">Pincode</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="DdlPermanentPincode" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentAddressLine" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPermanentAddressLine" runat="server" AssociatedControlID="DdlPermanentAddressLine">Address</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="DdlPermanentAddressLine" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div>
                    <h3>PRESENT ADDRESS</h3>
                    <hr />
                    <asp:CheckBox ID="SameAsPermanent" runat="server" Text="Same As Permanent Address" OnCheckedChanged="SameAsPermanent_CheckedChanged" />
                    <div>
                        <div id="divPresentCountry" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="lblPresentCountry" runat="server" AssociatedControlID="DdlPresentCountry">Nationality</asp:Label>
                            <asp:DropDownList ID="DdlPresentCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PresentCountryState" ClientIDMode="Static" CssClass="w-100">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="India">India</asp:ListItem>
                                <asp:ListItem Value="america">America</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <div id="DivPresentState" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentState" runat="server" AssociatedControlID="DdlPresentState">State</asp:Label>
                            <asp:DropDownList ClientIDMode="static" ID="DdlPresentState" runat="server" CssClass="w-100" DataTextField="StateName" DataValueField="StateId">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <div id="DivPresentCity" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentCity" runat="server" AssociatedControlID="DdlPresentCity">City</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="DdlPresentCity" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div id="DivPresentPincode" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentPincode" runat="server" AssociatedControlID="DdlPresentPincode">Pincode</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="DdlPresentPincode" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div id="DivPresentAddressLine" class="pb-3">
                            <asp:Label CssClass="pe-2 w-25" ClientIDMode="static" ID="LblPresentAddressLine" runat="server" AssociatedControlID="DdlPresentAddressLine">Address</asp:Label>
                            <asp:TextBox ClientIDMode="static" ID="DdlPresentAddressLine" runat="server" CssClass="w-100"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <uc1:ucDocumentUserControl runat="server" ID="ucDocumentUserControl" />
            </asp:Panel>
        </div>
        <uc1:ucNoteControl runat="server" ID="ucNoteControl" />
    </main>
</asp:Content>
