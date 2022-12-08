
$(document).ready(function () {
    var carousel_main_div = document.getElementById("carousel_music_img");
    var ol = document.getElementById("carousel_ol");
    var item_div = document.getElementById("carousel_item_div");
    console.log(img_list)
    for (var i = 0; i < img_list.length; i++) {
        //console.log(img_list[i])
        var li = document.createElement("li");
        li.setAttribute("data-target", "#carousel_music_img");
        li.setAttribute("data-slide-to", i);
        if (i == 0) {
            li.className = "active";
        }
     
        ol.appendChild(li);

        var item = document.createElement("div");
        if (i == 0) {
            item.className = "carousel-item active";
        }
        else {
            item.className = "carousel-item";
        }

        var a = document.createElement("a");
        a.href = "\\" + img_list[i];
        a.target = "_blank";
        a.className = "d-block";
        var img = document.createElement("img");
        img.className = "music_img d-block";
        img.src = "\\" + img_list[i];
        a.appendChild(img);


        item.appendChild(a);
        item_div.appendChild(item);



    }
})