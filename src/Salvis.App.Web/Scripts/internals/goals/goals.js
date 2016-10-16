var Salvis = Salvis || {};

(function () {

    Salvis.GoalModel = {};

    //This method  remove "tr" object that has been received by parameter.
    function removeRow(tableRow) {
        tableRow.remove();
    }

    Salvis.GoalModel = function (name, isAdding) {
        self = this;
        self.Type = ko.observable(name);
        self.showingMore = ko.observable(false);
        self.ipd = ko.observable();

        self.initialize = function () {
            if (isAdding) {
                self.initializeAddView();
            } else {
                self.initializeMainView();
            }
            $('#formAddGoals').parsley();
        };

        self.initializeMainView = function () {
            $('.md-trigger').modalEffects();
            $("[class='close md-trigger']").filter("[data-modal='delete-confirm']").click(function () {
                var id = this.attributes["data-salvis"].value;
                self.ipd(id);
            });

            $("[data-salvis='alterComponents']").click(function () {
                self.showingMore(!self.showingMore());
            });
        };

        self.initializeAddView = function () {
            //$('#toSave').hide();

            $('#toValidate').click(self.validateGoal());

            $('#addNotificationRow').click(function () {
                var rownum = $('#notificationTable >tbody >tr').length;
                Salvis.ajaxCall('/' + self.Type() + "/CallNotificationRow/?rownum=" + rownum, 'GET', null, function (html) {
                    $('#notificationTable').append(html);
                    App.init();
                    $('#notificationTable').show();
                });
            });

            //$('#fee-table').dataTable({
            //    "sScrollY": "200px",
            //    "bPaginate": false,
            //    "bFilter": false
            //});

            /*
            $('#btnSave').click(function () {
                callSaveSaving();
            });
        
            $('input[type="text"], select').change(function () {
                $('#btnSave').hide();
            });
            */
        }

        self.validateGoal = function () {
            var data = ko.observable();
            //Salvis.ajaxCall('/' + self.Type() + '/ValidateGoal/', "GET", data, null);
        };
        
        self.deleteItem = function () {
            console.log('deleteItem', self.ipd());

            Salvis.UI.loadingOverlayShow();
            var url = '/' + self.Type() + '/DeleteGoal/?parentId=' + $('#goal-item-info').val();

            Salvis.ajaxCall(url, "POST", null,
                function (obj) {
                    switch (obj.Code) {
                        case 200:
                            //self.loadAndFillGoals();
                            Salvis.UI.messageBox(obj.Message);
                            break;
                        default:
                            Salvis.UI.messageBox(obj.Message);
                            break;
                    }
                    Salvis.UI.loadingOverlayHidden();
                    $('#btnCloseConfirm').click();
                });

        };

    }

})();