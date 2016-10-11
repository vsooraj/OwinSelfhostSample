(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("OperationListCtrl",
                    ["operationResource", OperationListCtrl]);

    function OperationListCtrl(operationResource) {
        var vm = this;

        vm.status = {
            type: "info",
            message: "ready",
            busy: false
        };
        vm.sortKey = 'operationId';
        vm.reverse = true;
        vm.sort = function (keyname) {
            vm.sortKey = keyname;   //set the sortKey to the param passed
            vm.reverse = !vm.reverse; //if true make it false and vice versa
        }
        vm.currentPage = 0;
        vm.searchText = "";
        vm.navigate = navigate;

        activate();

        function activate() {
            //if this is the first activation of the controller load the first page
            if (vm.currentPage === 0) {
                navigate(1);
            }
        }
        function navigate(pageNumber) {
            if (typeof (pageNumber) == "undefined") {
                pageNumber = vm.currentPage;
            }
            vm.status.busy = true;
            vm.status.message = "loading records";
            vm.currentPage = pageNumber;           
            operationResource.get({ pageSize: '10', pageNumber: pageNumber, filterBy: vm.searchText, orderBy: '' }, function (data) {
                console.log(data);
                vm.operations = data.operations;
                vm.totalCount = data.totalCount;
            });
        }

        //operationResource.query(function (data) {
        //    vm.operations = data;
        //});       

      
    }
}());
