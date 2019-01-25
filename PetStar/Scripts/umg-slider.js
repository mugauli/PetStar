
var itemSelected = 0;
var itemNumber = 4;
var items = 0;
var itemWidth = 0;
var marginItem = 0;
var widthMargin = 0;
var contWidth = 0;
var maxPrev = items - itemNumber;


//Se agrega el ancho dinamico al div contenedor
$("#conMainGalery").width(contWidth);

var sizingItemSlider = function () {
    //debugger;

    var paddingItemSt = 8;

    if ($(window).width() < 410) {
        itemNumber = 2;
        paddingItemSt = 4;
        
    }
    else if ($(window).width() < 570)
        itemNumber = 3;
    else
        itemNumber = 4;

    


    $(".itemGaleriaNews").css("padding", (paddingItemSt / 2) + "px");

    var widtVideoCont = $("#videoGal").width();
    var widthItem = (widtVideoCont * 0.9) / itemNumber;
    $(".itemGaleriaNews img").width(widthItem - (paddingItemSt * 2));
    $(".itemGaleriaNews").width(widthItem - 8);
}

var sizingSlider = function () {

    itemSelected = 0;
    $("#containerGaleriaNews").css("margin-left", 0);
    items = $(".itemGaleriaNews").length;
    itemWidth = $(".itemGaleriaNews").first().width();
    marginItem = parseFloat($(".itemGaleriaNews").first().css("padding-left").replace("px", "")) * 2;
    widthMargin = (itemWidth + marginItem);
    contWidth = widthMargin * itemNumber;
    maxPrev = items - itemNumber;
    //Se agrega el ancho dinamico al div contenedor
    $("#conMainGalery").width(contWidth);

}

$(document).ready(function () {

    sizingItemSlider();
    sizingSlider();

    $("#prevNews").click(function () {

        itemSelected++;

        margin = itemSelected * widthMargin;

        if (itemSelected < maxPrev || itemSelected == maxPrev)
            $("#containerGaleriaNews").css("margin-left", margin * (-1));
        else {
            $("#containerGaleriaNews").css("margin-left", 0);
            itemSelected = 0;
        }
    });

    $("#nextNews").click(function () {

        itemSelected--;

        margin = itemSelected * widthMargin;

        if (itemSelected < 0) {
            $("#containerGaleriaNews").css("margin-left", (-1) * (maxPrev * widthMargin));
            itemSelected = maxPrev;
        }

        else
            $("#containerGaleriaNews").css("margin-left", (-1) * margin);
    });

    $(window).resize(function () {
        sizingItemSlider();
        sizingSlider();
    });
});