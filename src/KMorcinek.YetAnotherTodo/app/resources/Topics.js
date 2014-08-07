angular.module('YetAnotherTodo')
    .factory('Topics', function ($resource) {
        return $resource('/api/topics/:id', { id: '@id' });
    });