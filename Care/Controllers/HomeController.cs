using Care.Models;
using Care.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ServiceDbContext db;

        public HomeController(ILogger<HomeController> logger, ServiceDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            //PostSorter sortPosts = SortPostsByName;
            //Posts <PostModel> postList = sortPosts();
            return View(null);
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

        private delegate Posts<PostModel> PostSorter();
        private Posts<PostModel> SortPostsByName() {
            PostModel[] postList = new List<PostModel>(db.Posts).ToArray();
            Array.Sort(postList);

            return new Posts<PostModel>(postList.ToArray());
        }
    }
}
