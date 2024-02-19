
$(document).ready(function () {
    initializePage();
});

function getQueryStringParameter(parameterName) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(parameterName);
}

function initializePage() {
    populateCountries();
    //populateRoles();

    $("#DdlPermanentCountry").on("change", function () {
        var selectCountryId = $("#" + this.id).val();
        var targetDropdownId = $("#" + this.id.replace("Country", "State")).attr("id");
        populateState(selectCountryId, targetDropdownId);
    });

    $("#DdlPresentCountry").on("change", function () {
        var selectCountryId = $("#" + this.id).val();
        var targetDropdownId = $("#" + this.id.replace("Country", "State")).attr("id");
        populateState(selectCountryId, targetDropdownId);
    });
    $("#btnSaveUser").on("click", function () {
        saveUser();
    });

    $("#SameAsPermanent").change(function () {
        copyPermanentAddress();
    });
}

function loginUser() {
    var username = $("#txtUsername").val();
    var password = $("#txtPassword").val();

    $.ajax({
        type: "POST",
        url: "Login_v2.aspx/LoginUser",
        data: JSON.stringify({ username: username, password: password }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result && result.d && result.d !== -1) {
                var userId = result.d;
                window.location.href = 'UserDetails_v2.aspx?UserID=' + userId;
                console.log(userId);
                loadUserDetails(userId);
            } else {
                $("#lblMessage").text("Invalid Username or password");
            }
        },
        error: function () {
            console.error("Error");
        }
    });
}

function newUser() {
    window.location.href = 'UserDetails_v2.aspx';
}

function saveUser() {
    var userDetails = {
        firstName: $("#TxtFirstName").val(),
        middleName: $("#TxtMiddleName").val(),
        lastName: $("#TxtLastName").val(),
        gender: $("#TxtGender").val(),
        email: $("#TxtEmailId").val(),
        password: $("#TxtPassword").val(),
        confirmPassword: $("#TxtConfirmPassword").val(),
        phone: $("#TxtPhone").val(),
        dateOfBirth: $('#TxtDateOfBirth').val(),
        fatherName: $("#TxtFatherName").val(),
        motherName: $("#TxtMotherName").val()
    };

    var presentAddressDetails = {
        addressType: 2,
        country: $("#DdlPresentCountry").val(),
        state: $("#DdlPresentState").val(),
        city: $("#TxtPresentCity").val(),
        pincode: $("#TxtPresentPincode").val(),
        street: $("#TxtPresentAddressLine").val(),
    };

    var permanentAddressDetails = {
        addressType: 1,
        country: $("#DdlPermanentCountry").val(),
        state: $("#DdlPermanentState").val(),
        city: $("#TxtPermanentCity").val(),
        pincode: $("#TxtPermanentPincode").val(),
        street: $("#TxtPermanentAddressLine").val(),
    };

    var addressDetails = [presentAddressDetails, permanentAddressDetails];

    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/SaveUser",
        data: JSON.stringify({ userDetails: userDetails, addressDetails: addressDetails }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d === -1) {
                $("#lblMessage").text("Registration failed.");
            } else if (result.d === -2) {
                window.location.href = 'Users.aspx' + result.d;
            } else {
                $("#lblMessage").text("User Added Successfully.");
            }
        },
        error: function (xhr, status, error) {
            console.error("Error calling SaveUser: " + error);
        }
    });
}

function populateCountries() {
    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/GetCountries",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var countryNames = result.d.map(function (country) {
                return country.CountryName;
            });
            console.log(countryNames)
            populateDropdown("#DdlPermanentCountry", countryNames);
            populateDropdown("#DdlPresentCountry", countryNames);
        },
        error: function (xhr, status, error) {
            console.error("Error fetching countries: " + error);
        }
    });
}

function populateState(selectCountryId, targetDropdownId) {
    var data = { countryId: selectCountryId };

    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/PopulateState",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                var stateNames = result.data.map(function (state) {
                    return state.StateName;
                });
                console.log(stateNames);
                populateDropdown("#" + targetDropdownId, stateNames);
            } else {
                console.error("Error fetching state data: " + result.message);
                onError(result.message);
            }
        },

        error: function (result) {
            onError(result.d);
        }
    });
}

function onSuccess(data, targetDropdownId) {
    populateDropdown("#" + targetDropdownId, data);
}

function onError(data) {
    console.error("Error fetching state data: " + data);
}

function populateDropdown(selector, data) {
    var dropdown = $(selector);
    dropdown.empty();

    dropdown.append($("<option></option>").val("").text("Select"));

    $.each(data, function (index, item) {
        var option = $("<option></option>").text(item);
        dropdown.append(option);
    });
}

function copyPermanentAddress() {
    var sameAsPermanent = $("#SameAsPermanent").prop("checked");
    var permanentCountry = $("#DdlPermanentCountry").val();
    var permanentState = $("#DdlPermanentState").val();
    var permanentCity = $("#TxtPermanentCity").val();
    var permanentPincode = $("#TxtPermanentPincode").val();
    var permanentAddressLine = $("#TxtPermanentAddressLine").val();

    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/CopyPermanentAddress",
        data: JSON.stringify({
            sameAsPermanent: sameAsPermanent,
            permanentCountry: permanentCountry,
            permanentState: permanentState,
            permanentCity: permanentCity,
            permanentPincode: permanentPincode,
            permanentAddressLine: permanentAddressLine
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            updatePresentAddress(sameAsPermanent);
        },
        error: function (xhr, status, error) {
            console.error("Error copying permanent address: " + error);
        }
    });
}

function loadUserDetails(userId) {
    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/GetUserDetails",
        data: JSON.stringify({ userId: userId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var user = response.d;
            console.log(user);
            if (user != null) {
                $("#TxtFirstName").val(user.FirstName);
                $("#TxtMiddleName").val(user.MiddleName);
                $("#TxtLastName").val(user.LastName);
                $("#TxtGender").val(user.Gender);
                $("#TxtEmail").val(user.Email);
                $("#TxtPhoneNumber").val(user.PhoneNumber);
                $("#TxtDateOfBirth").val(user.DateOfBirth);
                $("#TxtHobbies").val(user.Hobbies);
                $("#TxtFatherName").val(user.FatherName);
                $("#TxtMotherName").val(user.MotherName);

                if (user.PresentAddress != null) {
                    $("#TxtPresentStreet").val(user.PresentAddress.Street);
                    $("#TxtPresentCity").val(user.PresentAddress.City);
                    $("#TxtPresentPincode").val(user.PresentAddress.Pincode);
                    $("#DdlPresentCountry").val(user.PresentAddress.CountryID);
                    $("#DdlPresentState").val(user.PresentAddress.StateID);
                }

                if (user.PermanentAddress != null) {
                    $("#TxtPermanentStreet").val(user.PermanentAddress.Street);
                    $("#TxtPermanentCity").val(user.PermanentAddress.City);
                    $("#TxtPermanentPincode").val(user.PermanentAddress.Pincode);
                    $("#DdlPermanentCountry").val(user.PermanentAddress.CountryID);
                    $("#DdlPermanentState").val(user.PermanentAddress.StateID);
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching user details: " + error);
        }
    });
}
