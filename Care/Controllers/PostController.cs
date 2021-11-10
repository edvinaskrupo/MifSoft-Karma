using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Care.Controllers;
using Care.Models;
using System.Text.RegularExpressions;

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
            return PartialView("/Views/Shared/_PostBuilder.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> AddOrg(NewPostModel newPostModel)
        {
            bool loginFailed = false;

            PostModel post = new PostModel();

            post.OrgName = newPostModel.OrgName;

            post.OrgShortDescr = newPostModel.OrgShortDescr;

            post.OrgLongDescr = newPostModel.OrgLongDescr;

            post.OrgLink = newPostModel.OrgLink;

            post.OrgLogo = newPostModel.OrgLogo;

            post.OrgPhoto = newPostModel.OrgPhoto;


            if (post.OrgName == null)
            {
                ModelState.AddModelError("OrgName", "Please enter the name of the organization.");
                loginFailed = true;
            }

            if (post.OrgShortDescr == null)
            {
                ModelState.AddModelError("OrgShortDescr", "Please enter quick description of the organization.");
                loginFailed = true;
            }
            else if (post.OrgShortDescr.Length < 50 || post.OrgShortDescr.Length > 100)
            {
                ModelState.AddModelError("OrgShortDescr", "Short organization description must be 50-100 symbols length.");
                loginFailed = true;
            }

            if (post.OrgLongDescr == null)
            {
                ModelState.AddModelError("OrgLongDescr", "Please enter the name of the organization.");
                loginFailed = true;
            }
            else if (post.OrgLongDescr.Length < 100)
            {
                ModelState.AddModelError("OrgLongDescr", "The length of longer organization description should be at least 100 symbols.");
                loginFailed = true;
            }

            if (post.OrgLink == null)
            {
                ModelState.AddModelError("OrgLink", "Please enter the link of the organization website.");
                loginFailed = true;
            }

            if (post.OrgLogo == null)
            {
                ModelState.AddModelError("OrgLogo", "Please enter the name of the organization.");
                loginFailed = true;
            }

            if (post.OrgPhoto == null)
            {
                ModelState.AddModelError("OrgPhoto", "Please enter the name of the organization.");
                loginFailed = true;
            }

            if (!loginFailed)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return View("~/Views/Home/Index.cshtml");
            }
            else return View("Index");
        }
    }
}
