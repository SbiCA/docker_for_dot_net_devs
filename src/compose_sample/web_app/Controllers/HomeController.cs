using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var counter = 1;
           
            var value = await _cache.GetAsync("counter");
            if (value != null)
            {
                counter = BitConverter.ToInt32(value,0);
            }

            ViewData["Message"] = $"Your application description page. You visited {counter} times";
            counter++;
            await _cache.SetAsync("counter", BitConverter.GetBytes(counter));
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
