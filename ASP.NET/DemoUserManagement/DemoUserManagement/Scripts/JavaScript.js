
$(document).ready(function () {
    alert("Hello");
    var currentPage = getQueryStringValue('page');

    $('#navBarLinks a.nav-link').each(function () {
        if ($(this).attr('href').endsWith(currentPage)) {
            $(this).addClass('active');
            $(this).addClass('fw-bold'); 
            return false; 
        }
    });
});

function getQueryStringValue(key) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(key);
}
