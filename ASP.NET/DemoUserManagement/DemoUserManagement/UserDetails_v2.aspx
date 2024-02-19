<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserDetails_v2.aspx.cs" Inherits="DemoUserManagement.UserDetails_v2" %>

<%@ Register Src="~/ucNoteControl.ascx" TagPrefix="uc1" TagName="ucNoteControl" %>
<%@ Register Src="~/ucDocumentUserControl.ascx" TagPrefix="uc1" TagName="ucDocumentUserControl" %>

<asp:Content ClientIDMode="static" ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div id="UserInformation" class="d-flex">
            <div id="pnlPersonalDetails" class="w-50">
                <h3>PERSONAL DETAILS</h3>
                <hr />
                <div>
                    <div id="DivTxtFirstName" class="pb-3">
                        <label for="TxtFirstName" class="pe-2 w-25">First Name</label>
                        <input type="text" id="TxtFirstName" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivMiddleName" class="pb-3">
                        <label for="TxtMiddleName" class="pe-2 w-25">Middle Name</label>
                        <input type="text" id="TxtMiddleName" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivLastName" class="pb-3">
                        <label for="TxtLastName" class="pe-2 w-25">Last Name</label>
                        <input type="text" id="TxtLastName" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivGender" class="pb-3">
                        <label for="TxtGender" class="pe-2 w-25">Gender</label>
                        <input type="text" id="TxtGender" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivEmailID" class="pb-3">
                        <label for="TxtEmailID" class="pe-2 w-25">Email ID</label>
                        <input type="text" id="TxtEmailID" class="w-100" oninput="checkEmail()">
                    </div>
                    <label id="LblEmailExists"></label>
                </div>
                <div>
                    <div id="DivPassword" class="pb-3">
                        <label for="TxtPassword" class="pe-2 w-25">Password</label>
                        <input type="text" id="TxtPassword" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivConfirmPassword" class="pb-3">
                        <label for="TxtConfirmPassword" class="pe-2 w-25">Confirm Password</label>
                        <input type="text" id="TxtConfirmPassword" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivPhoneNumber" class="pb-3">
                        <label for="TxtPhone" class="pe-2 w-25">Phone Number</label>
                        <input type="text" id="TxtPhone" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivDateOfBirth" class="pb-3">
                        <label for="TxtDateOfBirth" class="pe-2 w-25">Date Of Birth</label>
                        <input type="text" id="TxtDateOfBirth" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivFatherName" class="pb-3">
                        <label for="TxtFatherName" class="pe-2 w-25">Father Name</label>
                        <input type="text" id="TxtFatherName" class="w-100">
                    </div>
                </div>
                <div>
                    <div id="DivTxtMotherName" class="pb-3">
                        <label for="TxtMotherName" class="pe-2 w-25">Mother Name</label>
                        <input type="text" id="TxtMotherName" class="w-100">
                    </div>
                </div>

                <div id="AddRoleToUser" class="pt-5">
                    <label for="DdlAddRole" class="pe-2 w-25">Add Roles</label>
                    <select id="DdlAddRole" class="w-100" onchange="AddRole()">
                        <option value="">Select</option>
                    </select>
                </div>

                <div class="pt-5">
                    <button id="SaveUserButton" class="w-25" onclick="SaveUser()">Save User</button>
                    <button id="DeleteUserButton" class="w-25">Delete User</button>
                </div>

                <label id="ErrorMessage"></label>
            </div>

            <div id="pnlAddress" class="w-50">
                <div>
                    <h3>PERMANENT ADDRESS</h3>
                    <hr />

                    <div>
                        <div id="DivPermanentCountry" class="pb-3">
                            <label for="DdlPermanentCountry" class="pe-2 w-25">Country</label>
                            <select id="DdlPermanentCountry" class="w-100" onchange="onPermanentCountryChange()">
                                <option value="">Select</option>
                            </select>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentState" class="pb-3">
                            <label for="DdlPermanentState" class="pe-2 w-25">State</label>
                            <select id="DdlPermanentState" class="w-100">
                                <option value="">Select</option>
                            </select>
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentCity" class="pb-3">
                            <label for="TxtPermanentCity" class="pe-2 w-25">City</label>
                            <input type="text" id="TxtPermanentCity" class="w-100">
                        </div>
                    </div>

                    <div>
                        <div id="DivPermanentPincode" class="pb-3">
                            <label for="TxtPermanentPincode" class="pe-2 w-25">Pincode</label>
                            <input type="text" id="TxtPermanentPincode" class="w-100">
                        </div>
                    </div>
                    <div>
                        <div id="DivPermanentAddressLine" class="pb-3">
                            <label for="TxtPermanentAddressLine" class="pe-2 w-25">Address Line</label>
                            <input type="text" id="TxtPermanentAddressLine" class="w-100">
                        </div>
                    </div>
                </div>
                <div>
                    <h3>PRESENT ADDRESS</h3>
                    <hr />
                    <input type="checkbox" id="SameAsPermanent" onchange="SameAsPermanent_CheckedChanged()">Same As Permanent Address
                         <div>
                             <div id="DivPresentCountry" class="pb-3">
                                 <label for="DdlPresentCountry" class="pe-2 w-25">Country</label>
                                 <select id="DdlPresentCountry" class="w-100" onchange="onPresentCountryChange()">
                                     <option value="">Select</option>
                                 </select>
                             </div>
                         </div>
                    <div>
                        <div id="DivPresentState" class="pb-3">
                            <label for="DdlPresentState" class="pe-2 w-25">State</label>
                            <select id="DdlPresentState" class="w-100">
                                <option value="">Select</option>
                            </select>
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentCity" class="pb-3">
                            <label for="TxtPresentCity" class="pe-2 w-25">City</label>
                            <input type="text" id="TxtPresentCity" class="w-100">
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentPincode" class="pb-3">
                            <label for="TxtPresentPincode" class="pe-2 w-25">Pincode</label>
                            <input type="text" id="TxtPresentPincode" class="w-100">
                        </div>
                    </div>
                    <div>
                        <div id="DivPresentAddressLine" class="pb-3">
                            <label for="TxtPresentAddressLine" class="pe-2 w-25">Address Line</label>
                            <input type="text" id="TxtPresentAddressLine" class="w-100">
                        </div>
                    </div>
                </div>
                <uc1:ucDocumentUserControl runat="server" ID="ucDocumentUserControl" />
            </div>
        </div>
        <uc1:ucNoteControl runat="server" ID="ucNoteControl" />
    </main>
</asp:Content>
