using ApplicationB.Db.Models;

namespace ApplicationB.Db.Repository;

public interface IMessagesRepository
{
    IEnumerable<Messages> GetMessages();
    public Task<Messages> GetMessageByIdAsync(int id);
    public Task<Messages> GetLastMessageAsync();
    public void ClearDataTable();
    public void RemoveLastMessage();
}