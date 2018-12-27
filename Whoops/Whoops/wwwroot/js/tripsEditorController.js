(function () {
    "use strict";
    angular.module("app-trips")
        .controller("tripsEditorController", tripsEditorController);

    function tripsEditorController($routeParams,$http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
       // vm.tripName = "SuperTrip";

        $http.get("/api/trips/" + vm.tripName + "/stops")
            .then(function (response) {
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops);
            },
                function (err) {
                    vm.errorMessage = "Failed to load stops " + err.data;
            }).finally(function () {
                vm.isBusy = false;
            })
    }

    function _showMap(stops) {

        if (stops && stops.length > 0) {


           // var lat = stops[3].latitude;
            //var ln = stops[3].longtitude;
            var lat =  24.0297;
            var ln = 49.8397;

            var map = L.map('map').setView([ln,lat], 13);

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            }).addTo(map);

            L.marker([ln, lat]).addTo(map)
                .bindPopup('A pretty CSS3 popup.<br> Easily customizable.')
                .openPopup();
            }
    }

})();
