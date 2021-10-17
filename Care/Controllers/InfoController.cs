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
        public InfoController(ServiceDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string id)
        {
            try {
                UInt32 idInt = UInt32.Parse(id);
                PostModel org = _context.Posts.FirstOrDefault(m => m.OrgId == idInt);
                if (org == null) {
                    ModelState.AddModelError("Info error", "Requested organisation doesn't exist");
                    return View();
                }
                else {
                    return View(org);
                }
            }
            catch {
                ModelState.AddModelError("Info error", "Organisation ID must be an integer!");
                return View();
            }
        }
    }
}
