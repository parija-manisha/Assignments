$(document).ready(function () {
    //$('#TxtEmailID').on('input', function () {
    //    checkEmail();
    //});

    $('#SaveUserButton').click(function () {
        saveUser();
    });

    $("#DdlPermanentCountry").change(function () {
        onPermanentCountryChange();
    });

    $("#DdlPresentCountry").change(function () {
        onPresentCountryChange();
    });

    populateCountries();
});

function populateCountries() {
    PageMethods.GetCountries(onGetCountriesSuccess, onGetCountriesError);
}

function onGetCountriesSuccess(response) {
    populateCountryDropdown(response);
}

function onGetCountriesError(error) {
    console.error("Error fetching countries:", error);
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

function onGetStatesSuccess(response) {
    populateDropdown("#DdlPermanentState", response);
    populateDropdown("#DdlPresentState", response);
}

function onGetStatesError(error) {
    console.error("Error fetching states:", error);
}

function populateCountryDropdown(data) {
    populateDropdown("#DdlPermanentCountry", data);
    populateDropdown("#DdlPresentCountry", data);
}

function onPermanentCountryChange() {
    var selectedCountryId = $("#DdlPermanentCountry").val();
    PageMethods.GetStates(selectedCountryId, onGetStatesSuccess, onGetStatesError);
}

function onPresentCountryChange() {
    var selectedCountryId = $("#DdlPresentCountry").val();
    PageMethods.GetStates(selectedCountryId, onGetStatesSuccess, onGetStatesError);
}

// Rest of your existing code...

// Uncomment and use the email validation functions if needed
// function checkEmail() {
//     var email = $("#TxtEmailID").val();
//     console.log("Email to check:", email);
//     if (email.trim() !== "") {
//         PageMethods.IsEmailExists(email, onCheckEmailSuccess, onCheckEmailError);
//     } else {
//         $("#LblEmailExists").text("Email cannot be empty.");
//     }
// }

// Rest of your existing code...
