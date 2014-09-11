(function () {
    'use strict';

    function TopicsListCtrl($scope, TopicsFactory) {
        TopicsFactory.getTopics().then(function (data) {
            $scope.topics = data;
        });
    }
    
    angular
        .module('YetAnotherTodo')
        .controller('TopicsListCtrl', TopicsListCtrl);
})();