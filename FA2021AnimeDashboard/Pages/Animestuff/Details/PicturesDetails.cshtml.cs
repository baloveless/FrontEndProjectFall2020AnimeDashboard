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
    public class PicturesDetailsModel : PageModel
    {
        public string Anime { get; set; }
        public pictureResponse response { get; set; }
        public async Task OnGetAsync(string Anime, int AnimeId)
        {
            this.Anime = Anime;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/anime/" + AnimeId + "/pictures"),
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
                var myJObject = JsonConvert.DeserializeObject<pictureResponse>(body);
                this.response = (myJObject);
            }
        }
    }

    public class picture
    {
        public string large { get; set; }
        public string small { get; set; }
    }

    public class pictureResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int request_cache_expiry { get; set; }
        public List<picture> pictures { get; set; }
    }
}
