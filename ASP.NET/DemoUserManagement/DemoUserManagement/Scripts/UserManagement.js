$(document).ready(function () {
    populateCountries();

    $("#UserDetailsLink").hide();
    $("#UpdateUserLink").hide();
    $("#ucNoteControl").hide();
    $("#documentUserControl").hide();
    $("#AddRoleToUser").hide();

    $("#DdlPermanentCountry").on("change", function () {
        var selectCountryId = $("#DdlPermanentCountry").val();
        var targetDropdownId = $("#DdlPermanentCountry").attr("id").replace("Country", "State");
        populateState(selectCountryId, targetDropdownId);

    });

    $("#DdlPresentCountry").on("change", function () {
        var selectCountryId = $(this).val();
        var targetDropdownId = $(this).attr("id").replace("Country", "State");
        populateState(selectCountryId, targetDropdownId);
    });

    $("#SameAsPermanent").on("change", function () {
        copyPermanentAddress();
    });

    var userId = getUserIdFromUrl();
    loadUserDetails(userId);

    function getUserIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.has("UserID") ? parseInt(urlParams.get("UserID")) : 0;
    }
});

function checkUserAuthorization(userSession) {
    console.log("Second ajax call");

    var data = { userSession: userSession };

    $.ajax({
        type: "POST",
        url: "Login_v2.aspx/CheckUserAuthorisation",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (result) {
            if (result && result.d) {
                var isLoggedIn = result.d.IsLoggedIn;
                var isAdmin = result.d.IsAdmin;

                redirectAfterLoad(data.userSession.UserId, isAdmin, isLoggedIn);

            } else {
                console.log("Error in CheckUserAuthorisation WebMethod.");
            }
        },
        error: function () {
            console.error("Error");
        }
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
            if (result && result.d) {
                var userSession = result.d;
                if (userSession.UserId !== -1) {
                    checkUserAuthorization(userSession);
                } else {
                    $("#lblMessage").text("Invalid Username or password");
                }
            }
        },
        error: function () {
            console.error("Error");
            $("#lblMessage").text("An error occurred during login. Please try again.");
        }
    });
}


function saveUser() {
    var userDetails = {
        firstName: $("#TxtFirstName").val(),
        middleName: $("#TxtMiddleName").val(),
        lastName: $("#TxtLastName").val(),
        gender: $("#TxtGender").val(),
        email: $("#TxtEmailID").val(),
        password: $("#TxtPassword").val(),
        confirmPassword: $("#TxtConfirmPassword").val(),
        phoneNumber: $("#TxtPhone").val(),
        dateOfBirth: $('#TxtDateOfBirth').val(),
        fatherName: $("#TxtFatherName").val(),
        motherName: $("#TxtMotherName").val()
    };

    var presentAddressDetails = {
        addressType: 2,
        countryId: $("#DdlPresentCountry").val(),
        stateId: $("#DdlPresentState").val(),
        city: $("#TxtPresentCity").val(),
        pincode: $("#TxtPresentPincode").val(),
        street: $("#TxtPresentAddressLine").val(),
    };

    var permanentAddressDetails = {
        addressType: 1,
        countryId: $("#DdlPermanentCountry").val(),
        stateId: $("#DdlPermanentState").val(),
        city: $("#TxtPermanentCity").val(),
        pincode: $("#TxtPermanentPincode").val(),
        street: $("#TxtPermanentAddressLine").val(),
    };
    var addressDetails = [presentAddressDetails, permanentAddressDetails];


    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/SaveUser",
        data: JSON.stringify({ userDetails: userDetails, addressDetails: addressDetails }),
        contentType: "application/json",
        success: function (result) {
            if (result.d === -1) {
                $("#lblMessage").innerHTML = "Registration failed.";
            } else if (result.d === -2) {
                window.location.href = 'Users.aspx' + result.d;
            } else {
                $("#lblMessage").innerHTML = "User Added Successfully.";
                uploadFiles(result.d);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error calling SaveUser: " + error);
        }
    });
}

function newUser() {
    window.location.href = 'UserDetails_v2.aspx';
}

function populateCountries() {
    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/GetCountries",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var countries = result.d;
            populateDropdown("#DdlPermanentCountry, #DdlPresentCountry", countries, "CountryID", "CountryName");
        },
        error: function (xhr, status, error) {
            console.error("Error fetching countries: " + error);
        }
    });
}

function populateState(selectCountryId, targetDropdownId) {
    var data = { countryId: parseInt(selectCountryId) };

    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/PopulateState",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (result) {
            var stateList = result.d;
            populateDropdown("#" + targetDropdownId, stateList, "StateID", "StateName");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching state data: " + errorThrown);
            onError(errorThrown);
        }
    });
}

function onSuccess(data, targetDropdownId) {
    populateDropdown("#" + targetDropdownId, data);
}

