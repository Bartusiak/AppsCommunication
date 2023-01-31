using System.Text;
using ApplicationB.Controllers;
using ApplicationB.Db.Models;
using ApplicationB.Db.Repository;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core;

namespace ApplicationBTests;

public class Tests
{
    private IMessagesRepository? _msgRepository;
    private Messages _messages;
    private MessageToDecrypt _messageToDecrypt;
    private DataToDecrypt _dataToDecrypt;
    private List<Messages> _messagesList;
    private byte[] _fakeEncodedMsg;
    
    [SetUp]
    public void Setup()
    {
        _msgRepository = Substitute.For<IMessagesRepository>();
        
        _fakeEncodedMsg = Convert.FromBase64String("fKpQaVWZrt8x7j8D29DMMg==");
        _messages = new Messages {MsgId = 1, EncodedMsg = _fakeEncodedMsg.ToArray()};
        _messagesList = new List<Messages> {_messages, _messages};
        
        _msgRepository.GetLastMessage().Returns(_messages);
        _msgRepository.GetMessageById(1).Returns(_messages);
        _msgRepository.GetMessages().Returns(_messagesList);
    }

    [TestCase("I'm a teapot \nDecrypted text", "rdZ49cRVtuasVcAIXGt8bQ==")]
    [TestCase("I'm a teapot \nSo many books, so little time", "YIKetqm7ZfacTliqv9ZvCets+c1llTWKrc02ND2MZbA=")]
    public void Decrypt_TextToDecrypt_ReturnExpectedText(string expectedText, string textTodecrypt)
    {
        //Arrange
        var controller = new MessagesController(_msgRepository);
        var fakeEncodedMsg = Convert.FromBase64String(textTodecrypt);

        _messages = new Messages {MsgId = 1, EncodedMsg = fakeEncodedMsg.ToArray()};
        _dataToDecrypt = new DataToDecrypt
        {
            Key = Encoding.UTF8.GetBytes("Abcdefghijklmnop"),
            SymmetricAlgorithm = Encoding.UTF8.GetBytes("PonmlkjihgfedcbA")
        };
        _messageToDecrypt = new MessageToDecrypt(_messages.EncodedMsg, _dataToDecrypt);
        
        //Act
        var result = controller.Decrypt(_messageToDecrypt);
        var objResult = result.Result as ObjectResult;

        //Assert
        Assert.AreEqual(expectedText, objResult.Value.ToString());
    }
    
    [TestCase("I'm a teapot \nDecrypted text", "rdZ49cRVtuasVcAIXGt8bQ==")]
    [TestCase("I'm a teapot \nSo many books, so little time", "YIKetqm7ZfacTliqv9ZvCets+c1llTWKrc02ND2MZbA=")]
    public void Decrypt_WrongKey_ExpectedError(string expectedText, string textTodecrypt)
    {
        //Arrange
        var controller = new MessagesController(_msgRepository);
        var fakeEncodedMsg = Convert.FromBase64String(textTodecrypt);

        _messages = new Messages {MsgId = 1, EncodedMsg = fakeEncodedMsg.ToArray()};
        _dataToDecrypt = new DataToDecrypt
        {
            Key = Encoding.UTF8.GetBytes("Abcdefghijkldasc"),
            SymmetricAlgorithm = Encoding.UTF8.GetBytes("PonmlkjihgfedcbA")
        };
        _messageToDecrypt = new MessageToDecrypt(_messages.EncodedMsg, _dataToDecrypt);
        
        //Act
        var result = controller.Decrypt(_messageToDecrypt);

        //Assert
        Assert.AreEqual(TaskStatus.Faulted, result.Status);
    }
    
    [TestCase("I'm a teapot \nSo many books, so little time", "YIKetqm7ZfacTliqv9ZvCets+c1llTWKrc02ND2MZbA=")]
    public void Decrypt_WrongSymetricAlgorithm_ExpectedError(string expectedText, string textTodecrypt)
    {
        //Arrange
        var controller = new MessagesController(_msgRepository);
        var fakeEncodedMsg = Convert.FromBase64String(textTodecrypt);

        _messages = new Messages {MsgId = 1, EncodedMsg = fakeEncodedMsg.ToArray()};
        _dataToDecrypt = new DataToDecrypt
        {
            Key = Encoding.UTF8.GetBytes("Abcdefghijklmnop"),
            SymmetricAlgorithm = Encoding.UTF8.GetBytes("Abcdefghijklmnop")
        };
        _messageToDecrypt = new MessageToDecrypt(_messages.EncodedMsg, _dataToDecrypt);
        
        //Act
        var result = controller.Decrypt(_messageToDecrypt);
        var objResult = result.Result as ObjectResult;

        //Assert
        Assert.AreNotEqual(expectedText, objResult.Value);
    }

    [Test]
    public void GetMessageById_IdOne_ReturnExpectedEncodedMsg()
    {
        //Arrange
        var controller = new MessagesController(_msgRepository);
        
        //Act
        var result = controller.GetMessageById(1);
        var objResult = result as OkObjectResult;
        var message = objResult.Value as Messages;

        //Assert
        Assert.AreEqual(_fakeEncodedMsg, message.EncodedMsg);
    }
    
    [Test]
    public void GetMessageById_IdTwo_ReturnNull()
    {
        //Arrange
        _messages = new Messages();
        _msgRepository.GetMessageById(6).Returns(_messages);
        var controller = new MessagesController(_msgRepository);

        //Act
        var result = controller.GetMessageById(2);
        var objResult = result as OkObjectResult;

        //Assert
        Assert.AreEqual(null, objResult.Value);
    }

    [Test]
    public void RemoveMessages_ReturnExpectedMessage()
    {
        //Arrange
        var controller = new MessagesController(_msgRepository);
        
        //Act
        var result = controller.RemoveMessages();
        var objResult = result as OkObjectResult;
        
        //Assert
        Assert.AreEqual("Data from table 'Messages' was successfully removed.", objResult.Value);
    }
    
    [Test]
    public void GetMessages_ReturnListMessages()
    {
        //Arrange
        var controller = new MessagesController(_msgRepository);

        //Act
        var result = controller.GetMessages();
        var objResult = result as OkObjectResult;

        //Assert
        Assert.AreEqual(_messagesList, objResult.Value);
    }
}