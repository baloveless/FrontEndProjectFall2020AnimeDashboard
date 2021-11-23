using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FA2021AnimeDashboard.Pages.Animestuff.partials
{
    public class _characterModel : PageModel
    {
        public character toShow;
        public characterResponse singleResponse;
        public charactersResponse multiResponse;

        public async Task OnGet() {
            var myJObject = JsonConvert.DeserializeObject<character>("" +
                "{'mal_id': 0," +
                "'url': '',"+
                "'image_url': '',"+
                "'name': '',"+
                "'role': '',"+
                "'voice_actors': [],"+
                "}");
            toShow = (myJObject);
        }

        public async Task onPost(character toShow)
        {
            var myJObject = JsonConvert.DeserializeObject<character>(toShow.ToString());
            this.toShow = (myJObject);
        }

        public async Task CreatePage()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://jikan1.p.rapidapi.com/character/" + 1 + "/request?=pictures"),
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
                var myJObject = JsonConvert.DeserializeObject<characterResponse>(body);
                this.singleResponse = (myJObject);
            }
        }
    }

    public class characterResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int cache_expiry { get; set; }

        public List<String> pictures { get; set; }
    }
}
