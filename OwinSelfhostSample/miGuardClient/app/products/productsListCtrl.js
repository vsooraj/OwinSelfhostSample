(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("ProductsListCtrl",
                    ["productResource", ProductsListCtrl]);

    function ProductsListCtrl(productResource) {
        var vm = this;
        productResource.query({
            $filter: "ProductId eq 2 or substringof('Lumia', ProductName) eq true",
            $orderby: "ProductId asc",
            $inlinecount:"allpages"
        }, function (data) {
            vm.products = data;
        });       
        

     }
}());
