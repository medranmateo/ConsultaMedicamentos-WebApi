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
    public class RegistroEmailRepository : IRegistroEmailRepository
    {
        private readonly AppDbContext _context;

        public RegistroEmailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> maximoID()
        {
            var result = await _context.desconocimientosSociales.MaxAsync(d => d.Id) + 1;
           
            return result;
        }

        public async Task<Matriculado> ObtenerMatriculado(string tipoDocumento, string numeroDocumento)
        {
            var matriculado = await _context.matriculado
                .FirstOrDefaultAsync(m => m.TipoDocumento == tipoDocumento && m.NumeroDocumento == numeroDocumento);

            return matriculado ?? new Matriculado();
        }

        public async Task<ParametrosApi> ObtenerParametrosMail(string clave)
        {
            var param = await _context.parametrosApi.FirstOrDefaultAsync(p => p.Calve ==  clave);
            return param ?? new ParametrosApi();
        }

        public async Task<int> RegistarDesconocimiento(DesconocimientosSociales requestDto)
        {
            await _context.desconocimientosSociales.AddAsync(requestDto);

            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<MatriculadoPersona> ObtenerTitular(string numDocumento, string tipoDocumento)
        {
            // ejecutamos sql raw por incompatibilidades con sqlserver 13 del servidor
            var sql = $@"
                            SELECT TOP 1  PER_NOMBRE, PER_APELLI, MAT_EMAIL, 
                            M.MAT_TCEL_CAR, M.MAT_TCEL_NRO 
                            FROM PERSONA P 
                            LEFT JOIN MATRICULA M 
                            ON P.PER_NRODOC = M.PER_NRODOC 
                            WHERE P.PER_NRODOC = '{numDocumento}' 
                            AND P.TPE_CODIGO in (1) 
                            ORDER BY M.MAT_FECALT DESC";
            var matriculado = new MatriculadoPersona();
            try
            {

                matriculado = await _context.martriculadoPersona.FromSqlRaw(sql)
                      .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            return matriculado;
        }

    }
}
