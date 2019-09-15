"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (senderId, recieverId, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var el = document.createElement('div');

    var side = null;
    var user = null;
    if (senderId == currentUser.id && recieverId == otherUser.id) { // ja saljem poruku otherUseru
        side = "left";
        user = currentUser;
    }
    else if (senderId == otherUser.id && recieverId == currentUser.id) {// ja primam poruku od currentUsera
        side = "right";
        user = otherUser;
    }
    else { // ovo nije moj chat. 
        return;
    }

    var porukaHtml =
        '<div class="row ' + side + '">' +
            '<p class="pull-' + side + '">' +
                '<small>' + '--' + user.firstName + ': ' + '</small>' 
                    + '<font size=4><b>' + msg + '</b></font>' + '&nbsp;' +
                '<sub>' + '--' + getDateTimeNow() + '</sub>' +
            '</p>' +
        '</div>';
    el.innerHTML = porukaHtml;
    document.getElementById("chatDiv").appendChild(el.firstChild);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var recieverId = otherUser.id;
    var message = document.getElementById("userInput").value;
    connection.invoke("SendMessage", recieverId, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    document.getElementById("userInput").value = "";

    setTimeout(function () { document.getElementById("chat").scrollTop = 100000; }, 100);
    

});

function getDateTimeNow() {
    let current_datetime = new Date()

    let dan = current_datetime.getDate()
    dan = (dan < 10) ? ("0" + dan.toString()) : dan

    let mjesec = current_datetime.getMonth() + 1;
    mjesec = (mjesec < 10) ? ('0' + mjesec.toString()) : mjesec

    let sat = current_datetime.getHours()
    sat = (sat < 10) ? ('0' + sat.toString()) : sat

    let minuta = current_datetime.getMinutes()
    minuta = (minuta < 10) ? ('0' + minuta.toString()) : minuta

    let sekunda = current_datetime.getSeconds()
    sekunda = (sekunda < 10) ? ('0' + sekunda.toString()) : sekunda

    let formatted_date =
        dan + "." + mjesec + "." + current_datetime.getFullYear() + "."
        + " " +
        sat + ":" + minuta + ":" + sekunda

    return formatted_date;
}

