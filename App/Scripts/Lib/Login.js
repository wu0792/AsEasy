require.config({
    paths: {
        jquery: 'jquery-2.2.3.min',
        jqueryVal: '../jquery.validate.min',
        jqueryValZh: '../jquery.validate.messages_zh',
    }
});

require(['jquery', 'jqueryVal', 'jqueryValZh'], function ($) {
    $.extend($.validator.messages, {
        required: "不能为空"
    });

    $.validator.setDefaults({
        errorPlacement: function (error, element) {
            $(element).attr('title', $(error).html());
        }
    });

    var app = {
        Init: function () {
            var $submit = $('#submit');

            $('form').validate({
                rules: {
                    loginName: {
                        required: true
                    },
                    pwd: {
                        required: true
                    },
                    checkCode: {
                        required: true
                    }
                }
            });

            $submit.click(function () {
                $('input').removeAttr('title');
                var validator = $('form').validate();
                var isValid = validator.form();

                if (isValid) {
                    $.ajax({
                        type: "POST",
                        dataType: "JSON",
                        url: "./LoginSubmit",
                        data: $('form').serialize(),
                        beforeSend: function () {
                            $submit.html('登录中...');
                        },
                        success: function (data) {
                            if (data.IsSuccess) {
                                window.location.href = data.Url;
                            } else {
                                alert(data.Message);
                            }
                        },
                        complete: function () {
                            $submit.html('登录');
                        }
                    });
                }
            });
        },

    };

    app.Init();
});