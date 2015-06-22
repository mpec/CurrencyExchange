(function () {
    'use strict';

    var app = angular.module('app', [
        'ngRoute',
        'ui.bootstrap'
    ]);

    app.config([
        '$routeProvider',
        function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/App/views/select.html'
                })
                .when('/result', {
                    templateUrl: '/App/views/result.html'
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ]);

   //app.config([
   //     '$httpProvider',
   //     function ($httpProvider) {
   //         $httpProvider.interceptors.push('httpQuestInterceptor');
   //     }
   // ]);

    app.run();

})();