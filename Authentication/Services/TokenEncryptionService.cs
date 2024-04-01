using System.Security.Cryptography;
using System.Text;

namespace Authentication.Services
{
    public class TokenEncryptionService: ITokenEncryptionService
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

        public string Encrypt(string token)
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
                            swEncrypt.Write(token);
                        }
                        byte[] encryptedBytes = msEncrypt.ToArray();
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
        }
    }
}
