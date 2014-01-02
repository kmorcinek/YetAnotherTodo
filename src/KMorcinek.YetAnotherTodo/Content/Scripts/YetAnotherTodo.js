var yetAnotherTodoConfig = function ($routeProvider) {
    $routeProvider
      .when('/:anyTopicName/:topicId', {
          controller: 'TopicController',
          templateUrl: 'Content/view/topic.html'
      })
      .when('/', {
          controller: 'TopicController',
          templateUrl: 'Content/view/topic.html'
      })
    .when('/admin/', {
        controller: 'AdminController',
        templateUrl: 'Content/view/admin.html'
    })
    ;
};

var YetAnotherTodo = angular.module('YetAnotherTodo', ['ngRoute', 'ngSanitize']).
config(yetAnotherTodoConfig);