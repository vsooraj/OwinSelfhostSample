(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("ItemListCtrl",
                    ["itemResource", ItemListCtrl]);

    function ItemListCtrl(itemResource) {
        var vm = this;
        itemResource.query(function (data) {
           // alert(data);
            vm.items=data;
        });      

        vm.showImage = false;

        vm.toggleImage = function () {
            vm.showImage = !vm.showImage;
        }
    }
}());
