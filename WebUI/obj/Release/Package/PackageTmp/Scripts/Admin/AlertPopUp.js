﻿function ShowPopupAlert(e) {
    $(e).css("left", $(window).width() / 2 - $(e).width() / 2 + "px");
    $(e).css("top", $(window).height() / 2 - $(e).height() / 2 + "px"); $("body").append('<div id="Popup"></div>');
    $("#Popup").fadeIn(); $(e).fadeIn(); $("#OKAlert").on("click", function () {
        $("#PopUp-Content-Alert").fadeOut(function () { $("#Popup").fadeOut(function () { $("#Popup").remove() }) })
    });
    $("a.TB_closeWindowButton").on("click", function () {
        $(e).fadeOut(
            function () { $("#Popup").fadeOut(function () { $("#Popup").remove() }) })
    });
}

