using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ramand_Application.Model;
using Ramand_Application.ServiceContract.Persistence;
using Ramand_Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ramand_Persistence.Repositories
{
    public class AuthenticationRespository : BaseConnectionString , IAuthenticationService
    {
        #region Fileds

        private readonly JWTSettings _jwtSettings;
      
        #endregion

        #region Ctor

        public AuthenticationRespository(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        #endregion

        #region Methods

        public List<User> GetUsers()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<User>("GetAllUser",commandType:System.Data.CommandType.StoredProcedure).AsList();
                return result;
            }

        }

        public AuthResponse Login(AuthRequest request)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new { Username = request.Username };
                var user = connection.QueryFirstOrDefault<User>("GetUserByUsername", parameters,commandType: System.Data.CommandType.StoredProcedure);
                if (!user.Password.Equals(request.Password))
                {
                    return new AuthResponse();
                }
                else
                {
                    var jwtSecurityToken = GenerateToken(user);
                    var response = new AuthResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        Username = user.Username
                    };

                    return response;
                }
            }
        }

        #endregion

        #region Utilities

        private JwtSecurityToken GenerateToken(User user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name,user.FirstName),
                new Claim(JwtRegisteredClaimNames.NameId , user.Id.ToString())
            };

            var symmtericSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredential = new SigningCredentials(symmtericSecurity, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(audience: _jwtSettings.Audience,
                issuer: _jwtSettings.IsSure,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredential);

            return jwtSecurityToken;
        }


        #endregion
    }
}