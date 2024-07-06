using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AiAnswerBackend.Config;
using Microsoft.IdentityModel.Tokens;

namespace AiAnswerBackend.Utils;

public class JwtUtils
{
    public static string GenerateToken(Guid userId,string userRole)
    {
        var claims = new[]
        {
            //JwtRegisteredClaimNames.Sub:
            // 这是一个预定义的 JWT 声明类型，表示 "subject"（主体）。
            // 在此上下文中，它通常用于存储用户的唯一标识符（如用户名或用户ID）。
            new Claim("userId", userId.ToString()),
            new Claim("userRole", userRole)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BaekKimAIAnswerWEBApplicationAuthorization"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}