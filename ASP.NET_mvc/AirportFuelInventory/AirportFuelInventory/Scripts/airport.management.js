
$('#exportPdf').on('click', function () {
    var pdf = new jsPDF();
    pdf.autoTable({ html: '#availableFuelTable' });
    pdf.save('AvailableFuelReport.pdf');
});


function isEmailExist() {
    var email = $("#TxtEmailId").val();
    $.ajax({
        type: "POST",
        url: "/SignUp/IsEmailExist",
        data: { email: email },
        success: function (response) {
            if (response) {
                alert("EmailId Already Exists!!");
                $("#RegisterButton").prop("disabled", true);
            } else {
                $("#RegisterButton").prop("disabled", false);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    })
}