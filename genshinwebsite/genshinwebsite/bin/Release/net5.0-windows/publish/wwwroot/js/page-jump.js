
function changeURLArg(url, arg, arg_val) {
    var pattern = arg + '=([^&]*)';
    var replaceText = arg + '=' + arg_val;
    if (url.match(pattern)) {
        var tmp = '/(' + arg + '=)([^&]*)/gi';
        tmp = url.replace(eval(tmp), replaceText);
        return tmp;
    } else {
        if (url.match('[\?]')) {
            return url + '&' + replaceText;
        } else {
            return url + '?' + replaceText;
        }
    }
}
function have_question_mark(str) {
    for (var i; i < str.length; i++) {
        if (str[i] == "?") {
            return true;
        }
    }
    return false;
}
function jump_click() {
    let url = window.location.href;
    let input_box = document.getElementById('page_num_input');
    if (input_box.value == null || input_box.value == "") {
        return;
    }
    if (url.search("page_offset") != -1) {
        url = changeURLArg(url, "page_offset", input_box.value)
        window.location = url;
    }
    else if (have_question_mark(url)) {
        window.location = url + "&page_offset=" + input_box.value;
    }
    else {
        window.location = "?page_offset=" + input_box.value;
    }
}
