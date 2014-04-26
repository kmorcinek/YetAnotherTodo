var yetAnotherTodoConfig = function ($routeProvider) {
    $routeProvider
      .when('/:anyTopicName/:topicId', {
          controller: 'TopicCtrl',
          templateUrl: 'Content/view/topic.html'
      })
      .when('/', {
          controller: 'TopicCtrl',
          templateUrl: 'Content/view/topic.html'
      })
    .when('/admin/', {
        controller: 'AdminCtrl',
        templateUrl: 'Content/view/admin.html'
    })
    ;
};

var YetAnotherTodo = angular.module('YetAnotherTodo', ['ngRoute', 'ngSanitize']).
config(yetAnotherTodoConfig);