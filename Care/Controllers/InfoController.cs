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
                int idInt = Int32.Parse(id);
                Models.InfoViewModel orgInfo = new Models.InfoViewModel {
                    Id = idInt
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
