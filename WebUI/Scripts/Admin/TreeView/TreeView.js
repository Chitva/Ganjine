﻿function AddMenuSubMenu(e) {
    debugger;
    if ($("#red").find(".SelectedLi").parent().parent().parent().attr("value") != undefined && e == 'SubMenu')
    { alert("درج زیر منو امکان پذیر نیست ."); }
    else
    {
        var HasImage = 0;
        if (e == 'SubMenu') {
            $.ajax({
                url: "/ImageGallery/HasImage",
                data: "{ 'id': '" + $("#HiddenMenuId").val() + "'}", dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "POST", async: false,
                success: function (res) { HasImage = res.Cnt }
            });
        }
        if (HasImage > 0) {
            alert('این گروه شامل عکس می باشد ، امکان درج زیر گروه نیست');
        } else {
            var t = $("#IsSelected").val();
            if (t != "1" && e == "SubMenu") {
                return ShowPopupAlert("#PopUp-Content-Alert");
            } $("#TypeOpr").val(e);
            $("#MenuInfoDetail").find("input[type=text]").val("");
            $("#MenuInfoDetail").addClass("DivMenuInfoDetailShow");

        }
        if (e == "Menu") {
            $("#IsSelected").val("");
            $("span").filter(function() {
                return this.className;
            }).removeClass("SelectedLi");
        }
    }
}
function EditMenu() {
    $("#ParentMenuName").val(""); var e = $("#IsSelected").val();
    if (e == "1") {
        $("#TypeOpr").val("edit");
        $.ajax({
            url: "/ImageGallery/GetInfoImageGroup",
            data: "{ 'id': '" + $("#HiddenMenuId").val() + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            success: function(e) { $("#ParentMenuName").val(e.MenuNameTitle); }
        });
        $("#MenuInfoDetail").addClass("DivMenuInfoDetailShow");
    } else return ShowPopupAlert("#PopUp-Content-Alert");
}
function DeleteMenu() {
    var e = $("#IsSelected").val();
    if (e == "1") { return ShowPopupAlertBeforeDelete('PopUp-Content-BoxBeforDelete'); }
    else { return ShowPopupAlert("#PopUp-Content-Alert") }
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
            ChangePriority(i, r);
        }
    } else return ShowPopupAlert("#PopUp-Content-Alert");
}
function DownMenu() {
    var e = $("#IsSelected").val();
    if (e == "1") {
        var t = $("span[class='SelectedLi']").parent();
        var n = $("span[class='SelectedLi']").parent().next();
        var r = n.val();
        var i = t.val();
        t.before(n);
        if (r != null && i != null) {
            ClassSettingUpDown(n, t);
            ChangePriority(i, r);
        }
    } else return ShowPopupAlert("#PopUp-Content-Alert");
}
function ClassSettingUpDown(e, t) {
     if (e.hasClass("last") && !t.hasClass("collapsable") && !t.hasClass("expandable")) {
          e.removeClass("last");
         t.addClass("last");
     } if (!t.hasClass("collapsable") && !t.hasClass("expandable") && (e.hasClass("lastCollapsable") || e.hasClass("lastExpandable"))) { e.removeClass("lastCollapsable"); e.removeClass("lastExpandable"); t.addClass("last") } if ((t.hasClass("collapsable") || t.hasClass("expandable")) && (e.hasClass("lastCollapsable") || e.hasClass("lastExpandable"))) { e.removeClass("lastCollapsable"); e.removeClass("lastExpandable"); if (t.hasClass("collapsable")); { t.addClass("lastCollapsable"); t.find("div").addClass("lastCollapsable-hitarea") } if (t.hasClass("expandable")) { t.addClass("lastExpandable"); t.find("div").addClass("lastExpandable-hitarea") } } if ((t.hasClass("collapsable") || t.hasClass("expandable")) && e.hasClass("last")) { if (t.hasClass("collapsable")) { t.addClass("lastCollapsable"); t.find("div").addClass("lastCollapsable-hitarea") } if (t.hasClass("expandable")) { t.addClass("lastExpandable"); t.find("div").addClass("lastExpandable-hitarea") } e.removeClass("last") }
}
function ChangePriority(e, t) {
    $.ajax({ url: "/ImageGallery/UpDownImageGroup", data: "{CurrentId: " + parseInt(e) + ", PrevId: " + parseInt(t) + " }", dataType: "json", contentType: "application/json; charset=utf-8", type: "POST", success: function(e) {} });
} function WhichOprForPriority() {
    var e = $("#HiddenMenuId").val(); var t = $("#TypeOpr").val(); if (t == "Menu") { e = null } if (t != "edit") {
        $.ajax({
            url: "/ImageGallery/GetPriority",
            data: "{ 'id': '" + e + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            async: false,
            success: function(e) { ValuePriority = e.Priority }
        });
    } if (t == "Menu") { return AddNodeSetting(null, "add", ValuePriority) } else if (t == "SubMenu") { return AddNodeSetting($("#HiddenMenuId").val(), "add", ValuePriority) } else if (t == "edit") { $("span[class='SelectedLi']").text($("#ParentMenuName").val()); return AddNodeSetting($("#HiddenMenuId").val(), "edit", 0) }
}