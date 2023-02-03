using ApplicationB.Db.Models.Interfaces;

namespace ApplicationB.Db.Models;

public class MessageToDecrypt : IMessageToDecrypt
{
    public MessageToDecrypt(byte[] messages, DataToDecrypt keyToDecrypt)
    {
        Message = messages;
        KeyToDecrypt = keyToDecrypt;
    }
    
    public byte[] Message { get; }
    public DataToDecrypt KeyToDecrypt { get; }
}