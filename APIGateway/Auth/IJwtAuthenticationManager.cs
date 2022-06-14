namespace APIGateway.Auth
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string key);
    }
}
