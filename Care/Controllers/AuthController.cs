using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return PartialView("/Views/Shared/_Login.cshtml");
        }

        public IActionResult Registration()
        {
            return PartialView("/Views/Shared/_Registration.cshtml");
        }

    }
}
