using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Authentication.Application.Users.Helpers
{
    public class UserUtils
    {
        private readonly static Random RANDOM = new Random();
        private readonly static string ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string GetSalt()
        {
            return GenerateRandomString(50);
        }

        public static string GenerateSecurePassword(string password, string salt)
        {
            return Convert.ToBase64String(Encrypt(password, salt));
        }

        public static byte[] Encrypt(string password, string salt)
        {
            return KeyDerivation.Pbkdf2(
                    password,
                    Encoding.ASCII.GetBytes(salt),
                    KeyDerivationPrf.HMACSHA1,
                    10000,
                    256
            );
        }

        private static string GenerateRandomString(int lenght)
        {
            var str = "";

            for (int i = 0; i < lenght; i++)
            {
                str += ALPHABET[RANDOM.Next(0, lenght - 1)];
            }

            return str;
        }
    }
}