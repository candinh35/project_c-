using Business.Core.Contracts;

namespace Business.Core.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// login system
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<AuthLoginResponse> Login(AuthLoginRequest request);
    
    /// <summary>
    /// refresh token for login
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    Task<AuthLoginResponse> Refresh(string refreshToken);
}