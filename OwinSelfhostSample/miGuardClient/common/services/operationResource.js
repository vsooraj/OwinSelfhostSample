(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("operationResource",
                ["$resource", "currentUser", "appSettings",
                 operationResource]);

    function operationResource($resource, currentUser, appSettings) {       // return $resource("/api/operations/:operationId")
        return $resource(appSettings.serverPath + "/api/operations/:operationId", null,
          {
              'get': {
                  headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
              },
              'query': {
                  headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
              }

          })


    }
    //function operationResource($resource) {
    //    return $resource("/api/operations/:pageSize/:pageNumber/:filterBy/:orderBy/:reverse", { pageSize: '@pageSize', pageNumber: '@pageNumber', filterBy: '@filterBy', orderBy: '@orderBy', reverse: '@reverse' });
    //}    

}());


