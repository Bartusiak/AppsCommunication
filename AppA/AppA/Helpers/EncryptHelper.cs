using System.Security.Cryptography;

namespace AppA.Helpers;

public class EncryptHelper
{
    public static async Task<byte[]> EncryptStringToBytes_Aes(string text, byte[] key, byte[] IV)
    {
        return await Task.Run(() =>
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (key is not {Length: > 0})
                throw new ArgumentNullException(nameof(key));
            if (IV is not {Length: > 0})
                throw new ArgumentNullException(nameof(IV));

            byte[] encrypted;

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        });
    }
}