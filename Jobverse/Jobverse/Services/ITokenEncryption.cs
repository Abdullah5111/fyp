namespace Jobverse.Services
{
    public interface ITokenEncryptionService
    {
        string EncryptToken(string token);
    }

}
