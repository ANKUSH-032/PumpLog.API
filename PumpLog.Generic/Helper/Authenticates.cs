using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PumpLog.Core.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Generic.Helper
{
    public class Authenticates
    {
        public static string? ConnectionString { get; private set; }
        public static string? SecretKey { get; private set; }

        public static void Initialize(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DataAccessConnection");
            SecretKey = config["AppSettings:Secret"];
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }

        }
        private static LoginUser Authentication<T>(AuthDto request)
        {
            LoginUser loginUser = new();

            dynamic response = GetUserDetails<T>(Email: request.Email);
            if (response == null)
            {
                return null!;
            }
            else if (response.Name.ToUpper().Equals("USERNOTREGISTER") || response.Name.ToUpper().Equals("DELETED"))
            {
                return response;
            }
            else
            {
                if (!VerifyPasswordHash(request.Password, response.PasswordHash, response.PasswordSalt))
                    return null!;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                          new Claim(ClaimTypes.Name , response.UserId ), new Claim(ClaimTypes.Role, response.Role), new Claim(ClaimTypes.Email, response.Email)
                    }
                    ),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                loginUser.UserId = response.UserId;
                loginUser.Email = response.Email;
                loginUser.Name = response.Name;
                loginUser.Token = tokenHandler.WriteToken(token);
                loginUser.Role = response.Role;

                return loginUser;

            }
        }
        public static dynamic Login<T>(AuthDto loginCredentials)
        {
            return Authentication<T>(loginCredentials);
        }
        public static dynamic GetUserDetails<T>(string? UserId = null, string? Email = null)
        {
            dynamic? response;

            using (IDbConnection db = Connection)
            {
                response = db.QueryFirstOrDefault<T>("[dbo].[uspUserDetailsGet]", new
                {
                    UserId,
                    Email
                }, commandType: CommandType.StoredProcedure);
            }

            return response!;
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            try
            {
                string storedSaltStr = Encoding.ASCII.GetString(storedSalt);
                var newPassword = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, storedSaltStr);
                string oldPassword = Encoding.Default.GetString(storedHash);
                return newPassword == oldPassword;
            }
            catch
            {
                throw;
            }
        }
    }
}
