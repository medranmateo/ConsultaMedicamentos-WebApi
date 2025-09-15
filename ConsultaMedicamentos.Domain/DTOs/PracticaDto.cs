namespace ConsultaMedicamentos.Domain.DTOs
{
    public class PracticaDto
    {
        //public PracticaMedica PracticaMedica { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set;} = string.Empty;

        public string NumeroDocumento { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Prestador { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Practica { get; set; } = string.Empty;

        public int Codigo { get; set; } = 0;

        public string Localidad { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

    }
}
