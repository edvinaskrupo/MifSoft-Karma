using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddOrg()
        {
            return View("/Views/Shared/_OrgBuilder.cshtml");
        }
    }
}
