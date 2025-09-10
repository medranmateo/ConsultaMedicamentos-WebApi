using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    [Table("CONSUMO_FARMANDAT")]
    public class ConsumoMedico
    {
        [Column("TIP_TIPDOC")]
        public string TipoDocumento { get; set; } = string.Empty;

        [Column("PER_NRODOC")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Column("CF_CODIGO")]
        public string Codigo { get; set; } = string.Empty;

        [Column("CF_FARMACIA")]
        public string Farmacia { get; set; } = string.Empty;

        [Column("CF_FEC")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column("CF_MEDICAMENTO")] 
        public string Medicamento { get;set; } = string.Empty;

        [Column("CF_CANTIDAD")]
        public int Cantidad { get; set; } = 0;

        [Column("CF_MEDICO")]
        public string Medico { get; set; } = string.Empty;

        [Column("CF_PRECIO")]
        public decimal Precio { get; set; } = 0;
    }
}
