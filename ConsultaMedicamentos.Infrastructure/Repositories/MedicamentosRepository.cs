using ConsultaMedicamentos.Domain.DTOs;
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

        public async Task<IEnumerable<Afiliado>> ObtenerAfiliados(string numeroDocumento)
        {
            var afiliados = await _context.Set<Afiliado>()
                .FromSqlInterpolated($"EXEC SP_VER_AFILIADOS_SS_POR_NRODOC DNI, {numeroDocumento}")
                .ToListAsync();


            return afiliados;
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

        public async Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicosConAfiliados(IEnumerable<DocumentoDto> documentos)
        {

            try
            {

                if (documentos == null || !documentos.Any())
                    return Enumerable.Empty<ConsumoMedico>();

                // 1. Generar listas con comillas para SQL
                var tipos = string.Join(",", documentos.Select(d => $"'{d.TipoDocumento}'"));
                var docs = string.Join(",", documentos.Select(d => $"'{d.NumeroDocumento}'"));

                // 2. Armar query dinámico
                var sql = $@"
                            SELECT C.* 
                            FROM CONSUMO_FARMANDAT C 
                            WHERE C.TIP_TIPDOC IN ({tipos}) 
                            AND C.PER_NRODOC IN ({docs})  ";

                // 3. Ejecutar query
                var consumos = await _context.consumoMedicos
                    .FromSqlRaw(sql)
                    .ToListAsync();

                // 4. Eliminar duplicados
                return consumos
                    .DistinctBy(c => new { c.TipoDocumento, c.NumeroDocumento, c.Codigo })
                    .ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicas(string numeroDocumento)
        {
            var practicas = await _context.practicaMedicas
            .Where(p => p.NumeroDocumento == numeroDocumento)
            .GroupBy(p => p.Codigo)
            .Select(g => g.First()) // Evitamos duplicados
            .ToListAsync();

            return practicas;
        }

        public async Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicasConAfiliados(string[] numerosDocumento)
        {

            try
            {
                var documentos = string.Join(",", numerosDocumento.Where(nd => !string.IsNullOrWhiteSpace(nd)).ToArray());
                
                var sql = $@"
                            SELECT G.* 
                            FROM GECROS_AUTORIZACION G 
                            WHERE GA_DOCUMENTO IN ({documentos})";

                var practicas = await _context.practicaMedicas.FromSqlRaw(sql)
                   .ToListAsync();

                return practicas.DistinctBy(p => new { p.NumeroDocumento, p.Codigo }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
