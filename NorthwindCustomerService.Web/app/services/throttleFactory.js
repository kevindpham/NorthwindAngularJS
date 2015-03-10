(function () {
    'use strict';

    angular
        .module('app')
        .factory('throttleFactory', throttleFactory);

    function throttleFactory() {
        var service = {
            createThrottle: createThrottle
        };

        return service;

        function createThrottle(delay) {
            var throttleTimer = null;
            var throttleDelay = delay;

            if (!throttleDelay) {
                // use default value 250ms
                throttleDelay = 250;
            }

            return {
                run: function (action) {
                    return function () {
                        clearTimeout(throttleTimer);

                        throttleTimer = setTimeout(function () {
                            // execute action
                            action.apply();

                            // dispose timer
                            throttleTimer = null;
                        }, throttleDelay);
                    }();
                }
            };
        }
    }
})();