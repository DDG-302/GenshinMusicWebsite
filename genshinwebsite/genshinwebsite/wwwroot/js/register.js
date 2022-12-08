//function submit_click() {
//    if ($("#accountInput").val() == "" || $("#passwordInput").val() == "") {
//        console.log("应当填写完整信息");
//        alert("应当填写完整信息");
//    }
//    else if ($("#passwordInput").val().length < 6) {
//        console.log("密码应至少为6位");
//        alert("密码应至少为6位");
//    }
//    else {
//        $("form").submit();
//    }
//}

function get_VCode_click() {
    let email = document.getElementById("email_input");
    let email_value = email.value;
    if (email_value == undefined) {
        console.log("未定义")
        email_value = email.innerText;
    }
    console.log(email_value)
    var formData = new FormData();
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);
    formData.append("email", email_value)
    console.log(document.getElementsByName("__RequestVerificationToken")[0].value)
    console.log(email_value)
    $.ajax({
        url: '/Email/Email',

        type: 'POST',
        processData: false,
        contentType: false,  
        data: formData,
        success: function (data) {
            alert("验证码已发送到邮箱，请在5分钟内填写");
        },
        error: function (data) {
            if (data.status == 400) {
                if (data.responseText == "") {
                    alert("拒绝访问");
                }
                else {
                    alert(textStatus);
                }
                
            }
            else if (data.status == 403) {
                alert("短期内多次请求验证码，暂时不可请求")
            }
            else {
                alert("发生了一些错误...\n" + "状态: " + jqXHR.status);
            }
        }
    });

}

    
