using System.ComponentModel.DataAnnotations;

namespace GameStoreMVC.ViewModels
{
    public class CadastroViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
    }
}
