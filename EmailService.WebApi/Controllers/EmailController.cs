using EmailService.Application.Services;
using EmailService.Domain.Entities;
using EmailService.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            var email = new Email
            {
                Id = Guid.NewGuid(),
                To = model.To,
                Subject = model.Subject,
                Body = model.Body,
                IsSent = false
            };

            await _emailService.SendAsync(email);
            return Ok();
        }
    }
}

