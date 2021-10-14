using Care.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
