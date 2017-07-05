using GetServiceApi.Models.Enums;
using Newtonsoft.Json;

namespace GetServiceApi.DTOs
{
    public class ServicoDto
    {
        public string Profissional { get; set; }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Sobre { get; set; }

        public bool Ativo { get; set; }

        public int EstadoId { get; set; }

        public int CidadeId { get; set; }

        public int CategoriaId { get; set; }

        public string Categoria { get; set; }

        public int SubCategoriaId { get; set; }

        public string SubCategoria { get; set; }

        public TipoValor TipoValor { get; set; }

        public double Valor { get; set; }

        public int QtdComentarios { get; set; }

        public double? Avaliacao { get; set; }
    }
}