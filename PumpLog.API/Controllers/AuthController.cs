using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PumpLog.Core.Auth;
using PumpLog.Core.comman;
using PumpLog.Generic.Helper;
using System.Globalization;

namespace PumpLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthDto authenticationRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        new
                        {
                            Status = false,
                            Message = string.Join(Environment.NewLine, ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage))
                        });
                }
                authenticationRequest.Email = authenticationRequest?.Email?.ToLower();
                LoginUser user = Authenticates.Login<LoginUser>(authenticationRequest!);

                if (user == null)
                {
                    ClsResponse rep = new()
                    {

                        Message = MessageHelper.invalidCredentials,
                        Data = null
                    };
                    return StatusCode(StatusCodes.Status401Unauthorized, rep);

                }

                else
                {
                    ClsResponse rep = new();
                    if (user.Name.ToUpper(CultureInfo.CurrentCulture).Equals("DELETED"))
                    {
                        rep.Message = MessageHelper.userNotFoundForEmail;
                        rep.Data = null;
                        return StatusCode(StatusCodes.Status403Forbidden, rep);
                    }


                    if (!string.IsNullOrWhiteSpace(user.Name) && user.Name.ToUpper(CultureInfo.CurrentCulture).Equals("USERNOTREGISTER"))
                    {
                        rep.Message = MessageHelper.userNotRegister;
                        rep.Data = null;
                        return StatusCode(StatusCodes.Status403Forbidden, rep);
                    }

                }

                return Ok(new { Status = true, Message = "Success", Userdetails = user });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }

        }
    }
}
/*
 * {
  "email": "ankush@yopmail.com",
  "password": "Admin@1234"
}
 */