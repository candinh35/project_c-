using Model.Core.Entities;

namespace Business.Core.Interfaces;

public interface IAuthService
{
    Task<string> GenerateAccessToken(Guid userId);
    Task<string> GenerateRefreshToken(Guid userId);
    Task<bool> ValidateRefreshToken(Guid userId, string refreshToken);
}