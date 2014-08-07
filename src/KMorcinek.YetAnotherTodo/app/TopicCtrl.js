﻿angular.module('YetAnotherTodo').controller('TopicCtrl',
    function ($scope, $http, $location, $stateParams, Topics) {
        $scope.notes = [];
    
        $scope.topicId = $stateParams.topicId;

        if ($scope.topicId !== undefined) {
            $scope.notes = Topics.get({id: $scope.topicId}, function(data){
                $scope.notes = data.notes;
                $scope.topicName = data.name;
            });
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

            $http.post('/api/topics/insert/' + $scope.topicId, newNote).
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
                $http.get('/api/topics/delete/' + $scope.topicId + '/' + item.id).
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

        Topics.query(function(data){
            for (var i = 0; i < data.length; i++) {
                data[i].slug = generateSlug(data[i].name);
            }

            $scope.topics = data;

            setFocusOnNewNote();
        });
    }
);