$(document).ready(function () {
    $("#add").click(function () {
        var album = $('#album').val();
        var product = $('#product').val();
        var quantity = $('#quantity').val();
        var price = $('#price').val();
        var newRow = "<tr><td><input type='text' id='albumToSave' class='form-control' value=" + album + " name='OrderDetail.AlbumId'/></td><td><input type='text' id='productToSave' class='form-control' value=" + product + " name='OrderDetail.ProductId'/></td><td><input type='text' id='albumToSave' class='form-control' value=" + quantity + " name='OrderDetail.Quantity'/></td><td><input type='text' id='albumToSave' class='form-control' value=" + price + " name='OrderDetail.UnitPrice'/></td><td><input type='button' class='btn btn-danger remove' value='Remove'/></td></tr>";
        $("#orderdetailsItems").append(newRow);
        console.log($('#albumToSave').val());
    });
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });
})
const orderValue = $("#orderdetailsItems");
function getInputForAddAlbum() {
    let orderDetails = {};
    orderDetails.AlbumId = orderValue.find("input[name='OrderDetail.AlbumId']").val();
    orderDetails.ProductId = orderValue.find("input[name='OrderDetail.ProductId']").val();
    orderDetails.Quantity = orderValue.find("input[name='OrderDetail.Quantity']").val();
    orderDetails.UnitPrice = orderValue.find("input[name='OrderDetail.UnitPrice']").val();
    console.log(orderDetails);
}
$("#submit").click(function () {
    getInputForAddAlbum();
})