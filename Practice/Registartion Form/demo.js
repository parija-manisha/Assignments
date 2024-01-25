$(document).ready(function () {
    $('#user-form').submit(function (e) {
        e.preventDefault();

        let isFormValid = true;

        const fields = $('p[data_attribute_error]');
        for (let i = 0; i < fields.length; i++) {
            let fieldname = fields.eq(i);
            let inputField = $('#' + fieldname.attr('id').substr(0, fieldname.attr('id').length - 9));
            let inputValue = inputField.val();
            alert(inputValue);

            if (inputField.hasClass('imp')) {
                if (inputValue.trim() === "") {
                    isFormValid = false;
                    setValidationStyle(
                        $("#" + inputField.attr('id') + "_validate"),
                        $("#div_" + inputField.attr('id')),
                        $("#lbl_" + inputField.attr('id')),
                        "Please fill this field!",
                        "red"
                    );
                } else {
                    setValidationStyle(
                        $("#" + inputField.attr('id') + "_validate"),
                        $("#div_" + inputField.attr('id')),
                        $("#lbl_" + inputField.attr('id')),
                        "",
                        "black"
                    );
                }
            }
        }

        if (isFormValid) {
            const displayFields = $('div[data_attribute_display]');
            for (let i = 0; i < displayFields.length; i++) {
                let fieldname = displayFields.eq(i);
                let inputField = fieldname.find('input');
                let inputValue = inputField.val();
                let inputLabel = $('#lbl_' + fieldname.attr('id').substr(4));

                localStorage.setItem(inputLabel.text(), inputValue);
            }

            let popupContent = '';
            for (let i = 0; i < displayFields.length; i++) {
                let fieldname = displayFields.eq(i);
                let inputLabel = $('#lbl_' + fieldname.attr('id').substr(4));
                let inputValue = localStorage.getItem(inputLabel.text());
                popupContent += inputLabel.text() + ' : ' + (inputValue || 'N/A') + '<br>';
            }

            $('#popup_content').html(popupContent);
            $('#popup').show();
        }
    });
});

function setValidationStyle(validationMessageElement, divElement, labelElement, message, color) {
    if (validationMessageElement) {
        validationMessageElement.html(message);
    }
    if (divElement) {
        divElement.css("border", "1px solid " + color);  // Corrected this line
    }
    if (labelElement) {
        labelElement.css("color", color);
    }
}

function closePopup() {
    $('#popup').hide();
}
