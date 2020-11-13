"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/kafkaHub").build();

connection.on("ReceiveMessage", function (message) {
    var msg = message
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");

    var li = document.createElement("li");
    li.textContent = msg;

    var list = document.getElementById("messages");

    if (list.getElementsByTagName("li").length >= 9) {
        list.removeChild(list.childNodes[0]);
    }

    list.appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});