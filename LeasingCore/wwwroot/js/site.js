// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#filterProduct2').autocomplete({
        source: '/Products/Search'
    });
});

$(document).ready(function () {
    $('#barCategories').get({
        source: '/Categories/Index'
    });
});

$(document).ready(function () {
    $('#cartAmount').get({
        source: '/Cart/GetCartAmount'
    });
});

$(document).ready(function () {
    //FANCYBOX
    //https://github.com/fancyapps/fancyBox
    $(".fancybox").fancybox({
        openEffect: "none",
        closeEffect: "none"
    });
});

$.ajax({
    type: "GET",
    url: "/Cart/GetCartAmount",
    contentType: "application/json",
    dataType: "json",
    success: function (response) {
        var cartAmount = $("#cartAmount");
        cartAmount.empty();
        $('<a href="/Cart/Index" method="get">').append('<span class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span> Cart <span class="badge">' + response + '</span>').appendTo(cartAmount);
    },
    failure: function (response) {
        alert(response);
    }
});

$.ajax({
    type: "GET",
    url: "/Categories/Search",
    contentType: "application/json",
    dataType: "json",
    success: function (response) {
        var categoriesDDL = $("#categoriesDDL");
        categoriesDDL.empty();
        $.each(response, function (i, item) {
            $('<li>').append('<a href="/Products/Index?categoryFilter=' + item.categoryId + '"><span class="glyphicon glyphicon-hand-right"></span> ' + item.categoryName + '</option>').appendTo(categoriesDDL);
        });
    },
    failure: function (response) {
        alert(response);
    }
});