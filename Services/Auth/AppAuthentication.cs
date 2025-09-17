
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Projet101.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Projet101.DAL;



namespace Projet101.Services.Auth
{
    public class AppAuthentication(IConfiguration configuration, IUnitOfWork unitOfWork) : IAppAuthentication
    {
        public async Task<object?> LoginAsync(UserDto request)
        {

            // à changer par l'utilisateur de la base de données 
            /*if (request.User == "user1" && request.Password == "pass1")
            {
                var token = CreateToken(request);

                return new { token = token };
            }*/
            var user = unitOfWork.UserRepository
    .Get(u => u.Email == request.User) // or u.Name if you want
    .FirstOrDefault();

            if (user == null)
                return null;

            // Verify password hash
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            // Generate JWT token
            var token = CreateToken(user);

            return new { token };
        }

        private string CreateToken(User user)
        {

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
           // new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
           // new Claim("FirstName", user.FirstName),
           // new Claim("FamilyName", user.FamilyName)
         
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }





    }
}
