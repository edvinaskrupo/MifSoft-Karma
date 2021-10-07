//  https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

$("#loginBtnUser").click(function () {

    var url = "/Auth/UserLogIn";
    $.ajax({
        url: url,
        cache: false,
        type: "GET",
        success: function (data) {
            $(data).modal('show');
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
});

$("#loginBtnAdmin").click(function () {

    var url = "/Auth/AdminLogIn";
    $.ajax({
        url: url,
        cache: false,
        type: "GET",
        success: function (data) {
            $(data).modal('show');
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
});

$("#registrationBtn").click(function () {

    var url = "/Auth/SignUp";
    $.ajax({
        url: url,
        cache: false,
        type: "GET",
        success: function (data) {
            $(data).modal('show');
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
});