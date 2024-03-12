
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

function generateAvailableFuelReport() {
    var doc = new jsPDF('p', 'pt', 'letter');
    var pageHeight = 0;
    pageHeight = doc.internal.pageSize.height;
    margins = {
        top: 150,
        bottom: 60,
        left: 40,
        right: 40,
        width: 600
    };
    var y = 20;
    doc.setLineWidth(2);
    doc.text(200, y = y + 30, "Available Fuel Summary Report");
    doc.autoTable({
        html: '#availableFuelTable',
        startY: 70,
        theme: 'grid',
        columnStyles: {
            0: {
                cellWidth: 180,
            },
            1: {
                cellWidth: 180,
            },
            2: {
                cellWidth: 180,
            }
        },
        styles: {
            minCellHeight: 40
        }
    })
    doc.save('AvailableFuelReport_' + getFormattedDateTime() + '.pdf');
}

function generateFuelConsumptionReport() {
    var doc = new jsPDF('p', 'pt', 'letter');
    var pageHeight = 0;
    pageHeight = doc.internal.pageSize.height;
    margins = {
        top: 150,
        bottom: 60,
        left: 40,
        right: 40,
        width: 600
    };
    var y = 20;
    doc.setLineWidth(2);
    doc.text(200, y = y + 30, "Fuel Consumption Summary Report");
    doc.autoTable({
        html: '#fuelConsumptionTable',
        startY: 70,
        theme: 'grid',
        columnStyles: {
            0: {
                cellWidth: 180,
            },
            1: {
                cellWidth: 180,
            },
            2: {
                cellWidth: 180,
            }
        },
        styles: {
            minCellHeight: 40
        }
    })
    doc.save('FuelConsumptionReport_' + getFormattedDateTime() + '.pdf');
}

function getFormattedDateTime() {
    var now = new Date();
    var year = now.getFullYear();
    var month = (now.getMonth() + 1).toString().padStart(2, '0');
    var day = now.getDate().toString().padStart(2, '0');
    var hours = now.getHours().toString().padStart(2, '0');
    var minutes = now.getMinutes().toString().padStart(2, '0');
    var seconds = now.getSeconds().toString().padStart(2, '0');

    return year + '-' + month + '-' + day + '_' + hours + '-' + minutes + '-' + seconds;
}
