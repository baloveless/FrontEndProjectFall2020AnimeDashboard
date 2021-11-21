let getShowDetails = async (id, category) => {
	let Url = 'https://api.jikan.moe/v3/anime/' + id + '/' + category;
	try {
		let response = await fetch(Url);
		let data = await response.json();
		console.log(data);
	} catch (error) {
		console.error(error);
    }
}

getShowDetails('1735', 'stats');


// genre is an id number as a string
// use this to get a group of shows by genre. 
// then each one can have their averages taken for a bar graph.
// also contains the rating so you can have bar graphs for how many of each genre 
// are rated pg-R
let getShowsByGenre = async (genre) => {
	let Url = 'https://api.jikan.moe/v3/search/anime?q=&genre='+ genre + '&order_by=score&sort=desc';
	try {
		let response = await fetch(Url);
		let data = await response.json();
		console.log(data);
	} catch (error) {
		console.error(error);
    }
}

getShowsByGenre('46');

