let choices = []; // hold response to each question

let questions = ["What is your favorite fruit?",
	"What sounds like the best thing to do with you night?",
	"What is your favorite color?",
	"My Friends see me as ...",
	"When faced with a challenge my first instinct is to ...",
	"I always have a ______ with me",
	"If my neighbors were having a big party ...",
	"When one of my friends suggests an idea I ..."];

let curr = 0; // current question

let buttons = document.getElementsByClassName("btn");

newQuestion();

buttons[0].addEventListener("click", event => {
	choices.push(0);
	newQuestion();
});

buttons[1].addEventListener("click", event => {
	choices.push(1);
	newQuestion();
});

buttons[2].addEventListener("click", event => {
	choices.push(2);
	newQuestion();
});

buttons[3].addEventListener("click", event => {
	choices.push(3);
	newQuestion();
});

// cycles to next question on choice
function newQuestion() {
	if (curr < questions.length) {
		document.getElementById("question").innerHTML = questions[curr];
		curr++;
		return;
	}
	resultsScreen();
}

// show results of the 
function resultsScreen() {
	document.getElementById("body").remove(); // remove questions box
	let results = document.getElementsByClassName("quiz")[0];
	let header = document.getElementById("question");
	header.innerHTML = "You are (Some character)!";
	let container = document.createElement("div");
	container.className = "flex-container";
	let output = document.createElement("div");
	output.className = "flex-item";
	let image = document.createElement("img");
	image.src = "http://placekitten.com/300/300/";
	image.id = "output";
	output.appendChild(image);
	let link = document.createElement("p");
	link.innerHTML = choices + "<br>Link to the characters info page";
	output.appendChild(link);
	container.appendChild(output);
	results.appendChild(container);
}