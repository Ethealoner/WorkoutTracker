

var chartData = [];
var chart;

$(document).on('click', "#getChartData", function (e) {
    var exerciseName = $("#exerciseNameInput").val();
    $.ajax({
        url: "/Charts/GetChartData",
        cache: false,
        data: { 'exerciseName': exerciseName },
        success: function (data)
        {
            if (chart != null) {
                chart.destroy();
                chartData = [];
            }

            addData(data)

            if (data.length == 0) {
                $("#chartMessage").css("color", "red");
                $("#chartMessage").html("Exercise not found");
                return;
            }

            $("#chartMessage").html("");

            chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                theme: "light2",
                title: {
                    text: exerciseName + " Summary"
                },
                axisY: {
                    title: "Score",
                    titleFontSize: 24
                },
                data: [{
                    type: "line",
                    dataPoints: chartData
                }]
            })

            chart.render();
        }
    });
    return false;
});

function addData(data) {
    for (var i = 0; i < data.length; i++) {
        chartData.push({
            y: data[i].X,
            x: new Date(data[i].Y)
        });
    }
}

