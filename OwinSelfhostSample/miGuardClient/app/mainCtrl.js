(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("MainCtrl",
                    ["userAccount", "currentUser", MainCtrl]);
    function MainCtrl(userAccount, currentUser) {
        var vm = this;
        vm.isLoggedIn = function () {
            return currentUser.getProfile().isLoggedIn;
        };
        vm.message = "Welcome ";
    
        vm.userData = {            
            userName:'cabot\\sooraj.v',
            password:  btoa('c@b0t1234')
        }
      
        vm.login = function () {
            vm.userData.userName = vm.userName;
            vm.userData.password = window.btoa(vm.password);
            userAccount.login.loginUser(vm.userData,
                function (data) {                
                    vm.message = "";//data.accessToken;
                    vm.password = "";
                    //vm.token = data.accessToken;
                    currentUser.setProfile(vm.userData.userName, data.accessToken);
                },
                function (response) {
                    vm.password = "";                   
                    vm.message = response.statusText + "\r\n";
                    if (response.data.exceptionMessage)
                        vm.message += response.data.exceptionMessage;

                    if (response.data.error) {
                        vm.message += response.data.error;
                    }
                });
        }
        vm.logout = function () {
            vm.message = "Logged out ";
         
            vm.userData = {
                userName: '',
                email: '',
                password: '',
                confirmPassword: ''
            }
        }
    }

}());