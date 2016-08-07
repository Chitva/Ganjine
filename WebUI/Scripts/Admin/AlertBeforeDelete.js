function ShowPopupAlertBeforeDelete(e) { $("#PopUp-Content-BoxBeforDelete").css("left", $(window).width() / 2 - $("#PopUp-Content-BoxBeforDelete").width() / 2 + "px"); $("#PopUp-Content-BoxBeforDelete").css("top", $(window).height() / 2 - $("#PopUp-Content-BoxBeforDelete").height() / 2 + "px"); $("body").append('<div id="Popups"></div>'); $("#Popups").fadeIn(); $("#PopUp-Content-BoxBeforDelete").fadeIn(); $("#cancelBtn1").bind("click", function () { $("#PopUp-Content-BoxBeforDelete").fadeOut(function () { $("#Popups").fadeOut(function () { $("#Popups").remove() }) }) }); $("a.TB_closeWindowButton").bind("click", function () { $("#PopUp-Content-BoxBeforDelete").fadeOut(function () { $("#Popups").fadeOut(function () { $("#Popups").remove() }) }) }) }
function Deletes() {

    $("#PopUp-Content-BoxBeforDelete").fadeOut(
        function () { $("#Popups").fadeOut(function () { $("#Popups").remove() }) });
  
    return ShowPopupz('#PopUp-Content', this, $('#HiddenMenuId').val())
}

