angular.module('YetAnotherTodo').controller('AdminCtrl',
    function ($scope, $location, TopicsService) {
        TopicsService.getTopics().then(function (data) {
            $scope.topics = data;
        });

        $scope.addNew = function () {
            TopicsService.add($scope.newText).then(function(data){
                $location.path('/' + data.id + '/' + generateSlug($scope.newText));
            });
        };

        $scope.isShownChange = function (item) {
            TopicsService.save(item);
        };

        $scope.remove = function (item) {
            var confirmed = confirm("Delete?");

            if (confirmed) {
                TopicsService.remove(item);
            }
        };
    }
);