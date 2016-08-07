$(document).ready(function () {
    $('#red  li span').click(function () {
        debugger;
     var GroupName=$(this).text()
     if ($(this).parent().find('ul').length == 0) {
         
            $.ajax({
                url: "/Admin/Product/GetList",
                data: "{ 'id': '" + $(this).parent().attr("value") + "'}",
                dataType: "json", contentType: "application/json; charset=utf-8",
                type: "POST",
                beforeSend: function () {
                    $("#divLoader").css("display", "block");
                    $("#divTextLoader").html(" در حال دریافت اطلاعات گروه " + GroupName);
                },
                success: function (result) {
                    $("#divLoader").css("display", "none");
                    $("#BoxContent1").html(result.Partial);
                }
            });
        }
    });

});
function GetListA(id,page)
{
    $.ajax({
        url: "/Admin/Product/GetList",
        data: '{' + '"id": "' + id + '", "Page": "' + page + '"' + '}',
        dataType: "json", contentType: "application/json; charset=utf-8",
        type: "POST", success: function (result) { $("#BoxContent1").html(result.Partial); }
    });

}
