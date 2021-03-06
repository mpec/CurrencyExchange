﻿(function() {

    'use strict';

    var currencyService = function($http, $log) {

        var self = this;

        self.get = function(params) {
            return $http.get('/api/currency/get', {
                    params: {
                        currency: params.currency,
                        date: params.selectedDate
                    }
                })
                .success(function(data) {
                    return data;

                })
                .error(function(data, status) {
                    $log.error(status);
                    return (data);
                });
        };
    };

    angular.module('app').service('currencyService', ['$http', '$log', currencyService]);
})();