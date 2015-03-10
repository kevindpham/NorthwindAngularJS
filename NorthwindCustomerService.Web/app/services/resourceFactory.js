(function () {
    'use strict';

    angular
        .module('app')
        .factory('resourceFactory', resourceFactory);

    resourceFactory.$inject = ['$resource'];

    function resourceFactory($resource) {

        var service = {
            getService : _getService
        }

        return service;

        function _getService(endPoint) {
            if (endPoint === '') {
                throw "Invalid end point";
            }

            // create resource to make AJAX calls
            return $resource(endPoint + '/:id',
            {
                id: '@customerID' // default URL params, '@' Indicates that the value should be obtained from a data property 
            },
            {
                'update': { method: 'PUT' } // add update to actions (is not defined by default)
            });
        }

    }

})();