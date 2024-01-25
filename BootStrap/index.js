$(document).ready(function () {
    $('#register').submit(function (e) {
        e.preventDefault();
        let isFormValid = true;
        const fields = $('p[data-attribute-error]');
        for (let i = 0; i < fields.length; i++) {
            let fieldname = fields.eq(i);
            let inputField = $('#' + fieldname.attr('id').substr(0, fieldname.attr('id').length - 9));
            let inputValue = inputField.val();
            if (inputField.hasClass('imp')) {
                if (inputValue.trim() === "") {
                    isFormValid = false;
                    setValidationStyle(
                        $("#" + inputField.attr('data-message-validate')),
                        $(inputField.parent()),
                        $("#" + inputField.attr('data-label-id')),
                        "Please fill this field!",
                        "red"
                    );
                } else {
                    setValidationStyle(
                        $("#" + inputField.attr('data-message-validate')),
                        $(inputField.parent()),
                        $("#" + inputField.attr('data-label-id')),
                        "",
                        "black"
                    );
                }
            }
        }

    })
})