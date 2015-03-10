(function () {
    'use strict';

    var id = 'app';

    // TODO: Inject modules as needed.
    var app = angular.module('app', [
        // Angular modules 
        'ngRoute',
        'ngAnimate',        // animations
        'ui.router',
        'ui.bootstrap',
        'ngResource',

        // Custom modules 
        
        // 3rd Party Modules
        'wc.Directives'
    ]);

    // Execute bootstrapping code and any dependencies.
    // TODO: inject services as needed.
    app.run(['$q', '$rootScope',
        function ($q, $rootScope) {

        }]);
})();