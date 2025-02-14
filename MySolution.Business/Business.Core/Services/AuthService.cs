using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Core.Entities;

namespace Business.Core.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _refreshTokenRepository = _unitOfWork.GetRepository<RefreshToken>();
        _userRepository = _unitOfWork.GetRepository<User>();
        _configuration = configuration;
    }

    public async Task<AuthLoginResponse> Login(AuthLoginRequest request)
    {
        var response = new AuthLoginResponse();
        var user = await _userRepository.GetQuery()
            .FirstOrDefaultAsync(x => x.Code == request.code);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.password, user.HashPass))
            return response;

        response.AccessToken = await GenerateAccessToken(user.id);
        response.RefreshToken = await GenerateRefreshToken(user.id);

        var refreshToken = new RefreshToken
        {
            Token = response.AccessToken,
            Refresh = response.RefreshToken,
            UserId = user.id,
            IsRevoked = false,
            ExpiryDate = DateTime.UtcNow.AddDays(10),
            create_date = DateTime.UtcNow,
            create_user = user.id,
            update_date = DateTime.UtcNow,
            update_user = user.id
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return response;
    }

    public async Task<AuthLoginResponse> Refresh(string refreshToken)
    {
        var response = new AuthLoginResponse();
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(refreshToken) as JwtSecurityToken;

        if (jsonToken == null) return response;

        var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            return response;

        var user = await _userRepository.GetQuery()
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.id == userGuid);

        if (user == null) return response;

        var validToken = user.RefreshTokens.FirstOrDefault(rt =>
            rt.Refresh == refreshToken &&
            rt.ExpiryDate > DateTime.UtcNow &&
            !rt.IsRevoked);

        if (validToken == null) return response;

        response.AccessToken = await GenerateAccessToken(user.id);
        response.RefreshToken = await GenerateRefreshToken(user.id);

        validToken.Token = response.AccessToken;
        validToken.Refresh = response.RefreshToken;
        validToken.ExpiryDate = DateTime.UtcNow.AddDays(10);

        await _refreshTokenRepository.UpdateSync(validToken);
        await _unitOfWork.SaveChangesAsync();

        return response;
    }

    private async Task<string> GenerateAccessToken(Guid userId)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["AccessTokenExpirationMinutes"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
    }

    private async Task<string> GenerateRefreshToken(Guid userId)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
        var expirationDays = int.Parse(jwtSettings["RefreshTokenExpirationDays"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(expirationDays),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
    }
}