


function initialize() {
    loadingOverlayShow();

    $('#addItemRow').click(function () {
        //var rownum = $('#formItems > div').length;
        //var url = "/Apps/MonthlyViewerRow?rownum=" + rownum;
        ////$('#notificationTable').show();
        //callerBase(url, "GET", null, addRow,App.init);
    });

    //$('#btnDelete').click(function () {
    //    loadingOverlayShow();
    //    var value = $('#goal-item-info').val();
    //    removeGoal(value);
    //});
    //callerBase('/' + window.goalName + '/ListGoals/', "GET", null, initializeGoals, loadingOverlayHidden);

    //$('#addCaller').click(function () {
    //    loadingOverlayShow();
    //    callerBase('/' + window.goalName + '/LoadPartialAdd/', "GET", null, function (html) { $('#md-addModal').html(html); }, function () {
    //        App.init();
    //        
    //    });
    //});

    loadingOverlayHidden();
};



////This method adds object receiving the html object.
function addRow(html) {
    debugger;
    $('#formItems').append(html);
}

////This method  remove object that has been received by parameter.
function removeRow(tableRow) {
    tableRow.remove();
}






//    checkForSessionMessages();
//}

///*
// *  For Partial views
// */
//function initializeGoals(html) {
//    $('#goals-list-content').html(html);
//    $('.md-trigger').modalEffects();
//}

//function removeGoal(id) {
//    var token = $('[name=__RequestVerificationToken]').val();
//    var headers = {};
//    var url = '/' + window.goalName + '/DeleteGoal/?parentId=' + id;
//    headers["__RequestVerificationToken"] = token;
//    callerBase(url, "POST", headers, function (obj) {
//        switch (obj.Code) {
//            case 200:
//                callerBase('/' + window.goalName + '/ListGoals', "GET", null, initializeGoals, loadingOverlayHidden);
//                messageBox(obj.Message);
//                break;
//            default:
//                messageBox(obj.Message);
//                break;
//        }
//    }, function () {
//        loadingOverlayHidden();
//        $('#btnCloseConfirm').click();
//    }, function () {
//        loadingOverlayHidden();
//        $('#btnCloseConfirm').click();
//    });
//}

//function selectorDelete(item) {
//    var attr = item.nextElementSibling.attributes;
//    var value = attr['parentdata'].value;
//    $('[name="goal-item-info"]').val(value);
//}


///*
// *  Full Add
// */

//function initializeAdd() {

//    $('#addNotificationRow').click(function () {
//        var rownum = $('#notificationTable >tbody >tr').length;
//        var url = '/' + window.goalName + "/CallNotificationRow/?rownum=" + rownum;
//        $('#notificationTable').show();
//        callerBase(url, "GET", null, addRow, App.init);
//    });

//    $('#fee-table').dataTable({
//        "sScrollY": "200px",
//        "bPaginate": false,
//        "bFilter": false
//    });

//    /*
//    $('#btnSave').click(function () {
//        callSaveSaving();
//    });

//    $('#btnSave').hide();

//    $('#btnValidate').click(function () {
//        callValidateGoal();
//    });

//    $('input[type="text"], select').change(function () {
//        $('#btnSave').hide();
//    });
//    */

//    if (window.goalName == 'Saving') {
//        var url = "/Comm/GetTypesWithDescription?cat=SAVE_TYPE";
//        fillDropListWithRequest('#Category', url);
//    }


//    $('#formAddGoals').parsley();

//}

////VALIDATE OPERATION
////Request to validate Goal
//function callValidateGoal() {
//    var form = $('#formAddGoals');
//    form.attr('action', '/' + goalName + '/ValidateGoal/');
//    form.submit();
//}

