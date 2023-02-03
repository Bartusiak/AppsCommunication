namespace ApplicationB.Db.Models.Interfaces;

public interface IMessages
{
    int MsgId { get; }
    byte[] EncodedMsg { get;  }
}