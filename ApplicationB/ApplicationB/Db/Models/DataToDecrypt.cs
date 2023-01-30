namespace ApplicationB.Db.Models;

public class DataToDecrypt
{
    public byte[] Key { get; set; }
    public byte[] SymmetricAlgorithm { get; set; }
}