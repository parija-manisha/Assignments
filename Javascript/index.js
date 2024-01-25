document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('register').addEventListener('submit', function (e) {
        e.preventDefault();
        let isFormValid = true;
        const fields = document.querySelectorAll('p[data_attribute_error]');

        debugger

        fields.forEach(function (fieldname) {
            let inputField = document.getElementById(fieldname.id.substr(0, fieldname.id.length - 9));
            if (inputField) {
                let inputValue = inputField.value;

                // alert(inputField.value)

                if (inputField.classList.contains('imp')) {
                    if (inputValue.trim() === "") {
                        isFormValid = false;
                        setValidationStyle(
                            document.getElementById(inputField.getAttribute('data-message-validate')),
                            inputField.closest('.form-group'),
                            document.getElementById(inputField.getAttribute('data-label-id')),
                            "Please fill this field!",
                            "red"
                        );
                    } else {
                        setValidationStyle(
                            document.getElementById(inputField.getAttribute('data-message-validate')),
                            inputField.closest('.form-group'),
                            document.getElementById(inputField.getAttribute('data-label-id')),
                            "",
                            "black"
                        );
                    }
                }
            }
        });

        if (isFormValid) {
            const fieldsDob = document.querySelectorAll('input[id$="_dob"]');
            const fieldsPhone = document.querySelectorAll('input[id$="_phone"]');
            const fieldsEmail = document.querySelectorAll('input[id$="_email"]');

            fieldsDob.forEach(function (inputField) {
                let inputValue = inputField.value;
                if (!isValidDob(inputField, inputValue)) {
                    isFormValid = false;
                }
            });

            fieldsPhone.forEach(function (inputField) {
                let inputValue = inputField.value;
                if (!isValidPhone(inputField, inputValue)) {
                    isFormValid = false;
                }
            });

            fieldsEmail.forEach(function (inputField) {
                let inputValue = inputField.value;
                if (!isValidEmail(inputField, inputValue)) {
                    isFormValid = false;
                }
            });
        }

        if (isFormValid) {
            const fields = document.querySelectorAll('div[data_attribute_display]');
            fields.forEach(function (inputField) {
                let inputValue = inputField.value;
                let inputLabel = document.getElementById(inputField.getAttribute('data-label-id'));
                localStorage.setItem(inputLabel.textContent, inputValue);
            });

            let popupContent = '';
            fields.forEach(function (inputField) {
                let inputLabel = document.getElementById(inputField.getAttribute('data-label-id'));
                let inputValue = localStorage.getItem(inputLabel.textContent);
                popupContent += inputLabel.textContent + ' : ' + (inputValue || 'N/A') + '<br>';
            });

            document.getElementById('popup_content').innerHTML = popupContent;
            document.getElementById('popup').style.display = 'block';
        }
    });

    document.getElementById('check').addEventListener('change', copyAddressDetails);

    function isValidDob(inputField, inputValue) {
        const years = new Date(new Date() - new Date(inputValue)).getFullYear() - 1970;
        if (years < 20) {
            setValidationStyle(
                document.getElementById(inputField.getAttribute('data-message-validate')),
                inputField.closest('.input-field'),
                document.getElementById(inputField.getAttribute('data-label-id')),
                "Invalid Date of Birth",
                "red"
            );
            return false;
        } else {
            setValidationStyle(
                document.getElementById(inputField.getAttribute('data-message-validate')),
                inputField.closest('.input-field'),
                document.getElementById(inputField.getAttribute('data-label-id')),
                "",
                "black"
            );
            return true;
        }
    }

    function isValidPhone(inputField, inputValue) {
        const phonePattern = /^\d+$/;
        if ((inputValue.length < 10) || !inputValue.match(phonePattern)) {
            setValidationStyle(
                document.getElementById(inputField.getAttribute('data-message-validate')),
                inputField.closest('.input-field'),
                document.getElementById(inputField.getAttribute('data-label-id')),
                "Invalid Phone Number",
                "red"
            );
            return false;
        } else {
            setValidationStyle(
                document.getElementById(inputField.getAttribute('data-message-validate')),
                inputField.closest('.input-field'),
                document.getElementById(inputField.getAttribute('data-label-id')),
                "",
                "black"
            );
            return true;
        }
    }

    function isValidEmail(inputField, inputValue) {
        const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (!inputValue.match(emailPattern)) {
            setValidationStyle(
                document.getElementById(inputField.getAttribute('data-message-validate')),
                inputField.closest('.input-field'),
                document.getElementById(inputField.getAttribute('data-label-id')),
                "Invalid Email ID",
                "red"
            );
            return false;
        } else {
            setValidationStyle(
                document.getElementById(inputField.getAttribute('data-message-validate')),
                inputField.closest('.input-field'),
                document.getElementById(inputField.getAttribute('data-label-id')),
                "",
                "black"
            );
            return true;
        }
    }

    function setValidationStyle(validationMessageElement, divElement, labelElement, message, color) {
        if (validationMessageElement) {
            validationMessageElement.innerHTML = message;
            validationMessageElement.style.color = color;
        }
        if (divElement) {
            divElement.style.border = "2px solid " + color;
        }
        if (labelElement) {
            labelElement.style.color = color;
        }
    }

    function copyAddressDetails() {
        if (document.getElementById('check').checked) {
            document.getElementById('select_present_counrty').value = document.getElementById('select_permanent_country').value;
            document.getElementById('select_present_state').value = document.getElementById('select_permanent_state').value;
            document.getElementById('txt_present_city').value = document.getElementById('txt_permanent_city').value;
            document.getElementById('txt_present_pincode').value = document.getElementById('txt_permanent_pincode').value;
            document.getElementById('txt_present_address_one').value = document.getElementById('txt_permanent_address_one').value;
            document.getElementById('txt_present_address_two').value = document.getElementById('txt_permanent_address_two').value;
            document.getElementById('txt_present_landmark').value = document.getElementById('txt_permanent_landmark').value;
        } else {
            const presentAddressFields = document.querySelectorAll('.address_present select, .address_present input, .address_present textarea');
            presentAddressFields.forEach(function (field) {
                field.value = "";
            });
        }
    }
});