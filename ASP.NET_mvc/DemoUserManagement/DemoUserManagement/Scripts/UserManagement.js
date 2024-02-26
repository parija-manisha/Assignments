$(document).ready(function () {
    populateCountries();

    $("#DdlPermanentCountry").on("change", function () {
        var selectCountryId = $("#DdlPermanentCountry").val();
        var targetDropdownId = $("#DdlPermanentCountry").attr("id").replace("Country", "State");
        populateStates(selectCountryId, targetDropdownId);

    });

    $("#DdlPresentCountry").on("change", function () {
        var selectCountryId = $(this).val();
        var targetDropdownId = $(this).attr("id").replace("Country", "State");
        populateStates(selectCountryId, targetDropdownId);
    });
});

function populateCountries() {
    $.ajax({
        type: "GET",
        url: "/Registration/CountryList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            populateDropdown("#DdlPermanentCountry", result, "CountryID", "CountryName");
            populateDropdown("#DdlPresentCountry", result, "CountryID", "CountryName");
        },
        error: function (xhr, status, error) {
            console.error("Error fetching countries: " + error);
        }
    });
}

function populateStates(countryId, targetDropdownId) {
    if (countryId) {
        $.ajax({
            type: "POST",
            url: "/Registration/StateList",
            data: JSON.stringify({ countryId: countryId }),
            contentType: "application/json",
            success: function (result) {
                populateDropdown(targetDropdownId, result, "StateID", "StateName");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching state data: " + errorThrown);
            }
        });
    } else {
        clearDropdown(targetDropdownId);
    }
}

function populateDropdown(selector, data, valueKey, textKey) {
    var dropdown = $(selector);
    dropdown.empty();

    dropdown.append($("<option></option>").val("").text("Select"));

    if (Array.isArray(data)) {
        $.each(data, function (item) {
            var option = $("<option></option>").val(item[valueKey]).text(item[textKey]);
            dropdown.append(option);
        });
    } else {
        var option = $("<option></option>").val(data[valueKey]).text(data[textKey]);
        dropdown.append(option);
    }
}

function clearDropdown(selector) {
    var dropdown = $(selector);
    dropdown.empty();
}