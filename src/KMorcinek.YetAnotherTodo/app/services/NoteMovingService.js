(function () {
    'use strict';

    function NoteMovingService($http) {
        this.dropCallback = function (data, evt, targetTopicId) {
            $http.post('/api/topics/' + targetTopicId + '/notes/move/' + data.id);
        };
    }

    angular
        .module('YetAnotherTodo')
        .service('NoteMovingService', NoteMovingService);
})();