﻿function delete_my_music(muid) {
    if (!window.confirm("确定要删除吗？")) {
        return;
    }
    var formData = new FormData();
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);
    formData.append("muid", muid);
    $.ajax({
        type: "POST",
        url: "/Music/DeleteMyMusic",
        data: formData,
        processData: false,
        contentType: false,  
        success: function (data) {
            console.log("ok");
            console.log(data);
            alert("删除完成");
            location = location;
            //if (window.confirm("删除完成，要刷新页面吗？")) {
            //    location = location;
            //}
            
        },
        error: function (data) {
            console.log("error")
            console.log(data.responseText)
            alert("出错了，" + data.responseText);
        },
        complete: function (XMLHttpRequest, status) {
            if (status == "timeout") {
                alert("等待超时")
                console.log(XMLHttpRequest)
            }
        },
        timeout: 10000
    })
}


function delete_music(muid) {
    if (!window.confirm("确定要删除吗？")) {
        return;
    }
    var formData = new FormData();
    formData.append("__RequestVerificationToken", document.getElementsByName("__RequestVerificationToken")[0].value);
    formData.append("muid", muid);
    $.ajax({
        type: "POST",
        url: "/Music/DeleteMusic",
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            console.log("ok");
            console.log(data);
            alert("删除完成");
            location = location;
            //if (window.confirm("删除完成，要刷新页面吗？")) {
            //    location = location;
            //}

        },
        error: function (data) {
            console.log("error")
            console.log(data.responseText)
            alert("出错了，" + data.responseText);
        },
        complete: function (XMLHttpRequest, status) {
            if (status == "timeout") {
                alert("等待超时")
                console.log(XMLHttpRequest)
            }
        },
        timeout: 10000
    })
}

