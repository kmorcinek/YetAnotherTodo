var yetAnotherTodoConfig = function ($stateProvider, $urlRouterProvider, $locationProvider, localStorageServiceProvider) {
    
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

    localStorageServiceProvider.cookie.expiry = 365;
    localStorageServiceProvider.prefix = window.location.hostname;
};

var YetAnotherTodo = angular.module('YetAnotherTodo', ['ui.router', 'ngSanitize', 'ngResource', 'LocalStorageModule']).
config(yetAnotherTodoConfig);