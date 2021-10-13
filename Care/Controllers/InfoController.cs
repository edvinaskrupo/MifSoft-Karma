using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index(string id)
        {
            try {
                UInt32 idInt = UInt32.Parse(id);
                Models.InfoViewModel orgInfo = new Models.InfoViewModel {
                    Title = "Organisation no. " + idInt,
                    LongDescription = "This is the long description of organisation no. " + idInt + ". So far the database isn't ready, so the only thing that changes in the page is the ID. However, the homepage and donate links currently work as previous/next buttons to test if I can change hyperlinks dynamically.",
                    HomeLink = "https://localhost:5001/Info/Index/" + (idInt - 1),
                    DonateLink = "https://localhost:5001/Info/Index/" + (idInt + 1)
                };
                return View(orgInfo);
            }
            catch {
                ModelState.AddModelError("ID parsing", "Organisation ID must be an integer!");
                return View();
            }
        }
    }
}
