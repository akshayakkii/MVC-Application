// Update the store table whenever the store page is displayed
updateStoreTable();

// Posts the store data from the form to the controller
function addStore() {
    var store = {
        StoreName: $("#addStoreModal #StoreName").val(),
        StoreAddress: $("#addStoreModal #StoreAddress").val()
    };

    //$.post("/Stores/AddStore", store, function (data, status) {
    //    $("#addStoreModal #StoreName").val('');
    //    $("#addStoreModal #StoreAddress").val('');

    //    updateStoreTable();
    //});
    $.ajax({
        type: "POST",
        url: "/Stores/AddStore",
        data: store,
        error: function (xhr, statusText, error) {
            alert("Error: " + statusText + " " + error);
        },
        success: function (data) {
            debugger;
            $("#addStoreModal #StoreName").val('');
            $("#addStoreModal #StorePrice").val('');

            updateStoreTable();
        }
    });
}

// Saves the edited store to the model
function editStore() {
    var store = {
        StoreId: $("#StoreId").val(),
        StoreName: $("#editStoreModal #StoreName").val(),
        StoreAddress: $("#editStoreModal #StoreAddress").val()
    };

    $.post("/Stores/EditStore", store, function (data, status) {
        updateStoreTable();
    });
}

// Deletes the selected store
function deleteStore() {
    var data = {
        StoreId: $("#StoreId").val()
    };

    $.post("/Stores/DeleteStore", data, function (data, status) {
        updateStoreTable();
    });
}

// Opens the edit store modal. Fills out the information of the form
function openEditStoreModal(store) {
    $("#editStoreModal").modal("toggle");
    $("#StoreId").val(store.StoreId);
    $("#editStoreModal #StoreName").val(store.StoreName);
    $("#editStoreModal #StoreAddress").val(store.StoreAddress);
}

// Opens the delete store modal. Fills out the information of the store you want to delete
function openDeleteStoreModal(store) {
    $("#deleteStoreModal").modal("toggle");
    $("#StoreId").val(store.StoreId);
    $("#deleteStoreModal #StoreName").text(store.StoreName);
    $("#deleteStoreModal #StoreAddress").text(store.StoreAddress);
}

// Populates the store table
function updateStoreTable() {
    $.get("/Stores/GetStore", function (data, status) {
        $("#storeTable tr td").remove();

        for (var i = 0; i < data.length; i++) {
            updateStoreRow(i, data[i]);
        }
    });
}

// Updates one row of the Store table
function updateStoreRow(index, store) {
    $("#storeTable").append("<tr><td>" + store.StoreName + "</td>" +
        "<td>" + store.StoreAddress + "</td>" +
        "<td><button type='button' class='btn btn-warning' onclick='openEditStoreModal(" + JSON.stringify(store) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteStoreModal(" + JSON.stringify(store) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}