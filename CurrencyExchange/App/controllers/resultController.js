(function () {

    'use strict';

    angular.module('app').controller('resultController', [
        '$location', 'currencyService', 
        function($location, currencyService) {
            var self = this;

            self.data = $location.search();

            currencyService.get(self.data).success(function(data) {
                self.result = data;
            }).error(function(data, status) {
                if (status === 400) {
                    self.error = "Please select correct date.";
                } else {
                    self.error = "Error. Please try again in a short while.";
                }
            });
        }
    ]);
})();