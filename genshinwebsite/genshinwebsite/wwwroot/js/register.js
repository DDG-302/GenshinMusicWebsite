function submit_click() {
    if ($("#accountInput").val() == "" || $("#passwordInput").val() == "") {
        console.log("应当填写完整信息");
        alert("应当填写完整信息");
    }
    else if ($("#passwordInput").val().length < 6) {
        console.log("密码应至少为6位");
        alert("密码应至少为6位");
    }
    else {
        $("form").submit();
    }
}