function onError(data) {
    console.error("Error fetching state data: " + data);
}

function populateDropdown(selector, data, valueKey, textKey) {
    var dropdown = $(selector);
    dropdown.empty();

    dropdown.append($("<option></option>").val("").text("Select"));

    $.each(data, function (index, item) {
        var option = $("<option></option>").val(item[valueKey]).text(item[textKey]);
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

function updatePresentAddress(sameAsPermanent) {
    if (sameAsPermanent) {
        $("#TxtPresentCountry").val($("#DdlPermanentCountry").val());

        $("#TxtPresentState").val($("#DdlPermanentState").val());
        $("#TxtPresentCity").val($("#TxtPermanentCity").val());
        $("#TxtPresentPincode").val($("#TxtPermanentPincode").val());
        $("#TxtPresentAddressLine").val($("#TxtPermanentAddressLine").val());
    } else {
        $("#TxtPresentCountry").val('');
        $("#TxtPresentState").val('');
        $("#TxtPresentCity").val('');
        $("#TxtPresentPincode").val('');
        $("#TxtPresentAddressLine").val('');
    }
}

function loadUserDetails(userID) {
    var data = { userId: parseInt(userID) };
    console.log("LoadUser")
    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/GetUserDetails",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            console.log(response)
            var user = response.d;
            console.log(user);

            if (user != null) {
                $("#TxtFirstName").val(user.FirstName);
                $("#TxtLastName").val(user.LastName);
                $("#TxtGender").val(user.Gender);
                $("#TxtEmailID").val(user.Email);
                $("#TxtPassword").val(user.Password);
                $("#TxtConfirmPassword").val(user.ConfirmPassword)
                $("#TxtPhoneNumber").val(user.PhoneNumber);
                $("#TxtDateOfBirth").val(new Date(parseInt(user.DateOfBirth)));
                $("#TxtFatherName").val(user.FatherName);
                $("#TxtMotherName").val(user.MotherName);

                if (user.PresentAddress != null) {
                    $("#TxtPresentAddressLine").val(user.PresentAddress.Street);
                    $("#TxtPresentCity").val(user.PresentAddress.City);
                    $("#TxtPresentPincode").val(user.PresentAddress.Pincode);
                    $("#DdlPresentCountry").val(user.PresentAddress.CountryID, function () {
                        populateState(user.PresentAddress.CountryID, "DdlPresentCountry")
                        $("#DdlPresentState").val(user.PresentAddress.StateID);
                    });
                }

                if (user.PermanentAddress != null) {
                    $("#TxtPermanentAddressLine").val(user.PermanentAddress.Street);
                    $("#TxtPermanentCity").val(user.PermanentAddress.City);
                    $("#TxtPermanentPincode").val(user.PermanentAddress.Pincode);
                    $("#DdlPermanentCountry").val(user.PermanentAddress.CountryID);
                    populateState(user.PermanentAddress.CountryID, "DdlPermanentCountry")
                    $("#DdlPermanentState").val(user.PermanentAddress.StateID);
                }

                var urlParam = new URLSearchParams(window.location.search);
                var isAdmin = urlParam.get('isAdmin');
                var isLoggedIn = urlParam.get('isLoggedIn');

                if (isAdmin == 'true') {
                    $("#UpdateUserLink").show();
                    $("#UserDetailsLink").show();
                    $("#AddRoleToUser").show();
                } else {
                    $("#UpdateUserLink").hide();
                    $("#UserDetailsLink").hide();
                    $("#AddRoleToUser").hide();
                }

                if (isLoggedIn) {
                    $("#logoutLink").show();
                } else {
                    $("#logoutLink").hide();
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching user details: " + error);
        }
    });
}

function redirectAfterLoad(userId, isAdmin, isLoggedIn) {
    setTimeout(function () {
        window.location.href = 'UserDetails_v2.aspx?UserID=' + userId + '&isAdmin=' + isAdmin + '&isLoggedIn=' + isLoggedIn;
    }, 1000);
}

function emailExists() {
    var email = $("#TxtEmailID");

    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/IsEmailExists",
        data: JSON.stringify({ email: email.val() }),
        contentType: "application/json",

        success: function (response) {
            if (response) {
                $("#LblEmailExists").show();
                $("#LblEmailExists").text("Email Id already exists");
            }
            else {
                $("#LblEmailExists").hide();
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching user details: " + error);
        }
    });
}

function uploadFiles(userId) {
    var documentType = $("#DocumentDropDown").val();
    $.ajax({
        url: 'UserDetails_v2.aspx/FileUploadUrl()',
        type: 'POST',
        data: JSON.stringify({ userId: userId, documentType: documentType }),
        success: function (response) {
            window.location.href = response;
        },
        error: function (error) {
            return error;
        }
    });
}