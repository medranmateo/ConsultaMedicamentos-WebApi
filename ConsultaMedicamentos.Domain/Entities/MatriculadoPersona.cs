using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.Entities
{
    
    public class MatriculadoPersona
    {

        [Column("PER_NOMBRE")]
        public string PER_NOMBRE { get; set; } = string.Empty;

        [Column("PER_APELLI")]
        public string PER_APELLI { get; set; } = string.Empty;

        [Column("MAT_TCEL_CAR")]
        public decimal? MAT_TCEL_CAR { get; set; }

        [Column("MAT_TCEL_NRO")]
        public decimal? MAT_TCEL_NRO { get; set; }

        [Column("MAT_EMAIL")]
        public string? MAT_EMAIL { get; set; }

    }
}
