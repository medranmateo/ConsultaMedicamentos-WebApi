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
        private readonly IEmailService _emailService;

        public RegistroEmailService(IRegistroEmailRepository registroEmailRepository, IEmailService emailService)
        {
            _registroEmailRepository = registroEmailRepository;
            _emailService = emailService;
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
            //desconocimiento.Id = await _registroEmailRepository.maximoID(); // solo en test

            var result = 0;
            try
            {

                result = await _registroEmailRepository.RegistarDesconocimiento(desconocimiento);
                var email = await sendEmail(desconocimiento);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"{ex.Message}");
            }

            return result;

        }

        private async Task<bool> sendEmail(DesconocimientosSociales desconocimientos) 
        {
            var result = false;

            string parametro = desconocimientos.Tipo switch
            {
                1 => "EMAIL-DS-PRACTICA",
                2 => "EMAIL-DS-MEDICAMENTOS",
                _ => throw new ArgumentException("Tipo no válido", nameof(desconocimientos.Tipo))
            };
            var tipoDesconocimiento = desconocimientos.Tipo == 1 ? "Practicas Medicas" : "Farmacia/Medicamentos";
            var listDestinatarios = await _registroEmailRepository.ObtenerParametrosMail(parametro);
            var toList = listDestinatarios.Valor.ToLower().Split(';');

            var matriculado = await _registroEmailRepository.ObtenerMatriculado(desconocimientos.TipoDocumento, desconocimientos.NumeroDocumento);

            var subject ="Desconocimiento Consumos";
            var body = $"<p>Un matriculado ha solicitado revisión de su consumo de {tipoDesconocimiento} </p> " +
                $"<p> Matriculado: {matriculado.TipoDocumento} - {matriculado.NumeroDocumento} - {matriculado.Nombre} {matriculado.Apellido} </p> " +
                $"<P> Fecha: {desconocimientos.Fecha:dd/MM/yyyy} </p> " +
                $"<p> Tipo: {tipoDesconocimiento} </p>";


            result = await _emailService.SendEmailAsync(toList, subject, body);

            return result;

        }
    }
}
