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
                    ModelState.AddModelError("Info error", "The requested organisation doesn't exist.");
                    return View(new PostModelWithErrorHandling ("The requested organisation doesn't exist."));
                }
                else {
                    return View(new PostModelWithErrorHandling(org));
                }
            }
            catch {
                ModelState.AddModelError("Info error", "Organisation ID must be an unsigned 32-bit integer.");
                return View(new PostModelWithErrorHandling ("Organisation ID must be an unsigned 32-bit integer."));
            }
        }
    }
}
