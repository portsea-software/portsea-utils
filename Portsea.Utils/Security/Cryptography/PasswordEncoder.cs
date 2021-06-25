using System;
using System.Security.Cryptography;
using System.Text;

namespace Portsea.Utils.Security.Cryptography
{
    public class PasswordEncoder
    {
        private readonly HashAlgorithm hashAlgorithm;

        public PasswordEncoder(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }

        public string EncodePassword(string password, string salt)
        {
            byte[] passwordAndSalt = GetPasswordAndSaltBytes(password, salt);
            byte[] hash = this.hashAlgorithm.ComputeHash(passwordAndSalt);

            return GetHashBase64String(hash);
        }

        private static byte[] GetPasswordAndSaltBytes(string password, string salt)
        {
            return CombinePasswordAndSaltBytes(
                GetPasswordBytes(password),
                GetSaltBytes(salt));
        }

        private static string GetHashBase64String(byte[] hash)
        {
            return Convert.ToBase64String(hash);
        }

        private static byte[] CombinePasswordAndSaltBytes(byte[] password, byte[] salt)
        {
            byte[] passwordAndSaltBytes = new byte[salt.Length + password.Length];
            Buffer.BlockCopy(salt, 0, passwordAndSaltBytes, 0, salt.Length);
            Buffer.BlockCopy(password, 0, passwordAndSaltBytes, salt.Length, password.Length);

            return passwordAndSaltBytes;
        }

        private static byte[] GetPasswordBytes(string password)
        {
            return Encoding.Unicode.GetBytes(password);
        }

        private static byte[] GetSaltBytes(string salt)
        {
            return Convert.FromBase64String(salt);
        }
    }
}
