$(document).ready(function () { $("#PartName").keydown(function () { $("#DivValidation").addClass("HideElement") }) }); function CheckValidation() { if ($("#PartName").val().length > 0) { $("#DivValidation").addClass("HideElement"); return true } else { $("#DivValidation").removeClass("HideElement"); return false } }
$('.IconEdit').click(function () {
    $(this).attr("disabled", true);
    $(this).parent().parent().parent().find("#NameM").removeAttr("readonly").removeAttr("style").attr("style", "width:250px; font-family:Tahoma;");
    $(this).parent().parent().parent().find(".CancelIcon").css("display", "block");
    $(this).parent().parent().parent().find(".OkIcon").css("display", "block");

});
$('.OkIcon').click(function () {
    $(this).parent().parent().parent().find(".IconEdit").attr("disabled", false);
    $(this).parent().parent().parent().find("#DBName").val($(this).parent().parent().parent().find("#NameM").val());
    $(this).parent().parent().parent().find("#NameM").attr("readonly", true).attr("style", "width:250px;border:none;height:20px; font-family:Tahoma;background-color:#f5f5f5;");
    $(this).parent().parent().parent().find(".CancelIcon").css("display", "none");
    $(this).parent().parent().parent().find(".OkIcon").css("display", "none");

});
$('.CancelIcon').click(function () {
    $(this).parent().parent().parent().find(".IconEdit").attr("disabled", false);
    $(this).parent().parent().parent().find("#NameM").val($(this).parent().parent().parent().find("#DBName").val());
    $(this).parent().parent().parent().find("#NameM").attr("readonly", true).attr("style", "width:250px;border:none;height:20px; font-family:Tahoma;background-color:#f5f5f5;");
    $(this).parent().parent().parent().find(".CancelIcon").css("display", "none");
    $(this).parent().parent().parent().find(".OkIcon").css("display", "none");

});