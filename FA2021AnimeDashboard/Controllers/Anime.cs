using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FA2021AnimeDashboard.Controllers
{
    public class Anime : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
