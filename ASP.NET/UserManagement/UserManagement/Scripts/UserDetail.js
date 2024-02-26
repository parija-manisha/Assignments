$(document).ready(function () {
    populateCountries();

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

    var userId = getUserIdFromUrl();
    loadUserDetails(userId);

    function getUserIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.has("UserID") ? parseInt(urlParams.get("UserID")) : 0;
    }
})

function loginUser() {
    var userName = $("#TxtUserName").val();
    var password = $("#TxtPassword").val();

    var data = { userName: userName, password: password };

    $.ajax({
        type: "POST",
        url: "Login.aspx/LoginUser",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            if (result && result.d) {
                var userSession = result.d;

                if (userSession.UserId != -1) {
                    redirectToPage(userSession.UserId);
                }

                else {
                    $("#LblMessage").innerHTML = "Invalid Username or password";
                }
            }
        },
        error: function () {
            error: function (xhr, status, error) {
                console.error("Error fetching user details: " + error);
            }

        }
    });
}

function newUser() {
    window.location.href = 'UserDetailForm.aspx';
}

function redirectToPage(userId) {
    window.location.href = 'UserDetailForm?UserID=' + userId;
}
