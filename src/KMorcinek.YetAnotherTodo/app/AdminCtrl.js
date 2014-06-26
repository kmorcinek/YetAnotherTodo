angular.module('YetAnotherTodo').controller('AdminCtrl',
    function ($scope, $http) {
        $scope.notes = [];

        $http.get('/api/topic').
            success(function (data) {
                $scope.notes = data;
            });

        $scope.addNew = function (text) {
            var newSlimTopic = { Name: text };

            $http.post('/api/topic/insert/', newSlimTopic).
                success(function (data) {
                    $scope.notes.push(data);
                    $scope.newText = "";
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        };

        $scope.isShownChange = function (item) {
            $http.post('/api/topic/update/', item)
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        };

        $scope.remove = function (item) {
            var confirmed = confirm("Delete?");

            if (confirmed) {
                $http.get('/api/topic/delete/' + item.Id).
                    success(function (data) {
                        var index = $scope.notes.indexOf(item);
                        $scope.notes.splice(index, 1);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            }
        };
    }
);