using System.Security.Cryptography;

namespace Portsea.Utils.Security.Cryptography
{
    public class Sha1PasswordEncoder : PasswordEncoder
    {
        private static readonly HashAlgorithm Sha1Algorithm = SHA1.Create();

        public Sha1PasswordEncoder()
            : base(Sha1Algorithm)
        {
        }
    }
}
