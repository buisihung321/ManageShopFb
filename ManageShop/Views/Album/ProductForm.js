const productForm = `<div  class="card m-2 product-card shadow card-loading ">
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
                                            <input type="number" name="Quantity" id="quantity" placeholder="Quantity" value="0" class="form-control form-control-sm" />
                                    </div>
                                    <div class="card-footer text-muted">
                                        <button class="btn btn-danger btn-sm product-remove__btn">Remove</button>
                                        <button id="set-album-cover__btn" class="btn btn-primary btn-sm">Set as album photo</button>
                                    </div>
                                </div>`;

class ProductForm {
    constructor(name, price, quantity, btnFooter) {
        this.name = name;
        this.price = price;
        this.quantity = quantity;
        this.btnFooter = btnFooter;
    }

    render() {
        return `<div  class="card m-2 product-card shadow card-loading ">
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
                                            <input type="number" name="Quantity" id="quantity" placeholder="Quantity" value="0" class="form-control form-control-sm" />
                                    </div>
                                    <div class="card-footer text-muted">
                                        <button class="btn btn-danger btn-sm product-remove__btn">Remove</button>
                                        <button id="set-album-cover__btn" class="btn btn-primary btn-sm">Set as album photo</button>
                                    </div>
                                </div>`;
    }

}