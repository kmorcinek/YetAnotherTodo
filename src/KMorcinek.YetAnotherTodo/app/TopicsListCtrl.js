angular.module('YetAnotherTodo').controller('TopicsListCtrl',
    function ($scope, TopicsService) {
        TopicsService.getTopics().then(function (data) {
            $scope.topics = data;
        });
    }
);