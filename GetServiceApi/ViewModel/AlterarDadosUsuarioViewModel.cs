using System.ComponentModel.DataAnnotations;

namespace GetServiceApi.ViewModel
{
    public class AlterarDadosUsuarioViewModel
    {
        [Required]
        public string NomeCompleto { get; set; }

        public string Status { get; set; }

        [Required]
        public int CidadeId { get; set; }

        public string Endereco { get; set; }

        [Required]
        public bool Profissional { get; set; }
    }
}