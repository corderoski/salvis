function initializeDetail() {
    $('#btnDelete').click(function () {
        var url = this.getAttribute("data-ref");
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        loadingOverlayShow();
        callerBase(url, "POST", headers, operationComplete, null, null);
    });
}
function initializeInbox() {
    $('[name="mailing-tools"]').hide();

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-blue checkbox',
        radioClass:
        'icheckbox_square-blue'
    });

    $('.item').click(function () {
        window.location.href = this.getAttribute("data-ref");
    });

    $('.checkbox-conteiner input').on('ifChanged', function () {
        if ($(this).is(':checked')) {
            $('[name="mailing-tools"]').show('fast');
        } else {
            if (!$('.checkbox-conteiner input').is(':checked')) {
                $('[name="mailing-tools"]').hide('fast');
            }

        }
    });

    $("[name='checkall']").on('ifChanged', function () {
        var checkboxes = $(".mails").find(':checkbox');
        if ($(this).is(':checked')) {
            checkboxes.iCheck('check');

        } else {
            checkboxes.iCheck('uncheck');
        }
    });


    $('#btnDelete').click(function () {
        loadingOverlayShow();
        var ids = [];
        $('.checkbox-conteiner input:checked').each(function () {
            var id = this.getAttribute("data-inf");
            ids.push(id);
        });
        var url = '/Message/DeleteAll/';
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;
        callerBase(url, "POST", headers, operationComplete, null, null, JSON.stringify(ids));

    });
}