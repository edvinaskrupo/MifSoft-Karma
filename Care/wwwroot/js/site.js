
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


$(document).on('wheel', '#bootsrap-Carousel', (e) => {
    changeByScroll(e.target)
});

function changeByScroll(e) {
    $('.carousel').carousel('next')
}



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


$(document).ready(function () {
    var current_fs, next_fs, previous_fs; 
    var opacity;

    $(".next").click(function () {
        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
        changeStep(next_fs);
    });

    $(".previous").click(function () {

        current_fs = $(this).parent().parent();
        previous_fs = current_fs.prev();

        var current_li = $("#progressbar li").eq($("fieldset").index(current_fs));
        if (current_li.attr('id') == 'method') {
            $(current_li).parent().removeClass("three-li");
            $(current_li).parent().addClass("two-li");
            current_li.remove();
        }
        else {
            current_li.removeClass("active");
        }
        $(current_fs).children().hide();
        changeStep(previous_fs);
    });


    $(".role-button").click(function (e) {
        current_fs = $(this).parent().parent();
        next_fs = current_fs.next();

        if ($(this).val() == "Login as User") {

            var ul = document.getElementById("progressbar");
            updateProgressBar(ul);
            ul.classList.remove('two-li');
            ul.classList.add('three-li');

            changeStep(next_fs, 0, "#method-fs");
        }
        else {
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
            changeStep(next_fs, 1, "#admin-login-fs");
        }
    })

    $(".method-button").click(function (e) {
        current_fs = $(this).parent().parent().parent();
        next_fs = current_fs.next();
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        if ($(this).val() == "Log In") {
            changeStep(next_fs, 0, "#user-login-fs");

        }
        else {
            changeStep(next_fs, 1, "#user-register-fs");
        }
    })

        
    function changeStep(e, value, id) {
        $(e).attr("value", value);
        $(id).show();
        $(e).show();

        current_fs.animate({ opacity: 0 }, {
            step: function (now) {

                opacity = 1 - now;
                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                $(e).css({ 'opacity': opacity });
            },
            duration: 600
        });
    }

    function updateProgressBar(e) {

        var li = document.createElement("li");
        var strong = document.createElement("STRONG");
        strong.appendChild(document.createTextNode("Method"))

        li.setAttribute("id", "method");
        li.classList.add('active');
        li.appendChild(strong);
        e.insertBefore(li, e.firstElementChild.nextSibling);
    }
});
