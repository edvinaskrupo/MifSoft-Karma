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

        public async Task<IActionResult> Index()
        {
 
            List<PostModel> postList = new List<PostModel>(db.Posts);

            // 1, 0, or -1 is returned depending on whether one value is greater than, equal to, or less than the other. 
             postList.Sort((PostModel x, PostModel y) => 
             x.OrgName == null && y.OrgName == null
                ? 0
                : x.OrgName == null
                    ? -1
                    : y.OrgName == null
                        ? 1
                        : x.OrgName.CompareTo(y.OrgName));
            //Array.Sort(postList, PostModel.SortByNameAscending());
            

            Posts <PostModel> postList2 = new Posts<PostModel>(postList.ToArray());

            return View(postList2);
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
