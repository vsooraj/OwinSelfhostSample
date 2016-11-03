(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("ItemListCtrl",
                    ["itemResource", "currentUser", '$state', ItemListCtrl]);

    function ItemListCtrl(itemResource, currentUser,$state) {

        var vm = this;
        vm.itemsToDelete = [];
        vm.masterCheck = false;
        vm.isLoggedIn = function () {
            return currentUser.getProfile().isLoggedIn;
        };
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

        vm.deleteAll = function () {            
            vm.itemsToDelete = [];
            var boxes = document.getElementsByClassName("rowSelector");
            for (var bix in boxes) {
                if (boxes[bix].checked) {
                    var ix = boxes[bix].value;
                    vm.itemsToDelete.push(ix);
                }
            }          
         
            itemResource.remove({ ids: vm.itemsToDelete.toString() }, function (data) {
                vm.message = response.data.message + "\r\n";
                toastr.error(vm.message, { timeOut: 5000 })
            },
                function (response) {
                    vm.password = "";
                    vm.message = response.data.message + "\r\n";
                    toastr.error(vm.message, { timeOut: 5000 })
                    if (response.data.exceptionMessage) {
                        vm.message += response.data.exceptionMessage;
                        toastr.error(vm.message, { timeOut: 5000 });
                    }

                    if (response.data.error) {
                        vm.message += response.data.error;
                        toastr.error(vm.message, { timeOut: 5000 })
                    }
                });
            vm.navigate(vm.currentPage);
        }
      
    }

   
}());
