using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA2021AnimeDashboard.Pages.Animestuff
{
    public class AnimeDetailsModel : PageModel
    {
        public Anime response { get; set; }
        public async Task OnGet(string Anime)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/search/anime?q=" + Anime),
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
                var myJObject = JsonConvert.DeserializeObject<AnimeResponse>(body);
                this.response = (myJObject.results.FirstOrDefault());
            }
        }
    }
    public class Anime
    {
        public int? mal_id { get; set; }
        public string? url { get; set; }
        public string? image_url { get; set; }
        public string? title { get; set; }
        public bool? airing { get; set; }
        public string? synopsis { get; set; }
        public string? type { get; set; }
        public int? episodes { get; set; }
        public float? score { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public int? members { get; set; }
        public string? rated { get; set; }

    }

    public class AnimeResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int cache_expiry { get; set; }
        public List<Anime>? results { get; set; }
        public int? last_page { get; set; }

    }
}
