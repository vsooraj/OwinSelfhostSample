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
        vm.sortBy = 'asc';

        vm.sort = function (keyname) {
            vm.sortKey = keyname;   //set the sortKey to the param passed
            vm.reverse = !vm.reverse; //if true make it false and vice versa
            if (vm.reverse) { vm.sortBy = 'asc' } else { vm.sortBy = 'desc' }
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
                vm.searchText = ""
            }
            vm.status.busy = true;
            vm.status.message = "loading records";
            vm.currentPage = pageNumber;           
            //operationResource.get({ pageSize: '10', pageNumber: pageNumber, filterBy: vm.searchText, orderBy:vm.sortKey, reverse: vm.reverse }, function (data) {
            //    console.log(data);
            //    vm.operations = data.operations;
            //    vm.totalCount = data.totalCount;
            //});
            var opeartionId = 0;
            opeartionId = parseInt(vm.searchText);
            // var require = true;
            //require = parseBool(vm.searchText)
            var myBool = null;// Boolean(vm.searchText > 0);
            var filterby = "";
          
            if (isNaN(opeartionId)) {
                filterby = "";
            }
            else {

                //if (filterby.length > 0) {
                    filterby = "operationId eq " + opeartionId;
                //}
            }
            if (vm.searchText === "true" || vm.searchText === "false") {
                myBool = vm.searchText;
                if (filterby.length > 0) {
                    filterby += " or required eq " + myBool;
                }
                else { filterby += " required eq " + myBool; }
            }
            var myObj = new Object();
            myObj.$orderby = vm.sortKey + " " + vm.sortBy + " ";
            myObj.$inlinecount = "allpages";
            if (vm.searchText != "" && vm.searchText != "Search") {
                myObj.$filter = filterby;
            }
            if (vm.currentPage === 0) {
                myObj.$skip = vm.currentPage;
            }
            else {
                myObj.$skip = (vm.currentPage - 1) * 10;
            }

             //alert(myObj.toString());
            operationResource.get(myObj, function (data) {
                //console.log(data);
                //alert(data.items);
                //alert(data.count);
                vm.operations = data.items;
                vm.totalCount = data.count;
            });

        }
            
        //operationResource.query(function (data) {
        //    vm.operations = data;
        //});       

      
    }
}());
