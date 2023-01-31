using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationB.Db.Models.Interfaces;

namespace ApplicationB.Db.Models;

[Table("Messages")]
public class Messages : IMessages
{
    public Messages(){}
    public Messages(byte[] encodedMsg)
    {
        EncodedMsg = encodedMsg;
    }

    [Key]
    public int MsgId { get; set; }
    public byte[] EncodedMsg { get; set; }
}