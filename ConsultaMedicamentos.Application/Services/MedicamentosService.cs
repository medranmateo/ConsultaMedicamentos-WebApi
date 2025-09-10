using ConsultaMedicamentos.Domain.Entities;
using ConsultaMedicamentos.Domain.IRepositories;
using ConsultaMedicamentos.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Application.Services
{
    public class MedicamentosService : IMedicamentosService
    {
        private readonly IMedicamentosRepository _repository;
        public MedicamentosService(IMedicamentosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicos(string tipoDocumento, string numeroDocumento)
        {
            var consumos = await _repository.ObtenerConsumosMedicos(tipoDocumento, numeroDocumento);
            return consumos;
        }

        public async Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicas(string numeroDocumento)
        {
            var practicas = await _repository.ObtenerPracticasMedicas(numeroDocumento);

            return practicas;

        }
    }
}
