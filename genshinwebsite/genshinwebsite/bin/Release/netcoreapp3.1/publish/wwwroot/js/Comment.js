function comment_submit_click() {
    var input_box = document.getElementById("comment_input_box");
    if (input_box.value == "") {
        console.log("还没有评论捏");
        alert("还没有评论捏");
    }
    else {
        document.getElementById("comment_input_hidden").value = input_box.value;
        console.log(document.getElementById("comment_input_hidden").value);
        alert("功能未实现")
        //$("form").submit();
    }
}





// JavaScript source code
var page_num = 10
var offset = 0
var comment_list = []
var user_name_list = []
var user_id_list = []
var is_comment_empty = false;





function add_comments(data) {
    if (is_comment_empty) {
        return;
    }
    if (data.length == 0) {
        is_comment_empty = true;
        $("#main_div").append("<div class=\"block\"><p style=\"font-style: italic\">已抵达查询末尾...<p></div>")
        return;
    }

    // todo: 增加评论block
}

function get_comment_list(muid) {

    if (is_comment_empty) {
        return;
    }

    $.get("http://127.0.0.1:8001/search",
        {
            "muid": muid,
            "page_num": page_num,
            "offset": offset
        },
        function (data) {
            var i = 0;
            

        },
        "json")
    offset = offset + page_num
}

function get_my_comment() {
    // todo: 获取我的评论，注意每人只能对一个乐谱发送一条评论
}

$(document).ready(function () {
   
    $(window).scroll(function () {
        if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
            get_comment_list(muid)
        }
    });
})

