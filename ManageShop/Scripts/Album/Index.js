//const widget = uploadcare.Widget('[role=uploadcare-uploader]');

const uploadBtn = $("#uploadBtn");
const saveAlbumBtn = $("#album-modal .modal-footer button#save-album__btn");
const albumWrapper = $("#album-modal #album-wrapper");


saveAlbumBtn.click(sendAlbumAddRequest);

function sendAlbumAddRequest() {
    //get info for Album obj
    let album = {};


    album.Name = albumWrapper.find(`input[name="Name"]`).val();
    album.AlbumId = albumWrapper.find(`input[name="AlbumId"]`).val();
    album.Description = albumWrapper.find(`textarea[name="Description"]`).val();
    console.log(album);
    //get info for Product
    let products = [];

    let productsObj = $("#album-modal #product-container .product-card");

    //validate at least 1 product

    if (productsObj.length <= 0) {
        alert('Please add at least one product')
        return;
    }


    console.log(productsObj)
    productsObj.each((index, ele) => {
        let product = {};

        product.AlbumId = album.AlbumId;
        product.Name = $(ele).find(`input[name="Name"]`).val();
        product.Price = $(ele).find(`input[name="Price"]`).val();
        product.PhotoUUID = $(ele).attr('id');

        products.push(product);
    })


    album.PhotoCover = albumWrapper.find(`input[name="PhotoCover"]`).val() == '' || null
        ? products[0].PhotoUUID : albumWrapper.find(`input[name="PhotoCover"]`).val();


    let data = JSON.stringify({ "album": album, "products": products });
    console.log(data)
    //send ajax request
    $.ajax({
        url: '/Album/New',
        type: 'POST',
        content: "application/json; charset=utf-8",
        datatype: 'json',
        data: {album,products},
        success: function (res) {
            window.location.href = res.newUrl;
        },
        err: function () {
            console.log("Error when calling ajax")
        }
    });

    console.log(products)
}



//add progress bar
function installProgressBar(widget) {
    let currentObject;
    widget.onChange(function (widgetObject) {
        currentObject = widgetObject
        if (widgetObject) {

            //create the card hidden
            //show the progress bar
            let productHtml = `<div  class="card m-2 product-card shadow card-loading ">
                                    <div class="upload-progress-bar">
                                        <div class="progress-wrapper">
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-striped" role="progressbar"
                                                     style="width: 1%" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="progress-title">Loadding Photo</span>
                                        </div>
                                    </div>
                                    <img class="product-img" src=""
                                         width="100%" height="180"
                                         alt="Alternate Text" />
                                    <div class="card-body">
                                            <input type="text" name="Name" id="name" placeholder="Name" class="form-control form-control-sm" />
                                            <input type="number" name="Price" id="price" placeholder="Price"  class="form-control form-control-sm" />
                                    </div>
                                    <div class="card-footer text-muted">
                                        <button class="btn btn-danger btn-sm product-remove__btn">Remove</button>
                                        <button id="set-album-cover__btn" class="btn btn-primary btn-sm">Set as album photo</button>
                                    </div>
                                </div>`;

            //add this card to DOM

            let product = $($.parseHTML(productHtml));

            product.insertBefore(uploadBtn)

            let progress = product.find('.upload-progress-bar .progress-bar');
            console.log(progress);

            //change progress-bar value
            widgetObject
                .promise()
                .progress(function (info) {
                    progress.width((info.uploadProgress * 100).toFixed(2) + '%')
                    if (currentObject === widgetObject) {
                    }
                })
                .done(function (info) {
                    console.log('upload finished!')
                    let img = product.find('img.product-img');
                    var size = '' + img.width() * 2 + 'x' + img.height() * 2
                    var previewUrl = info.cdnUrl + '-/scale_crop/' + size + '/center/'
                    widget.value(null)
                    //add cndURl to the Onchange product added
                    img.attr({
                        src: previewUrl,
                        alt: info.name
                    });
                    

                    //remove the progress bar
                    product.removeClass('card-loading')
                    //add id to the product
                    product.attr({
                        id: info.uuid
                    })
                    //add remove btn 
                    addRemoveEventToBtn(product.find("button.product-remove__btn"), info.uuid)
                    //add photo cover button for album
                    addSetAlbumCoverBtn(product.find("button#set-album-cover__btn"), info.uuid);

                })
                .fail(function (err) {
                    //user cancel upload
                    if (err == 'user') {
                        product.remove();
                    }
                    //err when uploadding
                    else if (err == 'upload') {
                        alert('Err when upload file')
                        product.remove();
                    }
                    console.log(err);
                })
                .always(function (obj) {
                    if (currentObject === widgetObject) {
                        //product.removeClass('card-loading')
                    } else {
                    }
                })
        }
        //else {
        //    alert("Loi khi upload file")
        //}
    })

}

$(function () {
    installProgressBar(uploadcare.Widget('[role=uploadcare-uploader]'));
})

function addSetAlbumCoverBtn(btn, photoUuid) {
    btn.click(function () {
        albumWrapper.find(`input[name="PhotoCover"]`).val(photoUuid);
    })
}


//function to add click event to product
function addRemoveEventToBtn(btn, productUuid) {
    //let removeBtn = $(btn); //constructor jquery

    btn.click(function () {
        console.log(btn);
        //find the wrapper of this btn

        let wrapper = $(`div#${productUuid}`);
        console.log(wrapper)
        wrapper.remove();
    })

}



//confirm delete
var deleteLink = $("a.delete-link");
deleteLink.click(function () {
    return confirm('You want to delete this?');
})