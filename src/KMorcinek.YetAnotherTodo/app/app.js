var yetAnotherTodoConfig = function ($stateProvider, $urlRouterProvider, $locationProvider) {
    
    $locationProvider.html5Mode(true);

    $urlRouterProvider.otherwise('/');
    
    $stateProvider
        .state('admin', {
            url: '/admin',
            templateUrl: '/app/view/admin.html',
            controller: 'AdminCtrl',
        })
        .state('topic', {
            url: '/:topicId/:anyTopicName',
            templateUrl: '/app/view/topic.html',
            controller: 'TopicCtrl',
        })
        .state('home', {
            url: '/',
            templateUrl: '/app/view/topic.html',
            controller: 'TopicCtrl',
        });
};

var YetAnotherTodo = angular.module('YetAnotherTodo', ['ui.router', 'ngSanitize']).
config(yetAnotherTodoConfig);