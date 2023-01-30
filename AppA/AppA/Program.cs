// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using AppA.Helpers;
using static AppA.Consts.PhrasesList;

namespace AppA
{
    class Program
    {
        public static async Task Main()
        {
            while (true)
            {
                var random = new Random();
                var randomIndex = random.Next(PHRASES_LIST.Length);
                var msg = PHRASES_LIST[randomIndex];
                Console.WriteLine($"Message to send: {msg}");

                using var aes = Aes.Create();
                var encryptedMsg = await EncryptHelper.EncryptStringToBytes_Aes(msg, aes.Key, aes.IV);
            
                await SendDataHelper.SendEncryptedMessageToDb(encryptedMsg);
                await SendDataHelper.SendKeyAndIVToAppB(aes.Key, aes.IV);
                
                Thread.Sleep(15000);
                Console.Clear();
            }
        }
    }
}