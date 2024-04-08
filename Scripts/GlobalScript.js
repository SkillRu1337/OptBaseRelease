function logout() {
    $.ajax({
        type: "POST",
        url: "Logout.aspx",
        success: function () {
            window.location.href = "Login.aspx";
        }
    });
}

$(document).ready(function () {
    $('#MainContent_DateFrom').inputmask('99.99.9999', { placeholder: 'дд.мм.гггг' });
    $('#MainContent_DateTo').inputmask('99.99.9999', { placeholder: 'дд.мм.гггг' });
});