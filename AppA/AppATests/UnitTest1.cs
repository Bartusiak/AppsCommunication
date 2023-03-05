using System.Text;
using AppA.Helpers;
using System.Linq;

namespace AppATests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EncryptMessage_KeyIsNull_ExpectedArgumentNullException()
    {
        //Arrange
        var msgToEncrypt = "Life is a life";
        var IV = Encoding.UTF8.GetBytes("PonmlkjihgfedcbA");

        //Assert
        Assert.Throws<ArgumentNullException>(() => EncryptHelper.EncryptStringToBytes_Aes(msgToEncrypt, null, IV));
    }
    
    [Test]
    public void EncryptMessage_IVIsNull_ExpectedArgumentNullException()
    {
        //Arrange
        var msgToEncrypt = "Life is a life";
        var key = Encoding.UTF8.GetBytes("Abcdefghijklmnop");

        //Assert
        Assert.Throws<ArgumentNullException>(() => EncryptHelper.EncryptStringToBytes_Aes(msgToEncrypt, key, null));
    }
    
    [Test]
    public void EncryptMessage_MessageIsNull_ExpectedArgumentNullException()
    {
        //Arrange
        var key = Encoding.UTF8.GetBytes("Abcdefghijklmnop");
        var IV = Encoding.UTF8.GetBytes("PonmlkjihgfedcbA");

        //Assert
        Assert.Throws<ArgumentNullException>(() => EncryptHelper.EncryptStringToBytes_Aes(null, key, IV));
    }
    
    [Test]
    public async Task EncryptMessage_ExpectedSuccess()
    {
        //Arrange
        var msgToEncrypt = "Life is a life";
        var key = Encoding.UTF8.GetBytes("Abcdefghijklmnop");
        var IV = Encoding.UTF8.GetBytes("PonmlkjihgfedcbA");
        
        //Act
        var result = await EncryptHelper.EncryptStringToBytes_Aes(msgToEncrypt, key, IV);
        var expectedResult = new List<byte>() {178, 22, 244, 156, 139, 136, 174, 124, 162, 245, 105, 12, 201, 198, 244, 149};

        //Assert
        Assert.IsTrue(expectedResult.SequenceEqual(result));
    }
}