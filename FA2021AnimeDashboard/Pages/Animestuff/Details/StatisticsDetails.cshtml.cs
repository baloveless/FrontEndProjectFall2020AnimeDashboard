using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FA2021AnimeDashboard.Pages.Animestuff.Details
{
    public class StatisticsDetailsModel : PageModel
    {
        public string Anime { get; set; }
        public async Task OnGetAsync(string Anime, int AnimeId)
        {
            this.Anime = Anime;
        }
    }
}
