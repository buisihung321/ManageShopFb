const widget = uploadcare.Widget('[role=uploadcare-uploader]');

var imageUUID;
var updateModal;
widget.onUploadComplete(function (info) {
    console.log(info)
    //save the uuid of image create the hidden field
    let formContainer = $("#add-product-form");
    console.log(formContainer)
    formContainer.append(`<input type="hidden" name="photoUUID" value="${info.uuid}" />`)
    //widget.value(null);

})



