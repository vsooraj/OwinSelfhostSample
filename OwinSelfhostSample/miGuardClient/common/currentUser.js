(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("currentUser",
                  currentUser)

    function currentUser() {
        var profile = {
            isLoggedIn: false,
            username: "",
            token: ""
        };

        var setProfile = function (username, token, LoggedIn) {
            profile.username = username;
            profile.token = token;
            profile.isLoggedIn = LoggedIn;
        };

        var getProfile = function () {

            return profile;
        }

        return {
            setProfile: setProfile,
            getProfile: getProfile
        }
    }
})();
