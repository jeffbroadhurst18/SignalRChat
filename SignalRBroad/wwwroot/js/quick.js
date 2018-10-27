"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/broadcastHub").build();
var myUser;

connection.on("BroadcastMessage", function (message) {
	var currentTime = moment(new Date().getTime()).format('HH:mm:ss');
	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = currentTime + ": " + msg;
	var li = document.createElement("li");

	li.textContent = encodedMsg;
	document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
	return console.error(err.toString());
});



// Send request



