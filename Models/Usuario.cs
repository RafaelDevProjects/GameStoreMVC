using System.ComponentModel.DataAnnotations;

namespace GameStoreMVC.Models
{
    public class Usuario
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
