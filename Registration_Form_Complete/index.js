$(document).ready(function () {
    $('#register').submit(function (e) {
        e.preventDefault();

        let isFormValid = true;

        const field = $('p[data-attribute-error]');
        for (let i = 0; i < field.length; i++) {
            let fieldname = array[i];
            let inputField = $('#' + fieldname.attr('id').substr(0, fieldname.attr('id').length - 9));
            let inputValue = inputField.val();
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
});

function closePopup() {
    $('.modal').hide();
}
