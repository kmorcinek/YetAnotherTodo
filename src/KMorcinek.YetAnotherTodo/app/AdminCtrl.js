(function () {
    'use strict';

    function AdminCtrl($scope, $location, TopicsFactory, SlugGeneratorService) {
        TopicsFactory.getTopics().then(function (data) {
            $scope.topics = data;
        });
    
        $scope.addNew = function () {
            TopicsFactory.add($scope.newText).then(function(data){
                $location.path('/' + data.id + '/' + SlugGeneratorService.generateSlug($scope.newText));
            });
        };
    
        $scope.isShownChange = function (item) {
            TopicsFactory.save(item);
        };
    
        $scope.remove = function (item) {
            var confirmed = confirm("Delete?");
    
            if (confirmed) {
                TopicsFactory.remove(item);
            }
        };
    }
    
    angular
        .module('YetAnotherTodo')
        .controller('AdminCtrl', AdminCtrl);
})();