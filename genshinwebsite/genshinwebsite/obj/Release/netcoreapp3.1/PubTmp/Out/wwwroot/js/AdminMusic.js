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





function admin_update_btn_click(muid) {

    var formData = new FormData();

    var info_div = document.getElementById("info_div");
    info_div.innerHTML = "";

    var abs = document.getElementById("abs").value

    if (abs.length > 500) {
        info_div.appendChild(make_info("简介请不要大于500字", 3));
        return;
    }
    View_num = document.getElementById("View_num").value
    Download_num = document.getElementById("Download_num").value
    MusicTitle= document.getElementById("MusicTitle").value
    formData.append("abs", abs);
    formData.append("muid", muid);
    formData.append("View_num", View_num);
    formData.append("Download_num", Download_num);
    formData.append("music_title", MusicTitle);
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);

    info_div.appendChild(make_info("已接收上传指令，请等待上传结果，超时时间为10秒，请不要重复点击提交", 3));

    $.ajax(
        {
            type: "POST",
            url: "/Music/UpdateMusicAdmin",
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
                    console.log(XMLHttpRequest);
                    info_div.appendChild(make_info("等待超时", 2));
                }
            },
            timeout: 10000
        }
    )
}