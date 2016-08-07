
       $(document).ready(function () {
           $('a[data-toggle="collapse"]').on('click', function () {

               var objectID = $(this).attr('href');

               if ($(objectID).hasClass('in')) {
                   $(objectID).collapse('hide');
               }

               else {
                   $(objectID).collapse('show');
               }
           });
           $(".mtsscroll").mCustomScrollbar({
               theme: "rounded-dots",
               scrollInertia: 400
           });
           $(".dels").click(function () {
               if (!$("#divsuccess").hasClass("hidden")) {
                   $("#divsuccess").addClass("hidden");
               }
           });
           $(".hrefelseimgu").click(function () {
               var s = "";
               var id = $(this).find('i').attr('value');
               var m = "#h2tab" + id;
               $(m).removeClass("tab-pane");
               var t = $("#hfprevelement").val();

               if (t != null && t != "") {
                   $(t).addClass("tab-pane");
               }
               $("#hfprevelement").val(m);

           });

           $(".hreftabgpu").click(function () {
               var s = "";
               var id = $(this).find('i').attr('value');
               var m = "#h2tab" + id;

               $(m).removeClass("tab-pane");
               var t = $("#hfprevelement").val();

               if (t != null && t != "") {
                   $(t).addClass("tab-pane");
               }
               $("#hfprevelement").val(m);
               $.ajax({
                   url: "/EN/Lawyer/GetImageGalleryList",
                   data: "{ 'id': '" + id + "'}",
                   dataType: "json", contentType: "application/json; charset=utf-8",
                   type: "POST",
                   success: function (result) {
                       s = "#ImageGalleryDiv" + id;
                       $(s).html(result.Partial);
                   }
               });
           });

           $(".hrefelseimg").click(function () {
               var s = "";
               var id = $(this).find('i').attr('value');
               var m = "#h2tab" + id;
               $(m).removeClass("tab-pane");
               var t = $("#hfprevelement").val();

               if (t != null && t != "") {
                   $(t).addClass("tab-pane");
               }
               $("#hfprevelement").val(m);

           });

           $(".hreftabgp").click(function () {
               var s = "";
               var id = $(this).find('i').attr('value');
               var m = "#h2tab" + id;

               $(m).removeClass("tab-pane");
               var t = $("#hfprevelement").val();

               if (t != null && t != "") {
                   $(t).addClass("tab-pane");
               }
               $("#hfprevelement").val(m);

               $.ajax({
                   url: "/EN/Product/GetImageGalleryList",
                   data: "{ 'id': '" + id + "'}",
                   dataType: "json", contentType: "application/json; charset=utf-8",
                   type: "POST",
                   success: function (result) {
                       s = "#ImageGalleryDiv" + id;
                       $(s).html(result.Partial);
                   }
               });
           });


           $(".stbnodew").click(function () {
               var m = $(this).text();
               $.ajax({
                   url: "/EN/Gallery/GetList",
                   data: "{ 'id': '" + $(this).parent().val() + "'}",
                   dataType: "json", contentType: "application/json; charset=utf-8",
                   type: "POST",
                   beforeSend: function () {
                       $("#WorkExpDiv").html("");
                       var over = '<div id="target" class="loading">' +
'<div class="loading-overlay">' +
   '<p class="loading-spinner">' +
       '<span class="loading-icon"></span>' +
       '<span class="loading-text">' + ' Loading ' + m + '</span>' +
   '</p>' +
'</div>' +
'</div>';
                       $(over).appendTo('#WorkExpDiv');
                   },
                   success: function (result) {
                       $("#navdiv1").html(result.Partial1);
                       $("#WorkExpDiv").html(result.Partial);



                   }
               });
           });

           $(".stbnode").click(function () {
               var m = $(this).text();
               $.ajax({
                   url: "/EN/Product/GetList",
                   data: "{ 'id': '" + $(this).parent().val() + "'}",
                   dataType: "json", contentType: "application/json; charset=utf-8",
                   type: "POST",
                   beforeSend: function () {
                       $("#ServiceDiv").html("");
                       var over = '<div id="target" class="loading">' +
'<div class="loading-overlay">' +
   '<p class="loading-spinner">' +
       '<span class="loading-icon"></span>' +
       '<span class="loading-text">' + ' Loading ' + m + '</span>' +
   '</p>' +
'</div>' +
'</div>';
                       $(over).appendTo('#ServiceDiv');
                   },
                   success: function (result) {
                       $("#navdiv").html(result.Partial1);
                       $("#ServiceDiv").html(result.Partial);

                   }
               });
           });

          
           $(".hrefabout").click(function () {
               $("#navdiv").text($(this).text());
           });

           $(".dropdown-tree-a").click(function () {
               if ($(this).parent().hasClass("open-tree")) {
                   $(this).parent().removeClass("open-tree");
                   $(this).parent().addClass("active");
               }
               else if ($(this).parent().hasClass("active")) {
                   $(this).parent().removeClass("active");
                   $(this).parent().addClass("open-tree");
               }
           });
       });

       
jQuery(function ($) {
    var target = $('#target');

    $('.toggle-loading').click(function () {
        $('#target').removeAttr("style");

    });
});
