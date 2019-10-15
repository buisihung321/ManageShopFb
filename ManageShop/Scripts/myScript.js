$(document).ready(function () {
    //remove button
    //button
    $('#loadProduct').click(function () {
        var albumId = $('#album').val();
        $.ajax({
            url: '/Album/LoadProduct',
            type: 'POST',
            content: "application/json; charset=utf-8",
            datatype: 'json',
            data: { albumId: albumId },
            success: function (res) {
                RenderProduct(res.product);
            },
            err: function () {
                console.log("Error when calling ajax");
            }
        });
    });
})

var orderDetailsInput = $("#totalAmount");

function RenderProduct(products) {
    $('#product-container').empty();
    function addClickEvent(btn, productID) {

        //TODO:
        //1. Total
        //2. Save
        //3. Remove Product
        //4. Update Product

        btn.click(() => {
            let productWrapper = $(`#${productID}`);
            var quantity = productWrapper.find("#quantity").val();
            var quantityToCheck = productWrapper.find("#checkQuantity").text();
            let productsDetail = $("#orderdetailsItems");
            var price = productWrapper.find("#price").text();
            var isValid = true;
            //tim xem co hay chua
            let product = productsDetail.find(`#${productID}`);
            console.log(product);
            if (product.length != 0) {
                //Cap nhat quantity

                var newQuantity = parseInt(product.find("#quantityToSave").val()) + parseInt(quantity);
                if (newQuantity > quantityToCheck) {
                    isValid = false;
                    productWrapper.find("#outOfStock").css("display","block");
                } else {

                    productWrapper.find("#outOfStock").css("display","none");
                }

                if (quantity.length<1 || parseInt(quantity) <= 0 ) {
                    isValid = false;
                    productWrapper.find("#errorQuantity").css("display","block");
                } else {
                    productWrapper.find("#errorQuantity").css("display","none");
                }
                if (isValid) {
                    
                    product.find("#quantityToSave").val(newQuantity);
                    amount = newQuantity * price;
                    product.find("#amountToSave").val(amount);
                    orderDetailsInput.text(parseInt(orderDetailsInput.text()) + parseInt(quantity * price));    
                }
            } else {
                //console.log(product)
              
                if (quantity > quantityToCheck) {
                    isValid = false;
                    productWrapper.find("#outOfStock").css("display","block");
                } else {

                    productWrapper.find("#outOfStock").css("display","none");
                }

                if (parseInt(quantity) <= 0  || quantity.length < 1) {
                    isValid = false;
                    productWrapper.find("#errorQuantity").css("display","block");
                } else {
                    productWrapper.find("#errorQuantity").css("display","none");
                }



                if (isValid) {
                    var productName = productWrapper.find("#productName").text();
                    var amount = price * quantity;
                    var newRow = `<tr class='valueOrder' id='${productID}'>
                <td>
                    <div class="text-truncate" >
                                    <p class="font-italic product-title text-truncate"
                                        data-toggle="tooltip" title="${productName}" data-placement="left"    
                                        style="max-width: 65px;">${productName}</p>
                                    <input type='hidden' id='productToSave' class='form-control' value="${productName}" name='OrderDetail.ProductName' />
                                    <input type="hidden" name="OrderDetail.ProductId" value="${productID}" />
                                </div>
                </td>               
                <td>
                                        <input type='text' id='quantityToSave' class='' value="${quantity}" name='OrderDetail.Quantity' disabled/>
                </td>
                <td>
                                        <input type='text' id='priceToSave' class='' value="${price} " name='OrderDetail.UnitPrice' disabled/>
                </td>
                <td>
                                        <input type='text' id='amountToSave' class='' value="${amount} " name='OrderDetail.Amount' disabled/>
                </td>
                <td>
                    <button class="btn btn-danger btn-sm remove"><i class="far fa-trash-alt"></i></button>
                </td>
                </tr >`;
                    $("#orderdetailsItems").append(newRow);
                    removeOrder($(`#orderdetailsItems tr#${productID} button.remove`), productID);
                    orderDetailsInput.text(parseInt(orderDetailsInput.text()) + parseInt(quantity * price));    
                }

            }
        });
        //productname, quantity, price
        //luu xuong albumid, productid
    }
    function removeOrder(btn, productID) {
        btn.click(() => {
            let productsDetail = $("#orderdetailsItems");
            let product = productsDetail.find(`#${productID}`);
            console.log("Remove");
            console.log(product);
            product.remove();
            orderDetailsInput.text(parseInt(orderDetailsInput.text()) - parseInt(product.find("#amountToSave").val()));
        })
    }
    for (var i = 0; i < products.length; i++) {
        let product = `<div class="d-flex">
            <div id="${products[i].Id}" class="card mx-2 my-2 shadow product-card" style="width: 18rem;">
                <img src="https://ucarecdn.com/${products[i].PhotoUUID}/-/preview/950x950/-/filter/sorlen/-34/"
                     width="100%" height="180"
                     alt="Alternate Text" />
                <div class="card-body">
                    <div id="info-container">
                        <h5 class="card-title text-truncate" id="productName"
                                style="max-width: 100%;"    
                                data-toggle="tooltip" title="${products[i].Name}" data-placement="left"
                            >${products[i].Name}</h5>
                        <p class="card-text">
                            Price: <span id="price">${products[i].Price}</span>
                                   <span id="checkQuantity" hidden>${products[i].Quantity}</span>                             
                        </p>
                            <input type="number" placeholder="${products[i].Quantity} items is available" class="form-control" id="quantity"/>
                            <span id="errorQuantity" style="color:red; display:none">Please enter quantity again</span>
                            <span id="outOfStock" style="color:red; display:none">Quantity is not enought. Please enter again!!!</span>
                    </div>
                    
                </div>
                <div class="card-footer text-right">
                    <button id="add" class="btn btn-success btn-sm" >Add</button>
                </div>
            </div>`;
        $('#product-container').append(product);
        addClickEvent($('#product-container').find(`div#${products[i].Id} #add`), products[i].Id);

    }
}
//cai nay la de lay gia tri luu vo bang ordertails
function getInputForAddAlbum() {
    let orderDetails = [];
    var isValid = true;
    let order = {};
    order.OrderDate = $('#orderDate').val();

    if (order.OrderDate.length < 10) {
        isValid = false;
        $("#errorDate").css("display","block");
    } else {
        $("#errorDate").css("display","none");
    }


    order.Phone = $('#orderPhone').val();
    var phoneNumber =/[0-9-()+]{10}/;

    if (!phoneNumber.test(order.Phone) || order.Phone.length<1) {
        isValid = false;
        $("#errorPhone").css("display","block");
    } else {
        $("#errorPhone").css("display","none");
    }

    order.Name = $('#orderName').val();

    if (order.Name.length < 3) {
        isValid = false;
        $("#errorName").css("display","block");
    } else {
        $("#errorName").css("display","none");
    }
    order.Address = $('#orderAddress').val();

    if (order.Address.length < 4) {
        isValid = false;
        $("#errorAddress").css("display","block");
    } else {
        $("#errorAddress").css("display","none");
    }
    order.Description = $('#orderDes').val();
    order.Total = parseInt($('#totalAmount').text());
    let orderValue = $('.valueOrder');
    if (isValid) {
        orderValue.each((index, ele) => {
            let orderDetail = {};
            orderDetail.ProductId = $(ele).find("input[name='OrderDetail.ProductId']").val();
            orderDetail.ProductName = $(ele).find("input[name='OrderDetail.ProductName']").val();
            orderDetail.Quantity = $(ele).find("input[name='OrderDetail.Quantity']").val();
            orderDetail.UnitPrice = $(ele).find("input[name='OrderDetail.UnitPrice']").val();
            orderDetail.Amount = $(ele).find("input[name='OrderDetail.Amount']").val();
            orderDetails.push(orderDetail);
        });
        $("#orderDate").val("");
        $("#orderPhone").val("");
        $("#orderName").val("");
        $("#orderAddress").val("");
        $("#orderDes").val("");
        $('#product-container').empty();
        $('#orderdetailsItems').empty();
        orderDetailsInput.text(0);
        return { "orderDetails": orderDetails, "order": order };
    }
}

$("#submit").click(function () {
    let data = getInputForAddAlbum();
    console.log(data);
    $.ajax({
        url: '/Order/Save',
        type: 'POST',
        content: "application/json; charset=utf-8",
        datatype: 'json',
        data: data,
        success: function (res) {
            bootbox.alert("Save successful", function() {
                window.location.href = res.newUrl;
            });
        },
        err: function () {
            bootbox.alert("Error when calling ajax")
        }
    })
})