using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PumpLog.Core.comman;
using PumpLog.Generic.Helper;

namespace PumpLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonRepository _commonRepository;
        private const string _controller = "CommonController";


        public CommonController(ICommonRepository commonRepository, IConfiguration configuration)
        {
            _commonRepository = commonRepository;
            _configuration = configuration;
        }


        [HttpPost, Route("/api/masterlist")]
        public async Task<IActionResult> GetMasterListAsync([FromBody] MasterListParams masterListParams)
        {
            try
            {
                if (string.IsNullOrEmpty(masterListParams.Type))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        Message = string.Join(Environment.NewLine, "Type parameter cant be empty")
                    });
                }

                masterListParams.Type = masterListParams.Type.ToUpper();

                ClsResponse<MasterList> response = await _commonRepository.MasterListAsync(masterListParams.Type).ConfigureAwait(false);

                if (response.Status)
                {
                    if (response.Data == null || !response.Data.Any())
                    {
                        return StatusCode(200, new
                        {
                            status = false,
                            message = string.Join(Environment.NewLine, "No master data exists.")
                        });
                    }
                    else if (string.IsNullOrEmpty(response.Data.First().Value) && Convert.ToInt64(response.Data.First().ID) == 0)
                    {
                        return StatusCode(500, new
                        {
                            status = false,
                            message = string.Join(Environment.NewLine, "No such Type exists for master data.")
                        });
                    }
                }

                response.Message = "Master List successfully fetched.";
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
    }
}
