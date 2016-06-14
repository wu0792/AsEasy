require.config({
    paths: {
        jquery: 'jquery-2.2.3.min',
        jqueryVal: '../jquery.validate.min',
        jqueryValZh: '../jquery.validate.messages_zh'
    }
});

require(['jquery', 'jqueryVal', 'jqueryValZh'], function ($) {
    $('#btnSearch').click(function () {

        $.validator.setDefaults({
            errorPlacement: function (error, element) {
                $(element).attr('title', $(error).html());
            }
        });

        var btnSearch = $('#btnSearch');

        $('form').validate({
            rules: {
                status: {
                    required: true
                }
            }
        });

        btnSearch.click(function () {
            $('input').removeAttr('title');
            var validator = $('form').validate();
            var isValid = validator.form();

            if (isValid) {
                $('form')[0].submit();
            } else {
                return false;
            }
        });
    });
});