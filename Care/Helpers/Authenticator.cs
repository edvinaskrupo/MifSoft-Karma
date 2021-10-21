using Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Care.Helpers
{
    public class Authenticator
    {
        public bool Authenticate(UserRegistrationModel user)
        {
            bool authorized = ValidateEmail(user.EmailAddress) && ValidatePassword(user.Password);
            return authorized;
        }

        public bool AuthenticateAdmin(UserRegistrationModel admin) {
            String hash, salt;
            try {
                using (StreamReader authFile = new StreamReader("auth")) {
                    hash = authFile.ReadLine();
                    salt = authFile.ReadLine();
                }
            }
            catch {
                return false;
            }

            if (hash == null || salt == null) {
                return false;
            }
            else {
                return PasswordManager.VerifyHashedPassword(admin.Password, hash, salt);
            }
        }

        public bool AuthenticateLogin(string pass, string hash, string salt)
        {
            bool authorizedLogin = PasswordManager.VerifyHashedPassword(pass, hash, salt);
            return authorizedLogin;
        }

        private bool ValidateEmail(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool ValidatePassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
        }
    }
}
