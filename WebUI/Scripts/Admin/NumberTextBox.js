﻿function isNum(e) { var t; if (window.event) t = window.event.keyCode; else if (e) t = e.which; if (t > 31 && (t < 48 || t > 57)) return false; return true }