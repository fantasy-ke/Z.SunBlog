using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Domain.Authorization;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtSettings _jwtConfig;
    public JwtTokenProvider(IConfiguration configuration)
    {
        _jwtConfig = configuration.GetSection("App:JWtSetting").Get<JwtSettings>() ?? throw new ArgumentException("请先检查appsetting中JWT配置");
    }
    public string GenerateAccessToken(UserTokenModel user)
    {
        // 设置Token的Claims
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName), //HttpContext.User.Identity.Name
            new Claim("Id", user.UserId.ToString()),
            new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes)).ToUnixTimeSeconds()}"),
            new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes).ToString()),
        };

        if (user.RoleIds != null && user.RoleIds.Any())
        {
            claims.AddRange(user.RoleIds.Select(p => new Claim("RoleIds", p.ToString())));
        }
        if (user.RoleNames != null && user.RoleNames.Any())
        {
            claims.AddRange(user.RoleNames.Select(p => new Claim(ClaimTypes.Role, p)));
        }

        user.Claims = claims.ToArray();

        // 生成Token的密钥
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        // 生成Token的签名证书
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);

        // 设置Token的过期时间
        DateTime expires = DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes);

        // 创建Token
        JwtSecurityToken token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        // 生成Token字符串
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

    public bool ValidateAccessToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtConfig.SecretKey))
            }, out var validatedToken);
        }
        catch (Exception x)
        {
            return false;
        }
        return true;
    }
}

public interface IJwtTokenProvider : ITransientDependency
{
    string GenerateAccessToken(UserTokenModel user);
    bool ValidateAccessToken(string token);
}
