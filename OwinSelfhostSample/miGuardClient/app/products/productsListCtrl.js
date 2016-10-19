(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("ProductsListCtrl",
                    ["productResource", ProductsListCtrl]);

    function ProductsListCtrl(productResource) {
        var vm = this;
        productResource.get({
            $filter: "ProductId eq 2 or substringof('Lumia', ProductName) eq true",
            $orderby: "ProductId desc",
            $inlinecount: "allpages"
        }, function (data) {
            alert(data);
            vm.products = data.items;
            vm.totalCount = data.count;
           // alert();
        });       
        

     }
}());
