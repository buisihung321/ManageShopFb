const widget = uploadcare.Widget('[role=uploadcare-uploader]');

var imageUUID;


widget.onUploadComplete(function (info) {
    console.log(info)
    //save the uuid of image create the hidden field
    let formContainer = $("#add-product-form");
    console.log(formContainer)
    formContainer.append(`<input type="hidden" name="photoUUID" value="${info.uuid}" />`)
    //saveImage(info.cdnUrl).then(() => {
    //    $('#uploadedImage').parent().html('<a href="javascript:refreshPage()">Refresh it!</a>')
    //})
})

//confirm delete


var deleteLink = $("a.delete-link");
deleteLink.click(function () {
    return confirm('You want to delete this?');
})
//var deleteLink;
//$("a.delete-link").each(function (index) {
//    $(this).click(function () {
//        console.log($(this));
//        return confirm('You want to delete this?');
//    })
//})
