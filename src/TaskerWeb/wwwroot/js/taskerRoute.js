(function () {
    'use strict';

    angular.module('taskerApp').config([
        '$routeProvider',
        function ($routeProvider) {
            $routeProvider.
                when('/newTask', {
                    templateUrl: '/partials/taskNew.html',
                    controller: 'taskNewController'
                }).
                when('/', {
                    templateUrl: '/partials/taskList.html',
                    controller: 'taskController'
                })
        }
    ]);

})();