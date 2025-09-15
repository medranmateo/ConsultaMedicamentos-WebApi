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
    public class MedicamentosService : IMedicamentosService
    {
        private readonly IMedicamentosRepository _repository;
        public MedicamentosService(IMedicamentosRepository repository)
        {
            _repository = repository;
        }

        private async Task<IEnumerable<Afiliado>> ObtenerAfiliados(string numeroDocumento)
        {
            var afiliados = await _repository.ObtenerAfiliados(numeroDocumento);
            return afiliados;
        }

        public async Task<IEnumerable<ConsumoMedico>> ObtenerConsumosMedicos(string tipoDocumento, string numeroDocumento)
        {
            var consumos = await _repository.ObtenerConsumosMedicos(tipoDocumento, numeroDocumento);
            return consumos;
        }

        public async Task<IEnumerable<ConsumoMedicoDto>> ObtenerConsumosMedicosConAfiliados(string tipoDocumento, string numeroDocumento) 
        {
            var afiliados = await _repository.ObtenerAfiliados(numeroDocumento);
            var documentos = afiliados.Select(a => new DocumentoDto
            {
                TipoDocumento = a.TipoDocumento,
                NumeroDocumento = a.NumeroDocumento
            }).ToArray();

            var consumos = await _repository.ObtenerConsumosMedicosConAfiliados(documentos);
            
            var resultado = from consumo in consumos
                            join afiliado in afiliados
                            on new { consumo.TipoDocumento, consumo.NumeroDocumento } equals new { afiliado.TipoDocumento, afiliado.NumeroDocumento }
                            select new ConsumoMedicoDto
                            {
                                Nombre = afiliado.Nombre,
                                Apellido = afiliado.Apellido,
                                TipoDocumento = consumo.TipoDocumento,
                                NumeroDocumento = consumo.NumeroDocumento,
                                Fecha = consumo.Fecha,
                                Codigo = consumo.Codigo,
                                Farmacia = consumo.Farmacia,
                                Medicamento = consumo.Medicamento,
                                Cantidad = consumo.Cantidad,
                                Medico = consumo.Medico,
                                Precio = consumo.Precio
                            };
            return resultado;
        }

        public async Task<IEnumerable<PracticaMedica>> ObtenerPracticasMedicas(string numeroDocumento)
        {
            var practicas = await _repository.ObtenerPracticasMedicas(numeroDocumento);

            return practicas;

        }

        public async Task<IEnumerable<PracticaDto>> ObtenerPractiasConAfiliados(string numeroDocumento)
        {
            var resultado = Enumerable.Empty<PracticaDto>();

            var afiliados = await _repository.ObtenerAfiliados(numeroDocumento);

            var documentos = afiliados.Select(a => a.NumeroDocumento).ToArray();

            try
            {
                var practicas = await _repository.ObtenerPracticasMedicasConAfiliados(documentos);

                // Unimos las prácticas con los afiliados por numero de documento
                resultado = from practica in practicas
                                join afiliado in afiliados
                                on practica.NumeroDocumento equals afiliado.NumeroDocumento
                                select new PracticaDto
                                {
                                    Nombre = afiliado.Nombre,
                                    Apellido = afiliado.Apellido,
                                    NumeroDocumento = practica.NumeroDocumento,
                                    Fecha = practica.Fecha,
                                    Prestador = practica.Prestador,
                                    Tipo = practica.Tipo,
                                    Practica = practica.Practica,
                                    Codigo = practica.Codigo,
                                    Localidad = practica.Localidad,
                                    Estado = practica.Estado
                                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            return resultado;
        }

    }
}
