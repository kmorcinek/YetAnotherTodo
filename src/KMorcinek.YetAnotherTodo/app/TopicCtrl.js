angular.module('YetAnotherTodo').controller('TopicCtrl',
    function ($scope, $location, $stateParams, TopicsService, TopicNotes, LastTopicIdService) {
        $scope.notes = [];
    
        $scope.topicId = $stateParams.topicId;

        var lastId = $scope.topicId || LastTopicIdService.get();
        if (lastId !== undefined) {
            $scope.topicId = lastId;
            
            TopicsService.get($scope.topicId, function(data){
                $scope.notes = data.notes;
                $scope.topicName = data.name;
            });

            LastTopicIdService.set($scope.topicId);
        } else {
            $scope.topicName = 'Choose a topic';
        }

        $scope.addNote = function () {
            var notes = $scope.notes;
            var maxId = 0;
            for (var i = 0; i < notes.length; i++) {
                if (notes[i].id > maxId) {
                    maxId = notes[i].id;
                }
            }
            var newNote = { content: $scope.newNoteText, id: maxId + 1 };

            TopicNotes.save({topicId: $scope.topicId}, newNote, function () {
                $scope.notes.push(newNote);
                $scope.newNoteText = "";

                setFocusOnNewNote();
            });
        };

        $scope.addNoteByEnter = function(e) {
            if(e.keyCode !== 13) return;

            $scope.addNote();
        }

        $scope.remove = function (item) {
            var confirmed = confirm("Delete?");

            if (confirmed) {
                TopicNotes.remove({topicId: $scope.topicId, noteId: item.id}, function () {
                    var index = $scope.notes.indexOf(item);
                    $scope.notes.splice(index, 1);
                });
            }
        };

        var setFocusOnNewNote = function () {
            $('#new-content-text').focus();
        }

        setFocusOnNewNote();
    }
);