(function () {
    'use strict';
    
    angular.module('app').controller('selectController', [
        '$location',
        function ($location) {
            var self = this;
            self.opened = false;
            self.selectedDate = new Date();
            self.currency = 'USD';

            self.toggleOpen = function() {
                self.opened = !self.opened;
            };

            self.availableCurrencies = ['USD', 'EUR', 'GBP', 'CHF'];

            self.submit = function() {
                $location.path('/result');
                $location.search({
                    selectedDate: self.selectedDate.toISOString(), //the code doesn't take timezones into account
                    currency: self.currency
                });
            };

        }
    ]);
})();