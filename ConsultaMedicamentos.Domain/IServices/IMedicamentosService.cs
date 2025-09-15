using ConsultaMedicamentos.Domain.DTOs;
using ConsultaMedicamentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.IServices
{
    public interface IMedicamentosService
    {
        public Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicas(string numeroDocumento);

        public Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicos(string tipoDocumento, string numeroDocumento);

        public Task<IEnumerable<PracticaDto>> ObtenerPractiasConAfiliados(string numeroDocumento);

        public Task<IEnumerable<ConsumoMedicoDto>> ObtenerConsumosMedicosConAfiliados(string tipoDocumento, string numeroDocumento);
    }
}
