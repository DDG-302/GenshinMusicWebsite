// JavaScript source code
var page_num = 10
var offset = 0
var comment_list = []
var user_name_list = []
var user_id_list = []
var is_comment_empty = false;
let muid = 0;
var is_login = false;
const io = new IntersectionObserver(entrys => {
    entrys.forEach((entry) => {
        console.log(entry);
        console.log(entry.isIntersecting)
        if (!entry.isIntersecting) {
            console.log("not");
            return;
        }
        else {
            get_comment_list(muid)
            console.log("yes");
        }
        //img.src = img.getAttribute('data-src');
        //io.unobserve(img);
    })
}, {})

function comment_submit_click() {
    var input_box = document.getElementById("comment_input_box");
    if (input_box.value == "") {
        alert("还没有评论捏");
        return;
    }
    else if (input_box.value.length > 400) {
        alert("字数太多了，最多输入400字，当前有" + input_box.value.length + "个字");
        return;
    }
    else {
        var enter_count = 0;
        for (var i in input_box.value) {
            if (input_box.value[i] == '\n') {
                enter_count++;
            }
            if (enter_count >= 20) {
                alert("不要刷屏哦，最多只能输入20次换行");
                return;
            }
        }
        //alert("功能未实现");
        var formData = new FormData();
        formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value)
        formData.append("comment", document.getElementById("comment_input_box").value)
        formData.append("muid", muid);
        $.ajax(
            {
                type: "POST",
                url: "/Music/Submit_comment",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    alert(data);
                    get_my_comment(muid);

                },
                error: function (data) {
                    if (data.responseText != undefined) {
                        alert(data.responseText);
                    }

                },
                complete: function (XMLHttpRequest, status) {
                    if (status == "timeout") {
                        alert("time out");
                    }
                },
                timeout: 10000
            }
            )
    }
}






// 增加评论div调用，用以构建样式
function _make_comment_div(info) {
    // info是data列表中的一个元素
    var row_div = document.createElement("div"); // 行div
    row_div.className = "row";

    var div = document.createElement("div"); // 主div
    div.className = "block offset-md-2 col-md-8 offset-sm-2 col-sm-8 offset-xs-2 col-xs-8";
    row_div.appendChild(div);

    var body_div = document.createElement("div"); // 信息div
    div.appendChild(body_div);

    var div_card_head = document.createElement("div"); // 头像div
    div_card_head.className = "card_head";

    var img = document.createElement("img"); // 头像img
    img.setAttribute("src", "/img/login-user.png");
    img.className = "user_icon"

    var p_user_name = document.createElement("p"); // 用户名
    p_user_name.innerHTML = info["userName"];
    
    div_card_head.appendChild(img);
    div_card_head.appendChild(p_user_name);
    

    body_div.appendChild(div_card_head);

    var p_upload_date = document.createElement("p");
    var p_update_date = document.createElement("p");
    p_upload_date.innerHTML = "发布时间：" + info["uploadDateStr"];
    p_update_date.innerHTML = "最后更新时间：" + info["updateDateStr"];
    p_upload_date.className = "date_time_p";
    p_update_date.className = "date_time_p";
    body_div.appendChild(p_upload_date);
    body_div.appendChild(p_update_date);
    body_div.appendChild(document.createElement("hr"));
    var p_comment = document.createElement("p");
    p_comment.innerHTML = info["commentContent"];
    p_comment.className = "comment_content";

    body_div.appendChild(p_comment);

    return row_div;
}


// 增加评论div
function add_comments(data) {
    if (is_comment_empty) {
        return;
    }
    if (data.length == 0) {
        is_comment_empty = true;
        io.disconnect();
        $("#main_div").append("<div class=\"block row\" style=\"margin-bottom:50px;\"><p style=\"font-style: italic\">已抵达末尾...<p></div>")
        return;
    }

    // todo: 增加评论block
    idx = 0
    var main_div = document.getElementById("main_div");
    while (idx < data.length) {
        main_div.appendChild(_make_comment_div(data[idx]));
        idx++;
    }
}

function get_comment_list(muid) {

    if (is_comment_empty) {
        return;
    }

    $.ajax(
        {
            type: "GET",
            url: "/Music/get_comment_list",
            data: {
                "muid": muid,
                "page_num": page_num,
                "page_offset": offset
            },
            success: function (data) {
                offset = offset + page_num
                add_comments(data)
            },
            error: function (data) {

            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    info_div.appendChild(make_info("等待超时", 2));
                }
            },
            timeout: 10000
        }
    )
    //$.get("*",
    //    {
    //        "muid": muid,
    //        "page_num": page_num,
    //        "offset": offset
    //    },
    //    function (data) {
    //        var i = 0;
    //        // todo:获取评论
            

    //    },
    //    "json")

}

function delete_my_comment() {
    if (!window.confirm("确认删除评论吗?")) {
        return;
    }
    var formData = new FormData();
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value)
    formData.append("muid", muid);

    $.ajax(
        {
            type: "POST",
            url: "/Music/delete_user_comment",
            contentType: false,
            processData: false,
            data: formData,
            success: function (data) {
                alert(data);
            },
            error: function (data) {
                alert(data.responseText);
            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    alert("等待超时");
                }
            },
            timeout: 10000
        })
    delete_user_comment
}

function set_my_comment(data) {
    if (data != undefined) {
        var p_upload_date = document.getElementById("my_upload_date");
        var p_update_date = document.getElementById("my_update_date");
        p_upload_date.innerHTML = "发布时间：" + data["uploadDateStr"];
        p_update_date.innerHTML = "最后更新时间：" + data["updateDateStr"];
        var p_comment = document.getElementById("my_comment");
        p_comment.innerHTML = data["commentContent"];
        p_comment.className = "comment_content";
        document.getElementById("delete_btn").removeAttribute("disabled")
    }
    else {
        var p_comment = document.getElementById("my_comment");
        p_comment.setAttribute("style", "color:green;font-style: italic;")
        p_comment.className = "comment_content";
        p_comment.innerHTML = "还没有评论，可以发送一条";
        
    }

}

function set_my_comment_error(data) {
    var p_upload_date = document.getElementById("my_upload_date");
    var p_update_date = document.getElementById("my_update_date");
    p_upload_date.innerHTML = "发布时间：查询出错";
    p_update_date.innerHTML = "最后更新时间：查询出错";

    var p_comment = document.getElementById("my_comment");
    p_comment.className = "comment_content";
    p_comment.setAttribute("style", "color:red;font-style: italic;");
    p_comment.innerHTML = "查询出错了...\n" + data;
    
}

function get_my_comment(muid) {
    // todo: 获取我的评论，注意每人只能对一个乐谱发送一条评论
    $.ajax(
        {
            type: "GET",
            url: "/Music/get_user_comment",
            data: {
                "muid": muid,
            },
            success: function (data) {
                set_my_comment(data);
            },
            error: function (data) {
                set_my_comment_error(data.responseText);
            },
            complete: function (XMLHttpRequest, status) {
                if (status == "timeout") {
                    set_my_comment_error("等待超时");
                }
            },
            timeout: 10000
        })
}

$(document).ready(function () {
    if (is_login) {
        get_my_comment(muid);
    }


    
   
    io.observe(document.getElementById("guard"));
    
    //$(window).scroll(function () {
    //    if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
    //        get_comment_list(muid)
    //    }
    //});
})

