namespace GetServiceApi.DTOs
{
    public class SubCategoriaDto
    {
        public int Id { get; set; }
        
        public string Descricao { get; set; }
        
        public int CategoriaId { get; set; }
        
        public string CategoriaDescricao { get; set; }
    }
}