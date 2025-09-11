using ConsultaMedicamentos.Domain.DTOs;
using ConsultaMedicamentos.Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicamentos_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroMailController : ControllerBase
    {
        private readonly IRegistroEmailService _emailService;
        public RegistroMailController(IRegistroEmailService registroEmailService)
        {
            _emailService = registroEmailService;
        }

        [HttpGet("desconocimiento/{tipoDocumento}/{numeroDocumento}/{tipo}")]
        public async Task<IActionResult> RegistrarDesconocimiento(string tipoDocumento, string numeroDocumento, int tipo)
        {
            try
            {
                var request = new EmailRequestDto
                {
                    TipoDocumento = tipoDocumento,
                    NumeroDocumento = numeroDocumento,
                    Tipo = tipo
                };

                var result = await _emailService.RegistarDesconocimiento(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
