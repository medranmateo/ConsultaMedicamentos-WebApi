using ConsultaMedicamentos.Domain.DTOs;
using ConsultaMedicamentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.IRepositories
{
    public interface IMedicamentosRepository
    {
        public Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicas(string numeroDocumento);

        public Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicos(string tipoDocumento, string numeroDocumento);

        public Task<IEnumerable<Afiliado>> ObtenerAfiliados(string numeroDocumento);
        public Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicasConAfiliados(string[] numerosDocumento);

        public Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicosConAfiliados(IEnumerable<DocumentoDto> documentos);

        public Task<PracticaPersona> ObtenerPracticamedicaByPersona(int identificador);
    }
}
