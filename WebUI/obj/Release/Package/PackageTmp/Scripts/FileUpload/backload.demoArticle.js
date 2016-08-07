/*
 * jQuery File Upload Plugin JS Example 8.0.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, regexp: true */
/*global $, window, navigator */

$(function () {
    'use strict';

    var url = '/FileUploadArticle/FileHandler';
    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: url



    });



    // Enable iframe cross-domain access via redirect option:
    $('#fileupload').fileupload(
        'option',
        'redirect',
        window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        )
    );

    // Load existing files by an initial ajax request to the server after page loads up
    // This is done by a simple jQuery ajax call, not by the FIle Upload plugin.,
    // but the results are passed to the plugin with the help of the context parameter: 
    // context: $('#fileupload')[0] and the $(this)... call in the done handler. 
    // With ajax.context you can pass a JQuery object to the event handler and use "this".
    $('#fileupload').addClass('fileupload-processing');
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: url,
        dataType: 'json',
        context: $('#fileupload')[0]
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) {
        $(this).fileupload('option', 'done')
            .call(this, null, { result: result });
    });
});

$("document").ready(function () {

    var DelFileCounter = 0;
    var CountChecked = 0;
    $('#fileupload')
        .bind('fileuploaddestroy', function (e, data) {
            if ($("#hfDelFileCount").val() == 'yesSing')
                $("#hfDelFileCount").val('0');
            else
                $("#hfDelFileCount").val(parseInt($("#hfDelFileCount").val()) + 1);

            if (CountChecked == 0)
                CountChecked = $('input[name=delete]:checkbox:checked').length;
            if (CountChecked == parseInt($("#hfDelFileCount").val()) && $("#dels").val() == "0") {
                $("#dels").val("1");
                $("#hfDelFileCount").val('0');
                CountChecked = 0;
            }

        });
    //$('#fileupload').fileupload().bind('fileuploadadded', function (e, data) {
    //    var fileType = data.files[0].name.split('.').pop(), allowdtypes = 'jpeg,jpg,png,gif';
    //    if (allowdtypes.indexOf(fileType) < 0) {
    //        alert('Invalid file type, aborted');
    //        return false;
    //    }
    //});
    //fileuploadcompleted
    //fileuploaddone
    var filesuploadedSuccess = 0;
    $('#fileupload').bind('fileuploaddone', function (e, data) {


        filesuploadedSuccess++;

        if (filesuploadedSuccess == data.originalFiles.length) {

            if ($("#hfPage").val() != null && $("#hfPage").val().length > 0) {
                window.location.href = '/Admin/Article/ArticleList/?Page=' + $("#hfPage").val();
            }
            else
                window.location.href = '/Admin/Article/ArticleList';
        }
    });
});
