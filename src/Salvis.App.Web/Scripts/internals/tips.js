
(function () {

    $('#tip-container').scroll(function () {
        var obj = $('#tip-container');
        if ((obj.height() + obj.scrollTop()) >= obj[0].scrollHeight) {

            var url = "/Library/LoadNewTip/";
            callerBase(url, "GET", null, AddTip);
        }
    });

    //call the tippartial to get all
    function initializeTips(max) {
        var url = "/Library/LoadPartialTip/?max=" + max;
        callerBase(url, "GET", null, AddTip);
    }

    //Insert tips on the  html list...
    function AddTip(html) {
        $('#tip-container').append(html);
    }

})();