"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var myUser;

connection.on("ReceiveMessage", function (user, message) {
	var currentTime = moment(new Date().getTime()).format('HH:mm:ss');

	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = currentTime + ": " + user + " --> " + msg;

	var li = document.createElement("li");
	if (user === myUser) {
		li.style = "font-weight:bold; color:blue";
	}
	else {
		li.style = "color:red";
	}

	li.textContent = encodedMsg;
	document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
	return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

	// Create a request variable and assign a new XMLHttpRequest object to it.
	var request = new XMLHttpRequest();

	// Open a new connection, using the GET request on the URL endpoint
	request.open('GET', 'http://localhost/Quick/api/values', true);

	request.onload = function () {
		var data = JSON.parse(this.response);

		data.forEach(score => {
			// Log each movie's title
			console.log(score.homeName);
		});
	}
	request.send();
	event.preventDefault();
});

// Send request




//var message = document.getElementById("messageInput").value;
//document.getElementById("messageInput").value = "";
//connection.invoke("SendMessage", user, message).catch(function (err) {
//	return console.error(err.toString());
//});



//document.getElementById("userInput").addEventListener("change", function (event) {
//	myUser = document.getElementById("userInput").value;
//});