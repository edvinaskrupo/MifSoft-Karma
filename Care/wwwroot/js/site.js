//  https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

$("#loginBtn").click(function () {

    var url = "/Auth/Login";
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

