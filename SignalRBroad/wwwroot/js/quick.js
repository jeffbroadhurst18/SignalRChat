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

document.getElementById("sendButton").addEventListener("click", function (event) {
	var homeName = document.getElementById("home-name").value;
	var homeScore = document.getElementById("home-score").value;
	var awayName = document.getElementById("away-name").value;
	var awayScore = document.getElementById("away-score").value;
	var ident = document.getElementById("ident").value;
	if (ident === undefined) {
		ident = 0;
	}

	var json = "{\"id\":\"" + ident + "\"," + "\"AwayName\":\"" + awayName + "\",\"AwayScore\":\"" + awayScore + "\",\"HomeName\":\"" + homeName + "\",\"HomeScore\":\"" + homeScore + "\"}";

	connection.invoke("SendScore", json).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
});

// Send request



