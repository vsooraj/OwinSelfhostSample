(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("OperationListCtrl",
                    ["operationResource", OperationListCtrl]);

    function OperationListCtrl(operationResource) {
        var vm = this;
        operationResource.query(function (data) {
            vm.operations = data;
        });       

        vm.showImage = false;

        vm.toggleImage = function () {
            vm.showImage = !vm.showImage;
        }
    }
}());
