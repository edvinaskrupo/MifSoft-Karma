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

        [HttpPost]
        public ActionResult AddDescription(ItemModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: addToDatabase(model.Description);
            }
            return RedirectToAction("Index");
        }
    }
}
