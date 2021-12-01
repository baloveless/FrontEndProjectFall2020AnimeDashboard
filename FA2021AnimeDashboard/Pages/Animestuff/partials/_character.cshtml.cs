using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FA2021AnimeDashboard.Pages.Animestuff.Details;

namespace FA2021AnimeDashboard.Pages.Animestuff.partials
{
    public class _characterModel : PageModel
    {
        public character toShow;
        public characterResponse singleResponse { get; set; }
        public charactersResponse multiResponse;

        // creates an empty object to avoid null reference errors
        public async Task OnGet()
        {
            if (toShow == null || toShow.url == "")
            {
                var myJObject = JsonConvert.DeserializeObject<character>("" +
                    "{'mal_id': 0," +
                    "'url': ''," +
                    "'image_url': ''," +
                    "'name': ''," +
                    "'role': ''," +
                    "'voice_actors': []," +
                    "}");
                toShow = (myJObject);
            }
            singleResponse = new characterResponse();
            singleResponse.pictures = new List<picture>();
            picture hold = new picture();
            hold.image_url = toShow.image_url;
            for (int i = 0; i < 4; i++)
                singleResponse.pictures.Add(hold);
        }

        public async Task OnPostAsync()
        {
            toShow = new character();
            int temp = 0;
            int.TryParse(Request.Form["mal_id"].ToString(), out temp);
            toShow.mal_id = temp;
            toShow.url = Request.Form["url"];
            toShow.image_url = Request.Form["image_url"];
            toShow.name = Request.Form["name"];
            toShow.role = Request.Form["role"];
            if (toShow.mal_id > 0)
                await CreatePage();
            else
            {
                singleResponse = new characterResponse();
                singleResponse.pictures = new List<picture>();
                picture hold = new picture();
                hold.image_url = toShow.image_url;
                for (int i = 0; i < 4; i++)
                    singleResponse.pictures.Add(hold);
            }
        }

        // get list of characters pictures, (incomplete)
        public async Task CreatePage()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.jikan.moe/v3/character/" + toShow.mal_id + "/pictures"),
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

    public class picture
    {
        public string image_url { get; set; }
    }


    public class characterResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int cache_expiry { get; set; }

        public List<picture> pictures { get; set; }
    }
}
