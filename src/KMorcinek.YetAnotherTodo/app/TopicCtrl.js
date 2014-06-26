angular.module('YetAnotherTodo').controller('TopicCtrl',
    function ($scope, $http, $location, $stateParams) {
        $scope.notes = [];
    
        $scope.topicId = $stateParams.topicId;

        if ($scope.topicId !== undefined) {
            $http.get('/api/topic/' + $scope.topicId).
               success(function (data) {
                   $scope.notes = data.Notes;
                   $scope.topicName = data.Name;
               });
        } else {
            $scope.topicName = 'Choose a topic';
        }

        $scope.addNote = function () {
            var notes = $scope.notes;
            var maxId = 0;
            for (var i = 0; i < notes.length; i++) {
                if (notes[i].Id > maxId) {
                    maxId = notes[i].Id;
                }
            }
            var newNote = { Content: $scope.newNoteText, Id: maxId + 1 };

            $http.post('/api/topic/insert/' + $scope.topicId, newNote).
                success(function (data) {
                    $scope.notes.push(newNote);
                    $scope.newNoteText = "";

                    setFocusOnNewNote();
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        };

        $scope.addNoteByEnter = function(e) {
            if(e.keyCode !== 13) return;

            $scope.addNote();
        }

        $scope.remove = function (item) {
            var confirmed = confirm("Delete?");

            if (confirmed) {
                $http.get('/api/topic/delete/' + $scope.topicId + '/' + item.Id).
                    success(function (data) {
                        var index = $scope.notes.indexOf(item);
                        $scope.notes.splice(index, 1);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            }
        };

        $scope.topics = [];

        var setFocusOnNewNote = function () {
            $('#new-content-text').focus();
        }

        $http.get('/api/topic').
            success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].Slug = generateSlug(data[i].Name);
                }

                $scope.topics = data;

                setFocusOnNewNote();
            });
    }
);