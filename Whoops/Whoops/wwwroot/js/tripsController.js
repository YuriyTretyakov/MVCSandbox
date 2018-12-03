
(function () {
    "use strict";
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;
        vm.isBusy = true;
        vm.trips = [];
        vm.newTrip = {};
        vm.errorMessage = "";

        $http.get("/api/trips").then(function (response) {

            angular.copy(response.data, vm.trips);

        }, function (error) {
            vm.errorMessage = "Failed to load trips: " + error;
            }).finally(function () {
                vm.isBusy = false;
        });

        
        vm.addTrip = function () {

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips/add", vm.newTrip)
                .then(function (response) {
                    vm.trip.push(response.data);
                    vm.newTrip = {};
                },
                function (error) {
                    vm.errorMessage = "Failed to save new trip:" + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                    });

            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip = {};
        };
    }

})();