

var elements = $("#certSlide .col-cert");


$("#certSlide .carousel-inner").html('');
$("#certSlide .carousel-indicators").html('');

var first = true;

$(elements).each(function (key, value) {
    if (first) {
        $("#certSlide .carousel-inner").append("<div class='carousel-item active'>" + value.outerHTML + "</div>");
        first = false;
    }
    else
    {
        $("#certSlide .carousel-inner").append("<div class='carousel-item'>" + value.outerHTML + "</div>");
    }
});

first = true;
$(elements).each(function (key, value) {
    if (first) {
        $("#certSlide .carousel-indicators").append("<li data-target='#certSlide' data-slide-to='" + key + "' class='active'></li>");
        first = false;
    }
    else {
        $("#certSlide .carousel-indicators").append("<li data-target='#certSlide' data-slide-to='" + key + "'></li>");
    }
});

//.getElementsByClassName("carousel-inner")

document.addEventListener('touchmove', function (e) {
    e.preventDefault();
    var touch = e.touches[0];
    var touch2 = e.touches[3];
    alert(touch.pageX + " - " + touch2.pageX);
}, false);