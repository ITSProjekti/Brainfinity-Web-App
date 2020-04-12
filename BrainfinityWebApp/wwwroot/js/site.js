// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#sidebarExpanded").click(function (e) {
    e.preventDefault();

    $(".wrapper").toggleClass("expanded");
});

$(document).ready(function () {
    defaultScreen();

    $(window).resize(function () {
        defaultScreen();
    }).resize();

    $("#meni").on("click", ".item", function (e) {
        var itemActive = $(this);
        var a = itemActive.children("a");

        $(".item").removeClass("active");
        $(itemActive).addClass("active");

        a.children("img").attr("src", "../images/" + a.attr("id") + "-light.png");

        $("#meni > *").not(".item.active").children("a").each(function (i) {
            $(this).children("img").attr("src", "../images/" + $(this).attr("id") + "-dark.png");
        });
    });
});

function defaultScreen() {
    if ($(window).width() <= 450) {
        $(".wrapper").addClass("expanded");
    }
}