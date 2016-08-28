(function () {
    'use strict';

    var serviceId = 'todoFactory';

    angular.module('taskerApp').factory(serviceId, ['$http', todoFactory]);

    function todoFactory($http) {

        function getAll() {
            return $http.get('/api/todo');
        }

        function create(data) {
            return $http.post('/api/todo', data);
        }

        var service = {
            getAll: getAll,
            create: create
        };

        return service;
    }
})();