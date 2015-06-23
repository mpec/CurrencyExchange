(function () {

    'use strict';

    angular.module('app').controller('resultController', [
        '$location', 'currencyService', 
        function($location, currencyService) {
            var self = this;
            self.loading = true;
            self.data = $location.search();

            currencyService.get(self.data).success(function(data) {
                self.result = data;
                self.loading = false;
            }).error(function (data, status) {
                self.loading = false;
                if (status === 400) {
                    self.error = "Please select correct date.";
                } else {
                    self.error = "Error. Please try again in a short while.";
                }
            });
        }
    ]);
})();