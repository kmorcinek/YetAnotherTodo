(function () {
    'use strict';

    function LastTopicIdService(localStorageService) {
        var key = 'lastTopicId';
    
        this.set = function (id) {
            localStorageService.cookie.set(key, id);
        }
    
        this.get = function () {
            return localStorageService.cookie.get(key);
        }
    }
    
    angular
        .module('YetAnotherTodo')    
        .service('LastTopicIdService', LastTopicIdService);
})();