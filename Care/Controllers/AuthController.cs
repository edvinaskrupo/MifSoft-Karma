using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Care.Models;
using Care.Helpers;

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

        public IActionResult SignUp()
        {
            return PartialView("/Views/Shared/_Registration.cshtml");
        }

        [HttpPost]
        public async Task<ViewResult> SignUp(UserRegistrationModel userModel)
        {
            if (authenticator.Authenticate(userModel) && !UserEmailExists(userModel.EmailAddress))
            {
                UserModel newUser = new UserModel();

                newUser.EmailAddress = userModel.EmailAddress;
                newUser.PasswordHash = PasswordManager.CreateHashedPassword(userModel.Password).Item1;
                newUser.PasswordSalt = PasswordManager.CreateHashedPassword(userModel.Password).Item2;

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return View("/Home/Index.cshtml");
            }
            return View("/Home/Index.cshtml");
            //ModelState.AddModelError("Name", "Such user already exists!");
        }
        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
        private bool UserEmailExists(string emailAddress)
        {
            return _context.Users.Any(e => e.EmailAddress == emailAddress);
        }

    }
}
