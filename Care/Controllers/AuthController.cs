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
        private Authenticator authenticator;

        public AuthController(ServiceDbContext context)
        {
            _context = context;
            this.authenticator = new Authenticator();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegistrationModel userModel)
        {
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
        public async Task<IActionResult> UserLogIn(UserRegistrationModel user)
        {
            var storedUser = await _context.Users.FirstOrDefaultAsync(m => m.EmailAddress == user.EmailAddress);
            if (storedUser != null)
            {
                if (authenticator.AuthenticateLogin(user.Password, storedUser.PasswordHash, storedUser.PasswordSalt))
                {
                    HttpContext.Session.SetString("User", storedUser.EmailAddress);
                    HttpContext.Session.SetInt32("UserType", (int) Authenticator.UserType.USER);
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
        public IActionResult LoginAdmin(AdminModel admin)
        {
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
            HttpContext.Session.Clear();
            return View("~/Views/Auth/Index.cshtml");
        }

        private bool UserEmailExists(string emailAddress)
        {
            return _context.Users.Any(e => e.EmailAddress == emailAddress);
        }

    }
}
