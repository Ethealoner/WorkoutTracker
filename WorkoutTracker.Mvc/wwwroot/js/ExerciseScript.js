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

$(document.body).on("click", "#getBestSet", function () {
    var exerciseName = $("#exerciseNameInput").val();
    $("#best-exercise-container").empty();
    $.ajax({
        url: "/Exercise/GetBestSets",
        data: { 'exerciseName': exerciseName },
        cache: false,
        success: function (html) { $("#best-exercise-container").append(html); }

    });
    return false;
});

$(document.body).on("click", "#getLatestSet", function () {
    var exerciseName = $("#exerciseNameInput").val();
    $("#best-exercise-container").empty();
    $.ajax({
        url: "/Exercise/GetLatestSets",
        data: { 'exerciseName': exerciseName },
        cache: false,
        success: function (html) { $("#best-exercise-container").append(html); }

    });
    return false;
});
