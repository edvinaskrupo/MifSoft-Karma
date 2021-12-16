using Xunit;
using Care.Helpers;

namespace Tests {
    public class PasswordManagerTests {
        [Theory]
        [InlineData("test")]
        [InlineData("")]
        [InlineData("haef46141][.;]'.'")]
        public void VerifyHashedPasswordTest(string value) {
            string salt = PasswordManager.CreateSalt();
            string hash = PasswordManager.HashPassword(value, salt);
            Assert.True(PasswordManager.VerifyHashedPassword(value, hash, salt), $"Password verification fails for {value}");
        }
    }
}