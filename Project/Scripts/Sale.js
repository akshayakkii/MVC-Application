// Update the sale table whenever the sale page is displayed
updateSaleTable();

// Posts the sale data from the form to the controller
function addSale() {
    var sale = {
        // Get data from the form
        CustomerId: $("#addSaleModal #CustomerId").val(),
        ProductId: $("#addSaleModal #ProductId").val(),
        StoreID: $("#addSaleModal #StoreId").val(),
        DateSold: $("#addSaleModal #DateSold").val()
    };

    $.ajax({
        type: "POST",
        url: "/ProductSolds/AddSale",
        data: sale,
        error: function (xhr, statusText, error) {
            alert("Error: " + statusText + " " + error);
        },
        success: function (data) {
            updateSaleTable();
        }
    });

    //$.post("/ProductSolds/AddSale", sale, function (data, status) {
    //	$("#addSaleModal #CustomerId").val(''),
    //	$("#addSaleModal #ProductId").val(''),
    //	$("#addSaleModal #StoreId").val(''),
    //	$("#addSaleModal #DateSold").val('')

    //	updateSaleTable();
    //});
}

// Saves the edited sale to the model
function editSale() {
    var sale = {
        ProductSoldID: $("#ProductSoldID").val(),
        CustomerId: $("#editSaleModal #CustomerId").val(),
        ProductId: $("#editSaleModal #ProductId").val(),
        StoreId: $("#editSaleModal #StoreId").val(),
        DateSold: $("#editSaleModal #DateSold").val()
    };

    $.post("/ProductSolds/EditSale", sale, function (data, status) {
        updateSaleTable();
    });
}

// Deletes the selected sale
function deleteSale() {
    var data = {
        ProductSoldID: $("#ProductSoldID").val()
    };

    $.post("/ProductSolds/DeleteSale", data, function (data, status) {
        updateSaleTable();
    });
}

// Opens the edit sale modal. Fills out the information of the form
function openEditSaleModal(sale) {
    var dateObj = new Date(parseInt(sale.DateSold.substr(6)));
    var date = (dateObj.getMonth() + 1) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();

    $("#editSaleModal").modal("toggle");
    $("#ProductSoldID").val(sale.ProductSoldID);
    $("#editSaleModal #CustomerId").val(sale.CustomerId);
    $("#editSaleModal #ProductId").val(sale.ProductId);
    $("#editSaleModal #StoreID").val(sale.StoreID);
    $("#editSaleModal #DateSold").val(date);
}

// Opens the delete sale modal. Fills out the information of the sale you want to delete
function openDeleteSaleModal(sale) {
    var dateObj = new Date(parseInt(sale.DateSold.substr(6)));
    var date = (dateObj.getMonth() + 1) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();

    $("#deleteSaleModal").modal("toggle");
    $("#ProductSoldID").val(sale.ProductSoldID);
    $("#deleteSaleModal #CustomerId").val(sale.CustomerId);
    $("#deleteSaleModal #ProductId").val(sale.ProductId);
    $("#deleteSaleModal #StoreID").val(sale.StoreID);
    $("#deleteSaleModal #DateSold").val(date);
}

// Populates the sales table
function updateSaleTable() {
    $.get("/ProductSolds/GetSales", function (data, status) {
        $("#saleTable tr td").remove();
        for (var i = 0; i < data.length; i++) {
            updateSaleRow(i, data[i]);
        }
    });
}

// Updates one row of the sale table
function updateSaleRow(index, sale) {
    var dateObj = new Date(parseInt(sale.DateSold.substr(6)));
    var date = (dateObj.getMonth() + 1) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();

    $("#saleTable").append("<tr><td>" + date + "</td>" +
        "<td>" + sale.CustomerName + "</td>" +
        "<td>" + sale.ProductName + "</td>" +
        "<td>" + sale.StoreName + "</td>" +
        "<td><button type='button' class='btn btn-warning' onclick='openEditSaleModal(" + JSON.stringify(sale) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteSaleModal(" + JSON.stringify(sale) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}