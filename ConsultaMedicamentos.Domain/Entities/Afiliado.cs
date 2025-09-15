using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    public class Afiliado
    {
        [Column("TIP_TIPDOC")]
        public string TipoDocumento { get; set; } = string.Empty;

        [Column("PER_NRODOC")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Column("PER_NOMBRE")]
        public string Nombre { get; set; } = string.Empty;

        [Column("PER_APELLI")]
        public string Apellido { get; set; } = string.Empty;


        [Column("SER_TITOPT")]
        public string TitularOPariente { get; set; } = string.Empty;

    }
}
