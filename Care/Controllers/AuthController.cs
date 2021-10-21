﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Care.Models;
using Care.Helpers;
using Microsoft.AspNetCore.Http;

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

        public IActionResult SignUp()
        {
            return PartialView("/Views/Shared/_Registration.cshtml");
        }

        public IActionResult LogIn()
        {
            return PartialView("/Views/Shared/_Login.cshtml");
        }

        public IActionResult AdminLogIn()
        {
            return PartialView("/Views/Shared/_LoginAdmin.cshtml");
        }

        public IActionResult UserLogIn()
        {
            return PartialView("/Views/Shared/_LoginUser.cshtml");
        }

        [HttpPost]
        public async Task<ViewResult> SignUp(UserRegistrationModel userModel)
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
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ModelState.AddModelError("Email", "Invalid email or password");
                return View("~/Views/Home/Index.cshtml");
            }
        }

        [HttpPost]
        public async Task<ViewResult> UserLogIn(UserRegistrationModel user)
        {
            var storedUser = await _context.Users.FirstOrDefaultAsync(m => m.EmailAddress == user.EmailAddress);
            if (storedUser != null)
            {
                if (authenticator.AuthenticateLogin(user.Password, storedUser.PasswordHash, storedUser.PasswordSalt))
                {
                        HttpContext.Session.SetString("User", storedUser.EmailAddress);
                        return View("~/Views/Home/Index.cshtml", storedUser);
                }
                ModelState.AddModelError("Password", "Invalid password!");
            }
            else
            {
                ModelState.AddModelError("Name", "No such user!");
            }
            return !ModelState.IsValid ? View("~/Views/Home/Index.cshtml") : View("~/Views/Home/Index.cshtml", storedUser);
        }

        [HttpPost]
        public ViewResult AdminLogIn(AdminModel admin)
        {
            if (authenticator.AuthenticateAdmin(admin)) {
                HttpContext.Session.SetString("User", "!admin");
                return View("~/Views/Home/Index.cshtml");
            }
            else {
                ModelState.AddModelError("Password", "Invalid password!");
                return View("~/Views/Home/Index.cshtml");
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
