angular.module('YetAnotherTodo').controller('AdminCtrl',
    function ($scope, $location, Topics) {
        $scope.notes = Topics.query();

        $scope.addNew = function () {
            Topics.save({}, { name: $scope.newText }, function(data){
                $location.path('/' + data.id + '/' + generateSlug($scope.newText));
            });
        };

        $scope.isShownChange = function (item) {
            Topics.save({ id: item.id }, item);
        };

        $scope.remove = function (item) {
            var confirmed = confirm("Delete?");

            if (confirmed) {
                Topics.remove({ id: item.id}, function () {
                    var index = $scope.notes.indexOf(item);
                    $scope.notes.splice(index, 1);
                });
            }
        };
    }
);