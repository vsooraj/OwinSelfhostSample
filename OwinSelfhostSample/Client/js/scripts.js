var viewModel = {
    contacts: ko.observableArray()
}

$(document).ready(function () {
    jQuery.support.cors = true;
    $.ajax(
    {
        url: "http://localhost:8080/api/Items/",
        type: "GET"
    }
    ).done(function (data) {
        for (i = 0; i < data.length; i++) {
            viewModel.contacts.push(data[i]);
        }
        ko.applyBindings(viewModel);
    }).fail(function (data) {
        alert(data);
    });
});

$(document).on("click", "#search", function () {
    jQuery.support.cors = true;
    $.ajax(
    {
        url: "http://localhost:8080/api/Items?id=" + $("#searchText").val(),
        type: "GET"
    }
    ).done(function (data) {
        viewModel.contacts.removeAll();
        for (i = 0; i < data.length; i++) {
            viewModel.contacts.push(data[i]);
        }
    }).fail(function (data) {
        alert(data.message);
    });
});
