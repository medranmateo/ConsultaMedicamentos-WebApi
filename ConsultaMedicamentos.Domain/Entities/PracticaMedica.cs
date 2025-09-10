using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    [Table("GECROS_AUTORIZACION")]
    public class PracticaMedica
    {
        [Column("GA_DOCUMENTO")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Column("GA_FECHA")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column("GA_PRESTADOR")]
        public string Prestador { get; set; } = string.Empty;

        [Column("GA_TIPO")]
        public string Tipo {  get; set; } = string.Empty;

        [Column("GA_PRACTICA")]
        public string Practica { get; set; } = string.Empty;

        [Column("GA_CODIGO")]
        public int Codigo {  get; set; } = 0;

        [Column("GA_LOCALIDAD")]
        public string Localidad {  get; set; } = string.Empty;

        [Column("GA_ESTADO")]
        public string Estado {  get; set; } = string.Empty;
    }
}
