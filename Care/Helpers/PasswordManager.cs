using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Care.Helpers
{
    public class PasswordManager
    {
        public static Tuple<string, string> CreateHashedPassword(string password)
        {
            var salt = CreateSalt();
            Console.WriteLine($"Using salt '{ Convert.ToBase64String(salt) }'.");

            var hash = HashPassword(password, salt);
            Console.WriteLine($"Hash is '{ Convert.ToBase64String(hash) }'.");

            return Tuple.Create(Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }
        private static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024;

            return argon2.GetBytes(16);
        }

        private static bool VerifyHashedPassword(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }

        private static byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}
