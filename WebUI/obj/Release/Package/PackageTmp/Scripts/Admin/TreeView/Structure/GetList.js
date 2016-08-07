$(document).ready(function () {
    $('#red  li span').click(function () {
        //$.ajax({
        //    url: "/Gallery/GetData",
        //    data: "{ 'PrevId': '" + $(this).parent().prev().val() + "'}",
        //    dataType: "json", contentType: "application/json; charset=utf-8",
        //    type: "POST",
        //    beforeSend: function () {
        //        $("#divLoader").css("display", "block");
        //        $("#divTextLoader").html(" در حال دریافت اطلاعات گروه " + GroupName);
        //    },
        //    success: function (result) { $("#BoxContentFileUpload").html(result.Partial); $("#divLoader").css("display", "none"); }
        //});
        var GroupName = $(this).text;
        var m = $(this).parent().prev().val();
        if ($(this).parent().prev().val() == null)
            m = 0;
        if ($(this).parent().find('ul').length == 0 ) {
            window.location.href = '/Admin/OrganizationStructure/OrganizationStructureList/' + $(this).parent().attr("value") + '/' + m;
        }
        else
        {
            if (!$("#DivInfoForm").hasClass("HideElement"))
            $("#DivInfoForm").addClass("HideElement");
        }
    });
});

