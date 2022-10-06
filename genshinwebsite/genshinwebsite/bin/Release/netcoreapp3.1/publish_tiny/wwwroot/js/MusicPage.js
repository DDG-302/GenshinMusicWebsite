function search_click() {
    let search_content = document.getElementById("search_input_box");
    if (search_content.value != null && search_content.value != "") {
        window.location = "?music_title=" + search_content.value
    }
    else {
        window.location = "/Pages"
    }
    
}