angular.module('YetAnotherTodo')
    .service('LastTopicIdService', function (localStorageService) {
        var key = 'lastTopicId';

        this.set = function (id) {
            localStorageService.cookie.set(key, id);
        }

        this.get = function () {
            return localStorageService.cookie.get(key);
        }
    }
);