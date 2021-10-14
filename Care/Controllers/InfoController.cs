using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Care.Models;

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
                var org = _context.Posts.FirstOrDefault(m => m.OrgId == idInt);
                return View(org);
            }
            catch {
                ModelState.AddModelError("ID parsing", "Organisation ID must be an integer!");
                return View();
            }
        }
    }
}
