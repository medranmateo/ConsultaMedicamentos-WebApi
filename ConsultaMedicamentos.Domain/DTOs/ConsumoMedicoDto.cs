namespace ConsultaMedicamentos.Domain.DTOs
{
    public class ConsumoMedicoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        public string TipoDocumento { get; set; } = string.Empty;

        public string NumeroDocumento { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Codigo { get; set; } = string.Empty;

        public string Farmacia { get; set; } = string.Empty;


        public string Medicamento { get; set; } = string.Empty;

        public int Cantidad { get; set; } = 0;

        public string Medico { get; set; } = string.Empty;

        public decimal Precio { get; set; } = 0;
    }
}
