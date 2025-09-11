using ConsultaMedicamentos.Domain.DTOs;
using ConsultaMedicamentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.IServices
{
    public interface IRegistroEmailService
    {
        public Task<int> RegistarDesconocimiento(EmailRequestDto requestDto);

        public Task<ParametrosApi> ObtenerParametrosMail(string clave);
    }
}
