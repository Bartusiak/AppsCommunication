using ApplicationB.Db.Models.Interfaces;

namespace ApplicationB.Db.Models;

public class DataToDecrypt : IDataToDecrypt
{
    public byte[] Key { get; set; }
    public byte[] SymmetricAlgorithm { get; set; }
}