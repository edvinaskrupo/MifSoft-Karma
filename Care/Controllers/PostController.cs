using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Care.Controllers;
using Care.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public async Task<IActionResult> AddOrg(NewPostModel newPostModel)
        {
            ///It is possible to write more concise version of the code, but we need to use the optional argument.
            ///We can delete this code and make it simpler after the assignment.
            ///Also named params are redundant, since from variable names it is clear, what we try to pass.

            PostModel post = new PostModel();

            checkInput(OrgName: newPostModel.OrgName, newPostModel.OrgPhoto, post, newPostModel.OrgShortDescr, newPostModel.OrgLongDescr, newPostModel.OrgLink, newPostModel.OrgLogo);

            if(post == null)
            {
                return View("Index");
            }
            else
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Post");
            }      
        }

        private void checkInput(string OrgName, string? OrgPhoto = "Images/DefaultOrgPhoto.png", params Object[] args)
        {
            PostModel tempPost = (PostModel)args[0];
            for(int i = 1; i < args.Length; ++i)
            {
                if (args[i] == null)
                {
                    tempPost = null;
                    return;
                }
            }

            tempPost.OrgName = OrgName;

            tempPost.OrgShortDescr = (string)args[1];

            tempPost.OrgLongDescr = (string)args[2];

            tempPost.OrgLink = (string)args[3];

            tempPost.OrgLogo = (string)args[4];

            tempPost.OrgPhoto = OrgPhoto;
        }
    }
}
