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

var select = $("#ddlScheme");

//var serverResponse = [
//    {
//        Description: "foo"
//    },
//    {
//        Description: "bar"
//    }
//];

//$.each(serverResponse, function (i, item) {
//    select.append("<option value='" + item.Description + "'>" + item.Description + "</option>");
//});

$.ajax({
    type: "GET",
    url: "/Categories/Search",
    contentType: "application/json",
    dataType: "json",
    success: function (response) {
        var ctItems = $("#ctItems");
        ctItems.empty();
        $.each(response, function (i, item) {
            var $tr = $('<li>', '<a>', '#categorySearcher').append(item.categoryName).appendTo(ctItems);
        });
    },
    failure: function (response) {
        alert(response);
    }
});

$(document).ready(function () {
    $(".fancybox").fancybox({
        openEffect: "none",
        closeEffect: "none"
    });
});