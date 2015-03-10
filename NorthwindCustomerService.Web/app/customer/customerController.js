(function () {
    'use strict';

    angular
        .module('app')
        .controller('customerController', customerController);

    customerController.$inject = ['$scope','datacontext', 'notificationFactory', 'modalDialogFactory', 'throttleFactory'];

    function customerController($scope, datacontext, notificationFactory, modalDialogFactory, throttleFactory) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerController';

        var filterThrottle = throttleFactory.createThrottle(500);

        vm.customers = [];
        vm.prevCustomer = {};
        vm.newCustomer = {};

        vm.insertMode = false;
        vm.isLoading = false;
        
        vm.filter = "";
        vm.filterText = "";


        /*******************PAGING**********************/
        vm.totalCustomersPages = 0;
        vm.pageSize = 10;
        vm.currentPage = 1;
        vm.nextPage = nextPage;
        vm.prevPage = prevPage;
        vm.firstPage = firstPage;
        vm.lastPage = lastPage;
        vm.goToPage = goToPage;
        vm.searchPage = 1;
        vm.onSearchKeydown = onSearchKeydown;

        /*************************************************/
        /*swapping between insert and read mode*/
        vm.toggleInsertMode = toggleInsertMode;
        vm.cancel = cancel;
        vm.toggleEditMode = toggleEditMode;
        vm.cancelEdit = cancelEdit;
        vm.editing = false;

        /*help delaying the filtering*/
        vm.filterChanged = filterChanged;
        vm.clearFilter = clearFilter;

        /*CRUD Methods*/
        vm.loadPagedCustomers = loadPagedCustomers;
        vm.addCustomer = addCustomer;
        vm.updateCustomer = updateCustomer;
        vm.deleteCustomer = deleteCustomer;

        activate();

        function activate() {
            vm.isLoading = true;
            getCustomersCount();
            loadPagedCustomers("Customers Loaded.");
        }

        function failed(error) {

            vm.isLoading = false;
            var msg = "An error has occured. ";
            if (error)
                 msg = msg + error.statusText;
            notificationFactory.error(msg);
        }

        function toggleInsertMode()
        {
            vm.insertMode = !vm.insertMode;
        }

        function toggleEditMode(customer) {
            vm.editing = vm.customers.indexOf(customer);
            vm.prevCustomer = angular.copy(customer);
        }

        function cancel() {
            toggleInsertMode();
        }

        function cancelEdit() {
            if (vm.editing !== false) {
                vm.customers[vm.editing] = vm.prevCustomer;
                vm.editing = false;
            }

        }

        function filterChanged() {
           
            filterThrottle.run(function () {
                // update filter
                $scope.$apply(function () {
                    vm.filter = vm.filterText;
                });
            });
        }
        function clearFilter() {
            vm.filterText = "";
            vm.filter = "";
        }

        /*********************Paging Methods******************/
        function nextPage() {
            vm.searchPage= ++vm.currentPage;
            loadPagedCustomers();
        }

        function firstPage()
        {
            vm.searchPage = vm.currentPage = 1;
            loadPagedCustomers();
        }

        function lastPage() {
            vm.searchPage =  vm.currentPage = vm.totalCustomersPages;
            loadPagedCustomers();
        }

        function prevPage() {
            vm.searchPage = vm.currentPage = --vm.currentPage;
            loadPagedCustomers();
        }

        function goToPage()
        {
          
            var regex = new RegExp('^\\d+$');
            if (regex.test(vm.searchPage))
            {
                var page = Math.floor(vm.searchPage);
                if (vm.currentPage != page)
                {
                    vm.currentPage = page;
                    loadPagedCustomers();
                }
            }
        }

        function onSearchKeydown($event)
        {
            if ($event.which == 13)
            {
                goToPage();
            }
        }

/****************************************************/

        /**************CRUD Methods***************/

        function getCustomersCount()
        {
            datacontext.getCustomersCount().success(countSuccess, failed);

            function countSuccess(data) {
                vm.totalCustomersPages = Math.ceil(data / vm.pageSize);
            }
        }

        function loadPagedCustomers(msg) {
            datacontext.getPagedCustomers(vm.currentPage, vm.pageSize).then(success, failed);
            function success(result) {
                vm.isLoading = false;
                vm.customers = result.data;
                if (msg)
                    notificationFactory.success(msg);
            }

        }

        function addCustomer()
        {
            datacontext.addCustomer(vm.newCustomer).then(success, failed);
            function success(addedCustomer) {
                getCustomersCount(); //update Records count on addition;
                vm.customers.unshift(addedCustomer);
                vm.toggleInsertMode();
                notificationFactory.success('Customer Added.');
            }

        }
       
        function updateCustomer(customer) {
          
            datacontext.updateCustomer(customer).then(success, updateFailed);

            function success() {
                notificationFactory.success('Customer Updated.');
            }

            function updateFailed() {
                cancelEdit();
                failed();
            }
        }

        function deleteCustomer(customer) {
            var title = 'Confirm Delete';
            var msg = "Are you sure you want to delete customer ID:" + customer.customerID;
            modalDialogFactory.show(title, msg, deleteConfirmed);

            function deleteConfirmed() {

                datacontext.deleteCustomer(customer).then(success, failed);

                function success() {

                    getCustomersCount(); //update Records count on delete;
                    var index = vm.customers.indexOf(customer);
                    
                    if (index !== -1)
                        vm.customers.splice(index, 1);
                    notificationFactory.success("Customer Deleted");
                }
            }
        }
    }
})();
