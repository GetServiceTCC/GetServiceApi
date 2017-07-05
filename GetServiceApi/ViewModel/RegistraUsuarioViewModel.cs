using System.ComponentModel.DataAnnotations;

namespace GetServiceApi.ViewModel
{
    public class RegistraUsuarioViewModel
    {
        [Required]
        public string NomeUsuario { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        public string Status { get; set; }

        [Required]
        public int CidadeId { get; set; }
        
        public string Endereco { get; set; }

        [Required]
        public bool Profissional { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A senha e senha de confirmação estão diferentes.")]
        public string ConfirmaSenha { get; set; }
    }
}