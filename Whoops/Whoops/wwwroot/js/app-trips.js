(function () {
    "use strict";
    angular.module("app-trips", ["simpleControls", "ngRoute"])
        .config(['$routeProvider','$locationProvider', function ($routeProvider, $locationProvider) {

            $routeProvider.when("/",
                {
                    controller: "tripsController",
                    controllerAs: "vm",
                    templateUrl: "/views/tripsView.html"
                });
            $routeProvider.when("/editor/:tripName", {
                controller: "tripsEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripsEditorView.html"
            }
              
            );

            $routeProvider.otherwise({ redirectTo: "/" });
            $locationProvider.html5Mode(false).hashPrefix('!');
        }]);
})();