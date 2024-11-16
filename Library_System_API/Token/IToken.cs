namespace Library_System_API.Token
{
    public interface IToken
    {
        string GenerateJwtToken(string name, string email);
    }
}
