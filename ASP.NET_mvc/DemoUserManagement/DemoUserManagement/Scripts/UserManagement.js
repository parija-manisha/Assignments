$(function () {
    populateCountries();

    populateDocument();

    var start = 0;
    var length = 5;

    $("#UserListTable").on("click", function () {
        var sortExpression = $(this).data("sortExpression");

        if ($(this).hasClass("SortAsc")) {
            sortExpression = "DESC";
            $(this).removeClass("SortAsc").addClass("SortDesc");
        } else if ($(this).hasClass("SortDesc")) {
            sortExpression = "ASC";
            $(this).removeClass("SortDesc").addClass("SortAsc");
        } else {
            $(this).addClass("SortAsc");
        }
        userList(start, length, sortColumn, sortDirection);
    });


    $("#pagination").on("click", ".pagination a", function () {
        var buttonClicked = $(this).parent().data("action");

        if (buttonClicked === "next") {
            start += length;
        } else if (buttonClicked === "previous") {
            start -= length;
            if (start < 0) {
                start = 0;
            }
        }

        userList(start, length, sortColumn, sortDirection);
    });

    $("#UserListTable th").on("click", function () {
        var columnClicked = $(this).data("column");

        sortDirection = (sortColumn === columnClicked && sortDirection === "asc") ? "desc" : "asc";

        sortColumn = columnClicked;
        userList(start, length, sortColumn, sortDirection);
    });

    $("#DdlPermanentCountry").on("change", function () {
        var selectCountryId = $(this).val();
        populateStates(selectCountryId, "#DdlPermanentState");
    });

    $("#DdlPresentCountry").on("change", function () {
        var selectCountryId = $(this).val();
        populateStates(selectCountryId, "#DdlPresentState");
    });

    var userId = getUserIdFromUrl();
    loadUserDetails(userId);
    loadExistingNotes(userId);
    loadExistingDocuments(userId);

    function getUserIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.has("UserID") ? parseInt(urlParams.get("UserID")) : 0;
    }

    $("#UploadButton").on("click", function () {
        uploadFile();
    })

    $("#AddSuccess").on("click", function () {
        addNote();
    })

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

function populateStates(countryId, targetDropdownId, callback) {
    $.ajax({
        type: "POST",
        url: "/Registration/GetUserStates",
        data: { countryId: countryId },
        success: function (result) {
            console.log("Received state data:", result);

            if (result && result.Data) {
                populateDropdown(targetDropdownId, result.Data, "StateID", "StateName");

                if (typeof callback === "function") {
                    callback();
                }
            } else {
                console.error("Invalid state data format.");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching state data: " + errorThrown);
        }
    });
}

function populateDocument() {
    $.ajax({
        type: "GET",
        url: "/Registration/DocumentList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            populateDropdown("#DocumentDropdown", result, "DocumentID", "DocumentName");
        },
        error: function (xhr, status, error) {
            console.error("Error fetching document type: " + error);
            console.log(xhr.responseText);
        }

    })
}

function populateDropdown(selector, data, valueKey, textKey) {
    var dropdown = $(selector);
    dropdown.empty();

    dropdown.append($("<option></option>").val("").text("Select"));

    if (Array.isArray(data)) {
        $.each(data, function (index, item) {
            var option = $("<option></option>").val(item[valueKey]).text(item[textKey]);
            dropdown.append(option);
        });
    } else {
        var option = $("<option></option>").val(data[valueKey]).text(data[textKey]);
        dropdown.append(option);
    }
}

function loadUserDetails(userID) {
    $.ajax({
        type: "GET",
        url: "/Registration/GetUserDetails",
        data: { userId: userID },
        contentType: "application/json",
        success: function (response) {
            var user = response;

            if (user != null) {
                $("#TxtFirstName").val(user.FirstName);
                $("#TxtLastName").val(user.LastName);
                $("#TxtGender").val(user.Gender);
                $("#TxtEmailID").val(user.Email);
                $("#TxtPassword").val(user.Password);
                $("#TxtConfirmPassword").val(user.ConfirmPassword)
                $("#TxtPhone").val(user.PhoneNumber);
                $("#TxtDateOfBirth").val(formatDate(user.DateOfBirth));
                $("#TxtFatherName").val(user.FatherName);
                $("#TxtMotherName").val(user.MotherName);

                if (user.PresentAddress != null) {
                    $("#TxtPresentAddressLine").val(user.PresentAddress.Street);
                    $("#TxtPresentCity").val(user.PresentAddress.City);
                    $("#TxtPresentPincode").val(user.PresentAddress.Pincode);
                    $("#DdlPresentCountry").val(user.PresentAddress.CountryID);
                    populateStates(user.PresentAddress.CountryID, "#DdlPresentState", function () {
                        $("#DdlPresentState").val(user.PresentAddress.StateID);
                    });
                }

                if (user.PermanentAddress != null) {
                    $("#TxtPermanentAddressLine").val(user.PermanentAddress.Street);
                    $("#TxtPermanentCity").val(user.PermanentAddress.City);
                    $("#TxtPermanentPincode").val(user.PermanentAddress.Pincode);
                    $("#DdlPermanentCountry").val(user.PermanentAddress.CountryID);
                    populateStates(user.PermanentAddress.CountryID, "#DdlPermanentState", function () {
                        $("#DdlPermanentState").val(user.PermanentAddress.StateID);
                    });
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching user details: " + error);
            console.log(xhr.responseText);
        }
    });
}

function userList(start, length, sortColumn, sortDirection) {
    $.ajax({
        type: "GET",
        url: "/UserList/GetAllUsers",
        data: { start: start, length: length, sortColumn: sortColumn, sortDirection: sortDirection },
        contentType: "application/json",
        success: function (data) {
            var tableBody = $("#UserListTable tbody");
            tableBody.empty();
            var serialNumberStart = start + 1;

            $.each(data.data, function (index, user) {
                var row = $("<tr>");
                var customSerialNumber = serialNumberStart + index;
                row.append($("<td>").text(customSerialNumber));
                row.append($("<td>").text(user.FirstName + " " + (user.MiddleName != null ? user.MiddleName : "") + " " + user.LastName));
                row.append($("<td>").text(user.Gender));
                row.append($("<td>").text(user.Email));
                row.append($("<td>").text(user.PhoneNumber));
                row.append($("<td>").text(formatDate(user.DateOfBirth)));
                row.append($("<td>").text(user.FatherName));
                row.append($("<td>").text(user.MotherName));

                var editLink = $("<a>").text("Edit").attr("href", "/Registration/UserDetailForm?UserID=" + user.UserID);
                row.append($("<td>").append(editLink));

                tableBody.append(row);
            });
        },
        error: function () {
            console.log("Error");
        }
    });
}

function loadExistingNotes(userId) {
    $.ajax({
        type: "GET",
        url: "/PartialViews/GetNotes",
        data: { userId: userId, pageName: 1 },
        contentType: "application/json",
        success: function (response) {
            populateNoteList(response);
        },
        error: function (xhr, status, error) {
            console.error("Error fetching notes: " + error);
        }
    });
}

function populateNoteList(notes) {
    var tableBody = $("#NoteListTable tbody");
    tableBody.empty();

    if (notes && notes.length > 0) {
        var serialNumberStart = 1;

        $.each(notes, function (index, note) {
            var row = $("<tr>");
            var customSerialNumber = serialNumberStart + index;
            row.append($("<td>").text(customSerialNumber));
            row.append($("<td>").text(note.ObjectID));
            row.append($("<td>").text(1));
            row.append($("<td>").text(note.NoteText));
            row.append($("<td>").text(Date.now.toString("yyyy/MM/dd tt")));

            tableBody.append(row);
        });
    } else {
        tableBody.append("<tr><td colspan='5'>No Records Found!!</td></tr>");
    }
}

function uploadFile() {
    var formData = new FormData($('#UserDetailForm')[0]);

    $.ajax({
        type: 'POST',
        url: '/Registration/UploadFile',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            console.log(response);
            if (response.success) {
                alert('File uploaded successfully!');
            } else {
                alert('Error: ' + response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error uploading file: " + error);
        }
    });
}

function addNote() {
    var noteText = $("#AddNote").val();
    var userId = getUserIdFromUrl();
    var pageName = 1;
    $.ajax({
        type: "POST",
        url: "/PartialViews/AddNote",
        data: { userId: userId, pageName: pageName, noteText: noteText },
        success: function (response) {
            alert("Note Added Successfully!");
        },

        error: function (xhr, status, error) {
            console.error("Error in saving note: " + error);
        }
    })
}

function loadExistingDocuments(userId) {
    $.ajax({
        type: "GET",
        url: "/PartialViews/GetDocuments",
        data: { userId: userId, pageName: 1 },
        contentType: "application/json",
        success: function (response) {
            populateDocumentList(response);
        },
        error: function (xhr, status, error) {
            console.error("Error fetching Documents: " + error);
        }
    });
}

function populateDocumentList(documents) {
    var tableBody = $("#DocumentListTable tbody");
    tableBody.empty();

    if (documents && documents.length > 0) {
        var serialNumberStart = 1;

        $.each(documents, function (index, document) {
            var row = $("<tr>");
            var customSerialNumber = serialNumberStart + index;
            row.append($("<td>").text(customSerialNumber));
            row.append($("<td>").text(document.ObjectID));
            row.append($("<td>").text(document.ObjectType));
            row.append($("<td>").text(document.DocumentType));
            row.append($("<td>").text(document.DocumentNameOnDisk));
            var downloadLink = $("<a>")
                .attr("href", "/PartialViews/DownloadDocument?documentId=" + document.DocumentID)
                .attr("target", "_blank")
                .text(document.DocumentOriginalName);
            row.append($("<td>").append(downloadLink));
            tableBody.append(row);
        });
    } else {
        tableBody.append("<tr><td colspan='6'>No Records Found!!</td></tr>");
    }
}

function isEmailExists() {
    var email = $("#TxtEmailID").val();
    $.ajax({
        type: "POST",
        url: "/Registration/IsEmailExists",
        data: { email: email },
        success: function (response) {
            alert("EmailId Already Exists!!");
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    })
}

function formatDate(dateString) {
    if (dateString) {
        var formattedDate = dateString.substring(6, 19);
        var date = new Date(parseInt(formattedDate));
        return date.toISOString().split('T')[0];
    }
    return "";
}

