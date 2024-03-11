
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
    var doc = new jsPDF();
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
    doc.save('AvailableFuelReport_' + Date.now("yyyy-MM-dd_hh-mm-ss") + '.pdf');
}

//function generateFuelConsumptionReport() {
//    var doc = new jsPDF('p', 'pt', 'letter');
//    pageHeight = doc.internal.pageSize.height;
//    margins = {
//        top: 150,
//        bottom: 60,
//        left: 40,
//        right: 40,
//        width: 600
//    };
//    var y = 20;
//    doc.setLineWidth(2);
//    doc.text(200, y = y + 30, "Fuel Consumption Summary Report");
//    doc.autoTable({
//        html: '#fuelConsumptionTable',
//        startY: 70,
//        theme: 'grid',
//        columnStyles: {
//            0: {
//                cellWidth: 180,
//            },
//            1: {
//                cellWidth: 180,
//            },
//            2: {
//                cellWidth: 180,
//            }
//        },
//        styles: {
//            minCellHeight: 40
//        }
//    })
//    doc.save('FuelConsumptionReport_' + Date.now("yyyy-MM-dd_hh-mm-ss") + '.pdf');
//}  