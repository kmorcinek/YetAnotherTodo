(function () {
    'use strict';

    function TopicNotes($resource) {
        return $resource('/api/topics/:topicId/notes/:noteId', { topicId: '@topicId', noteId: '@noteId' });
    }
    
    angular
        .module('YetAnotherTodo')    
        .factory('TopicNotes', TopicNotes);
})();