(function () {

    'use strict';

    angular.module('app').controller('resultController', [
        '$location', 'currencyService', 
        function($location, currencyService) {
            var self = this;

            self.data = $location.search();

            currencyService.get(self.data).success(function(data) {
                self.result = data;
            });
        }
    ]);
})();