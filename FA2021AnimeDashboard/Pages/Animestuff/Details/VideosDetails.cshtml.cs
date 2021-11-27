using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FA2021AnimeDashboard.Pages.Animestuff.Details
{
    public class VideosDetailsModel : PageModel
    {
        public string Anime { get; set; }
        public async Task OnGetAsync(string Anime, int AnimeId)
        {
            this.Anime = Anime;
        }

    }
  
    public class promo
    {
        public string title { get; set; }
        public string image_url { get; set; }
        public string video_url { get; set; }
    }
    public class videoEpisode
    {

    }
    public class videoResponse
    {
        public string request_hash { get; set; }
        public string request_cached { get; set; }
        public int request_cache_expiry { get; set; }
        public List<promo> promo { get; set; }
        public List<videoEpisode> episodes { get; set; }
    }
}
