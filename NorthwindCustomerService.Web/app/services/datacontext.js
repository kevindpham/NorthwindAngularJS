(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, datacontext);

    datacontext.$inject = ['resourceFactory', '$http'];
    function datacontext(resourceFactory, $http) {
       
        var customerService = resourceFactory.getService('/api/v1/customers');

        var service = {
            getCustomersCount: getCustomersCount,
            getCustomers: getCustomers,
            getPagedCustomers: getPagedCustomers,
            getCustomer: getCustomer,
            updateCustomer: updateCustomer,
            addCustomer: addCustomer,
            deleteCustomer: deleteCustomer
        };

        return service;

        //customerize route so it is better to use low level $http
        function getCustomersCount() {
            return $http.get('/api/v1/customers/count');
        }

        //customerize route so it is better to use low level $http
        function getPagedCustomers(page, take) {
            return $http.get('/api/v1/customers?page=' + page + '&take=' + take);
        }

        function getCustomers() {
            return customerService.query().$promise;
        }

        function getCustomer(id) {
            return customerService.get({id: id}).$promise;
        }

        function updateCustomer(customer) {
            return customerService.update({ id: customer.customerID }, customer).$promise;
        }

        function addCustomer(customer) {
            return customerService.save(customer).$promise;
        }

        function deleteCustomer(customer) {
            return customerService.delete({ id: customer.customerID }, customer).$promise;
        }
       
    }
})();