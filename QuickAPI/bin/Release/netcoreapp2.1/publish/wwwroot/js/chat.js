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
	var user = document.getElementById("userInput").value;
	
	var message = document.getElementById("messageInput").value;
	document.getElementById("messageInput").value = "";
	connection.invoke("SendMessage", user, message).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
});

document.getElementById("userInput").addEventListener("change", function (event) {
	myUser = document.getElementById("userInput").value;
});