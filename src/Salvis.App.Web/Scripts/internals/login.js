
(function () {
    function initializing() {
        $('body').addClass("texture");

        /*Input & Radio Buttons*/
        if (jQuery().iCheck) {
            $('.icheck').iCheck({
                checkboxClass: 'icheckbox_square-blue checkbox',
                radioClass: 'iradio_square-blue'
            });
        }
    }

    $(document).ready(function () {
        //$('#accountForm').parsley();
        initializing();
    });

})();
