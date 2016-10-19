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
        vm.sortBy = 'asc';
        vm.sort = function (keyname) {
            vm.sortKey = keyname;   //set the sortKey to the param passed
            vm.reverse = !vm.reverse; //if true make it false and vice versa
            //alert(vm.sortKey);
            //alert(vm.reverse);
            if (vm.reverse) { vm.sortBy = 'asc' } else { vm.sortBy = 'desc' }
            navigate(vm.currentPage);
        }
        vm.currentPage = 0;
        vm.searchText = "";
        vm.filterBy = "";
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
         
            //itemResource.get({ pageSize: '10', pageNumber: pageNumber, filterBy: vm.searchText, orderBy:vm.sortKey, reverse: vm.reverse }, function (data) {
            //    console.log(data);
            //    vm.items = data.items;
            //    vm.totalCount = data.totalCount;
            //}); 
            //"substringof(tolower('" + vm.searchText + "'), tolower(sourceDevice)) eq true",
            // var f = ODataFilterBuilder;
            var filterby = "substringof(tolower('" + vm.searchText + "'), tolower(sourceDevice)) eq true";
            //var orderby = vm.sortKey+" "+vm.sortBy+" "
            //var filters = [orderby, " $inlinecount: 'allpages'"];
           
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
                myObj.$skip = (vm.currentPage-1)*10;
            }
            
           // alert(myObj);
            itemResource.get(myObj, function (data) {
                //console.log(data);
                //alert(data.items);
                //alert(data.count);
                vm.items = data.items;
                vm.totalCount = data.count;
            });
          
        }
      
    }
}());
