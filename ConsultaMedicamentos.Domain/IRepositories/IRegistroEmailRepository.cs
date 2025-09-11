using ConsultaMedicamentos.Domain.DTOs;
using ConsultaMedicamentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.IRepositories
{
    public interface IRegistroEmailRepository
    {
        public Task<int> RegistarDesconocimiento(DesconocimientosSociales requestDto);

        public Task<int> maximoID();

        public Task<ParametrosApi> ObtenerParametrosMail(string clave);
    }
}
