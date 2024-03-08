
function loginUser() {
    var userName = $("#TxtUserName").val();
    var password = $("#TxtPassword").val();

    $.ajax({
        type: "POST",
        url: "Login.aspx/LoginUser",
        data: JSON.stringify({ username: userName, password: password }),
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

        error: function (xhr, status, error) {
            console.error("Error Logging in " + error + " " + status + " " + xhr);
        }
    });
}

function newUser() {
    window.location.href = 'UserDetailForm.aspx';
}
function redirectToPage(userId) {
    window.location.href = 'UserDetailForm?UserID=' + userId;
}
