(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("operationResource",
                ["$resource",
                 operationResource]);

    //function operationResource($resource) {
    //    return $resource("/api/operations/:operationId")
    //}
    function operationResource($resource) {
        return $resource("/api/operations/:pageSize/:pageNumber/:filterBy/:orderBy/:reverse", { pageSize: '@pageSize', pageNumber: '@pageNumber', filterBy: '@filterBy', orderBy: '@orderBy', reverse: '@reverse' });
    }    

}());
