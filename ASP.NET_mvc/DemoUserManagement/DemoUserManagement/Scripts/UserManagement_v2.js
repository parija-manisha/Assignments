$(function () {
    $("#DdlPermanentCountry").on("change", function () {
        var selectCountryId = $(this).val();
        populateStates(selectCountryId, "#DdlPermanentState");
    });

    $("#DdlPresentCountry").on("change", function () {
        var selectCountryId = $(this).val();
        populateStates(selectCountryId, "#DdlPresentState");
    });

    $("#UploadButton").on("click", function () {
        uploadFile();
    });

    var userId = getUserIdFromUrl();
    loadExistingDocuments(userId);
    loadExistingNotes(userId);
});

function getUserIdFromUrl() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.has("UserID") ? parseInt(urlParams.get("UserID")) : 0;
}

function populateStates(countryId, targetDropdownId, callback) {
    $.ajax({
        type: "POST",
        url: "/Registration_v2/GetStates",
        data: { countryId: countryId },
        success: function (result) {
            if (result) {
                populateDropdown(targetDropdownId, result, "StateID", "StateName");

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

function uploadFile() {
    var formData = new FormData($('#UserDetailForm')[0]);

    $.ajax({
        type: 'POST',
        url: '/Registration_v2/UploadFile',
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
            row.append($("<td>").text(note.ObjectType));
            row.append($("<td>").text(note.NoteText));
            row.append($("<td>").text(formatDate(note.TimeStamp)));

            tableBody.append(row);
        });
    } else {
        tableBody.append("<tr><td colspan='5'>No Records Found!!</td></tr>");
    }
}

function addNote() {
    var note = $("#AddNote").val();
    var userId = getUserIdFromUrl();
    var objectType = $("#PageName").val();
    $.ajax({
        type: "POST",
        url: "/Registration_v2/AddNote",
        data: { userId: userId, note: note, objectType: objectType },
        success: function (response) {
            loadExistingNotes(userId)
            $("#AddNote").val() = "";
        },
        error: function (xhr, status, error) {
            console.error("Error fetching notes: " + error);
        }
    })
}

function isEmailExists() {
    var email = $("#TxtEmailID").val();
    $.ajax({
        type: "POST",
        url: "/Registration_v2/IsEmailExists",
        data: { email: email },
        success: function (response) {
            if (response) {
                alert("EmailId Already Exists!!");
                $("#SaveUserButton").prop("disabled", true);
            } else {
                $("#SaveUserButton").prop("disabled", false);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    })
}


function copyPermanentAddress() {
    var sameAsPermanent = $("#SameAsPermanent").prop("checked");

    if (sameAsPermanent) {
        $("#DdlPresentCountry").val($("#DdlPermanentCountry").val());
        var presentCountryId = $("#DdlPresentCountry").val();
        populateStates(presentCountryId, "#DdlPresentState", function () {
            $("#DdlPresentState").val($("#DdlPermanentState").val());
        });
        $("#TxtPresentCity").val($("#TxtPermanentCity").val());
        $("#TxtPresentPincode").val($("#TxtPermanentPincode").val());
        $("#TxtPresentAddressLine").val($("#TxtPermanentAddressLine").val());
    } else {
        $("#DdlPresentCountry").val('');
        $("#DdlPresentState").val('');
        $("#TxtPresentCity").val('');
        $("#TxtPresentPincode").val('');
        $("#TxtPresentAddressLine").val('');
    }
}

function formatDate(dateString) {
    if (dateString) {
        var formattedDate = dateString.substring(6, 19);
        var date = new Date(parseInt(formattedDate));
        return date.toISOString().split('T')[0];
    }
    return "";
}