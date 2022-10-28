function upload_btn_click() {
        $.ajax({
            url: '/Email/Email?email=' + email.value,

            type: 'GET',
            success: function (data) {
                alert("验证码已发送到邮箱，请在5分钟内填写");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == 400) {
                    alert("请填写邮箱");
                }
                else if (jqXHR.status == 403) {
                    alert("短期内多次请求验证码，暂时不可请求")
                }
                else {
                    alert("发生了一些错误...\n" + "状态: " + jqXHR.status);
                }
            }
        });
}