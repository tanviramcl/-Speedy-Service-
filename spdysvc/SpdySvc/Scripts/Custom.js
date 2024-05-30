function AjaxPageLoader()
{
    $('.wrapper').prepend("<div class='pageloading'></div>").fadeIn();

}
$(document).ajaxComplete(function (event, request, settings) {
    $(".pageloading").fadeOut().remove();
});

$(document).ajaxError(function () {
    $(".pageloading").fadeOut().remove();
});

function getTotalBasketItem() {
    $.ajax({
        url: '/basket/total',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: {},
        success: function (res) {
            var _basket = 'Basket (' + res + ')';
            $("#cart-top").text(_basket);
        }
    });
}

function AjaxLoaderOff()
{
    $(".pageloading").fadeOut().remove();
}



  
