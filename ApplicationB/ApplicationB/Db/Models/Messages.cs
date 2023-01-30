using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationB.Db.Models;

[Table("Messages")]
public class Messages
{
    public Messages(byte[] encodedMsg)
    {
        EncodedMsg = encodedMsg;
    }

    [Key]
    public int MsgId { get; set; }
    public byte[] EncodedMsg { get; set; }
}