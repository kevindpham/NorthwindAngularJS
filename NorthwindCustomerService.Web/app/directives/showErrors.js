(function() {
    'use strict';

    // TODO: replace app with your module name
    angular.module('app').directive('showErrors', showErrors);

    showErrors.$inject = ['$timeout'];
    function showErrors($timeout) {
        // Usage:
        // 
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'A',
            require: '^form'
        };
        return directive;

        function link(scope, element, attrs, formCtrl) {
            // find the text box element, which has the 'name' attribute
            var inputEl = element[0].querySelector("[name]");
            // convert the native text box element to an angular element
            var inputNgEl = angular.element(inputEl);
            // get the name on the text box so we know the property to check
            // on the form controller
            var inputName = inputNgEl.attr('name');

            // only apply the has-error class after the user leaves the text box
            inputNgEl.bind('blur', function () {
                element.toggleClass('has-error', formCtrl[inputName].$invalid);
            });

        }
    }

})();