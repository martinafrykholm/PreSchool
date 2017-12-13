$(document).ready(function () {


    $(".ButtonStyleLargeAutoSizeLate").hover(function () {

        $(this).css('background-color', 'rgba(249,110,175, 0.2)');

    }, function () {


        $(this).css('background-color', '');
    });




    $(".ButtonStyleLargeAutoSizeIll").hover(function () {

        $(this).css('background-color', 'rgba(249,110,175, 0.2)');

    }, function () {


        $(this).css('background-color', '');
    });

    var d = new Date();
    var weekday = new Array("Söndag", "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", "Lördag");
    $("p#GetDay")(weekday[d.getDay()]);

});

    //$(".ButtonStyleLargeAutoSizeIll").click(function () {

    //    alert("Okej." + name + " är sjuk idag");

    //});


    //var name = document.getElementById("FirstName").innerText;
    //var delay = document.getElementsByClassName("ButtonStyleLargeAutoSizeLate").value;

    //$(".ButtonStyleLargeAutoSizeLate").click(function () {

    //    alert("Okej. Du blir försenad med " + delay);

    //});