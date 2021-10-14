using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Care.Models;
using Care.Helpers;

namespace Care.Controllers
{
    public class InfoController : Controller
    {
        private readonly ServiceDbContext _context;
        private IndexRetriever indexRetriever;

        public InfoController(ServiceDbContext context)
        {
            _context = context;
            indexRetriever = new IndexRetriever();
        }
        public IActionResult Index(string id)
        {
            try {
                UInt32 idInt = UInt32.Parse(id);
                OrgModel org = _context.Posts.FirstOrDefault(m => m.OrgId == idInt);
                if (org == null) {
                    return View(indexRetriever.retrieveIndex(_context));
                }
                else {
                    return View("~/Views/Info/Org.cshtml", org);
                }
            }
            catch {
                ModelState.AddModelError("ID parsing", "Organisation ID must be an integer!");
                return View(indexRetriever.retrieveIndex(_context));
            }
        }
    }
}
