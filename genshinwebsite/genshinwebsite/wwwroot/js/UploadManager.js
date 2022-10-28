function delete_my_music(muid) {
    $.ajax({
        type: "POST",
        url: "/DeleteMyMusic",
        data: {
            "muid": muid,

        },
        success: function (data) {
            console.log("ok");
            console.log(data);
            alert("删好了，自行刷新一下");
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