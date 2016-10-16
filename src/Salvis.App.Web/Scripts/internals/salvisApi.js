var Salvis = Salvis || {};

(function () {
    var api = '/api/';

    Salvis.ajaxCall = function (url, method, data, callback) {
        console.log('ajax-request | To: ' + url);
        return $.ajax({
            type: method,
            cache: false,
            dataType: "json",
            url: url,
            data: data,
            async: true,
            contentType: "application/json",
            processData: true,
            error: JsonRError
        }).success(function (response) {
            if (callback !== undefined)
                callback(response.responseText);
        });
    };

    Salvis.ajaxCallAPI = function (url, method, data, callback) {
        console.log('api-ajax-request | To: ' + api + url);
        return $.ajax({
            type: method,
            cache: false,
            dataType: "json",
            url: api + url,
            data: data,
            async: true,
            contentType: "application/json",
            processData: true,
            error: JsonRError
        }).success(function (res) {
            if(callback)
                callback(res);
        });
    };

    function JsonRError(xhr) {
        if (xhr.responseJSON && xhr.responseJSON.ModelState) {
            var msg = 'Salvis-Error [' + xhr.status + '] in CallBack: ' + this.url;
            msg = msg + ' | Object: ' + xhr.responseJSON.ModelState;
            console.error(msg, xhr.responseText);
        } else {
            console.error('JsonRError ahead > ', xhr);
        }
    }

    Salvis.resources = {}; // Global variable.

    (function ($) {
        var resourcesLoaded = ko.observable();
        Salvis.resourcesLoaded = {
            subscribe: function (callback) {
                resourcesLoaded.subscribe(callback);
                if (Salvis.resources) callback(Salvis.resources);
            }
        };
        $.getJSON("/Resource/GetResources", function (data) {
            Salvis.resources = data;
            resourcesLoaded(data);
        });
    })(jQuery);

})();

