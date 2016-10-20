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
            vm.sortKey = keyname; 
            vm.reverse = !vm.reverse; //if true make it false and vice versa            
            vm.sortBy = vm.reverse == true ? "asc" : "desc";
            navigate(vm.currentPage);
        }
        vm.currentPage = 0;
        vm.searchText = "";
        vm.filterBy = "";
        vm.navigate = navigate;     
        activate();

        function activate() {           
            if (vm.currentPage === 0) {
                navigate(1);
            }
        }
        function getFilterBy(paramKey,paramValue) {
            if (paramValue != "" && paramKey != "") {
                return "substringof(tolower('" + paramValue + "'), tolower(" + paramKey + ")) eq true";
            }
            else
                return "";
        }
        function navigate(pageNumber) {          
            if (typeof (pageNumber) == "undefined") {
                pageNumber = vm.currentPage;
            }     
            vm.status.busy = true;
            vm.status.message = "loading records";
            vm.currentPage = pageNumber;

            var itemId = 0;
            itemId = parseInt(vm.searchText);

            var myBool = null;
            var filterby = "";
            var requestType = getFilterBy("requestType", vm.searchText);
            var encryptionProvider = getFilterBy("encryptionProvider", vm.searchText);
            var encryptionKey = getFilterBy("encryptionKey", vm.searchText);
            var sourceDevice = getFilterBy("sourceDevice", vm.searchText);

            filterby = isNaN(itemId) == true ? "" : "itemId eq " + itemId;           
            filterby += filterby.length > 0 ? " or " + requestType : requestType;
            filterby += filterby.length > 0 ? " or " + encryptionProvider : encryptionProvider;
            filterby += filterby.length > 0 ? " or " + encryptionKey : encryptionKey;
            filterby += filterby.length > 0 ? " or " + sourceDevice : sourceDevice;

            var myObj = new Object();
            myObj.$orderby = vm.sortKey + " " + vm.sortBy + " ";
            myObj.$inlinecount = "allpages";
            if (vm.searchText != "" && vm.searchText != "Search") {
                myObj.$filter = filterby;
            }        
            myObj.$skip = vm.currentPage === 0 ? vm.currentPage : (vm.currentPage - 1) * 10;
            itemResource.get(myObj, function (data) {
                vm.items = data.items;
                vm.totalCount = data.count;
            });
          
        }
      
    }
}());
