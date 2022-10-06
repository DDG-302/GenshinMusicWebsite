function submit_click() {
    if ($("#accountInput").val() == "" || $("#passwordInput").val() == "") {
        console.log("应当填写完整信息");
        alert("应当填写完整信息");
    }
    else {
        $("form").submit();
    }
}