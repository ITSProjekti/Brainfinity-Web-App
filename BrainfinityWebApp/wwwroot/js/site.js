// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#sidebarExpanded").on("click", function (e) {
    e.preventDefault();
    $(".wrapper").toggleClass("expanded");
    if ($(".wrapper").hasClass("expanded")) {
        sessionStorage.setItem("expanded", "expanded");
    } else {
        sessionStorage.setItem("expanded", "");
    }
});

if (sessionStorage.getItem("expanded")) {
    if (sessionStorage.getItem("expanded") == "expanded") {
        $(".wrapper").addClass("expanded");
    } else {
        $(".wrapper").removeClass("expanded");
    }
}
$(document).ready(function () {
    defaultScreen();

    $(".wrapper").show();

    $(window).resize(function () {
        defaultScreen();
    }).resize();

    $("#meni").on("click", ".item", function (e) {
        var itemId = $(this).attr("id");

        sessionStorage.setItem("active", itemId);
    });

    var index = sessionStorage.getItem("active");

    $("#meni").children(".item").each(function () {
        var item = $(this);
        var itemId = item.attr("id");

        var itemIcon = item.children("a").children("img");
        if (index) {
            if (itemId == index) {
                item.addClass("active");
                itemIcon.attr("src", "../images/" + itemId + "-light.png");
            }
        }
    });

    if ($(".breadcrumb").children("li").length == 1) {
        $("#meni").children(".item").each(function () {
            var item = $(this);
            var itemId = item.attr("id");
            var itemIcon = item.children("a").children("img");

            item.removeClass("active");
            itemIcon.attr("src", "../images/" + itemId + "-dark.png");
        })
    }
});

function defaultScreen() {
    if ($(window).width() <= 450) {
        $(".wrapper").addClass("expanded");
    }
}