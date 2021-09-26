//  https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

$("#loginBtn").click(function () {

    var url = "/Auth/LogIn";
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

