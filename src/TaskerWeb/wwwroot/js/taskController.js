(function () {
    'use strict';

    var controllerId = 'taskController';

    angular.module('taskerApp').controller(controllerId, ['$scope', 'todoFactory', todoController]);

    function todoController($scope, todoFactory) {

        $scope.todos = [];

        todoFactory.getAll().success(function (data) {
            $scope.todos = data;
        }).error(function (error) {
            alert(error)

        });
    }


})();