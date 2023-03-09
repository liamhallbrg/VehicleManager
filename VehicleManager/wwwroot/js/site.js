// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    if (location.pathname.split("/")[1].length == 0)
        $("#nav-home").addClass("active")
    else
        $('nav li a.nav-highlight[href="/' + location.pathname.split("/")[1] + '"]').addClass('active');
});