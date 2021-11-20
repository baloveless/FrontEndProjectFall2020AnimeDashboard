let form = document.getElementById("setup");
let search = document.getElementById("search");			// user search button 
let show = document.getElementById("show");				// user search input
let quizOptions = document.getElementById("showChoice");// list of shows to quiz on
let quiz = document.getElementById("quiz");				// quiz dom element
let quizOn = 0;											// show the quiz is on by array
let queryRes; // holds query results to help with getting the character list of the show

// calls jikan api for a list of shows matching that title.
// ** this is only set up for tv series currently
search.addEventListener("click", event => {
	event.preventDefault();
	if (show.value == null || show.value === "") {
		defaultOpts();
		return;
	}
	let Url = 'https://api.jikan.moe/v3/search/anime?q=#&type=TV&limit=10';
	Url = Url.replace("#", show.value);
	console.log(Url);
	fetch(Url)
		.then(data => { return data.json() })
		.then(res => { queryRes = res.results; showOptions(res) })
		.catch(error => { console.log(error); defaultOpts() });
})

function defaultOpts() {
	quizOptions.innerHTML = "";
	let newOpt = document.createElement("option");
	newOpt.innerText = "Search first";
	quizOptions.appendChild(newOpt);
	quizOptions.toggleAttribute("disabled");
	document.getElementById("startQuiz").remove();
}

// displays results of search to be chosen from
function showOptions(res) {
	quizOptions.innerHTML = "";
	for (i = 0; i < res.results.length; i++) {
		let newOpt = document.createElement("option");
		newOpt.innerText = res.results[i].title;
		quizOptions.appendChild(newOpt);
	}
	quizOptions.toggleAttribute("disabled");
	let btn = document.createElement("button");
	btn.className = "btn btn-block btn-success";
	btn.id = "startQuiz";
	btn.value = "submit"
	btn.innerText = "Start Quiz"
	form.appendChild(btn);
}

// listen for submit to start quiz
form.addEventListener("submit", event => {
	event.preventDefault();
	let qn = quizOptions.selectedIndex;
	if (qn === -1)
		return;
	quizOn = qn;
	quiz.toggleAttribute("hidden");
	form.toggleAttribute("hidden");
});

// this api can pull any pictures apparently? (needs a test)
let picsUrl = "https://imsea.herokuapp.com/api/1?q=#"

console.log(document.URL.replace("Animestuff/Quiz", ''));

// Populates a questions photo options
let getPhotos = async (i) => { 
	for (x = 0; x < 4; x++) {
		try {
			let response = await fetch(picsUrl.replace("#", answers[i][x]));
			console.log(response);
		} catch (error) {
			console.error(error);
        }
    }
}

/* Quiz questions */
let choices = []; // hold response to each question

let questions = ["What is your favorite fruit?",
	"What sounds like the best thing to do with your night?",
	"What is your favorite color?",
	"My Friends see me as ...",
	"When faced with a challenge my first instinct is to ...",
	"I always have a ______ with me",
	"If my neighbors were having a big party ...",
	"When one of my friends suggests an idea I ..."];

let answers = [["apple", "pear", "orange", "peach"]];

let curr = 0; // current question

let buttons = document.getElementsByClassName("btn-info");

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
		//getPhotos(0);
		curr++;
		return;
	}
	resultsCharacter(choices);
	//resultsScreen();
}

/* Results */
// show results of the 
function resultsScreen(character) {
	console.log(character);
	document.getElementById("body").remove(); // remove questions box
	let results = document.getElementsByClassName("quiz")[0];
	let header = document.getElementById("question");
	header.innerHTML = "You are (Some character)!";
	let container = document.createElement("div");
	container.className = "flex-container";
	let output = document.createElement("div");
	output.className = "flex-item";
	let image = document.createElement("img");
	image.src = "https://placekitten.com/300/300/";
	image.id = "output";
	output.appendChild(image);
	let link = document.createElement("p");
	link.innerHTML = choices + "<br>Link to the characters info page";
	output.appendChild(link);
	container.appendChild(output);
	results.appendChild(container);
}

/* get output character */


let resultsCharacter = async (choices) => {
	let Url = 'https://api.jikan.moe/v3/anime/' + queryRes[quizOn].mal_id + '/characters_staff';
	try {
		let response = await fetch(Url);
		let chars = await response.json();
		let resChar = 0;
		let primes = [2, 37, 11, 43, 5, 53, 7, 61];
		for (i = 0; i < choices.length; i++)
			resChar += choices[i] * primes[i];
		resultsScreen(chars.characters[resChar % chars.characters.length]);
	} catch (error) {
		console.error(error);
    }
}




