namespace ApplicationB.Db.Models.Interfaces;

public interface IMessageToDecrypt
{
    public byte[] Message { get; }
    public DataToDecrypt KeyToDecrypt { get; }
}