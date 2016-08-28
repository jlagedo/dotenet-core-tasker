(function () {
    'use strict';

    var controllerId = 'taskNewController';

    angular.module('taskerApp').controller(controllerId, ['$scope', 'todoFactory', '$location', taskNewController]);

    function taskNewController($scope, todoFactory, $location) {

        $scope.save = function () {

            var task = { name: $scope.taskTitle }

            todoFactory.create(task).success(function (data) {
                $location.path('/');
            }).error(function (error) {
                console.log(error);

            });

        };

  
    }

})();
