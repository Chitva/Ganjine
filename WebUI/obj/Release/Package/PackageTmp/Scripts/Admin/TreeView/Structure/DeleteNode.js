function ShowPopupz(e, t, n) {
    $("#DivInfoForm").addClass("HideElement");
    $("#MyFormDelete").attr("action", "/Admin/OrganizationStructure/DeleteOrganizationStructureGroup?Id=" + n);
    $(e).css("left", $(window).width() / 2 - $(e).width() / 2 + "px"); $(e).css("top", $(window).height() / 2 - $(e).height() / 2 + "px");
    $("body").append('<div id="Popup"></div>'); $("#Popup").fadeIn(); $(e).fadeIn(); $("#OkBtn").bind("click", function () {
        $("#PopUp-Content").fadeOut(function () {
            $("#Popup").fadeOut(function () { $("#Popup").remove(); $(t).parent("form").submit() });
            return "true"
        })
    }); $("#cancelBtn").bind("click", function () {
        $("#PopUp-Content").fadeOut(function () {
            $("#Popup").fadeOut(function () {
                $("#Popup").remove()
            })
        })
    }); $("a.TB_closeWindowButton").bind("click", function () {
        $(e).fadeOut(function () {
            $("#Popup").fadeOut(function () {
                $("#Popup").remove()
            })
        })
    })
}