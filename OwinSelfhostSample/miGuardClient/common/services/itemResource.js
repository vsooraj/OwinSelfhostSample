(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("itemResource",
                ["$resource", "currentUser", "appSettings",
                 itemResource]);

    //function itemResource($resource) {
    //    return $resource("/api/items/:pageSize/:pageNumber/:filterBy/:orderBy/:reverse", { pageSize: '@pageSize', pageNumber: '@pageNumber', filterBy: '@filterBy', orderBy: '@orderBy', reverse: '@reverse' });
    //}
    function itemResource($resource, currentUser, appSettings) {
        return $resource(appSettings.serverPath + "/api/items/:itemId", null,
            {
                'get': {
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                },
                'query': {
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                },
                'remove': {
                    method: 'DELETE',
                    headers: {
                        'Authorization': 'Bearer ' + currentUser.getProfile().token,
                        transformRequest: function (data, headersGetter) {
                            var str = [];
                            for (var d in data)
                                str.push(encodeURIComponent(d) + "=" +
                                                    encodeURIComponent(data[d]));
                            return str.join("&");
                        }
                    }


                }
            })
    }


}());
