(function () {
    'use strict';

    function NoteMovingService($http) {
        var self = this;
        this.draggedNote = undefined;
        
        this.startCallback = function (event, ui, note) {
            self.draggedNote = note;
        };

        this.dropCallback = function (event, ui, id) {
            $http.post('/api/topics/' + id + '/notes/move/' + self.draggedNote.id);

            self.removingNoteCallback(self.draggedNote);

            self.draggedNote = undefined;
        };

        this.registerRemovingNoteCallback = function(callback) {
            self.removingNoteCallback = callback;
        }
    }
    
    angular
        .module('YetAnotherTodo')    
        .service('NoteMovingService', NoteMovingService);
})();