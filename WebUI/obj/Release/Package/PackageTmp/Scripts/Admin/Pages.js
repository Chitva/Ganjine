  if ($("#PageColumnContentId :selected").val() == 5) {
            $("#divckeditor").css("display", "block");
        }
        else {
            $("#divckeditor").css("display", "none");
        }
        $("#PageColumnContentId").change(function () {
            if ($("#PageColumnContentId :selected").val() == 5) {
                $("#divckeditor").css("display", "block");
            }
            else {
                $("#divckeditor").css("display", "none");
            }
        });
        var Count = 6 - $("#StartColumn :selected").val();
        var num = 6;
        var Check = false;
        $("#PageColumnId option").attr("disabled", false);
        for (var item = 1; item < 6 - Count; item++) {
            if ($("#PageColumnId :selected").val() == num) {
                Check = true;
            }
            $("#PageColumnId option[value='" + num + "']").attr("disabled", "disabled");
            num--;
        }
        if (Check) {
            $("#PageColumnId").val('1');
        }
        $("#StartColumn").change(function () {
            var Count = 6 - $("#StartColumn :selected").val();
            var num = 6;
            var Check = false;
            $("#PageColumnId option").attr("disabled", false);
            for (var item = 1; item < 6 - Count; item++) {
                if ($("#PageColumnId :selected").val() == num) {
                    Check = true;
                }
                $("#PageColumnId option[value='" + num + "']").attr("disabled", "disabled");
                num--;
            }
            if (Check) {
                $("#PageColumnId").val('1');
            }
        });