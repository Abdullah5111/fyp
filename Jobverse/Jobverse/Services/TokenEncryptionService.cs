using Jobverse.Services;
using System.Security.Cryptography;
using System.Text;

namespace Jobverse.Services
{
    public class TokenEncryptionService : ITokenEncryptionService
    {
        private readonly byte[] _encryptionKey;

        public TokenEncryptionService(byte[] encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }
        public string EncryptToken(string token)
        {

            byte[] key = Encoding.UTF8.GetBytes("12345678123456781234567812345678");
            byte[] iv = Encoding.UTF8.GetBytes("1234567812345678");

            AesEncryption aes = new AesEncryption(key, iv);

            return aes.Encrypt(token);

            //using var aes = Aes.Create();
            //aes.Key = _encryptionKey;
            //aes.Mode = CipherMode.CBC;
            //aes.Padding = PaddingMode.PKCS7;

            //aes.IV = Encoding.UTF8.GetBytes("FixedIV123456789");

            //byte[] encryptedBytes;
            //using (var encryptor = aes.CreateEncryptor())
            //{
            //    encryptedBytes = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(token), 0, token.Length);
            //}

            //return Convert.ToBase64String(encryptedBytes);
        }
    }

    public class AesEncryption
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public AesEncryption(byte[] key, byte[] iv)
        {
            this.key = key;
            this.iv = iv;
        }

        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        byte[] encryptedBytes = msEncrypt.ToArray();
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
        }
    }
}
