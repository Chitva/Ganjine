﻿function step(e, t) { $("#LinkPathSubMenu").text(""); $("#imgSep1").show(); $("#LinkPathSub2Menu").text(""); $("#imgSep2").hide(); switch (e.id) { case "customer": { $("#PathMenu").text("گالری"); $("#liCustomer").addClass("ClickMenu"); $("#customer").show(20); break }; case "users": { $("#PathMenu").text("اخبار"); $("#liUsers").addClass("ClickMenu"); $("#users").show(20); break }; case "settings": { $("#PathMenu").text("تنظیمات"); $("#liSettings").addClass("ClickMenu"); $("#settings").show(20); break }; case "change": { $("#PathMenu").text("تغییر رمز عبور"); $("#liChange").addClass("ClickMenu"); $("#change").show(20); break } } if (pnlOpens[e.id] && t == 0) step_close(e); else step_open(e) } function step_close(e) { $("#PathMenu").text(""); $("#imgSep1").hide(); $("#imgSep2").hide(); $("#LinkPathSubMenu").text(""); $("#LinkPathSub2Menu").text(""); switch (e.id) { case "customer": { $("#liCustomer").removeClass("ClickMenu"); $("#customer").hide(20); break }; case "users": { $("#liUsers").removeClass("ClickMenu"); $("#users").hide(20); break }; case "settings": { $("#liSettings").removeClass("ClickMenu"); $("#settings").hide(20); break }; case "change": { $("#liChange").removeClass("ClickMenu"); $("#change").hide(20); break } } pnlOpens[e.id] = false; var t = e.offsetHeight; if (t < 10) { e.style.height = "0px" } else { e.style.height = t - 10 + "px" } if (t > 0) setTimeout(function () { step_close(e) }, 1) } function step_open(e) { pnlOpens[e.id] = true; var t = e.offsetHeight; if (t > pnlHeights[e.id] - 10) { e.style.height = pnlHeights[e.id] + "px" } else { e.style.height = t + 10 + "px" } if (t < pnlHeights[e.id]) setTimeout(function () { step_open(e) }, 1) } var pnlHeights = { customer: 70, users: 70, settings: 110, change: 30 }; var pnlOpens = { customer: false, users: false, settings: false }