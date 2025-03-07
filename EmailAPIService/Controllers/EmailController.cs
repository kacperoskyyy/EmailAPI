using EmailAPIService.Models;
using EmailAPIService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly SMTPService _smtpService;
        public EmailController(SMTPService smtpService)
        {
            _smtpService = smtpService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] Email email)
        {
            try
            {
                await _smtpService.SendEmail(email.recipient, email.templateId, email.placeholders);
                return Ok("Seccessfully sent.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("{recipient}/{templateId}/{placeholders}")]
        public async Task<IActionResult> SendEmail(string recipient, int templateId, string placeholders)
        {
            try
            {
                await _smtpService.SendEmail(recipient, templateId, placeholders);
                return Ok("Seccessfully sent.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
