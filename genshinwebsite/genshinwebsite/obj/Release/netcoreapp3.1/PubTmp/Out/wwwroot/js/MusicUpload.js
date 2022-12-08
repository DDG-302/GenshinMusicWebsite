// info: 消息字符串
// success_or_error: 1 - success
//                  2 - error
//                  3 - warning
//                  other - warning
function make_info(info, success_or_error) {
    var div = document.createElement("div");
    if (success_or_error == 1) {
        div.className = "alert alert-success alert-dismissable";
    }
    else if (success_or_error == 2) {
        div.className = "alert alert-danger alert-dismissable";
    }
    else if (success_or_error == 3) {
        div.className = "alert alert-warning alert-dismissable";
    }
    else {
        div.className = "alert alert-warning alert-dismissable";
    }
    
    //div.setAttribute("style", "top:-150px;")
    var button = document.createElement("button");
    button.type = "button";
    button.className = "close";
    button.setAttribute("data-dismiss", "alert");
    button.setAttribute("aria-hidden", "true");
    button.innerHTML = "&times";
    var p = document.createElement("p");
    p.innerHTML = info;
    p.id = "p";
    p.setAttribute("style", "margin:0;white-space: pre;");
    div.appendChild(button);
    div.appendChild(p);
    return div
}

// 上传成功后跳转回space
// second: second秒后跳转
function redirect_to_space(second, muid) {
    // muid暂时没法用
    var counter = Math.max(second, 1);
    var div = document.createElement("div");

    div.className = "alert alert-info";

    var a = document.createElement("a");
    a.className = "alert-link";
    div.appendChild(a);
    a.innerHTML = "将在" + counter.toString() + "秒后跳转（或者单击此处直接跳转）";
    a.href = "/User/UploadManager";

    var info_div = document.getElementById("info_div");
    info_div.appendChild(div);

    function timer() {
        counter--;
        if (counter > 0) {
            a.innerHTML = "将在" + counter.toString() + "秒后跳转（或者单击此处直接跳转）";
        }
        else {
            window.open("/Page/Detail/")
            location.href = "/User/UploadManager";
        }
    }
    setInterval(timer, 1000);   
}


function upload_btn_click() {

    var formData = new FormData();

    var info_div = document.getElementById("info_div");
    info_div.innerHTML = "";
    var file = document.getElementById("File").files[0]
    if (file == undefined) {
        info_div.appendChild(make_info("请先选择一个工程文件", 3));
        return;
    }
    formData.append("file", file);

    var abs = document.getElementById("abs").value

    if (abs.length > 500) {
        info_div.appendChild(make_info("简介请不要大于500字", 3));
        return;
    }
    formData.append("abs", abs);
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);

    info_div.appendChild(make_info("已接收上传指令，请等待上传结果，超时时间为10秒，请不要重复点击提交", 3));
    
    $.ajax(
        {
            type: "POST",
            url: "/Music/Upload",
            data: formData,
            processData: false,
            contentType:false,  
            success: function (data) {
                console.log("ok");
                console.log(data);
               

                //var info_div = document.getElementById("info_div");
         
                info_div.appendChild(make_info("上传成功", 1));
                //redirect_to_space(5, data);
                var div = document.createElement("div");
                info_div.appendChild(div);
                div.className = "alert alert-info  alert-dismissable";
                var button = document.createElement("button");
                button.type = "button";
                button.className = "close";
                button.setAttribute("data-dismiss", "alert");
                button.setAttribute("aria-hidden", "true");
                button.innerHTML = "&times";
                div.appendChild(button);

                var a = document.createElement("a");
                a.className = "alert-link";
                div.appendChild(a);
                a.innerHTML = "点击此处查看";
                a.href = "/Pages/" + data;
                a.setAttribute("target", "_blank");

            },
            error: function (data) {
                console.log("error");
                console.log(data);
                if (data.responseText != undefined && data.responseText != "") {
                    info_div.appendChild(make_info(data.responseText, 2));
                    
                }
                else if (data.status == 403) {
                    info_div.appendChild(make_info("上传失败，服务器拒绝", 2));
                }
                else {
                    info_div.appendChild(make_info("上传失败，发生错误", 2));
                }

                
            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    console.log(XMLHttpRequest);
                    info_div.appendChild(make_info("等待超时", 2));
                }
            },
            timeout:10000
        }
    )
}

function update_btn_click(muid) {

    var formData = new FormData();

    var info_div = document.getElementById("info_div");
    info_div.innerHTML = "";
    var file = document.getElementById("File").files[0]

    formData.append("file", file);

    var abs = document.getElementById("abs").value

    if (abs.length > 500) {
        info_div.appendChild(make_info("简介请不要大于500字", 3));
        return;
    }
    formData.append("abs", abs);
    formData.append("muid", muid);
    console.log(document.getElementById("abs").value.replaceAll("\n", "</br>"));
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);
    
    info_div.appendChild(make_info("已接收上传指令，请等待上传结果，超时时间为10秒，请不要重复点击提交", 3));

    $.ajax(
        {
            type: "POST",
            url: "/Music/UpdateMyMusic",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                console.log("ok");
                console.log(data);
                //var info_div = document.getElementById("info_div");

                info_div.appendChild(make_info("更新成功", 1));
                //redirect_to_space(5, data);
                var div = document.createElement("div");
                info_div.appendChild(div);
                div.className = "alert alert-info  alert-dismissable";
                var button = document.createElement("button");
                button.type = "button";
                button.className = "close";
                button.setAttribute("data-dismiss", "alert");
                button.setAttribute("aria-hidden", "true");
                button.innerHTML = "&times";
                div.appendChild(button);

                var a = document.createElement("a");
                a.className = "alert-link";
                div.appendChild(a);
                a.innerHTML = "点击此处查看";
                a.href = "/Pages/" + muid;
                a.setAttribute("target", "_blank");

            },
            error: function (data) {
                console.log("error");
                console.log(data);
                if (data.responseText != undefined && data.responseText != "") {
                    info_div.appendChild(make_info(data.responseText, 2));

                }
                else if (data.status == 403) {
                    info_div.appendChild(make_info("上传失败，服务器拒绝", 2));
                }
                else {
                    info_div.appendChild(make_info("上传失败，发生错误", 2));
                }


            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    info_div.appendChild(make_info("等待超时", 2));
                }
            },
            timeout: 10000
        }
    )
}