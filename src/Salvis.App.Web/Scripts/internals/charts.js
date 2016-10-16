
(function () {

    /*
     *  Charts & Graphics
     */

    function fillDataTable(obj) {
        $('#fee-table').dataTable({
            "bDestroy": true,
            "aaData": obj,
            "sScrollY": "200px",
            "bPaginate": false,
            "bFilter": false
        });
    }

    function setNextOperation(obj) {
        $('#expValue').text(obj.OperationExpectedRealAmount);
        $('#nextOperationDate').text(obj.OperationExpectedNextDate);
        $('#idealValue').text(obj.OperationExpectedExpAmount);
    }

    function GetDetails(obj) {
        Salvis.loadingOverlayShow();
        $('#goals-list > div > div > a').removeClass('active');
        obj.classList.add('active');
        var parent = obj.attributes["parentData"].value;
        //var parentType = obj.attributes["parentTypeData"].value;
        var url = "/" + window.goalName + "/FillGoalsGraphic?parentId=" + parent; //not used: + "&parentTypeId=" + parentType;
        console.log('Graficando: ' + url);

        callerBase(url, "GET", null, function (data) {
            drawChartGoal(data, "chartGoals");
            setNextOperation(data);
            fillDataTable(data.OperationDetails);
        }, loadingOverlayHidden);

        //fillGraphic(url);
    }

    //Draw Goal 
    function drawChartGoal(data, elementId) {
        elementId = '#' + elementId;
        if ($(elementId).size() == 0) return;
        $.plot($(elementId), data.Line, {
            series: {
                lines: {
                    show: true,
                    lineWidth: 2,
                    fill: true,
                    fillColor: {
                        colors: [{
                            opacity: 0.2
                        }, {
                            opacity: 0.01
                        }]
                    }
                },
                points: {
                    show: true
                },
                shadowSize: 2,
                yaxisSub: data.YaxisSub
            },
            legend: {
                show: false
            },
            grid: {
                labelMargin: 10,
                axisMargin: 500,
                hoverable: true,
                clickable: true,
                tickColor: "rgba(255,255,255,0.22)",
                borderWidth: 0
            },
            colors: ["#FFFFFF", "#4A8CF7", "#52e136"],
            xaxis: {
                mode: "categories"
            },
            yaxis: {
                mode: "categories"
            }
        });
        Salvis.loadToolTip($(elementId));
    }

})();