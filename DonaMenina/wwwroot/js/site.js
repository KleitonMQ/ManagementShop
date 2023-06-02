function showDateTime() {
    var d = new Date();
    var date = d.getUTCDate() + " / " + (d.getUTCMonth() + 1) + " / " + d.getUTCFullYear();
    var time = d.getUTCHours() + ":" + d.getUTCMinutes();
    var dateTime = date + " " + time;
    document.getElementById("currentDateTime").innerHTML = dateTime;
}

showDateTime();
setInterval(showDateTime, 1000);