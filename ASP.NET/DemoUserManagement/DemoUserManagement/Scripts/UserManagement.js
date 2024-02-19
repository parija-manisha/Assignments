$(document).ready(function () {
    initializePage();

    var userId = getQueryStringParameter('<%= ucNoteControl.ObjectIDName %>');

    if (userId) {
        LoadUserDetails(userId);

        showControls(true);
    } else {
        showControls(false);
    }
});

function getQueryStringParameter(parameterName) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(parameterName);
}

function showControls(show) {
    $("#ucNoteControl").toggle(show);
    $("#DeleteUserButton").toggle(show);
    $("#AddRoleToUser").toggle(show);
}

function initializePage() {
    populateCountries();
    populateRoles();

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
}

$("#btnSaveUser").on("click", function () {
    saveUser();
});

$("#SameAsPermanent").change(function () {
    copyPermanentAddress();
});

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
            onSuccess(result.d);
        },
        error: function (result) {
            onError(result.d);
        }
    });
}

function onSuccess(result) {
    if (result != -1)
        window.location.href = 'UserDetails_v2.aspx?UserID=' + result;
    else
        $("#lblMessage").text("Invalid Username or password");
}

function onError(result) {
    console.error("Error!!");
}

function newUser() {
    window.location.href = 'UserDetails_v2.aspx';
}

function saveUser() {
    var userDetails = {
        "firstName": $("#TxtFirstName").val(),
        "middleName": $("#TxtMiddleName").val(),
        "lastName": $("#TxtLastName").val(),
        "gender": $("#TxtGender").val(),
        "email": $("#TxtEmailId").val(),
        "password": $("#TxtPassword").val(),
        "confirmPassword": $("#TxtConfirmPassword").val(),
        "phone": $("#TxtPhone").val(),
        "dateOfBirth": $('#TxtDateOfBirth').val(),
        "fatherName": $("#TxtFatherName").val(),
        "motherName": $("#TxtMotherName").val()
    };

    var presentAddressDetails = {
        "addressType": "Present",
        "country": $("#DdlPresentCountry").val(),
        "state": $("#DdlPresentState").val(),
        "city": $("#TxtPresentCity").val(),
        "pincode": $("#TxtPresentPincode").val(),
        "street": $("#TxtPresentAddressLine").val(),
    };

    var permanentAddressDetails = {
        "addressType": "Permanent",
        "country": $("#DdlPermanentCountry").val(),
        "state": $("#DdlPermanentState").val(),
        "city": $("#TxtPermanentCity").val(),
        "pincode": $("#TxtPermanentPincode").val(),
        "street": $("#TxtPermanentAddressLine").val(),
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
            populateDropdown("#DdlPermanentCountry", result.d);
            populateDropdown("#DdlPresentCountry", result.d);
        },
        error: function (xhr, status, error) {
            console.error("Error fetching countries: " + error);
        }
    });
}

function populateState(selectCountryId, targetDropdownId) {
    $.ajax({
        type: "POST",
        url: "UserDetails_v2.aspx/PopulateState",
        data: JSON.stringify({ countryId: $("#" + selectCountryId).val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            onSuccess(result.d, targetDropdownId);
        },
        error: function (result) {
            onError(result.d);
        }
    });
}

function onSuccess(data, targetDropdownId) {
    populateDropdown("#" + targetDropdownId, data);
}

function populateDropdown(selector, data) {
    var ddl = $(selector);
    ddl.empty();
    $.each(data, function (index, item) {
        ddl.append($("<option></option>")
            .attr("value", item.Value)
            .text(item.Text));
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
        $("#DdlPresentCountry").val($("#DdlPermanentCountry").val());
        $("#DdlPresentState").val($("#DdlPermanentState").val());
        $("#TxtPresentCity").val($("#TxtPermanentCity").val());
        $("#TxtPresentPincode").val($("#TxtPermanentPincode").val());
        $("#TxtPresentAddressLine").val($("#TxtPermanentAddressLine").val());
    } else {
        $("#DdlPresentCountry").val("");
        $("#DdlPresentState").val("");
        $("#TxtPresentCity").val("");
        $("#TxtPresentPincode").val("");
        $("#TxtPresentAddressLine").val("");
    }
}