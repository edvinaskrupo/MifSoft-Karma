using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Care.Controllers;
using Care.Models;

namespace Care.Controllers
{
    public class PostController : Controller
    {
        private readonly ServiceDbContext _context;


        public PostController(ServiceDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddOrg()
        {
            return PartialView("/Views/Shared/_OrgBuilder.cshtml");
        }
        [HttpPost]
        public async Task<ViewResult> AddOrg(NewPostModel newPostModel)
        {
            PostModel post = new PostModel();

            post.OrgName = newPostModel.OrgName;

            post.OrgShortDescr = newPostModel.OrgShortDescr;

            post.OrgLongDescr = newPostModel.OrgLongDescr;

            post.OrgLink = newPostModel.OrgLink;

            post.OrgLogo = newPostModel.OrgLogo;

            post.OrgPhoto = newPostModel.OrgPhoto;

            _context.Add(post);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
