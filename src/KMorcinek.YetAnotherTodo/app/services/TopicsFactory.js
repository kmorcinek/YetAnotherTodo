(function () {
    'use strict';

    function TopicsFactory($q, Topics, SlugGeneratorService) {
        var topics = [];
    
        var deferred = $q.defer();
    
        Topics.query(function (data) {
            for (var i = 0; i < data.length; i++) {
                data[i].slug = SlugGeneratorService.generateSlug(data[i].name);
            }
    
            topics = data;
    
            deferred.resolve(topics)
        });
    
        this.getTopics = function () {
            return deferred.promise;
        }
    
        this.get = function(id, callback){
            Topics.get({id: id}, callback);
        }
    
        this.add = function (name) {
            var addDeferred = $q.defer();
    
            Topics.save({}, { name: name }, function (data) {
                var topic = {
                    name: name,
                    id: data.id,
                    isShown: true,
                }
                topics.push(topic);
                addDeferred.resolve(data);
            });
    
            return addDeferred.promise;
        }
    
        this.save = function(item) {
            Topics.save({ id: item.id }, item);
        }
    
        this.remove = function (item) {
            Topics.remove({ id: item.id }, function () {
                var index = topics.indexOf(item);
                topics.splice(index, 1);
            });
        }
    }
    
    angular
        .module('YetAnotherTodo')    
        .service('TopicsFactory', TopicsFactory);
})();