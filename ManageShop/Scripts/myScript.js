﻿$(document).ready(function () {
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
            if (product.length!=0) {
                //Cap nhat quantity
                var newQuantity = parseInt(product.find("#quantityToSave").val()) + parseInt(quantity);
                if (newQuantity > quantityToCheck) {
                    isValid = false;
                    productWrapper.find("#outOfStock").css("visibility", "visible");
                } else {

                    productWrapper.find("#outOfStock").css("visibility", "hidden");
                }

                if (quantity == '0' || quantity.length<1) {
                    isValid = false;
                    productWrapper.find("#errorQuantity").css("visibility", "visible");
                } else {
                    productWrapper.find("#errorQuantity").css("visibility", "hidden");
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
                    productWrapper.find("#outOfStock").css("visibility", "visible");
                } else {

                    productWrapper.find("#outOfStock").css("visibility", "hidden");
                }

                if (quantity == '0' || quantity.length < 1) {
                    isValid = false;
                    productWrapper.find("#errorQuantity").css("visibility", "visible");
                } else {
                    productWrapper.find("#errorQuantity").css("visibility", "hidden");
                }
                if (isValid) {
                    var productName = productWrapper.find("#productName").text();
                    var amount = price * quantity;
                    var newRow = `<tr class='valueOrder' id='${productID}'>
                <td> <input type='text' id='productToSave' class='form-control' value="${productName}" name='ProductId' disabled/>
                <input type="hidden" name="OrderDetail.ProductId" value="${productID}" />
                </td>               
                <td><input type='text' id='quantityToSave' class='form-control' value="${quantity}" name='OrderDetail.Quantity' disabled/></td>
                <td><input type='text' id='priceToSave' class='form-control' value="${price} " name='OrderDetail.UnitPrice' disabled/></td>
                <td><input type='text' id='amountToSave' class='form-control' value="${amount} " name='OrderDetail.Amount' disabled/></td>
                <td><input type='button' class='btn btn-danger remove' value='Remove' /></td></tr > `;
                    $("#orderdetailsItems").append(newRow);
                    removeOrder($(`#orderdetailsItems tr#${productID} input.remove`), productID);
                    orderDetailsInput.text(parseInt(orderDetailsInput.text()) + parseInt(quantity * price));    
                }

            }
                 
        });
        //productname, quantity, price
        //luu xuong albumid, productid
    }
    function removeOrder(btn, productID) {
        btn.click(()=> {
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
                        <h5 class="card-title" id="productName">${products[i].Name}</h5>
                        <p class="card-text">
                            Price: <span id="price">${products[i].Price}</span>
                                   <span id="checkQuantity" hidden>${products[i].Quantity}</span>                             
                        </p>
                            <input type="text" class="form-control" id="quantity"/>
                            <span id="errorQuantity" style="color:red; visibility:hidden">Please enter quantity again</span>
                            <span id="outOfStock" style="color:red; visibility:hidden">Quantity is not enought. Please enter again!!!</span>
                    </div>
                    
                </div>
                <div class="card-footer text-right">
                    <input id="add" type="button" value="add" class="btn btn-success" style="width:80px" />
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
        $("#errorDate").css("visibility", "visible");
    } else {
        $("#errorDate").css("visibility", "hidden");
    }


    order.Phone = $('#orderPhone').val();
    var phoneNumber = new RegExp('/[0-9-()+]{10}/');

    if (phoneNumber.test(order.Phone) || order.Phone.length<1) {
        isValid = false;
        $("#errorPhone").css("visibility", "visible");
    } else {
        $("#errorPhone").css("visibility","hidden");
    }

    order.Name = $('#orderName').val();

    if (order.Name.length < 3) {
        isValid = false;
        $("#errorName").css("visibility", "visible");
    } else {
        $("#errorName").css("visibility", "hidden");
    }
    order.Address = $('#orderAddress').val();

    if (order.Address.length < 4) {
        isValid = false;
        $("#errorAddress").css("visibility", "visible");
    } else {
        $("#errorAddress").css("visibility", "hidden");
    }
    order.Description = $('#orderDes').val();
    order.Total = parseInt($('#totalAmount').text());
    let orderValue = $('.valueOrder');
    if (isValid) {
        orderValue.each((index, ele) => {
            let orderDetail = {};
            orderDetail.ProductId = $(ele).find("input[name='OrderDetail.ProductId']").val();
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

        },
        err: function () {
            console.log("Error when calling ajax")
        }
    })
})