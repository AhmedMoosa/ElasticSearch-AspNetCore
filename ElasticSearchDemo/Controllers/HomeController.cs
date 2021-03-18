using ElasticSearchDemo.Data;
using ElasticSearchDemo.Models;
using ElasticSearchDemo.Services.Elastic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElasticService elasticService;

        public HomeController(ILogger<HomeController> logger , IElasticService elasticService)
        {
            _logger = logger;
            this.elasticService = elasticService;
        }

        public IActionResult Index()
        {
            var result = elasticService.GetAll();
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var result = elasticService.Search(term);
                return View("Index", result);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
