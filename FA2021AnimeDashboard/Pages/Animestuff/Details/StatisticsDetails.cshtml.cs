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
    public class StatisticsDetailsModel : PageModel
    {
        public float Score { get; set; }
        public statsResponse response { get; set; }
        public string Anime { get; set; }
        public async Task OnGetAsync(string Anime, int AnimeId, float Score)
        {
            this.Anime = Anime;
            this.Score = Score;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/anime/" + AnimeId + "/stats"),
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
                var myJObject = JsonConvert.DeserializeObject<statsResponse>(body.Replace("\"1\"", "one").Replace("\"2\"", "two").Replace("\"3\"", "three").Replace("\"4\"", "four").Replace("\"5\"", "five").Replace("\"6\"", "six").Replace("\"7\"", "seven").Replace("\"8\"", "eight").Replace("\"9\"", "nine").Replace("\"10\"", "ten"));
                this.response = (myJObject);
            }
        }
    }

    public class score
    {
        public int votes { get; set; }
        public float percentage { get; set; }
    }

    public class scores
    {
        public score one {get; set; }
        public score two { get; set; }
        public score three { get; set; }
        public score four { get; set; }
        public score five { get; set; }
        public score six { get; set; }
        public score seven { get; set; }
        public score eight { get; set; }
        public score nine { get; set; }
        public score ten { get; set; }
    }

    public class statsResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int request_cache_expiry { get; set; }
        public int watching { get; set; }
        public int on_hold { get; set; }
        public int dropped { get; set; }
        public int plan_to_watch { get; set; }
        public int total { get; set; }
        public scores scores { get; set; }
    }
}
