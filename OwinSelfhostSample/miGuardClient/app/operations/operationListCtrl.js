(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("OperationListCtrl",
                    ["operationResource", "currentUser", OperationListCtrl]);

    function OperationListCtrl(operationResource, currentUser) {
        var vm = this;
        //Status
        vm.isLoggedIn = function () {
            return currentUser.getProfile().isLoggedIn;
        };
        vm.status = {
            type: "info",
            message: "ready",
            busy: false
        };

        //Sort
        vm.sortKey = 'operationId';
        vm.reverse = true;
        vm.sortBy = 'asc';

        vm.sort = function (keyname) {
            vm.sortKey = keyname;   
            vm.reverse = !vm.reverse; //if true make it false and vice versa
            vm.sortBy = vm.reverse == true ? "asc" : "desc";
            navigate(vm.currentPage);
        }
        vm.currentPage = 0;
        vm.searchText = "";
        vm.navigate = navigate;

        activate();

        function activate() {
            if (vm.currentPage === 0) {
                navigate(1);
            }
        }
        function navigate(pageNumber) {
            if (typeof (pageNumber) == "undefined") {
                pageNumber = vm.currentPage;
            }
            //if (vm.searchText == "") {
            //    vm.searchText = ""
            //}
            //Status
            vm.status.busy = true;
            vm.status.message = "loading records";

            vm.currentPage = pageNumber;   
            var opeartionId = 0;
            opeartionId = parseInt(vm.searchText);
          
            var myBool = null;
            var filterby = "";
            filterby = isNaN(opeartionId) == true ? "" : "operationId eq " + opeartionId;
            if (vm.searchText === "true" || vm.searchText === "false") {
                myBool = vm.searchText;
                filterby += filterby.length > 0 ? " or required eq " + myBool : " required eq " + myBool;
            }
            var myObj = new Object();
            //Sort
            myObj.$orderby = vm.sortKey + " " + vm.sortBy + " ";
            myObj.$inlinecount = "allpages";
            if (vm.searchText != "" && vm.searchText != "Search") {
                myObj.$filter = filterby;
            }

            myObj.$skip = vm.currentPage === 0 ? vm.currentPage : (vm.currentPage - 1) * 10;

            operationResource.get(myObj, function (data) { 
                vm.operations = data.items;
                vm.totalCount = data.count;
            });

        }
 
    }
}());
