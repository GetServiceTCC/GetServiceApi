namespace GetServiceApi.DTOs
{
    public class CidadeDto
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public int EstadoId { get; set; }

        public string EstadoNome { get; set; }
    }
}