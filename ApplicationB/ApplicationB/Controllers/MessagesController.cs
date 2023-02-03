using ApplicationB.Db.Models;
using ApplicationB.Db.Repository;
using ApplicationB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    
    [SwaggerOperation(Summary = "Decrypt specified message, received from Application A.", 
                      Description = "That endpoint returns callback to Application A with custom status and decrypted message from Application B.")]
    [HttpPost("decrypt-message")]
    public async Task<IActionResult> DataToDecryptMsg([FromBody] DataToDecrypt data)
    {
        var message = _messageService.GetLastMessage();
        var msgToDecrypt = new MessageToDecrypt(message.EncodedMsg, data);
        
        return Ok(await Decrypt(msgToDecrypt));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Decrypt(MessageToDecrypt msgToDecrypt)
    {
        var decryptedMsg = await DecryptHelper.DecryptStringFromBytes_Aes(msgToDecrypt.Message, msgToDecrypt.KeyToDecrypt.Key, msgToDecrypt.KeyToDecrypt.SymmetricAlgorithm);

        //TODO Need to check it - situation when decryptedMsg not successfully finished - beloved method will be executed??
        //TODO Should be removed last message despite the thrown error?
        _messageService.RemoveLastMessage();
        
        return await Task.FromResult(Ok($"I'm a teapot \n{decryptedMsg}"));
    }
    
    [SwaggerOperation(Summary = "Get a specified message by Id.", 
                      Description = "That endpoint returns a specified message via Id from Application B.")]
    [HttpGet("get-msg/{id}")]
    public IActionResult GetMessageById(int id)
    {
        var msg = _messageService.GetMessageById(id);
        return Ok(msg);
    }

    [SwaggerOperation(Summary = "Clear data from Messages table.", 
                      Description = "That endpoint clear all stored messages from a table.")]
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

    [SwaggerOperation(Summary = "Display Application B status.", 
                      Description = "That endpoint return Application B status and a short description how to use it.")]
    [HttpGet("healthz")]
    public IActionResult HealthResponse()
    {
        return Ok("Status application - Healthy " +
                  "\n\n To start work with app please run Application A." +
                  "\n\n - If you want to manual clear database table 'Messages' with messages use endpoint: /api/Messages/rm-msg" +
                  "\n\n - You can check existing messages using endpoint: /api/Messages");
    }

    [SwaggerOperation(Summary = "Get all messages.", 
                      Description = "That endpoint returns all stored messages in the database table.")]
    [HttpGet]
    public IActionResult GetMessages() => Ok(_messageService.GetMessages());
}