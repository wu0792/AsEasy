require.config({
    paths: {
        jquery: '../jquery-2.2.3.min',
        jqueryVal: '../jquery.validate.min',
        jqueryValZh: '../jquery.validate.messages_zh',
        ko: '../knockout-3.4.0'
    }
});

require(['jquery', 'ko', 'jqueryVal', 'jqueryValZh'], function ($, ko) {
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
            //var $submit = $('#submit');

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

            //$submit.click(function () {
            //    $('input').removeAttr('title');
            //    var validator = $('form').validate();
            //    var isValid = validator.form();

            //    if (isValid) {
            //        $.ajax({
            //            type: "POST",
            //            dataType: "JSON",
            //            url: "./LoginSubmit",
            //            data: $('form').serialize(),
            //            beforeSend: function () {
            //                $submit.html('登录中...');
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess) {
            //                    window.location.href = data.Url;
            //                } else {
            //                    alert(data.Message);
            //                }
            //            },
            //            complete: function () {
            //                $submit.html('登录');
            //            }
            //        });
            //    }
            //});
        },
        loginMode: function () {
            var self = this;
            this.loginName = ko.observable();
            this.pwd = ko.observable();
            this.checkCode = ko.observable();
            this.message = ko.observable('');
            this.loginClick = function () {
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
                            self.message('登录中...');
                            $('#submit').attr('disabled', true);
                        },
                        success: function (data) {
                            if (data.IsSuccess) {
                                self.message('登录成功，正在跳转请稍后...');
                                window.location.href = data.Url;
                            } else {
                                self.message(data.Message);
                            }
                        },
                        complete: function () {
                            $('#submit').attr('disabled', false);
                        }
                    });
                }
            }
        }
    };

    app.Init();
    ko.applyBindings(app.loginMode);
});