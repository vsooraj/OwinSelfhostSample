﻿(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("operationResource",
                ["$resource",
                 operationResource]);

    function operationResource($resource) {
        return $resource("/api/operations/:operationId")
    }

}());