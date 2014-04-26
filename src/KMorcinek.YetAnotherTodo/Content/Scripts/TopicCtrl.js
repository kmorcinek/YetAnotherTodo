YetAnotherTodo.controller('TopicCtrl',
    function ($scope, $http, $location, $routeParams) {
        $scope.notes = [];
    
        $scope.topicId = $routeParams.topicId;

        if ($scope.topicId !== undefined) {
            $http.get('/api/topic/' + $scope.topicId).
               success(function (data) {
                   $scope.notes = data.Notes;
                   $scope.topicName = data.Name;
               });
        } else {
            $scope.topicName = 'Choose a topic';
        }

        $scope.addNewNote = function (text) {
            var notes = $scope.notes;
            var maxId = 0;
            for (var i = 0; i < notes.length; i++) {
                if (notes[i].Id > maxId) {
                    maxId = notes[i].Id;
                }
            }
            var newNote = { Content: text, Id: maxId + 1 };

            $http.post('/api/topic/insert/' + $scope.topicId, newNote).
                success(function (data) {
                    $scope.notes.push(newNote);
                    $scope.newNoteText = "";
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        };

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

        $http.get('/api/topic').
            success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].Slug = generateSlug(data[i].Name);
                }

                $scope.topics = data;
            });
    }
);