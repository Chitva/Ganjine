function Page() {
    var e = $("#MenuItems").val().split(":-");
    $("#ImageTit").attr("src", "/Images/Admin/Design/" + e[0]);
    $("#HeaderTitleP").text(e[2]); $("#LinkPathSubMenu").text(e[2]);
    $("#PathMenu").text(e[1]);
    $("#LinkPathSubMenu").attr("href", $(location).attr("href"));
    $("#imgSep1").show()
}
function PageList(e, t) {
    $("#HeaderTitleP").text(e);
    $("#LinkPathSub2Menu").text(e);
    $("#LinkPathSub2Menu").attr("href", $(location).attr("href"));
    $("#LinkPathSubMenu").attr("href", t);
    $("#imgSep2").show()
}