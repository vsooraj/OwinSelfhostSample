(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("itemResource",
                ["$resource",
                 itemResource]);

    function itemResource($resource) {
        return $resource("/api/items/:pageSize/:pageNumber/:filterBy/:orderBy", { pageSize: '@pageSize', pageNumber: '@pageNumber', filterBy: '@filterBy', orderBy: '@orderBy' });       
    }

}());
