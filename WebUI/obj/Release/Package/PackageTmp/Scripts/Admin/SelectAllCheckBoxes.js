function SelectAllCheckboxes(chk) {

    $('#' + $(chk).parent().attr("id")).find("input[name='Roles']").each(function () {
        if (this != chk) {
            this.checked = chk.checked;
            $(chk).parent().find('.ChangePremissionPic').removeAttr('disabled');
            if (!chk.checked)
            {
                $(chk).parent().find('.ChangePremissionPic').attr('disabled', 'disabled');
            }
        }
    });
    
    $('#' + $(chk).parent().attr("id")).find("input[name='Premissions']").each(function () {
        if (this != chk) {
            this.disabled = !chk.checked;
          
        }
        if (!chk.checked)
        {
            this.checked = false;
           
        }
    });
        
}