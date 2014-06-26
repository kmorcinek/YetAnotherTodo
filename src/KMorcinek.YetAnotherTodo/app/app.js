var yetAnotherTodoConfig = function ($routeProvider) {
    $routeProvider
      .when('/:anyTopicName/:topicId', {
          controller: 'TopicCtrl',
          templateUrl: 'app/view/topic.html'
      })
      .when('/', {
          controller: 'TopicCtrl',
          templateUrl: 'app/view/topic.html'
      })
    .when('/admin/', {
        controller: 'AdminCtrl',
        templateUrl: 'app/view/admin.html'
    })
    ;
};

var YetAnotherTodo = angular.module('YetAnotherTodo', ['ngRoute', 'ngSanitize']).
config(yetAnotherTodoConfig);