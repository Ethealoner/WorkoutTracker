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

$(document.body).on("click", "#loadNotes", function () {
    var exerciseName = $("#exerciseNameInput").val();

    $.ajax({
        url: "/Exercise/GetExerciseNotes",
        data: { 'exerciseName': exerciseName },
        cache: false,
        success: function (Notes) {
            $("#exerciseNotes").val(Notes);
        }
    });
    return false;
});

$(document.body).on("click", "#saveNotes", function () {
    var exerciseName = $("#exerciseNameInput").val();
    var exerciseNotes = $("#exerciseNotes").val();
    $.ajax({
        url: "/Exercise/SaveExerciseNotes",
        method: "post",
        data: { 'exerciseName': exerciseName, 'exerciseNotes': exerciseNotes },
        cache: false,
        success: function (Notes) { }
    });
    return false;
});