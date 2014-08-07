angular.module('YetAnotherTodo').controller('AdminCtrl',
    function ($scope, Topics) {
        $scope.notes = Topics.query();

        $scope.addNew = function () {
            var newTopic = { name: $scope.newText };

            Topics.save({}, newTopic, function(data){
                newTopic.id = data.id;
                newTopic.isShown = true;
                $scope.notes.push(newTopic);
                $scope.newText = "";
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