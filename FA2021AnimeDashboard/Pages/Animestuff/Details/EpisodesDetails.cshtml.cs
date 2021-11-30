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
    public class EpisodesDetailsModel : PageModel
    {
        public int fil = 0;
        public int rec = 0;
        public int epi = 0;
        public string Anime { get; set; }
        public episodesResponse response { get; set; }
        public async Task OnGetAsync(string Anime, int AnimeId)
        {
            this.Anime = Anime;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/anime/" + AnimeId + "/episodes"),
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
                var myJObject = JsonConvert.DeserializeObject<episodesResponse>(body);
                this.response = (myJObject);
                foreach(episode ep in myJObject.episodes)
                {
                    if (ep.filler == true) {
                        fil++;
                    }
                    else if (ep.recap == true)
                    {
                        rec++;
                    }
                    else
                    {
                        epi++;
                    }
                }
            }
        }
    }
    public class episode
    {
        public int? episode_id { get; set; }
        public string? title { get; set; }
        public string? title_japanese { get; set; }
        public string? title_romanji { get; set; }
        public DateTime? aired { get; set; }
        public bool? filler { get; set; }
        public bool? recap { get; set; }
        public string? video_url { get; set; }
        public string? forum_url { get; set; }
    }
    public class episodesResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int request_cache_expiry { get; set; }
        public int episodes_last_page { get; set; }
        public List<episode> episodes { get; set; }

    }
}
