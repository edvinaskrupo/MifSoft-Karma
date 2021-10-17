
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
}

var myelement = document.getElementById("bootsrap-Carousel");

function changeByScroll() {
    $('.carousel').carousel('next')
}

myelement.addEventListener('wheel', function (e) {
    changeByScroll();
});

function getRemainder(y) {
    return y % 3;
}

function addClassToItem() {

    let posts = $('.carousel-image');
    for (var i = 0; i < posts.length; i++) {
        var index = getRemainder(posts[i].getAttribute('value'));
        $(posts[i]).addClass(index == 0 ? "first" : index == 1 ? "second" : "third");
    }
}

