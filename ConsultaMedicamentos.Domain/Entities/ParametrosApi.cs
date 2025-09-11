using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    [Table("PARAMETROS_API")]
    public class ParametrosApi
    {
        [Column("PA_CLAVE")]
        public string Calve { get; set; } = string.Empty;

        [Column("PA_VALOR")]
        public string Valor { get; set; } = string.Empty;

    }
}
