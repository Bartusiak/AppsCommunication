namespace ApplicationB.Db.Models;

public class MessageToDecrypt
{
    public MessageToDecrypt(byte[] messages, DataToDecrypt keyToDecrypt)
    {
        Message = messages;
        KeyToDecrypt = keyToDecrypt;
    }
    
    public byte[] Message;
    public DataToDecrypt KeyToDecrypt;
}