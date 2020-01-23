$("body").on('click', 'a.delete-link', deleteLink);
$('[data-toggle="tooltip"]').tooltip();
var updateModal;
function deleteLink(e) {
    let href = $(this).attr('href');
    console.log(href);
    e.preventDefault();
    return bootbox.confirm({
        message: "Do you want to remove this product?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-danger'
            },
            cancel: {
                label: 'No',
                className: 'btn-info'
            }
        },
        callback: function (result) {
            if (result)
                window.location = href;

            //console.log($(this));
        }
    });
}


class ProductModal {
    constructor(data) {
        this.name = data.Name;
        this.price = data.Price;
        this.quantity = data.Quantity;
        this.id = data.Id;
        this.uuid = data.PhotoUUID;
        this.imageSrc = "https://ucarecdn.com/" + data.PhotoUUID + "/-/preview/950x950/-/filter/sorlen/-34/";
        this.modal = null;
    }

    getHtml() {
        return `<div id="edit_modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel">Edit product</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                            <img id="product-photo" src="${this.imageSrc}"
                                                 width="100%" height="180"
                                                 alt="Alternate Text" />
                                            <div id="edit-product-container">
                                                <div class="form-group">
                                                    <label for="name">Name</label>
                                                    <input type="text" value="${this.name}" name="Name" id="name" class="form-control" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="price">Price</label>
                                                    <input type="number" name="Price" value="${this.price}" id="price" class="form-control" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="price">Quantity</label>
                                                    <input type="number" name="Quantity" value="${this.quantity}" id="quantity" class="form-control" />
                                                </div>
                                                <input type="hidden" value="${this.id}" name="id" />
                                                <button id="submit-edit__btn" class="btn btn-primary">Save</button>
                                            </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
    }
    initializeEvent(editModal) {
        this.modal = editModal;
        this.modal.on('hidden.bs.modal', function (e) {
            // do something...
            console.log("Hideen modal");
            editModal.remove();
        })

    }
    show() {
        this.modal.modal({ show: true });
    }
    hide() {
        this.modal.modal('hide');
    }
    getData() {
        return {
            Id: this.id,
            Name: $("#edit_modal").find(`input[name="Name"]`).val(),
            Price: $("#edit_modal").find(`input[name="Price"]`).val(),
            Quantity: $("#edit_modal").find(`input[name="Quantity"]`).val()
        }
    }
}


//Binding EditBtn Event
$("body").on('click', '.edit__btn', showEditModal);

function showEditModal() {
    console.log("Edit button")
    let uuid = $(this).data("uuid");
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
}

$("body").on('click', '#submit-edit__btn', onEditSubmit);


function onEditSubmit() {
    console.log(updateModal.id)
    $.ajax({
        url: '/Product/Update',
        type: 'POST',
        content: "application/json; charset=utf-8",
        datatype: 'json',
        data: updateModal.getData(),
        success: function (res, textStatus, XmlHttpRequest) {
            if (XmlHttpRequest.status === 200) {
                updateModal.hide();
                bootbox.alert("Update success", function () {
                    location.reload(true);
                });
            } else {
                alert("Update not success");
            }
        }
    })
}

//3. When click edit we will get the info of that product from uuid
//then create update modal with that data



function createUpdateModal(data) {
     updateModal = new ProductModal(data);
    //create update modal form 
    $("body").append(updateModal.getHtml());
    updateModal.initializeEvent($("#edit_modal"));
    updateModal.show();

}



//set random image for header 

const unsplashKey = "1b94951cfab3a250714e5e460a5993fae101a1dffad7ce4e328a53287b8724d4";
const keyword = ["shopping", "supermarket","accessory"];

var item = keyword[Math.floor(Math.random() * keyword.length)];
$.ajax({
    url: `https://api.unsplash.com/search/photos?page=1&query=${item}&client_id=${unsplashKey}`,
    type: 'GET',
    success: function (res) {
        console.log("Headeer images");
        console.log(res);

        let random = Math.floor((Math.random() * res.results.length));

        let imageUrl = res.results[random].urls.full;
        $('.header_container').css('background-image', 'url("' + imageUrl + '")');
    },
    err: function () {
        console.log("Error when calling ajax")
    }
});
