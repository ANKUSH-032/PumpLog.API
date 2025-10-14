using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PumpLog.Core.PumpLog;

namespace PumpLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PumpLogController : ControllerBase
    {
        public readonly IPumpLogRepository _pumpLogRepository;

        public PumpLogController(IPumpLogRepository pumpLogRepository)
        {
            _pumpLogRepository = pumpLogRepository;
        }
    }
}
