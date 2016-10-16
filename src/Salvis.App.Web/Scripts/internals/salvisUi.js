var Salvis = Salvis || {};

(function () {

    Salvis.UI = {};

    //active the menu item relative to current page.
    function activeCurrentPage() {
        var path = window.location.pathname;
        path = path.replace(/\/$/, "");
        path = decodeURIComponent(path);
        $(".cl-vnavigation a ").each(function () {
            var href = $(this).attr('href');
            if (path === href) {
                $(this).closest('li').addClass('active');
            }
        });
    }

    function initCollapseBotton() {
        $('#sidebar-collapse').click(Salvis.UI.toggleSideBar);
    }

    function showTooltip(x, y, contents) {
        $("<div id='tooltip'>" + contents + "</div>").css({
            position: "absolute",
            display: "none",
            top: y + 5,
            left: x + 5,
            border: "1px solid #000",
            padding: "5px",
            'color': '#fff',
            'border-radius': '2px',
            'font-size': '11px',
            "background-color": "#000",
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    function loadToolTip(obj) {
        obj.bind("plothover", function (event, pos, item) {

            var str = "(" + pos.x.toFixed(2) + ", " + pos.y.toFixed(2) + ")";
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;
                    $("#tooltip").remove();
                    var x = item.series.data[item.dataIndex][0],
                        y = item.series.data[item.dataIndex][1];
                    var dateFee = item.series.yaxisSub[item.dataIndex];
                    showTooltip(item.pageX, item.pageY,
                        "(" + item.series.label + ") Cuota No. " + dateFee + " = " + y + "| Fecha: " + x);
                }
            } else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        });
    }

    Salvis.UI.toggleSideBar = function (show) {
        var b = $("#sidebar-collapse")[0];
        var w = $("#cl-wrapper");
        switch (show) {
            case true:
                $(".fa", b).addClass("fa-angle-left").removeClass("fa-angle-right");
                w.removeClass("sb-collapsed");
                break;
            case false:
                $(".fa", b).removeClass("fa-angle-left").addClass("fa-angle-right");
                w.addClass("sb-collapsed");
                break;
            default:
                if (w.hasClass("sb-collapsed")) {
                    $(".fa", b).addClass("fa-angle-left").removeClass("fa-angle-right");
                    w.removeClass("sb-collapsed");
                } else {
                    $(".fa", b).removeClass("fa-angle-left").addClass("fa-angle-right");
                    w.addClass("sb-collapsed");
                }
                break;
        }
    }

    //TODO: Check it
    Salvis.UI.callHideForm = function (item) {
        item.modal('hide');
        item.html('');
    }

    Salvis.UI.fillDropListWithRequest = function (e, url) {
        $(e).prop('disabled', 'disabled');
        $.getJSON(url, function (data) {
            $.each(data, function (item, result) {
                $("<option>").val(result.Id).text(result.Value).appendTo(e);
            });
        }).done(function () {
            $(e).removeProp('disabled');
        });
    }

    Salvis.UI.loadingOverlayShow = function (e) {
        if (e === null) {
            e = 'body';
        }
        var overlay = '<div id="overlay">' + '<img id="loading" src="../../Content/themes/base/images/spinner.gif">' + '</div>';
        $(overlay).appendTo(e);
    }

    Salvis.UI.loadingOverlayHidden = function () {
        $('#overlay').remove();
    }

    Salvis.UI.messageBox = function (obj) {
        $.gritter.add({
            title: obj.Title,
            text: obj.Message,
            class_name: obj.Type /* success, info, warning, danger*/
        });
    }

    Salvis.UI.messageBoxError = function (text) {
        var msg = {};
        msg.Message = text;
        msg.Title = 'Error';
        msg.Type = 'danger';
        Salvis.UI.messageBox(msg);
    }

    Salvis.UI.operationBegin = function (obj) {
        Salvis.UI.loadingOverlayShow();
    }

    Salvis.UI.operationComplete = function (obj) {
        if (!obj.Code && !obj.Data) {
                var parser = new DOMParser();
                var r = parser.parseFromString(obj, "text/html");
                Salvis.UI.messageBoxError($("#warning-zone", r).html());
                
        } else {
            switch (obj.Code) {
                case 206:   //PartialInformation
                    window.sessionStorage["Salvis-Message"] = JSON.stringify(obj.Message);
                    window.location = obj.Data !== null || obj.Data !== undefined ? obj.Data : window.location;
                    break;
                case 302:   //Redirect
                    window.location = obj.Data;
                    break;
                case 400:   //Error
                    Salvis.UI.messageBoxError(obj.Message.Message);
                    break;
                case 200:   //Success
                case 500:   //Internal
                default:
                    Salvis.UI.messageBox(obj.Message);
                    break;
            }
        }
        Salvis.UI.loadingOverlayHidden();
    }

    function checkForSessionMessages() {
        if (window.sessionStorage["Salvis-Message"] != null) {
            var msg = JSON.parse(window.sessionStorage["Salvis-Message"]);
            Salvis.UI.messageBox(msg);
            window.sessionStorage.removeItem("Salvis-Message");
        }
    }

    $(document).ready(function () {
        initCollapseBotton();
        activeCurrentPage();
        checkForSessionMessages();
    });

})();