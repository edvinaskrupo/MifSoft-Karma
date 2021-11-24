using Care.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Care.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult AddOrg()
        {
            return PartialView("/Views/Shared/_OrgBuilder.cshtml");
        }
        public IActionResult ItemIndex(UserRegistrationModel userModel)
        {
            return RedirectToAction("Index", "Item");
        }
    }
}
