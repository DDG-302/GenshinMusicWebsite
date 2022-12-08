let selected_filter = 1;

function search_click() {
    let search_content = document.getElementById("search_input_box");
    if (search_content.value != null && search_content.value != "") {
        window.location = "?select_order=" + selected_filter + "&music_title=" + search_content.value
    }
    else {
        window.location = "/Pages?select_order=" + selected_filter;
    }
}

function set_filter_ui_by_id(filter_id) {
    document.getElementById("filter_1").className = "nav-link";
    document.getElementById("filter_2").className = "nav-link";
    document.getElementById("filter_3").className = "nav-link";
    document.getElementById("filter_4").className = "nav-link";
    switch (filter_id) {
        case 1:
            document.getElementById("filter_1").className = "nav-link active";
            break;
        case 2:
            document.getElementById("filter_2").className = "nav-link active";
            break;
        case 3:
            document.getElementById("filter_3").className = "nav-link active";
            break;
        case 4:
            document.getElementById("filter_4").className = "nav-link active";
            break;
        default:
            document.getElementById("filter_1").className = "nav-link active";
            break;
    }
}

function set_filter(filter_id) {
    // 1: 时间顺序
    // 2: 下载顺序
    // 3: 浏览顺序
    // 4: 标题
    set_filter_ui_by_id(filter_id);
    selected_filter = filter_id;

    switch (filter_id) {
        case 1:
            document.getElementById("filter_1").className = "nav-link active";
            break;
        case 2:
            document.getElementById("filter_2").className = "nav-link active";
            break;
        case 3:
            document.getElementById("filter_3").className = "nav-link active";
            break;
        case 4:
            document.getElementById("filter_4").className = "nav-link active";
            break;
        default:
            document.getElementById("filter_1").className = "nav-link active";
            break;
    }
    let search_content = document.getElementById("search_input_box");
    if (search_content.value != null && search_content.value != "") {
        window.location = "?select_order=" + selected_filter + "&music_title=" + search_content.value
    }
    else {
        window.location = "/Pages?select_order=" + selected_filter;
    }

}

