namespace Authentication.Services
{
    public interface ITokenEncryptionService
    {
        string EncryptToken(string token);
    }

}
