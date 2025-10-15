using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Core.Auth
{
    public class AuthDto
    {
        public required string? Email { get; set; }
        public required string? Password { get; set; }
    }
    public class LoginUser
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? RoleID { get; set; }
        



     
    }
}
