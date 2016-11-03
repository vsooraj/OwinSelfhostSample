(function () {
    "use strict";
    angular
        .module("miGuard")
        .controller("MainCtrl",
                    ["userAccount", "currentUser", '$state', MainCtrl]);
    function MainCtrl(userAccount, currentUser, $state) {
        var vm = this;
        vm.isLoggedIn = function () {
            return currentUser.getProfile().isLoggedIn;
        };
       
        toastr.options = {
            "closeButton": true,         
            "positionClass": "toast-top-center",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        //toastr.success('Welcome ', { timeOut: 5000 })
       
        vm.userData = {
            userName: '',
            password: btoa('')
        }

        vm.login = function () {
            vm.userData.userName = vm.userName;
            vm.userData.password = window.btoa(vm.password);
            userAccount.login.loginUser(vm.userData,
                function (data) {
                    vm.message = "";
                    vm.password = "";
                    if (currentUser != null) {
                        currentUser.setProfile(vm.userData.userName, data.accessToken, true);
                    }
                    //toastr.info(vm.userData.userName + ' Logged in ', { timeOut: 5000 })
                },
                function (response) {
                    vm.password = "";
                    vm.message = response.data.message + "\r\n";
                    //toastr.error(vm.message, { timeOut: 5000 })
                    if (response.data.exceptionMessage) {
                        vm.message += response.data.exceptionMessage;
                        toastr.error(vm.message,  { timeOut: 5000 });
                    }

                    if (response.data.error) {
                        vm.message += response.data.error;
                        toastr.error(vm.message, { timeOut: 5000 })
                    }
                });
        }
        vm.logout = function () {
            vm.message = "Logged out ";
            toastr.info(vm.userData.userName + ' Logged out ', { timeOut: 5000 })
          
            vm.userData = {
                userName: '',
                email: '',
                password: '',
                confirmPassword: ''
            }
            currentUser.setProfile("", "", false);
            $state.go('home');
        }
    }

}());