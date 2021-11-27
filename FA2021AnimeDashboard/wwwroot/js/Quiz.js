
let form = document.getElementById("setup");
let search = document.getElementById("search");			// user search button 
let show = document.getElementById("show");				// user search input
let quizOptions = document.getElementById("showChoice");// list of shows to quiz on
let quiz = document.getElementById("quiz");				// quiz dom element
let quizOn = 0;											// show the quiz is on by array
let queryRes; // holds query results to help with getting the character list of the show
let character;

// calls jikan api for a list of shows matching that title.
// ** this is only set up for tv series currently
search.addEventListener("click", event => {
	event.preventDefault();
	if (show.value == null || show.value === "" || show.length < 3) {
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
	if (document.getElementById('startQuiz') === null) {
		let btn = document.createElement("button");
		btn.className = "btn btn-block btn-success";
		btn.id = "startQuiz";
		btn.value = "submit"
		btn.innerText = "Start Quiz"
		form.appendChild(btn);
	}
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

console.log(document.URL.replace("Animestuff/Quiz", ''));

// Populates a questions photo options
let getPhotos = async (i) => { 
	try {
		for (x = 0; x < 4; x++) {
			let picsUrl = 'https://imsea.herokuapp.com/api/1?q=peach';
			let response = await fetch(picsUrl);
			console.log(response);
		}
	} catch (error) {
		console.error(error);
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
	"If my neighbors were having a big party I would ...",
	"When one of my friends suggests an idea I ..."];

let answers = [["apple", "pear", "orange", "peach"],
	["Reading a book", "Going out to a Club", "Watching Netflix", "Seeing Grandma"],
	["Blue", "Red", "Green", "Yellow"],
	["a Goofball", "Kind", "Honest", "Trustworthy"],
	["tear it down", "talk it out", "get a friends help", "call mom"],
	["pocket knife", "snack", "map", "friend"],
	["join in", "call the cops", "start my own party", "sulk"],
	["listen carefully", "ignore them", "pretend it was mine", "use it everytime"]
];

let pictures = [
	"../QuizPictures/FruitBowl.jpg",
	"../QuizPictures/Night.png",
	"../QuizPictures/Color.jpg",
	"../QuizPictures/Friends.png",
	"../QuizPictures/Challenge.jpg",
	"../QuizPictures/Item.png",
	"../QuizPictures/Party.png",
	"../QuizPictures/Talking.jpeg",
]

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
		let btns = document.getElementsByClassName("btn-info");
		for (i = 0; i < btns.length; i++) {
			btns[i].innerText = answers[curr][i];
		}
		document.getElementById("picture").src = pictures[curr];
		//getPhotos(0);
		curr++;
		return;
	}
	// disable all buttons to avoid any errors from unneccesary input
	for (i = 0; i < 4; i++)
		buttons[i].toggleAttribute('hidden');
	document.getElementById("question");
	// show see results button
	document.getElementById("quiz").toggleAttribute('hidden')
	document.getElementById("results").toggleAttribute('hidden');
	resultsCharacter();
}

/* Results */

// gets result character to use in a request
function getCharacter() {
	return chracter;
}

/* get output character */


let resultsCharacter = async () => {
	let Url = 'https://api.jikan.moe/v3/anime/' + queryRes[quizOn].mal_id + '/characters_staff';
	try {
		let response = await fetch(Url);
		let chars = await response.json();
		let resChar = 0;
		let primes = [2, 37, 11, 43, 5, 53, 7, 61];
		for (i = 0; i < choices.length; i++)
			resChar += choices[i] * primes[i];
		character = chars.characters[resChar % chars.characters.length];
		document.getElementById('mal_id').setAttribute('value', '' + character.mal_id);
		document.getElementById('url').setAttribute('value', character.url);
		document.getElementById('image_url').setAttribute('value', character.image_url);
		document.getElementById('name').setAttribute('value', character.name);
		document.getElementById('role').setAttribute('value', character.role);
	} catch (error) {
		console.error(error);
    }
}

function getUrl() {
	return document.URL.replace("/Quiz", "/partials/_character")
}

let characterCard = document.getElementsByClassName('characterCard')[0];


// unhides results if they have a page cached
if (/[a-zA-z]/.test(characterCard.id)) {
	characterCard.toggleAttribute('hidden');
	setup.toggleAttribute('hidden');
	document.getElementById('results').toggleAttribute('hidden');
	let reset = document.getElementById("submit"); // get submit button
	reset.className = "btn btn-success btn-block btn-lg";
	reset.innerHTML = "Reset Quiz";
	document.getElementById("resultsHead").toggleAttribute('hidden'); 
	// set up empty form to reset quiz
	document.getElementById('mal_id').setAttribute('value', ' ');
	document.getElementById('url').setAttribute('value', " ");
	document.getElementById('image_url').setAttribute('value', " ");
	document.getElementById('name').setAttribute('value', " ");
	document.getElementById('role').setAttribute('value', " ");
	// when submitted again, the character card will be empty
}




