"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (senderId, recieverId, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var el = document.createElement('div');

    var side = null;
    var user = null;

    if (senderId == currentUser.id && recieverId == otherUser.id) {
        // current user sending messge to other useru
        side = "left";
        user = currentUser;
    }
    else if (senderId == otherUser.id && recieverId == currentUser.id) {
        // current user receiving message from other user
        side = "right";
        user = otherUser;
    }
    else {
        // not this chat
        return;
    }

    var messageHtml =
        '<div class="row ' + side + '">' +
            '<p class="pull-' + side + '">' +
                '<small>' + '--' + user.firstName + ': ' + '</small>' 
                    + '<font size=4><b>' + msg + '</b></font>' + '&nbsp;' +
                '<sub>' + '--' + getDateTimeNow() + '</sub>' +
            '</p>' +
        '</div>';

    el.innerHTML = messageHtml;
    document.getElementById("chatDiv").appendChild(el.firstChild);
    setTimeout(function () { document.getElementById("chat").scrollTop = 100000; }, 100);
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

    var userInput = document.getElementById("userInput");
    userInput.value = "";
    userInput.focus();
    userInput.select();

    setTimeout(function () { document.getElementById("chat").scrollTop = 100000; }, 100);
});

function getDateTimeNow() {
    let current_datetime = new Date()

    let days = current_datetime.getDate()
    days = (days < 10) ? ("0" + days.toString()) : days

    let months = current_datetime.getMonth() + 1;
    months = (months < 10) ? ('0' + months.toString()) : months

    let hours = current_datetime.getHours()
    hours = (hours < 10) ? ('0' + hours.toString()) : hours

    let minutes = current_datetime.getMinutes()
    minutes = (minutes < 10) ? ('0' + minutes.toString()) : minutes

    let seconds = current_datetime.getSeconds()
    seconds = (seconds < 10) ? ('0' + seconds.toString()) : seconds

    let formatted_date =
        days + "." + months + "." + current_datetime.getFullYear() + "."
        + " " +
        hours + ":" + minutes + ":" + seconds

    return formatted_date;
}

var input = document.getElementById("userInput");
input.addEventListener("keyup", function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById("sendButton").click();
    }
});