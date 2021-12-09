using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Care.Models;
using Care.Helpers;
using Microsoft.AspNetCore.Http;
//using AngleSharp.Html.Dom;

namespace Care.Controllers
{
    public class AuthController : Controller
    {
        private readonly ServiceDbContext _context;
        private Lazy<Authenticator> lazyAuthenticator;

        public AuthController(ServiceDbContext context)
        {
            _context = context;
            this.lazyAuthenticator = new Lazy<Authenticator>();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegistrationModel userModel)
        {
            Authenticator authenticator = lazyAuthenticator.Value;

            if (authenticator.Authenticate(userModel) && !UserEmailExists(userModel.EmailAddress))
            {
                UserModel newUser = new UserModel();

                newUser.EmailAddress = userModel.EmailAddress;

                string salt = PasswordManager.CreateSalt();
                newUser.PasswordHash = PasswordManager.HashPassword(userModel.Password, salt);
                newUser.PasswordSalt = salt;

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("User", newUser.EmailAddress);
                HttpContext.Session.SetInt32("UserType", (int) Authenticator.UserType.USER);
                HttpContext.Session.SetInt32("UserId", newUser.UserId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.CurrentStage = "stage3";
                ViewBag.CurrentId = "#user-register-fs";
                if (UserEmailExists(userModel.EmailAddress))
                    ModelState.AddModelError("EmailAddress", "An account with this email already exists");
                else
                    ModelState.AddModelError("Password", "Invalid password");
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserRegistrationModel user)
        {
            Authenticator authenticator = lazyAuthenticator.Value;

            var storedUser = await _context.Users.FirstOrDefaultAsync(m => m.EmailAddress == user.EmailAddress);
            if (storedUser != null)
            {
                if (authenticator.AuthenticateLogin(user.Password, storedUser.PasswordHash, storedUser.PasswordSalt))
                {
                    HttpContext.Session.SetString("User", storedUser.EmailAddress);
                    HttpContext.Session.SetInt32("UserType", (int) Authenticator.UserType.USER);
                    HttpContext.Session.SetInt32("UserId", storedUser.UserId);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Invalid password!");
            }
            else
            {
                ModelState.AddModelError("EmailAddress", "No such user!");
            }
            ViewBag.CurrentStage = "stage3";
            ViewBag.CurrentId = "#user-login-fs";
            return View("Index");
        }

        [HttpPost]
        public IActionResult AdminLogin(AdminModel admin)
        {
            Authenticator authenticator = lazyAuthenticator.Value;
            
            if (authenticator.AuthenticateAdmin(admin)) {
                HttpContext.Session.SetInt32("UserType", (int) Authenticator.UserType.ADMIN);
                return RedirectToAction("Index", "Admin");
            }
            else {
                ViewBag.CurrentStage = "stage2";
                ViewBag.CurrentId = "#admin-login-fs";
                ModelState.AddModelError("Password", "Invalid password!");
                return View("Index");
            }
        }

        [HttpGet]
        public ViewResult LogOut()
        {
            HttpContext.Session.SetInt32("UserType", (int) Authenticator.UserType.NONE);
            HttpContext.Session.SetInt32("UserId", -1);
            return View("~/Views/Auth/Index.cshtml");
        }

        private bool UserEmailExists(string emailAddress)
        {
            return _context.Users.Any(e => e.EmailAddress == emailAddress);
        }

    }
}
