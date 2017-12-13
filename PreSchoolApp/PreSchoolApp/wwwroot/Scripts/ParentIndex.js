$(document).ready(function () {


    $(".ButtonStyleLargeAutoSize").hover(function () {

        $(this).css('background-color', 'rgba(249,110,175, 0.2)');

    }, function () {


        $(this).css('background-color', '');
    });


    $(".ButtonStyleLargeAutoSize").hover(function () {

        $(this).css('background-color', 'rgba(249,110,175, 0.2)');

    }, function () {


        $(this).css('background-color', '');
    });

    var d = new Date();
        var weekday = new Array("Söndag", "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", "Lördag");
        $("p#GetDay").append(weekday[d.getDay()]);
    
});


