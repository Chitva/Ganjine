function ShowPopupTab(e, ll, f) {
   
    $(e).css("left", $(window).width() / 2 - $(e).width() / 2 + "px");
    $(e).css("top", $(window).height() / 2 - $(e).height() / 2 + "px");
    $("body").append('<div id="Popup"></div>');
    $("#Popup").fadeIn(); $(e).fadeIn();
    $("#OkBtns").bind("click", function () {
        $("#PopUp-Content-DeleteTab").fadeOut(function () {
            $("#Popup").fadeOut(function () {
                $("#Popup").remove(); $.ajax({
                    url: "/Admin/Product/DeleteService",
                    data: { Id: ll, Page: f },
                    type: "POST", dataType: "json",
                    cache: false, success: function (result) {
                        $("#BoxContent1").html(result.Partial);
                    }
                });
            });
            return "true";
        })
    });
    $("#cancelBtns").live("click", function () {
        $("#PopUp-Content-DeleteTab").fadeOut(function () {
            $("#Popup").fadeOut(function () {
                $("#Popup").remove()
            })
        });

    });
    $("a.TB_closeWindowButton").live("click", function () {
        $(e).fadeOut(function () {
            $("#Popup").fadeOut(function () {
                $("#Popup").remove()
            })
        })
    })
}