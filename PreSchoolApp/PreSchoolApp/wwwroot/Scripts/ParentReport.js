$(document).ready(function () {

    var name = document.getElementById("FirstName").innerText;
    var delay = document.getElementsByClassName("ButtonStyleLargeAutoSizeLate").value;

    $(".ButtonStyleLargeAutoSizeLate").click(function () {

        alert("Okej. Du blir försenad med " + delay);

    });

    $(".ButtonStyleLargeAutoSizeLate").hover(function () {

        $(this).css('background-color', 'rgba(249,110,175, 0.2)')

    }, function () {


        $(this).css('background-color', '')
    });


    $(".ButtonStyleLargeAutoSizeIll").click(function () {

        alert("Okej." + name + " är sjuk idag");

    });

    $(".ButtonStyleLargeAutoSizeIll").hover(function () {

        $(this).css('background-color', 'rgba(249,110,175, 0.2)')

    }, function () {


        $(this).css('background-color', '')
    });

});