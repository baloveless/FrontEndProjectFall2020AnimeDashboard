using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA2021AnimeDashboard.Pages.Animestuff
{
    public class genreSearchResultsModel : PageModel
    {
        public List<Anime> animes = new List<Anime>();

        public string[] captions = new string[4] {"Highest Rated", "Lowest Rated", "Longest", "Oldest" };

        public string Genre;
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
            if (index == -1)
                return;
            this.Genre = Genre;
            await search(index, "score", "desc", 1); // highest rated in genre
            await search(index, "score", "asc", 50); // lowest rated
            await search(index, "episodes", "desc", 1); // longest
           // await search(index, "episodes", "asc", 1); // shortest
            await search(index, "start_date", "asc", 1); // Earliest
          //  await search(index, "start_date", "asc", 1); // Newest

        }
        // genre is the genreid, ad is meant to be filled by either desc or asc
        public async Task search(int genre, string orderby, string ad, int limit)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/search/anime?q=&genre=" + genre 
                                    + "&order_by="+ orderby + "&sort=" + ad
                                    + "&limit="+limit),
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
                this.animes.Add((myJObject).results[0]);
            }
            
        }
    }




    public class genreResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int cache_expiry { get; set; }

        // contains animes and their information
        public List<Anime> results { get; set; }
        public int last_page { get; set; }

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
