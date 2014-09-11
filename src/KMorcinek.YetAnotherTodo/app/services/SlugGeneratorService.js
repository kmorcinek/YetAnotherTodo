(function () {
    'use strict';

    function SlugGeneratorService() {
        this.generateSlug = function (str) {
            str = str.replace(/[^a-zA-Z0-9\s]/g, "");
            str = str.toLowerCase();
            return str.replace(/\s/g, '-');
        };
    }
    
    angular
        .module('YetAnotherTodo')
        .service('SlugGeneratorService', SlugGeneratorService);
    
})();