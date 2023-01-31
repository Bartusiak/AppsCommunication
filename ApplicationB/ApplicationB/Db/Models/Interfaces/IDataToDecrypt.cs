namespace ApplicationB.Db.Models.Interfaces;

public interface IDataToDecrypt
{
    public byte[] Key { get; }
    public byte[] SymmetricAlgorithm { get; }
}