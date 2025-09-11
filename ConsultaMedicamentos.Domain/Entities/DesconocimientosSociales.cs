using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    [Table("DESCONOCIMIENTO_SSOCIALES")]
    public class DesconocimientosSociales
    {
        [Key]
        [Column("DS_ID")]
        public int Id { get; set; } = 0;

        [Column("TIP_TIPDOC")]
        public string TipoDocumento { get; set; } = string.Empty;

        [Column("PER_NRODOC")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Column("DS_FECHA")]
        public DateTime Fecha {  get; set; } = DateTime.Now;

        [Column("DS_TIPO")]
        public int Tipo {  get; set; } = 0;
    }
}
