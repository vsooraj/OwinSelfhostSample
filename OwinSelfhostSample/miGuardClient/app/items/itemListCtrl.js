(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("ItemListCtrl",
                    ["itemResource", ItemListCtrl]);

    function ItemListCtrl(itemResource) {      

        var vm = this;
        itemResource.query(function (data) {
            //alert(data);
            vm.items = data;           
        });   

        vm.sortKey = 'itemId';
        vm.reverse = true;
        vm.sort = function (keyname) {
          //  alert(keyname);
            vm.sortKey = keyname;   //set the sortKey to the param passed
            vm.reverse = !vm.reverse; //if true make it false and vice versa
        }
        //vm.showImage = false;

        //vm.toggleImage = function () {
        //    vm.showImage = !vm.showImage;
        //}
       
    }
}());
