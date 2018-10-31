"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/broadcastHub").build();
var myUser;

connection.on("BroadcastMessage", function (message) {

	document.getElementById("messagesList").innerHTML = "";

	message.forEach(function (m) {
		var msg = m.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
		var li = document.createElement("li");
		li.textContent = msg;
		document.getElementById("messagesList").appendChild(li);
	});

	var currentTime = moment(new Date().getTime()).format('HH:mm:ss');
	document.getElementById("current-time").innerText = currentTime;
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

	var json = "{\"Id\":\"" + ident + "\"," + "\"AwayName\":\"" + awayName + "\",\"AwayScore\":\"" + awayScore + "\",\"HomeName\":\"" + homeName + "\",\"HomeScore\":\"" + homeScore + "\"}";

	connection.invoke("SendScore", json).then(function (response) {
		console.log(response.id); ident = response.id;
		document.getElementById("ident").value = ident.toString();
		document.getElementById("ident").innerText = ident.toString();
	}).catch(function (err)
		{
			return console.error(err.toString());
		});
	
	event.preventDefault();
}); 

// Send request
document.getElementById("clearButton").addEventListener("click", function (event) {
	connection.invoke("ClearScores").then(function (response) {
		console.log("Scores Cleared");
	});
});
