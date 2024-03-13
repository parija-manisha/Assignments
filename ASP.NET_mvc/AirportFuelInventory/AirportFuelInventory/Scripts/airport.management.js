
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
    var margins = {
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

    var fileName = 'AvailableFuelReport_' + getFormattedDateTime() + '.pdf';

    var formData = new FormData();
    formData.append('fileName', fileName);
    formData.append('data', new Blob([doc.output('blob')], { type: 'application/pdf' }));

    $.ajax({
        type: 'POST',
        url: '/DownloadPdf/SavePDF',
        data: formData,
        processData: false,
        contentType: false,
        success: function () {
            openDocument(formData);
        },
        error: function (error) {
            console.error('Error saving PDF: ' + error.statusText);
        }
    });
}

function generateFuelConsumptionReport() {
    var doc = new jsPDF('p', 'pt', 'letter');
    var pageHeight = 0;
    pageHeight = doc.internal.pageSize.height;
    var margins = {
        top: 150,
        bottom: 60,
        left: 40,
        right: 40,
        width: 600
    };
    var y = 20;
    doc.setLineWidth(2);
    doc.text(200, y = y + 30, "Fuel Consumption Summary Report");

    var startY = 70;

    var autoTableOptions = {
        startY: startY,
        theme: 'grid',
        columnStyles: {
            0: { cellWidth: 180 },
            1: { cellWidth: 180 },
            2: { cellWidth: 180 }
        },
        styles: {
            minCellHeight: 40
        },
    };

    console.log($('#FuelConsumptionTable').html())

    doc.autoTable({
        html: '#FuelConsumptionTable',
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
    });

    var currentY = doc.autoTable.previous.finalY;
    startY = currentY + 20;

    $('#FuelConsumptionTable tbody tr td').each(function () {
        var innerTable = $(this).find('#InnerTable').html();
        console.log(innerTable)
        doc.autoTable({
            html: innerTable,
            startY: startY,
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
            },
        });

        startY = doc.autoTable.previous.finalY;
    });
    var fileName = 'FuelConsumptionReport_' + getFormattedDateTime() + '.pdf';



    var formData = new FormData();
    formData.append('fileName', fileName);
    formData.append('data', new Blob([doc.output('blob')], { type: 'application/pdf' }));

    $.ajax({
        type: 'POST',
        url: '/DownloadPdf/SavePDF',
        data: formData,
        processData: false,
        contentType: false,
        success: function () {
            openDocument(formData);
        },
        error: function (error) {
            console.error('Error saving PDF: ' + error.statusText);
        }
    });
}

function openDocument(formData) {
    $.ajax({
        type: 'POST',
        url: '/DownloadPdf/OpenReport',
        data: formData,
        processData: false,
        contentType: false,
        success: function () {
            window.open('/DownloadPdf/OpenReport?fileName=' + formData.get('fileName'), '_blank');
        },
        error: function (error) {
            console.error('Error saving PDF: ' + error.statusText);
        }
    });
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
