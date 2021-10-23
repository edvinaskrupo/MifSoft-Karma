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
                    return View(new PostModelWithErrorHandling ("The requested organisation doesn't exist."));
                }
                else {
                    return View(new PostModelWithErrorHandling(org));
                }
            }
            catch {
                return View(new PostModelWithErrorHandling ("The requested organisation ID is invalid."));
            }
        }
    }
}
