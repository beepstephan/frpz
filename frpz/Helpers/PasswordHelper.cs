using System.Security.Cryptography;
using System.Text;

namespace frpz.Helpers
{
    public static class PasswordHelper
    {
        public static string passwordHasher(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
