$(document).ready(function () {
    $('#red  li span').click(function () {
        
        var GroupName = $(this).text;
        var m = $(this).parent().prev().val();
        if ($(this).parent().prev().val() == null)
            m = 0;
        if ($(this).parent().find('ul').length == 0 ) {
            window.location.href = '/Admin/Commerce/CommerceList/' + $(this).parent().attr("value") + '/' + m;
        }
        else
        {
            if (!$("#DivInfoForm").hasClass("HideElement"))
            $("#DivInfoForm").addClass("HideElement");
        }
    });
});

