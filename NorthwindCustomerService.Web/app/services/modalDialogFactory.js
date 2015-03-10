(function () {
    'use strict';

    angular
        .module('app')
        .factory('modalDialogFactory', modalDialogFactory);

    modalDialogFactory.$inject = ['$modal'];

    function modalDialogFactory($modal)
    {
        var service = {
            show : show
        }

        return service;

        function show(title, msg, confirmCallback, cancelCallback) {

            title = title | "Delete Confirmation";
            // Show window
            var modalInstance = $modal.open({
                templateUrl: 'app/templates/modal-window.html',
                controller: modalWindowController,
                size: 'sm',
                resolve: {
                    title: function () {
                        return title;
                    },
                    body: function () {
                        return msg;
                    }
                }
            });

            // Register confirm and cancel callbacks
            modalInstance.result.then(
                // if any, execute confirm callback
                function () {
                    if (confirmCallback != undefined) {
                        confirmCallback();
                    }
                },
                // if any, execute cancel callback
                function () {
                    if (cancelCallback != undefined) {
                        cancelCallback();
                    }
                });
        }
    }
  
    // Internal controller used by the modal window
    var modalWindowController = ['$scope', '$modalInstance', 'title', 'body', function ($scope, $modalInstance, title, body) {
        $scope.title = "";
        $scope.body = "";

        // If specified, fill window title and message with parameters
        if (title) {
            $scope.title = title;
        }
        if (body) {
            $scope.body = body;
        }

        $scope.confirm = function () {
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };
    }];
        
})();