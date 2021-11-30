using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FA2021AnimeDashboard.Pages.Animestuff.Details
{
    public class CharactersDetailsModel : PageModel
    {
        public int mainChar = 0;
        public int supChar = 0;
        public charactersResponse response { get; set; }
        public string Anime { get; set; }
        public async Task OnGetAsync(string Anime, int AnimeId)
        {
            this.Anime = Anime;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/anime/" + AnimeId + "/characters_staff"),
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
                var myJObject = JsonConvert.DeserializeObject<charactersResponse>(body);
                this.response = (myJObject);
                foreach(character aChar in myJObject.characters)
                {
                    if(aChar.role == "Main")
                    {
                        mainChar++;
                    }
                    else if(aChar.role == "Supporting")
                    {
                        supChar++;
                    }
                }
            }
        }
    }

    public class character
    {
        public int mal_id { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public List<voiceActor> voice_actors { get; set; }
    }

    public class voiceActor
    {
        public int mal_id { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
        public string language { get; set; }
    }

    public class charactersResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int cache_expiry { get; set; }
        public List<character> characters { get; set; }

    }
}
