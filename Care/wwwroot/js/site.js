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

function chooseLogin () {
    var loginUser = document.getElementById("LoginUser");
    var loginAdmin = document.getElementById("LoginAdmin");
    var url;

    if (loginUser.checked) {
        url = "/Auth/UserLogIn";
    }
    else if (loginAdmin.checked) {
        url = "/Auth/AdminLogIn";
    }
    else {
        url = "/"
    }
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
};

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