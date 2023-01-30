using System.Security.Cryptography;

namespace ApplicationB.Helpers;

public static class DecryptHelper
{
    public static Task<string> DecryptStringFromBytes_Aes(byte[] encryptedTxt, byte[] key, byte[] IV)
    {
        if (encryptedTxt is not {Length: > 0})
            throw new ArgumentNullException(nameof(encryptedTxt));
        if (key is not {Length: > 0})
            throw new ArgumentNullException(nameof(key));
        if (IV is not {Length: > 0})
            throw new ArgumentNullException(nameof(IV));

        string decodedText;

        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = IV;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var ms = new MemoryStream(encryptedTxt))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs))
                    {
                        decodedText = sr.ReadToEnd();
                    }
                }
            }
        }

        return Task.FromResult(decodedText);
    }
}