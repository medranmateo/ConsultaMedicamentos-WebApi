using ConsultaMedicamentos.Domain.DTOs;
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
    public class RegistroEmailService : IRegistroEmailService
    {
        private readonly IRegistroEmailRepository _registroEmailRepository;

        public RegistroEmailService(IRegistroEmailRepository registroEmailRepository)
        {
            _registroEmailRepository = registroEmailRepository;
        }

        public async Task<ParametrosApi> ObtenerParametrosMail(string clave)
        {
            var destinatarios = new ParametrosApi();
            try
            {
                destinatarios = await _registroEmailRepository.ObtenerParametrosMail(clave);

            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
            return destinatarios;
        }

        public async Task<int> RegistarDesconocimiento(EmailRequestDto requestDto)
        {
            var desconocimiento = new DesconocimientosSociales();
            desconocimiento.TipoDocumento = requestDto.TipoDocumento;
            desconocimiento.NumeroDocumento = requestDto.NumeroDocumento;
            desconocimiento.Tipo = requestDto.Tipo;
            desconocimiento.Fecha = DateTime.Now;
            desconocimiento.Id = await _registroEmailRepository.maximoID(); // solo en test

            var result = 0;
            try
            {

                result = await _registroEmailRepository.RegistarDesconocimiento(desconocimiento);
                var email = sendEmail();
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"{ex.Message}");
            }

            return result;

        }

        private async Task<int> sendEmail() 
        {
            var result = 0;

            var destinatarios = await ObtenerParametrosMail("EMAIL-DS-MEDICAMENTOS");

            return result;

        }
    }
}
