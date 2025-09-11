using ConsultaMedicamentos.Domain.Entities;
using ConsultaMedicamentos.Domain.IRepositories;
using ConsultaMedicamentos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Infrastructure.Repositories
{
    public class MedicamentosRepository : IMedicamentosRepository
    {
        private readonly AppDbContext _context;

        public MedicamentosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicos(string tipoDocumento, string numeroDocumento)
        {
            var consumos = await _context.consumoMedicos
            .Where(c => c.TipoDocumento == tipoDocumento && c.NumeroDocumento == numeroDocumento)
            .GroupBy(g => g.Codigo)
            .Select(g => g.First()) // Eitamos duplicados
            .ToListAsync();
            return consumos;
        }

        public async Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicas(string numeroDocumento)
        {
            var practicas = await _context.practicaMedicas
            .Where(p => p.NumeroDocumento == numeroDocumento)
            .GroupBy(p => p.Codigo)
            .Select(g => g.First()) // Eitamos duplicados
            .ToListAsync();

            return practicas;
        }
    }
}
