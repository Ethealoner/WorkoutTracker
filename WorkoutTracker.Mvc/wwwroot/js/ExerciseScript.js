
$(document).on('click', "#addSet", function (e) {
    $.ajax({
        url: "/Exercise/AddNewSet",
        cache: false,
        success: function (html) { $("#sets").append(html); }
    });
    return false;
});


    
        

$(document.body).on("click", "#deleteSet", function () {
    $(this).parents(`div.set-box:first`).remove();
    return false;
});






