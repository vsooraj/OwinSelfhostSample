(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("ItemListCtrl",
                    ["itemResource", ItemListCtrl]);

    function ItemListCtrl(itemResource) {      

        var vm = this;
        vm.status = {
            type: "info",
            message: "ready",
            busy: false
        };
        vm.sortKey = 'itemId';
        vm.reverse = true;
        vm.sort = function (keyname) {
            vm.sortKey = keyname;   //set the sortKey to the param passed
            vm.reverse = !vm.reverse; //if true make it false and vice versa
            //alert(vm.sortKey);
            //alert(vm.reverse);
            navigate(vm.currentPage);
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
            if (vm.searchText == "") {
                vm.searchText = "Search"
            }
            vm.status.busy = true;
            vm.status.message = "loading records";
            vm.currentPage = pageNumber;         
         
            itemResource.get({ pageSize: '10', pageNumber: pageNumber, filterBy: vm.searchText, orderBy:vm.sortKey, reverse: vm.reverse }, function (data) {
                console.log(data);
                vm.items = data.items;
                vm.totalCount = data.totalCount;
            });
        }
      
    }
}());
