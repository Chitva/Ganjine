
function UserNameCheckChars(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    else if (e) keycode = e.which;
    if ((((keycode > 140 && keycode < 173) || (keycode > 64 && keycode < 133) || (keycode > 47 && keycode < 58)) && (keycode != 32)) || (keycode == 0 || keycode == 8))
        return true;
    else
        return false;
}
