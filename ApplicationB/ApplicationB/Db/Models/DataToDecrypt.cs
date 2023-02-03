using ApplicationB.Db.Models.Interfaces;

namespace ApplicationB.Db.Models;

public class DataToDecrypt : IDataToDecrypt
{
    public DataToDecrypt(byte[] key, byte[] symmetricAlgorithm)
    {
        Key = key;
        SymmetricAlgorithm = symmetricAlgorithm;
    }

    public DataToDecrypt() { }

    public byte[] Key { get; set; } = null!;
    public byte[] SymmetricAlgorithm { get; set; } = null!;
}