(function () {
    'use strict';

    angular
        .module('app')
        .factory('notificationFactory', notificationFactory);

    function notificationFactory() {

        toastr.options = {
            "showDuration": "10000",
            "hideDuration": "100",
            "timeOut": "2000",
            "positionClass": "toast-bottom-right",
            "extendedTimeOut": "5000",
        };

        var service = {
            success : success,
            error : error
        };

        return service;
       

        function success(text) {
            if (text === undefined) {
                text = '';
            }
            toastr.success("Success. " + text);
        }

        function error(text) {
            if (text === undefined) {
                text = '';
            }
            toastr.error("Error. " + text);
        }

    }
})();