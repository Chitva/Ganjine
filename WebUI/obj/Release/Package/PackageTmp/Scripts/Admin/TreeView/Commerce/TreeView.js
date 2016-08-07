function AddMenuSubMenu(e) {
    debugger;
    if (!$("#divValidationName").hasClass("HideElement"))
    {
        $("#divValidationName").addClass("HideElement");
    }
    var t = $("#IsSelected").val();
    if (t != "1" && e == "SubMenu") {
        return ShowPopupAlert("#PopUp-Content-Alert")
    }
    var HasTab = 0;
    $.ajax({
        url: "/Admin/Commerce/HasTab",
        data: "{ 'id': '" + $("#HiddenMenuId").val() + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        async: false,
        success: function (e) {
            debugger;
            HasTab = e.Count;
        }
    });
    if (HasTab == 0 || e == "Menu") {
        $("#TypeOpr").val(e);
        $("#MenuInfoDetail").find("input[type=text]").val("");
        $("#MenuInfoDetail").addClass("DivMenuInfoDetailShow");
    }
    else if (e == "SubMenu") {
        alert("امکان درج زیر گروه وجود ندارد . گروه جاری داری محتوا می باشد .");
    }
    if (e == "Menu") {
        $("#IsSelected").val("");
        $("span").filter(function () {
            return this.className
        }).removeClass("SelectedLi")
    }
}
function EditMenu() {
    $("#ParentMenuName").val("");
    var e = $("#IsSelected").val();
    if (e == "1") {
        $("#TypeOpr").val("edit");
        $.ajax({
            url: "/Admin/Commerce/GetInfoWorkGroup",
            data: "{ 'id': '" + $("#HiddenMenuId").val() + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            success: function (e) {
                $("#ParentMenuName").val(e.MenuNameTitle);
            }
        });
        $("#MenuInfoDetail").addClass("DivMenuInfoDetailShow")
    } else return ShowPopupAlert("#PopUp-Content-Alert")
}
function DeleteMenu() {
    var e = $("#IsSelected").val();
    if (e == "1") {
        return ShowPopupAlertBeforeDelete('PopUp-Content-BoxBeforDelete');
    } else { return ShowPopupAlert("#PopUp-Content-Alert") }
}
function UpMenu() {
    var e = $("#IsSelected").val();
    if (e == "1") {
        var t = $("span[class='SelectedLi']").parent();
        var n = $("span[class='SelectedLi']").parent().prev();
        var r = n.val();
        var i = t.val();
        t.after(n);
        if (r != null && i != null) {
            ClassSettingUpDown(t, n);
            ChangePriority(i, r)
        }
    }
    else return ShowPopupAlert("#PopUp-Content-Alert")
}
function DownMenu() {
    var e = $("#IsSelected").val();
    if (e == "1") {
        var t = $("span[class='SelectedLi']").parent();
        var n = $("span[class='SelectedLi']").parent().next();
        var r = n.val(); var i = t.val(); t.before(n);
        if (r != null && i != null) {
            ClassSettingUpDown(n, t);
            ChangePriority(i, r)
        }
    }
    else return ShowPopupAlert("#PopUp-Content-Alert")
}
function ClassSettingUpDown(e, t) {
    if (e.hasClass("last") && !t.hasClass("collapsable") && !t.hasClass("expandable")) {
        e.removeClass("last"); t.addClass("last")
    }
    if (!t.hasClass("collapsable") && !t.hasClass("expandable") && (e.hasClass("lastCollapsable") || e.hasClass("lastExpandable"))) {
        e.removeClass("lastCollapsable"); e.removeClass("lastExpandable"); t.addClass("last")
    }
    if ((t.hasClass("collapsable") || t.hasClass("expandable")) && (e.hasClass("lastCollapsable") || e.hasClass("lastExpandable"))) {
        e.removeClass("lastCollapsable");
        e.removeClass("lastExpandable");
        if (t.hasClass("collapsable"));
        {
            t.addClass("lastCollapsable");
            t.find("div").addClass("lastCollapsable-hitarea")
        }
        if (t.hasClass("expandable")) {
            t.addClass("lastExpandable");
            t.find("div").addClass("lastExpandable-hitarea")
        }
    }
    if ((t.hasClass("collapsable") || t.hasClass("expandable")) && e.hasClass("last")) {
        if (t.hasClass("collapsable"))
        {
            t.addClass("lastCollapsable");
            t.find("div").addClass("lastCollapsable-hitarea")
        }
        if (t.hasClass("expandable")) {
            t.addClass("lastExpandable");
            t.find("div").addClass("lastExpandable-hitarea")
        }
        e.removeClass("last")
    }
}
function ChangePriority(e, t) {
    $.ajax({
        url: "/Admin/Commerce/UpDownCommerceGroup",
        data: "{CurrentId: " + parseInt(e) + ", PrevId: " + parseInt(t) + " }",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST", success: function (e) { }
    })
}
function WhichOprForPriority() {
    if ($("#ParentMenuName").val().length == 0) {
        $("#divValidationName").removeClass("HideElement");
        return false;
    } else {
        if 
        (!$("#DivInfoForm").hasClass("HideElement")) {
            $("#DivInfoForm").addClass("HideElement");
        };
        if (!$("#divValidationName").hasClass("HideElement")) {
            $("#divValidationName").addClass("HideElement")
        }
        var e = $("#HiddenMenuId").val();
        var t = $("#TypeOpr").val();
        if (t == "Menu") { e = null }
        if (t != "edit")
        {
            $.ajax({
                url: "/Admin/Commerce/GetPriority",
                data: "{ 'id': '" + e + "'}", dataType: "json",
                contentType: "application/json; charset=utf-8", type: "POST",
                async: false, success: function (e) { ValuePriority = e.Priority }
            })
        }
        if (t == "Menu") {
            return AddNodeSetting(null, "add", ValuePriority)
        } else if (t == "SubMenu") {
            return AddNodeSetting($("#HiddenMenuId").val(), "add", ValuePriority)
        } else if (t == "edit") {
            $("span[class='SelectedLi']").text($("#ParentMenuName").val());
            return AddNodeSetting($("#HiddenMenuId").val(), "edit", 0)
        }
    }
}