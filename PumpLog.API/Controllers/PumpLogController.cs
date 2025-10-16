using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PumpLog.Core.comman;
using PumpLog.Core.PumpLog;
using PumpLog.Generic.Helper;

namespace PumpLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class PumpLogController : ControllerBase
    {
        public readonly IPumpLogRepository _pumpLogRepository;

        public PumpLogController(IPumpLogRepository pumpLogRepository)
        {
            _pumpLogRepository = pumpLogRepository;
        }
        [HttpPost, Route("insert")]
        public async Task<IActionResult> FuelFillingInsert([FromForm] FuelFillingInsertDto fillingInsertDto)
        {
            try
            {
                string? filePath = null;
                if (fillingInsertDto.PaymentProof != null && fillingInsertDto.PaymentProof.Length > 0)
                {
                    var ext = Path.GetExtension(fillingInsertDto.PaymentProof.FileName).ToLowerInvariant();
                    var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".pdf" };

                    if (!allowedExt.Contains(ext))
                        return BadRequest("Invalid file type. Only .jpg, .png, .pdf allowed.");

                    // Build local path
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "payments");
                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    // Unique filename
                    var fileName = $"payment_{Guid.NewGuid()}{ext}";
                    var fullPath = Path.Combine(uploadsDir, fileName);

                    // Save file locally
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await fillingInsertDto.PaymentProof.CopyToAsync(stream);
                    }

                    // Build file URL (this will be returned & saved to DB)
                    var baseUrl = $"{Request.Scheme}://{Request.Host}";
                    filePath = $"{baseUrl}/uploads/payments/{fileName}";
                    fillingInsertDto.PaymentProofPath = filePath;
                }

                var res = await _pumpLogRepository.FuelFillingInsert(fillingInsertDto);

                return res.Status ? StatusCode(StatusCodes.Status201Created, res) : StatusCode(StatusCodes.Status409Conflict, res);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
        [HttpPost, Route("list")]
        public async Task<IActionResult> FuelFillingList([FromBody] JqueryDataTable data)
        {
            try
            {
                var res = await _pumpLogRepository.FuelFillingList(data);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

        [HttpGet, Route("get")]

        public async Task<IActionResult> FuelFillingGet(string FuelFillingId)
        {
            try
            {
                var responce = await _pumpLogRepository.FuelFillingGet(FuelFillingId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
    }
}
