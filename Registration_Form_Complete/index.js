$(document).ready(function () {
    $('#register').submit(function (e) {
        e.preventDefault();
        let isFormValid = true;
        const fields = $('p[data-message-error]');
        for (let i = 0; i < fields.length; i++) {
            let fieldname = fields.eq(i);
            let inputField = $("#" + fieldname.attr('id').substr(0, fieldname.attr('id').length - 8));
            let inputValue = inputField.val();
            if (inputField.hasClass('imp')) {
                if (inputValue.trim() === "") {
                    isFormValid = false;
                    setValidationStyle(
                        $("#" + inputField.attr('data-message-validate')),
                        $(inputField),
                        $("#" + inputField.attr('data-label-id')),
                        "Please fill this field!",
                        "red"
                    );
                } else {
                    setValidationStyle(
                        $("#" + inputField.attr('data-message-validate')),
                        $(inputField),
                        $("#" + inputField.attr('data-label-id')),
                        "",
                        "black"
                    );
                }
            }
        }

        if (isFormValid) {
            const fieldsDob = $('input[id$="DOB"]');
            const fieldsPhone = $('input[id$="Phone"]');
            const fieldsEmail = $('input[id$="Email"]');

            for (let i = 0; i < fieldsDob.length; i++) {
                debugger
                let inputField = fieldsDob.eq(i).attr('id');
                let inputValue = $('#' + inputField).val();
                if (!isValidDob(inputField, inputValue)) {
                    isFormValid = false;
                }
            }

            for (let i = 0; i < fieldsPhone.length; i++) {
                let inputField = fieldsPhone.eq(i).attr('id');
                let inputValue = $('#' + inputField).val();
                if (!isValidPhone(inputField, inputValue)) {
                    isFormValid = false;
                }
            }

            for (let i = 0; i < fieldsEmail.length; i++) {
                let inputField = fieldsEmail.eq(i).attr('id');
                let inputValue = $('#' + inputField).val();
                if (!isValidEmail(inputField, inputValue)) {
                    isFormValid = false;
                }
            }
        }

        if (isFormValid) {
            const fields = $('div[data-attribute-display]');
            for (let i = 0; i < fields.length; i++) {
                let fieldname = fields.eq(i);
                let inputField = fieldname.find('input');
                let inputValue = inputField.val();
                let inputLabel = $('#' + inputField.attr('data-label-id'));
                localStorage.setItem(inputLabel.text(), inputValue);
            }

            let popupContent = '';
            for (let i = 0; i < fields.length; i++) {
                let fieldname = fields.eq(i);
                let inputField = fieldname.find('input');
                let inputLabel = $('#' + inputField.attr('data-label-id'));
                let inputValue = localStorage.getItem(inputLabel.text());
                popupContent += inputLabel.text() + ' : ' + (inputValue || 'N/A') + '<br>';
            }

            $('#popup_content').html(popupContent);
            $('.modal').show();
        }
    });
    $("#check").change(function () {
        copyAddressDetails();
    });
});

function isValidDob(inputField, inputValue) {
    const years = new Date(new Date() - new Date(inputValue)).getFullYear() - 1970;
    if (years < 20) {
        setValidationStyle(
            $("#" + inputField.attr('data-message-validate')),
            $(inputField.closest('.input-field')),
            $("#" + inputField.attr('[data-label-id]')),
            "Invalid Date of Birth",
            "red"
        );
        return false;
    }
    else {
        setValidationStyle(
            $("#" + inputField.attr('data-message-validate')),
            $(inputField.closest('.input-field')),
            $("#" + inputField.attr('[data-label-id]')),
            "",
            "black"
        );
        return true;
    }
}

function isValidPhone(inputField, inputValue) {
    const phonePattern = /^\d+$/;
    if ((inputValue.length < 10) & (!((inputValue).match(phonePattern)))) {
        setValidationStyle(
            $("#" + inputField.attr('data-message-validate')),
            $(inputField.closest('.input-field')),
            $("#" + inputField.attr('[data-label-id]')),
            "Invalid Email ID",
            "red"
        );
        return false;
    }
    else {
        setValidationStyle(
            $("#" + inputField.attr('data-message-validate')),
            $(inputField.closest('.input-field')),
            $("#" + inputField.attr('[data-label-id]')),
            "",
            "black"
        );
        return true;
    }
}

function isValidEmail(inputField, inputValue) {
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!((inputValue).match(emailPattern))) {
        setValidationStyle(
            $("#" + inputField.attr('data-message-validate')),
            $(inputField.closest('.input-field')),
            $("#" + inputField.attr('data-label-id')),
            "Invalid Phone Number",
            "red"
        );
        return false;
    }
    else {
        setValidationStyle(
            $("#" + inputField.attr('data-message-validate')),
            $(inputField.closest('.input-field')),
            $("#" + inputField.attr('data-label-id')),
            "",
            "black"
        );
        return true;
    }
}

function setValidationStyle(validationMessageElement, divElement, labelElement, message, color) {
    if (validationMessageElement) {
        validationMessageElement.html(message);
        validationMessageElement.css("color", color);
    }
    if (divElement) {
        divElement.css("border", "2px solid " + color);
    }
    if (labelElement) {
        labelElement.css("color", color);
    }
}


function closePopup() {
    $('#popup').hide();
}

function copyAddressDetails() {
    if ($("#check").is(":checked")) {
        $("#select_present_counrty").val($("#select_permanent_country").val());
        $("#select_present_state").val($("#select_permanent_state").val());
        $("#txt_present_city").val($("#txt_permanent_city").val());
        $("#txt_present_pincode").val($("#txt_permanent_pincode").val());
        $("#txt_present_address_one").val($("#txt_permanent_address_one").val());
        $("#txt_present_address_two").val($("#txt_permanent_address_two").val());
        $("#txt_present_landmark").val($("#txt_permanent_landmark").val());
    } else {
        $(".address_present select, .address_present input, .address_present textarea").val("");
    }
}