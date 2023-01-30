using AppB.Db.Data;
using ApplicationB.Db.Models;
using ApplicationB.Db.Repository;

namespace ApplicationB.Db.Services;

public class MessageService : IMessagesRepository
{
    private readonly MessagesDbContext _context;

    public MessageService(MessagesDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Messages> GetMessages() => _context.Message.ToList();

    public Messages GetMessageById(int msgId) => _context.Message.Find(msgId)!;
    
    public Messages GetLastMessage() => _context.Message.OrderByDescending(m => m.MsgId).FirstOrDefault()!;

    public void RemoveLastMessage()
    {
        var lastMsg = _context.Message.OrderByDescending(m => m.MsgId).FirstOrDefault();

        if (lastMsg != null)
        {
            _context.Message.Remove(lastMsg);
            _context.SaveChanges();
        }
    }

    public void ClearDataTable()
    {
        var messages = _context.Message;
        var msgList = messages.ToList();
        messages.RemoveRange(msgList);
        _context.SaveChanges();
    }
}