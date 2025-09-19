using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    
    public class PracticaPersona
    {
        [Column("GA_DOCUMENTO")]
        public string GA_DOCUMENTO { get; set; } = string.Empty;

        [Column("GA_AFILIADO")]
        public string GA_AFILIADO { get; set; } = string.Empty;

        [Column("GA_FECHA")]
        public DateTime GA_FECHA { get; set; } = DateTime.Now;

        [Column("GA_PRESTADOR")]
        public string GA_PRESTADOR { get; set; } = string.Empty;

        [Column("GA_TIPO")]
        public string GA_TIPO { get; set; } = string.Empty;

        [Column("GA_PRACTICA")]
        public string GA_PRACTICA { get; set; } = string.Empty;

        [Column("GA_CODIGO")]
        public int GA_CODIGO { get; set; } = 0;

        [Column("GA_LOCALIDAD")]
        public string GA_LOCALIDAD { get; set; } = string.Empty;

        [Column("GA_ESTADO")]
        public string GA_ESTADO { get; set; } = string.Empty;

        [Column("GA_IDENTIFICADOR")]
        public int GA_IDENTIFICADOR { get; set; } = 0;

        [Column("PER_NOMBRE")]
        public string PER_NOMBRE { get; set; } = string.Empty;

        [Column("PER_APELLI")]
        public string PER_APELLI { get; set; } = string.Empty;
    }
}
