(function () {
    'use strict';

    function TopicsListCtrl($scope, TopicsFactory, NoteMovingService) {
        TopicsFactory.getTopics().then(function (data) {
            $scope.topics = data;
        });

        $scope.dropCallback = NoteMovingService.dropCallback;
    }
    
    angular
        .module('YetAnotherTodo')
        .controller('TopicsListCtrl', TopicsListCtrl);
})();