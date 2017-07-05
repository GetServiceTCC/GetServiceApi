using System.ComponentModel.DataAnnotations;

namespace GetServiceApi.ViewModel
{
    public class AlterarSenhaUsuarioViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string AntigaSenha { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Compare("NovaSenha", ErrorMessage = "A senha e senha de confirmação estão diferentes.")]
        public string ConfirmaSenha { get; set; }
    }
}