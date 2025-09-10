using ConsultaMedicamentos.Domain.Entities;
using ConsultaMedicamentos.Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicamentos_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly IMedicamentosService _medicamentosService;


        public MedicamentosController(IMedicamentosService medicamentosService)
        {
            _medicamentosService = medicamentosService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API Consulta Medicamentos funcionando correctamente.");
        }

        [HttpGet("practicas/{numeroDocumento}")]
        public async Task<IActionResult> ObtenerPracticasMedicas(string numeroDocumento)
        {
            var practicas = await _medicamentosService.ObtenerPracticasMedicas(numeroDocumento);

            return practicas != null ? Ok(practicas) : NotFound();
        }

        [HttpGet("consumos/{tipoDocumento}/{numeroDocumento}")]
        public async Task<IActionResult> ObtenerConsumosMedicos(string tipoDocumento, string numeroDocumento)
        {
            var consumos = await _medicamentosService.ObtenerConsumosMedicos(tipoDocumento, numeroDocumento);
            return consumos != null ? Ok(consumos) : NotFound();
        }

    }
}
