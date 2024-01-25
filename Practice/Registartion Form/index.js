$(document).ready(function () {
    $('#user-form').submit(function (e) {
        e.preventDefault();
        let isFormValid = true;
        const fields = $('p[data_attribute_error]');
        for (let i = 0; i < fields.length; i++) {
            let fieldname = fields.eq(i);
            let inputField = $('#' + fieldname.attr('id').substr(0, fieldname.attr('id').length - 9));
            let inputValue = inputField.val();

            if (inputField.hasClass('imp')) {
                if (inputValue.trim() === "") {
                    isFormValid = false;
                    setValidationStyle(
                        $("#" + inputField.attr('data-validation-msg-holder-id')),
                        $(inputField.closest('.input-field')),
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
            debugger
        }
        if (isFormValid) {
            const fieldsDob = $('input[id$="_dob"]');
            const fieldsPhone = $('input[id$="_phone"]');
            const fieldsEmail = $('input[id$="_email"]');
            // const fieldsRadio = $('input[id$="_radio"]');

            for (let i = 0; i < fieldsDob.length; i++) {
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

            // for (let i = 0; i < fieldsRadio.length; i++) {
            //     let inputField = fieldsRadio.eq(i).attr('id');
            //     if (!isValidRadio(inputField)) {
            //         isFormValid = false;
            //     }
            // }
        }

        if (isFormValid) {
            const fields = $('div[data_attribute_display]');
            for (let i = 0; i < fields.length; i++) {
                let fieldname = fields.eq(i);
                let inputField = fieldname.find('input');
                let inputValue = inputField.val();
                let inputLabel = $('#lbl_' + fieldname.attr('id').substr(4));

                localStorage.setItem(inputLabel.text(), inputValue);
            }

            let popupContent = '';
            for (let i = 0; i < fields.length; i++) {
                let fieldname = fields.eq(i);
                let inputLabel = $('#lbl_' + fieldname.attr('id').substr(4));
                let inputValue = localStorage.getItem(inputLabel.text());
                popupContent += inputLabel.text() + ' : ' + (inputValue || 'N/A') + '<br>';
            }

            $('#popup_content').html(popupContent);
            $('#popup').show();
        }
    });
});

function isValidDob(inputField, inputValue) {
    const years = new Date(new Date() - new Date(inputValue)).getFullYear() - 1970;
    if (years < 20) {
        setValidationStyle(
            $("#" + inputField + "_validate"),
            $("#div_" + inputField),
            $("#lbl_" + inputField),
            "Invalid Date of Birth",
            "red"
        );
        return false;
    }
    else {
        setValidationStyle(
            $("#" + inputField + "_validate"),
            $("#div_" + inputField),
            $("#lbl_" + inputField),
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
            $("#" + inputField + "_validate"),
            $("#div_" + inputField),
            $("#lbl_" + inputField),
            "Invalid Email ID",
            "red"
        );
        return false;
    }
    else {
        setValidationStyle(
            $("#" + inputField + "_validate"),
            $("#div_" + inputField),
            $("#lbl_" + inputField),
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
            $("#" + inputField + "_validate"),
            $("#div_" + inputField),
            $("#lbl_" + inputField),
            "Invalid Phone Number",
            "red"
        );
        return false;
    }
    else {
        setValidationStyle(
            $("#" + inputField + "_validate"),
            $("#div_" + inputField),
            $("#lbl_" + inputField),
            "",
            "black"
        );
        return true;
    }
}
// function isValidRadio(inputField) {

//     let inputOptions = $('input[id$="_' + inputField + "]");
//     alert($(inputOptions).eq(1).attr('id'))
//     let flag = 0;
//     for (let i = 0; i < inputOptions.length; i++) {
//         if (inputOptions.eq(i).prop('checked')) {
//             flag = 1;
//             break;
//         }
//     }

//     if (flag === 0) {
//         setValidationStyle(
//             $("#" + inputField + "_validate"),
//             $("#div_" + inputField),
//             $("#lbl_" + inputField),
//             "Select One ",
//             "red"
//         );
//         return false;
//     }
//     else {
//         setValidationStyle(
//             $("#" + inputField + "_validate"),
//             $("#div_" + inputField),
//             $("#lbl_" + inputField),
//             "",
//             "black"
//         );
//         return true;
//     }
// }

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


// let i = 0;
// fieldPassword.forEach(field => {
//      let inputValue = document.getElementById(inputField).value;
//      if (!isCorrectPassword(inputValuePassword, inputFieldPassword)) {
//          isFormValid = false;
//      }

//     let inputFieldPassword,
//         inputFieldConfirmPassword;
//     (i % 2 == 0 ? inputFieldPassword.push(fieldPassword[i].id) : inputFieldConfirmPassword.push(fieldPassword[i].id))
//     i++

//     alert(inputFieldPassword)
//     alert(inputFieldConfirmPassword)

//      let inputValuePassword = document.getElementById(inputFieldPassword).value;
//      let inputValueConfirmPassword = document.getElementById(inputFieldConfirmPassword).value;