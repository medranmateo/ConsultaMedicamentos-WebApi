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

    }
}
