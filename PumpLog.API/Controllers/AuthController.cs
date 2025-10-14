using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PumpLog.Core.Auth;

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
    }
}
