var yetAnotherTodoConfig = function($stateProvider, $urlRouterProvider) {
    
    $urlRouterProvider.otherwise('/');
    
    $stateProvider
        .state('admin', {
            url: '/admin',
            templateUrl: 'app/view/admin.html',
            controller: 'AdminCtrl',
        })
        .state('topic', {
            url: '/:topicId/:anyTopicName',
            templateUrl: 'app/view/topic.html',
            controller: 'TopicCtrl',
        })
        .state('home', {
            url: '/',
            templateUrl: 'app/view/topic.html',
            controller: 'TopicCtrl',
        });
};

var YetAnotherTodo = angular.module('YetAnotherTodo', ['ui.router', 'ngSanitize']).
config(yetAnotherTodoConfig);