using Care.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Care.Helpers;

namespace Care.Controllers
{
    [MethodLogger]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ItemIndex(UserRegistrationModel userModel)
        {
            return RedirectToAction("Index", "Item");
        }
    }
}
