namespace Application.Interfaces
{
    public interface ISecurityService
    {
        (string EncryptedText, string Key, string IV) Encrypt(string username, string plainText);
        string Decrypt(string encryptedText, string key, string iv);
        bool VerifyPassword(string providedPassword, string storedPasswordHash, string key, string iv);
    }
}
