using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreMVC.Models
{
    public class Game
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200)]
        [Display(Name = "Título do Jogo")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(1000)]
        [Display(Name = "Descrição Curta")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 9999.99, ErrorMessage = "Preço deve ser entre R$ 0,01 e R$ 9.999,99")]
        [Display(Name = "Preço (R$)")]
        public decimal Preco { get; set; }

        [Display(Name = "URL da Capa")]
        [StringLength(500)]
        public string? UrlCapa { get; set; }

        [Display(Name = "Categoria")]
        [StringLength(50)]
        public string Categoria { get; set; } = "Ação";

        public bool Destaque { get; set; } = false;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
