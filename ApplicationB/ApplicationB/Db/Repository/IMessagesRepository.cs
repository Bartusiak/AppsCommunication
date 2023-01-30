using ApplicationB.Db.Models;

namespace ApplicationB.Db.Repository;

public interface IMessagesRepository
{
    IEnumerable<Messages> GetMessages();
    public Messages GetMessageById(int id);
    public Messages GetLastMessage();
    public void ClearDataTable();
    public void RemoveLastMessage();
}