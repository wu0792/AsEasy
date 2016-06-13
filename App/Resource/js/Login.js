define([], function () {
    var app = {
        Init: function () {
            var $submit = $('#submit');
            $submit.click(function () {
                $.ajax({
                    type: "POST",
                    dataType: "JSON",
                    url: "../LoginSubmit",
                    data: $('form').serialize(),
                    beforeSend: function () {
                        $submit.html('登录中...');
                    },
                    success: function (data) {
                        if (data.isSuccess) {
                            window.location.href = "../../Home/Index";
                        } else {
                            alert(data.Message);
                        }
                    },
                    complete: function () {
                        $submit.html('登录');
                    }
                });
            });
        },

    };

    return app;
});