using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FA2021AnimeDashboard.Pages.Animestuff
{
    public class genreSearchResultsModel : PageModel
    {
        public genreResponse response { get; set; }
        public async Task OnGet(string Genre)
        {
			string[] genres = new string[49]{
	null,
	"Action",
	"Adventure",
	"Cars",
	"Comedy",
	"Avante Garde",
	"Demons",
	"Mystery",
	"Drama",
	null,
	"Fantasy",
	"Game",
	null,
	"Historical",
	"Horror",
	"Kids",
	null,
	"Martial Arts",
	"Mecha",
	"Music",
	"Parody",
	"Samurai",
	"Romance",
	"School",
	"Sci Fi",
	"Shoujo",
	null,
	"Shounen",
	null,
	"Space",
	"Sports",
	"Super Power",
	"Vampire",
	null,
	null,
	"Harem",
	"Slice Of Life",
	"Supernatural",
	"Military",
	"Police",
	"Psychological",
	"Suspense",
	"Seinen",
	"Josei",
	null,
	null,
	"Award Winning",
	"Gourmet",
	"Work Life",
			};
			int index = Array.IndexOf(genres, Genre);
			if (index != -1)
				return;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.jikan.moe/v3/genre/anime/"+ Genre),
                Headers =
    {
        { "x-rapidapi-host", "jikan1.p.rapidapi.com" },
        { "x-rapidapi-key", "eacdba1ca3mshbbddb3f551ec180p1cf96djsn47f742ccbd6b" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var myJObject = JsonConvert.DeserializeObject<genreResponse>(body);
                this.response = (myJObject);
            }
        }
    }


    public class genreResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int cache_expiry { get; set; }
        public int item_count { get; set; }

        public malUrl mal_url { get; set; }

        // contains animes and their information
        public List<Anime> animes { get; set; }
      
    }

    // contains genre names and such
    public class malUrl
    {
        int mal_id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }


    }

    


}
