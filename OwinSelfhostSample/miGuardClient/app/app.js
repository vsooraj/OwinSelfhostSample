(function () {
    "use strict";
    var app = angular.module("miGuard", ["common.services", "ui.router"]);

    app.config(["$stateProvider",
           "$urlRouterProvider",
           function ($stateProvider, $urlRouterProvider) {
               $urlRouterProvider.otherwise("/");

               $stateProvider
                   .state("home", {
                       url: "/",
                       templateUrl: "app/welcomeView.html"
                   })
                   // Items
                   .state("itemList", {
                       url: "/items",
                       templateUrl: "app/items/itemListView.html",
                       controller: "ItemListCtrl as vm"
                   })
                   .state("itemDetail", {
                     url: "/items/:itemId",
                     templateUrl: "app/items/itemDetailView.html",
                     controller: "itemDetailCtrl as vm",
                     resolve: {
                         itemResource: "itemResource",

                         product: function (itemResource, $stateParams) {
                             var itemId = $stateParams.itemId;
                             return itemResource.get({ itemId: itemId }).$promise;
                         }
                     }
                   })
                // Operations
                   .state("operationList", {
                       url: "/operations",
                       templateUrl: "app/operations/operationListView.html",
                       controller: "OperationListCtrl as vm"
                   })
                   .state("operationDetail", {
                       url: "/operationitems/:operationId",
                       templateUrl: "app/operations/operationDetailView.html",
                       controller: "OperationListCtrl as vm",
                       resolve: {
                           operationResource: "operationResource",

                           operation: function (operationResource, $stateParams) {
                               var itemId = $stateParams.operationId;
                               return operationResource.get({ operationId: operationId }).$promise;
                           }
                       }
                   })

           }]
    );
}());

