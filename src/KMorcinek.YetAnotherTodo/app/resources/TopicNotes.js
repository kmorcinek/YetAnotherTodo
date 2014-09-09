angular.module('YetAnotherTodo')
    .factory('TopicNotes', function ($resource) {
        return $resource('/api/topics/:topicId/notes/:noteId', { topicId: '@topicId', noteId: '@noteId' });
    });