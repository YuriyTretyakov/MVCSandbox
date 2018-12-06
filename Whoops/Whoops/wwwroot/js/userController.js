(function () {
    "use strict";
    angular.module("app-user")
        .controller("userController", userController);


    function userController($http) {

        var vm = this;
        vm.isBusy = true;
        vm.newUser = {};
        vm.user = {};
        vm.errorMessage = "";
        vm.isPasswordValid = true;
        

        vm.createUser = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            console.log(vm.newUser);
            $http.post("/UserManagement/CreateUser", vm.newUser)
                .then(function (response) {
                    
                    vm.newUser = {};
                },
                function (error) {
                        vm.errorMessage = "Failed to create user:   " + error.data;
                    })
                .finally(function () {
                    vm.isBusy = false
                });
        }

            
            //vm.getUserInfo = function (userId) {
            //    vm.isBusy = true;
            //    vm.errorMessage = "";

            //    $http.get("UserManagement/GetUserInfo/" + userId).then(function (response) {

            //        angular.copy(response.data, vm.user);

            //    }, function (error) {
            //        vm.errorMessage = "Failed to get user info: " + error;
            //    }).finally(function () {
            //        vm.isBusy = false;
            //    });


            //}

        vm.validatePassword = function () {
            vm.isPasswordValid = (vm.newUser.confirmPassword === vm.newUser.password);
            console.log(vm.newUser.confirmPassword === vm.newUser.password);
        }

    }
})();