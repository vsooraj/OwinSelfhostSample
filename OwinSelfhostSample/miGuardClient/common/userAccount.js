﻿(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("userAccount",
                ["$resource", "appSettings",
                 userAccount]);

    function userAccount($resource, appSettings) {

        return {
            registration: $resource(appSettings.serverPath + "/api/Account/Register", null,
                    {
                        'registerUser': { method: 'POST' }
                    }),
            login: $resource("/api/Account/Token", null,
                    {
                        'loginUser': {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded'
                            },
                            transformRequest: function (data, headersGetter) {
                                var str = [];
                                for (var d in data)
                                    str.push(encodeURIComponent(d) + "=" +
                                                        encodeURIComponent(data[d]));
                                return str.join("&");
                            }

                        }
                    }),
            logout: $resource("/api/Account/Logout", null,
                    {
                        'logout': {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded'
                            },
                            transformRequest: function (data, headersGetter) {
                                var str = [];
                                for (var d in data)
                                    str.push(encodeURIComponent(d) + "=" +
                                                        encodeURIComponent(data[d]));
                                return str.join("&");
                            }
                        }
                    })
        }
    }




}());
