namespace Business.Core.Contracts;

public class AuthLoginResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}