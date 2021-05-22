using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Portsea.Utils.Security.Cryptography
{
    public static class X509Certificate2Builder
    {
        public static X509Certificate2 GetCertificate(string keyFilePath, string keyFilePassword)
        {
            if (File.Exists(keyFilePath))
            {
                return new X509Certificate2(keyFilePath, keyFilePassword, X509KeyStorageFlags.MachineKeySet);
            }
            else
            {
                throw new System.IO.FileNotFoundException($"Cannot find key file {keyFilePath}");
            }
        }
    }
}
