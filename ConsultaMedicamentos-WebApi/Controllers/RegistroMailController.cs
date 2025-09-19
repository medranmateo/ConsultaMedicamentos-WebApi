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

        [HttpGet("desconocimiento/{tipoDocumento}/{numeroDocumento}/{tipo}/{identificador?}")]
        public async Task<IActionResult> RegistrarDesconocimiento(string tipoDocumento, string numeroDocumento, int tipo, int? identificador = 0)
        {
            try
            {
                var request = new EmailRequestDto
                {
                    TipoDocumento = tipoDocumento,
                    NumeroDocumento = numeroDocumento,
                    Tipo = tipo,
                    Identificador = identificador ?? 0
                };

                var result = await _emailService.RegistarDesconocimiento(request);

                return Redirect("/api/RegistroMail/resultado/envio");

                
            }
            catch (Exception ex)
            {

                return Redirect("/api/RegistroMail/resultado/errorenvio");
            }
        }

        [HttpGet("resultado/envio")]
        public IActionResult Exito()
        {
            // HTML de éxito
            var successHtml = @"
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body { font-family: Arial, sans-serif; background-color: #f4f4f9; text-align: center; padding: 50px; }
                        .card { background: white; padding: 30px; border-radius: 10px; box-shadow: 0px 3px 6px rgba(0,0,0,0.1); display: inline-block; }
                        .success { color: green; font-size: 24px; }
                    </style>
                </head>
                <body>
                    <div class='card'>
                        <p class='success'>✅ El correo fue enviado con éxito</p>
                        <p><a href='https://autogestion.cpcecba.org.ar/default.asp'>Autogestion</a></p>
                    </div>
                </body>
                </html>";

            return new ContentResult
            {
                Content = successHtml,
                ContentType = "text/html; charset=utf-8"
            };
        }

        [HttpGet("resultado/errorenvio")]
        public IActionResult Error()
        {
            var errorHtml = $@"
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{ font-family: Arial, sans-serif; background-color: #f4f4f9; text-align: center; padding: 50px; }}
                        .card {{ background: white; padding: 30px; border-radius: 10px; box-shadow: 0px 3px 6px rgba(0,0,0,0.1); display: inline-block; }}
                        .error {{ color: red; font-size: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='card'>
                        <p class='error'>❌ Ocurrió un error al enviar el correo</p>
                        <p>En los próximos días será contactado por un representante del CPCE.</p>
                        <p><a href='https://autogestion.cpcecba.org.ar/default.asp'>Autogestion</a></p>
                    </div>
                </body>
                </html>";

            return new ContentResult
            {
                Content = errorHtml,
                ContentType = "text/html; charset=utf-8"
            };
        }

    }
}
