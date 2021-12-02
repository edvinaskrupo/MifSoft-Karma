using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Care.Controllers;
using Care.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Care.Controllers
{
    public class PostController : Controller
    {
        private readonly ServiceDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _dir;

        public PostController(ServiceDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            _dir = hostEnvironment.ContentRootPath;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrg(PostModel post)
        {

            bool loginFailed = false;
            if (post.OrgShortDescr != null)
            {
                if (post.OrgShortDescr.Length < 25 || post.OrgShortDescr.Length > 50)
                {
                    ModelState.AddModelError("OrgShortDescr", "Short organization description must be 25-50 symbols length.");
                    loginFailed = true;
                }
            }
            
            if (post.OrgLongDescr != null)
            {
                if (post.OrgLongDescr.Length < 50)
                {
                    ModelState.AddModelError("OrgLongDescr", "The length of longer organization description should be at least 50 symbols.");
                    loginFailed = true;
                }
            }
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(post.OrgLogoFile.FileName);
                string extension = Path.GetExtension(post.OrgLogoFile.FileName);
                post.OrgLogoName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                Directory.CreateDirectory(wwwRootPath + "/OrgImages/OrgLogos/");
                string path = Path.Combine(wwwRootPath + "/OrgImages/OrgLogos/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await post.OrgLogoFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                loginFailed = true;
            }

            if (post.OrgPhotoFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(post.OrgPhotoFile.FileName);
                string extension = Path.GetExtension(post.OrgPhotoFile.FileName);
                post.OrgPhotoName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                Directory.CreateDirectory(wwwRootPath + "/OrgImages/OrgPhotos/");
                string path = Path.Combine(wwwRootPath + "/OrgImages/OrgPhotos/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await post.OrgPhotoFile.CopyToAsync(fileStream);
                }
            }
            
            if (!loginFailed)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Admin");
            }
            return View("Index");
        }
    }
}
