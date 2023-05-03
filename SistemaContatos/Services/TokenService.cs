using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using SistemaContatos.Models;
using Microsoft.IdentityModel.Tokens;
using SistemaContatos.Interfaces;

namespace SistemaContatos.Services
{
	public static class TokenService
	{
		
		public static string GenerateToken(UserModel user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(SettingsToken.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.FirstName.ToString()),
					new Claim(ClaimTypes.Role, user.Perfil.ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(60),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);

		}
		public static async Task<string> Authenticate(UserModel user)
		{
			//if (user == null) return NotFound(new { message = "Usuário ou senha inválido" });

			var token = TokenService.GenerateToken(user);
			//return new
			//{
			//	user = user,
			//	token = token,


			//};
			return token;
		}
		public static bool TokenIsValid(string token)
		{
			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("065c14e0c2a0e35c4f7a1d86a12837674fba8ba1"));

			var handler = new JwtSecurityTokenHandler();

			try
			{
				var tokenValid = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = key
				};

				SecurityToken validateToken;
				handler.ValidateToken(token, tokenValid, out validateToken);
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
}
