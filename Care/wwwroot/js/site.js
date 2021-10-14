//  https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

function openSite(url) {
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

var myelement = document.getElementById("bootsrap-Carousel");

function changeByScroll() {
    $('.carousel').carousel('next')
}

myelement.addEventListener('wheel', function (e) {
    changeByScroll();
});