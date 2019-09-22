const widget = uploadcare.Widget('[role=uploadcare-uploader]');

var imageUUID;

widget.onUploadComplete(function (info) {
    console.log(info)
    //save the uuid of image create the hidden field
    let formContainer = $("#add-product-form");
    console.log(formContainer)
    formContainer.append(`<input type="hidden" name="photoUUID" value="${info.uuid}" />`)
    //widget.value(null);

})

//confirm delete
var deleteLink = $("a.delete-link");
deleteLink.click(function () {
    return confirm('You want to delete this?');
})


//2. handle submit update form
function onEditSubmit(data) {
    $.ajax({
        url: '/Product/Update',
        type: 'POST',
        content: "application/json; charset=utf-8",
        datatype: 'json',
        data: {
            Name: data.Name,
            Price: data.Price,
            Quantity: data.Quantity
        },
        success: function (res) {
            console.log(res)
        }
    })
}

//3. When click edit we will get the info of that product from uuid
//then create update modal with that data
$(".edit__btn").each(function () {
    let uuid = $(this).data("uuid");
    $(this).click(function () {
        console.log("Clicked: " + uuid)
        $.ajax({
            url: '/Product/Detail',
            type: 'POST',
            content: "application/json; charset=utf-8",
            datatype: 'json',
            data: {
                uuid: uuid
            },
            success: function (res) {
                if (res == null)
                    console.log("No response")
                else {
                    createUpdateModal(res);
                }
            },
            err: function () {
                console.log("Error when calling ajax")
            }
        })
    })
})

function createUpdateModal(data) {
    //append the edit modal with this res info
    let editModal = $("#edit_modal");
    editModal.find("img#product-photo").attr({
        src: 'https://ucarecdn.com/' + data.PhotoUUID + '/-/preview/950x950/-/filter/sorlen/-34/'
    })
    editModal.find(`div#edit-product-container input[name="Name"]`).val(data.Name);
    editModal.find(`div#edit-product-container input[name="Price"]`).val(data.Price);
    editModal.find(`div#edit-product-container input[name="Quantity"]`).val(data.Quantity);
    editModal.find(`div#edit-product-container input[name="AlbumId"]`).val(data.AlbumId);
    editModal.find(`div#edit-product-container input[name="PhotoUUID"]`).val(data.PhotoUUID);
    //show modal manually
    editModal.modal({ show: true })
}


