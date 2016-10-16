var Salvis = Salvis || {};

(function () {
    //TODO: refactor 
    Salvis.loadNotifications = function () {
        Salvis.ajaxCallAPI('/Messages/GetNotifications', 'GET', null, function (result) {
            var html = '<ul><li> <a href="Message/Details/testid-123" target="_self"><i class="fa fa-4x fa-email default"></i><span class="date pull-right">{input-date}</span><span class="name">{subject}</span>test ext</a></li></ul>';
            $('#messages-content').html(html);
            var messageCounter = result.count;
            if (messageCounter == 0) {
                $('#messages-counter').hide();
                $('#messageUnreadCount-menu').hide();
            } else {
                $('#messages-counter').html(messageCounter);
                $('#messages-counter').show();
                $('#messageUnreadCount-menu').html(messageCounter);
                $('#messageUnreadCount-menu').show();
            }
        });
    }

})();