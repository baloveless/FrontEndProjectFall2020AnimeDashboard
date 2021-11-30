// all generes with their array location corresponding to their id
let genres = [
	null,
	'Action',
	'Adventure',
	'Cars',
	'Comedy',
	'Avante Garde',
	'Demons',
	'Mystery',
	'Drama',
	null,
	'Fantasy',
	'Game',
	null,
	'Historical',
	'Horror',
	'Kids',
	null,
	'Martial Arts',
	'Mecha',
	'Music',
	'Parody',
	'Samurai',
	'Romance',
	'School',
	'Sci Fi',
	'Shoujo',
	null,
	'Shounen',
	null,
	'Space',
	'Sports',
	'Super Power',
	'Vampire',
	null,
	null,
	'Harem',
	'Slice Of Life',
	'Supernatural',
	'Military',
	'Police',
	'Psychological',
	'Suspense',
	'Seinen',
	'Josei',
	null,
	null,
	'Award Winning',
	'Gourmet',
	'Work Life',
]

let getShowDetails = async (id, category) => {
	let Url = 'https://api.jikan.moe/v3/anime/' + id + '/' + category;
	try {
		let response = await fetch(Url);
		let data = await response.json();
		return data;
	} catch (error) {
		console.error(error);
    }
}

// genre is an id number as a string
let getShowsByGenre = async (genre) => {
	let Url = 'https://api.jikan.moe/v3/search/anime?q=&genre=' + genre + '&order_by=score&sort=desc';
	try {
		let response = await fetch(Url);
		let data = await response.json();
		console.log(data);
		return data;
	} catch (error) {
		console.error(error);
    }
}

getShowsByGenre(30);

function createOptions() {
	let genreOptions = document.getElementById("showChoice");
	genreOptions.innerHTML = "";
	for (i = 0; i < genres.length; i++) {
		if (genres[i] !== null) {
			let newOpt = document.createElement("option");
			newOpt.innerText = genres[i];
			newOpt.id = '' + i;
			genreOptions.appendChild(newOpt);
        }
	}
	genreOptions.toggleAttribute("disabled");
	document.getElementById('search').toggleAttribute('disabled');
}

createOptions()

