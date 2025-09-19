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
        private readonly IMedicamentosService _medicamentosService;

        public RegistroEmailService(IRegistroEmailRepository registroEmailRepository, IEmailService emailService, IMedicamentosService medicamentosService)
        {
            _registroEmailRepository = registroEmailRepository;
            _emailService = emailService;
            _medicamentosService = medicamentosService;
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
            desconocimiento.TipoDocumento = requestDto.TipoDocumento.ToUpper();
            desconocimiento.NumeroDocumento = requestDto.NumeroDocumento;
            desconocimiento.Tipo = requestDto.Tipo;
            desconocimiento.Fecha = DateTime.Now;
            //desconocimiento.Id = await _registroEmailRepository.maximoID(); // solo en test



            var result = 0;
            try
            {
                var persona = new PracticaPersona();
                if (requestDto.Tipo == 1)
                {
                    persona = await _medicamentosService.ObtenerPracticamedicaByPersona(requestDto.Identificador ?? 0);

                }

                var afiliados = await _medicamentosService.ObtenerAfiliados(requestDto.NumeroDocumento);
                var afiliado = afiliados.FirstOrDefault(a => a.TitularOPariente.Equals("T"));

                var titular = await _registroEmailRepository.ObtenerTitular(afiliado?.NumeroDocumento!, afiliado?.TipoDocumento!);


                result = await _registroEmailRepository.RegistarDesconocimiento(desconocimiento);
                var email = await sendEmail(desconocimiento, persona, titular, requestDto.Identificador);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"{ex.Message}");
            }

            return result;

        }

        private async Task<bool> sendEmail(DesconocimientosSociales desconocimientos, PracticaPersona practicaPersona, MatriculadoPersona titular, int? identificador) 
        {
            
            string parametro;
            string body = "<p> </p>";
            var subject ="Desconocimiento Consumos";

            switch (desconocimientos.Tipo)
            {
                case 1:
                    parametro = "EMAIL-DS-PRACTICA";
                    //tipoDesconocimiento = ;

                    body = $"<p>Un matriculado ha solicitado revisión de su consumo de Practicas Medicas </p> " +
                        $"<p> </p> " +
                        $"<p> Afiliado: DNI - {practicaPersona?.GA_DOCUMENTO} - {practicaPersona?.GA_AFILIADO} </p> " +
                        $"<p> </p> " +
                        $"<P> Fecha: {practicaPersona?.GA_FECHA:dd/MM/yyyy} </p> " +
                        $"<p> </p> " +
                        $"<p> Tipo: {practicaPersona?.GA_TIPO} - {practicaPersona?.GA_PRACTICA} </p>" +
                        $"<p> </p> " +
                        $"<p>Código de autorización: {identificador} </p> " +
                        $"<p> </p>" +
                        $"<p>Datos de contacto del titular: </p> " +
                        $"<p> {titular.PER_NOMBRE} {titular.PER_APELLI} - {titular.MAT_EMAIL} - {titular.MAT_TCEL_CAR} {titular.MAT_TCEL_NRO} </P>";

                    break;

                case 2:
                    parametro = "EMAIL-DS-MEDICAMENTOS";
                    subject += " - Farmandat";

                    body = $"<p>Un afiliado ha solicitado revisión de su consumo de Medicamentos </p> " +
                        $"<p> </p> " +
                        $"<p>Datos de contacto del titular: </p> " +
                        $"<p> {titular.PER_NOMBRE} {titular.PER_APELLI} - {titular.MAT_EMAIL} - {titular.MAT_TCEL_CAR} {titular.MAT_TCEL_NRO} </P>";

                    break;
                default:
                    throw new ArgumentException("Tipo no válido", nameof(desconocimientos.Tipo));
                    //break;
            }

            var listDestinatarios = await _registroEmailRepository.ObtenerParametrosMail(parametro);
            var toList = listDestinatarios.Valor.ToLower().Split(';', StringSplitOptions.RemoveEmptyEntries);


            bool result = await _emailService.SendEmailAsync(toList, subject, body);

            return result;

        }
    }
}
