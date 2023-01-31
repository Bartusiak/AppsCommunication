using ApplicationB.Db.Models;
using ApplicationB.Db.Repository;
using ApplicationB.Db.Services;
using ApplicationB.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationB.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessagesRepository _messageService;

    public MessagesController(IMessagesRepository msgService)
    {
        _messageService = msgService;
    }

    [HttpPost("decrypt-message")]
    public async Task<IActionResult> DataToDecryptMsg([FromBody] DataToDecrypt data)
    {
        var message = _messageService.GetLastMessage();
        var msgToDecrypt = new MessageToDecrypt(message.EncodedMsg, data);
        
        return Ok(await Decrypt(msgToDecrypt));
    }

    public async Task<IActionResult> Decrypt(MessageToDecrypt msgToDecrypt)
    {
        var decryptedMsg = await DecryptHelper.DecryptStringFromBytes_Aes(msgToDecrypt.Message, msgToDecrypt.KeyToDecrypt.Key, msgToDecrypt.KeyToDecrypt.SymmetricAlgorithm);

        //TODO Need to check it - situation when decryptedMsg not successfully finished - beloved method will be executed??
        //TODO Should be removed last message despite the thrown error?
        _messageService.RemoveLastMessage();
        
        return Ok($"I'm a teapot \n{decryptedMsg}");
    }

    [HttpGet("get-msg/{id}")]
    public IActionResult GetMessageById(int id)
    {
        var msg = _messageService.GetMessageById(id);
        return Ok(msg);
    }

    [HttpGet("rm-msg")]
    public IActionResult RemoveMessages()
    {
        try
        {
            _messageService.ClearDataTable();
            return Ok("Data from table 'Messages' was successfully removed.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Received error when was trying delete data from Messages table. \nError: {ex.Message}");
        }
    }

    [HttpGet("healthz")]
    public IActionResult HealthResponse()
    {
        return Ok("Status application - Healthy " +
                  "\n\n To start work with app please run Application A." +
                  "\n\n - If you want to manual clear database table 'Messages' with messages use endpoint: /api/Messages/rm-msg" +
                  "\n\n - You can check existing messages using endpoint: /api/Messages");
    }

    [HttpGet]
    public IActionResult GetMessages() => Ok(_messageService.GetMessages());
}