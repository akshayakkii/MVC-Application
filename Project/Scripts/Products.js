// Update the product table whenever the product page is displayed
updateProductTable();

// Posts the product data from the form to the controller
function addProduct() {
    var product = {
        ProductName: $("#addProductModal #ProductName").val(),
        ProductPrice: $("#addProductModal #ProductPrice").val()
    };
    
    //$.post("/Products/AddProduct", product, function (data, status) {
    //    $("#addProductModal #ProductName").val('');
    //    $("#addProductModal #ProductPrice").val('');

    //    updateProductTable();
    //});
    $.ajax({
        type: "POST",
        url: "/Products/AddProduct",
        data: product,
        error: function (xhr, statusText, error) {
            alert("Error: " + statusText + " " + error);
        },
        success: function (data) {
            $("#addProductModal #ProductName").val('');
           $("#addProductModal #ProductPrice").val('');

            updateProductTable();
        }
    });
}


// Saves the edited product to the model
function editProduct() {
    var product = {
        ProductId: $("#ProductId").val(),
        ProductName: $("#editProductModal #ProductName").val(),
        ProductPrice: $("#editProductModal #ProductPrice").val()
    };

    $.post("/Products/EditProduct", product, function (data, status) {
        updateProductTable();
    });
}

// Deletes the selected product
function deleteProduct() {
    var data = {
        ProductId: $("#ProductId").val()
    };

    $.post("/Products/DeleteProduct", data, function (data, status) {
        updateProductTable();
    });
}

// Opens the edit product modal. Fills out the information of the form
function openEditProductModal(product) {
    $("#editProductModal").modal("toggle");
    $("#ProductId").val(product.ProductId);
    $("#editProductModal #ProductName").val(product.ProductName);
    $("#editProductModal #ProductPrice").val(product.ProductPrice);
}

// Opens the delete product modal. Fills out the information of the product you want to delete
function openDeleteProductModal(product) {
    $("#deleteProductModal").modal("toggle");
    $("#ProductId").val(product.ProductId);
    $("#deleteProductModal #ProductName").text(product.ProductName);
    $("#deleteProductModal #ProductPrice").text(product.ProductPrice);
}

// Populates the Products table
function updateProductTable() {
    $.get("/Products/GetProducts", function (data, status) {
        $("#productTable tr td").remove();

        for (var i = 0; i < data.length; i++) {
            updateProductRow(i, data[i]);
        }
    });
}

// Updates one row of the product table
function updateProductRow(index, product) {
    $("#productTable").append("<tr><td>" + product.ProductName + "</td>" +
        "<td>" + product.ProductPrice + "</td>" +
        "<td><button type='button' class='btn btn-warning' onclick='openEditProductModal(" + JSON.stringify(product) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteProductModal(" + JSON.stringify(product) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}