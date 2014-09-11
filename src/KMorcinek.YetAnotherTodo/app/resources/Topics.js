(function () {
    'use strict';

    function Topics($resource) {
        return $resource('/api/topics/:id', { id: '@id' });
    }
    
    angular
        .module('YetAnotherTodo')    
        .factory('Topics', Topics);
})();