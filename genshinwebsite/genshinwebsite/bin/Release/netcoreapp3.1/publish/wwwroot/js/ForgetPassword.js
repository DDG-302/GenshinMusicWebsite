var is_email_check_ok = false;
var valid_email = "";

function set_warning_info(msg) {
    var div = document.createElement("div");
    div.className = "alert alert-warning alert-dismissable";

    //div.setAttribute("style", "top:-150px;")
    var button = document.createElement("button");
    button.type = "button";
    button.className = "close";
    button.setAttribute("data-dismiss", "alert");
    button.setAttribute("aria-hidden", "true");
    button.innerHTML = "&times";
    var p = document.createElement("p");
    p.innerHTML = msg;
    p.id = "p";
    p.setAttribute("style", "margin:0;white-space: pre;");
    div.appendChild(button);
    div.appendChild(p);

    var info_div = document.getElementById("info_div");
    info_div.appendChild(div);
}

function set_error_info(msg) {
    var div = document.createElement("div");
    div.className = "alert alert-danger alert-dismissable";

    //div.setAttribute("style", "top:-150px;")
    var button = document.createElement("button");
    button.type = "button";
    button.className = "close";
    button.setAttribute("data-dismiss", "alert");
    button.setAttribute("aria-hidden", "true");
    button.innerHTML = "&times";
    var p = document.createElement("p");
    p.innerHTML = msg;
    p.id = "p";
    p.setAttribute("style", "margin:0;white-space: pre;");
    div.appendChild(button);
    div.appendChild(p);

    var info_div = document.getElementById("info_div");
    info_div.appendChild(div);
}

function set_success_info(msg){
    var div = document.createElement("div");
    div.className = "alert alert-success alert-dismissable";

    //div.setAttribute("style", "top:-150px;")
    var button = document.createElement("button");
    button.type = "button";
    button.className = "close";
    button.setAttribute("data-dismiss", "alert");
    button.setAttribute("aria-hidden", "true");
    button.innerHTML = "&times";
    var p = document.createElement("p");
    p.innerHTML = msg;
    p.id = "p";
    p.setAttribute("style", "margin:0;white-space: pre;");
    div.appendChild(button);
    div.appendChild(p);

    var info_div = document.getElementById("info_div");
    info_div.appendChild(div);
}

function clear_info_div() {
    var info_div = document.getElementById("info_div");
    info_div.innerHTML = "";
}


function add_VCode_input(email) {
    var vcode_div = document.getElementById("vcode_div").innerHTML = "";
    var email_input = document.getElementById("email_input");
    email_input.readOnly = "true";
    email_input.value = email;
    email_input.setAttribute("style", "font-style:italic");
    console.log(email_input)
    var vcode_div = document.getElementById("vcode_div");
    var row_div = document.createElement("div");
    row_div.className = "row";

    var input_div = document.createElement("div");
    input_div.className = "col-8";
    var input = document.createElement("input");
    input.setAttribute("style", "width:100%;");
    input.setAttribute("onmouseover", "this.title = \'邮箱验证码\'");
    input.name = "VCode";
    input.type = "text";
    input.id = "VCode_input";
    input.placeholder = "邮箱验证码";
    input.autocomplete = "off";

    input_div.appendChild(input);

    var btn_div = document.createElement("div");
    btn_div.className = "col-4";
    var btn = document.createElement("button");
    btn.setAttribute("style", "width:100%;height:100%");
    btn.setAttribute("onmouseover", "this.title = \'请先输入邮箱\'");
    btn.setAttribute("onclick", "get_VCode_click()");
    btn.name = "VCode";
    btn.type = "button";
    btn.className = "btn-info";
    btn.innerHTML = "获取邮箱验证码";

    btn_div.appendChild(btn)

    row_div.appendChild(input_div);
    row_div.appendChild(btn_div);

    vcode_div.append(row_div)

}

function submit_click() {
    clear_info_div();
    if (!is_email_check_ok) {
        check_email();
    }
    else {
        change_password_forget();
    }

}

function check_email() {

    var email = document.getElementById("email_input").value;
    var formData = new FormData();
    formData.append("email", email);
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);

    $.ajax(
        {
            type: "POST",
            url: "/ForgetPassword/IsEmailValid",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                console.log(data)
                if (data == true) {
                    set_success_info("请继续输入邮箱验证码，如果要重新选择要找回的账号请刷新页面");
                    add_VCode_input(email);
                    is_email_check_ok = true;
                    valid_email = email;
                }
                else {
                    set_warning_info("账号不存在");
                }

            },
            error: function (data) {
                console.log("error");
                set_error_info("未知错误");
            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    set_error_info("请求超时");
                }
            },
            timeout: 10000
        }
    )


}


function change_password_forget() {
    var email = document.getElementById("email_input").value;
    if (valid_email != email) {
        set_error_info("邮箱账号发生错误，请刷新重新验证");
        console.log(email);
        console.log(valid_email);
        return;
    }
    var formData = new FormData();
    var password = document.getElementById("password").value;
    var confirm_password = document.getElementById("confirm_password").value;
    var VCode = document.getElementById("VCode_input").value;
    formData.append("Account", valid_email);
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);
    formData.append("Password", password);
    formData.append("ConfirmPassoword", confirm_password);
    formData.append("VCode", VCode);
    $.ajax(
        {
            type: "POST",
            url: "/ForgetPassword/ResetPassword",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                console.log(data)
                set_success_info("已重置密码，请前往<a href=\"Login\">登录</a>");

            },
            error: function (data) {
                console.log("error");
                set_error_info(data.responseText);
            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    set_error_info("请求超时");
                }
            },
            timeout: 10000
        }
    )
}