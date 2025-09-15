namespace ConsultaMedicamentos.Domain.DTOs
{
    public class EmailRequestDto
    {
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public int Tipo { get; set; } = 0; // Tipo: 1 = MEDICAMENTOS, 2 = FARMANDAT
    }
}
